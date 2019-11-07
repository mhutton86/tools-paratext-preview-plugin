using AddInSideViews;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using tools_paratext_preview_plugin.Form;
using tools_paratext_preview_plugin.Util;
using tools_tpt_transformation_service.Models;
using static System.Environment;

namespace tools_paratext_preview_plugin.Workflow
{
    public class TypesettingPreviewWorkflow
    {
        private ProjectDetails _projectDetails;
        private PreviewJob _previewJob;
        private FileInfo _previewFile;

        public void Run(IHost host, string activeProjectName)
        {
            if (host == null)
            {
                throw new ArgumentNullException(nameof(host));
            }
            if (activeProjectName == null)
            {
                throw new ArgumentNullException(nameof(activeProjectName));
            }

#if DEBUG
            MessageBox.Show($"Attach debugger now to PID {Process.GetCurrentProcess().Id}, if needed!",
                "Notice...", MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif

            if (!CheckProjectName(activeProjectName, out _projectDetails))
            {
                return;
            }

            SetupForm setupForm = new SetupForm();
            setupForm.SetProjectDetails(_projectDetails);

            setupForm.ShowDialog();

            if (!setupForm.IsCreating)
            {
                return;
            }

            ProgressForm progressForm = new ProgressForm();
            progressForm.Cancelled += ProgressForm_Cancelled;
            progressForm.Show();

            try
            {
                PreviewJob newPreviewJob = new PreviewJob();

                newPreviewJob.ProjectName = setupForm.ProjectName;
                newPreviewJob.BookFormat = setupForm.BookFormat;
                newPreviewJob.FontSizeInPts = setupForm.FontSizeInPts;
                newPreviewJob.FontLeadingInPts = setupForm.FontLeadingInPts;
                newPreviewJob.PageWidthInPts = setupForm.PageWidthInPts;
                newPreviewJob.PageHeightInPts = setupForm.PageHeightInPts;
                newPreviewJob.PageHeaderInPts = setupForm.PageHeaderInPts;

                _previewJob = CreatePreviewJob(newPreviewJob);
                progressForm.SetStatus(_previewJob);

                DateTime lastCheckTime = DateTime.Now;
                int threadSleepInMs = (int)(1000f / (float)MainConsts.PROGRESS_FORM_UPDATE_RATE_IN_FPS);

                while (!_previewJob.IsError
                    && !_previewJob.IsCancelled
                    && !_previewJob.IsCompleted)
                {
                    progressForm.SetStatus(_previewJob);

                    Application.DoEvents();
                    Thread.Sleep(threadSleepInMs);

                    lock (this)
                    {
                        // double-check locking to prevent 
                        // crossover with manual cancel
                        if (!_previewJob.IsError
                            && !_previewJob.IsCancelled
                            && !_previewJob.IsCompleted)
                        {
                            DateTime nowTime = DateTime.Now;
                            if (nowTime.Subtract(lastCheckTime).TotalSeconds > MainConsts.PREVIEW_JOB_UPDATE_INTERVAL_IN_SEC)
                            {
                                _previewJob = UpdatePreviewJob(_previewJob);
                                lastCheckTime = nowTime;
                            }
                        }
                    }
                }

                if (_previewJob.IsError)
                {
                    MessageBox.Show($"Can't preview project\n\nPlease contact support (server error, reference ID: {_previewJob.Id}).",
                        "Notice...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (_previewJob.IsCancelled)
                {
                    return;
                }
            }
            finally
            {
                progressForm.Hide();
            }

            _previewFile = DownloadPreviewFile(_previewJob);
            PreviewForm previewForm = new PreviewForm();
            previewForm.FormClosed += PreviewForm_FormClosed;

            previewForm.SetPreviewFile(_previewJob, _previewFile);
            previewForm.ShowDialog();

            _previewFile.Delete();
        }

        private void PreviewForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MessageBox.Show("Save preview file?",
                "Notice...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SaveFileDialog saveFile = new SaveFileDialog())
                {
                    string dateTimeText = _projectDetails.ProjectUpdated.ToString("yyyyMMdd'T'HHmmss'Z'");
                    saveFile.FileName = $"preview-{_previewJob.ProjectName}-{_previewJob.BookFormat}-{dateTimeText}.pdf";
                    saveFile.InitialDirectory = Environment.GetFolderPath(SpecialFolder.MyDocuments);

                    if (saveFile.ShowDialog() == DialogResult.OK)
                    {
                        using (Stream outputStream = saveFile.OpenFile())
                        {
                            using (FileStream inputStream = _previewFile.OpenRead())
                            {
                                inputStream.CopyTo(outputStream);
                            }
                        }

                    }
                }
            }
        }

        private FileInfo DownloadPreviewFile(PreviewJob previewJob)
        {
            FileInfo fileInfo = new FileInfo(Path.Combine(Path.GetTempPath(), $"preview-{previewJob.Id}.pdf"));
            WebRequest webRequest = WebRequest.Create($"{MainConsts.DEFAULT_SERVER_URI}/PreviewFile/{previewJob.Id}");
            webRequest.Method = HttpMethod.Get.Method;

            using (Stream inputStream = webRequest.GetResponse().GetResponseStream())
            {
                using (FileStream outputStream = fileInfo.OpenWrite())
                {
                    inputStream.CopyTo(outputStream);
                }
            }

            return fileInfo;
        }

        private void ProgressForm_Cancelled(object sender, EventArgs e)
        {
            if (_previewJob == null)
            {
                return;
            }

            lock (this)
            {
                WebRequest webRequest = WebRequest.Create($"{MainConsts.DEFAULT_SERVER_URI}/PreviewJobs/{_previewJob.Id}");
                webRequest.Method = HttpMethod.Delete.Method;

                using (StreamReader streamReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
                {
                    _previewJob = JsonConvert.DeserializeObject<PreviewJob>(streamReader.ReadToEnd());
                }
            }
        }

        private PreviewJob CreatePreviewJob(PreviewJob previewJob)
        {
            WebRequest webRequest = WebRequest.Create($"{MainConsts.DEFAULT_SERVER_URI}/PreviewJobs");
            webRequest.Method = HttpMethod.Post.Method;
            webRequest.ContentType = "application/json";

            using (StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(previewJob));
            }
            using (StreamReader streamReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            {
                return JsonConvert.DeserializeObject<PreviewJob>(streamReader.ReadToEnd());
            }
        }

        private PreviewJob UpdatePreviewJob(PreviewJob previewJob)
        {
            WebRequest webRequest = WebRequest.Create($"{MainConsts.DEFAULT_SERVER_URI}/PreviewJobs/{previewJob.Id}");
            webRequest.Method = HttpMethod.Get.Method;

            using (StreamReader streamReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            {
                return JsonConvert.DeserializeObject<PreviewJob>(streamReader.ReadToEnd());
            }
        }

        private bool CheckProjectName(string projectName, out ProjectDetails projectDetails)
        {
            try
            {
                WebRequest webRequest = WebRequest.Create($"{MainConsts.DEFAULT_SERVER_URI}/ProjectDetails");
                webRequest.Method = HttpMethod.Get.Method;

                using (StreamReader streamReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
                {
                    List<ProjectDetails> allProjectDetails = JsonConvert.DeserializeObject<List<ProjectDetails>>(streamReader.ReadToEnd());

                    if (allProjectDetails.Count < 1)
                    {
                        projectDetails = null;

                        MessageBox.Show($"Can't preview project \"{projectName}\" (none present on server).\n\nPlease try again later or contact support.",
                        "Notice...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    projectDetails = allProjectDetails
                        .FirstOrDefault(detailsItem => detailsItem.ProjectName.Equals(projectName, StringComparison.CurrentCultureIgnoreCase));

                    if (projectDetails == null)
                    {
                        allProjectDetails.Sort((l, r) => l.ProjectName.CompareTo(r.ProjectName));
                        string availableProjects = string.Join("\", \"", allProjectDetails.Select(detailItem => detailItem.ProjectName));

                        MessageBox.Show($"Can't preview project \"{projectName}\" (not present on server).\n\nPlease try again later or contact support (available projects: \"{availableProjects}\").",
                        "Notice...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Can't contact typesetting preview server.\n\nPlease check Internet connection and try again, or contact support (Details: {ex.Message}).",
                    "Notice...", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                projectDetails = null;
                return false;
            }
        }
    }
}
