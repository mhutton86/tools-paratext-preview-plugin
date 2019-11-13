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
using TptMain.Form;
using TptMain.Util;
using TptMain.Models;
using static System.Environment;

namespace TptMain.Workflow
{
    /// <summary>
    /// Main typesetting preview workflow.
    /// </summary>
    public class TypesettingPreviewWorkflow
    {
        /// <summary>
        /// Current, observed project details from server.
        /// </summary>
        private ProjectDetails _projectDetails;

        /// <summary>
        /// Current, observed preview job from server.
        /// </summary>
        private PreviewJob _previewJob;

        /// <summary>
        /// Current preview file, downloaded from server.
        /// </summary>
        private FileInfo _previewFile;

        /// <summary>
        /// Details updated event handler.
        /// </summary>
        public EventHandler<ProjectDetails> DetailsUpdated;

        /// <summary>
        /// Job updated event handler.
        /// </summary>
        public EventHandler<PreviewJob> JobUpdated;

        /// <summary>
        /// File downloaded event handler.
        /// </summary>
        public EventHandler<FileInfo> FileDownloaded;

        /// <summary>
        /// Entry point method.
        /// </summary>
        /// <param name="host">Host interface, providing Paratext services (required).</param>
        /// <param name="activeProjectName">Active Paratext project name (required).</param>
        public virtual void Run(IHost host, string activeProjectName)
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
            // Provided because plugins are separate processes that may only be attached to,
            // once instantiated (can't run Paratext and automatically attach, as with shared libraries).
            ShowMessageBox($"Attach debugger now to PID {Process.GetCurrentProcess().Id}, if needed!",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif
            // Ensures the active project is available on the server.
            _projectDetails = CheckProjectName(activeProjectName);
            if (_projectDetails == null)
            {
                return;
            }
            DetailsUpdated?.Invoke(this, _projectDetails);

            // Create & show setup form to user to get preview input.
            SetupForm setupForm = new SetupForm();
            setupForm.SetProjectDetails(_projectDetails);

            ShowModalForm(setupForm);

            if (!setupForm.IsCreating)
            {
                return;
            }

            // Create, instrument, and show progress form.
            ProgressForm progressForm = new ProgressForm();
            progressForm.Cancelled += ProgressFormCancelled;

            ShowModelessForm(progressForm);

            try
            {
                // Create preview job on server and start monitoring.
                _previewJob = CreatePreviewJob(setupForm.PreviewJob);
                JobUpdated?.Invoke(this, _previewJob);

                // Update preview form with initial status.
                progressForm.SetStatus(_previewJob);
                DateTime lastCheckTime = DateTime.Now;
                int threadSleepInMs = (int)(1000f / (float)MainConsts.PROGRESS_FORM_UPDATE_RATE_IN_FPS);

                // Update preview form at fast interval (e.g., 20x/sec) to keep UI lively, but
                // update job status at slow interval (e.g., every 5sec) to keep network traffic sane.
                while (!_previewJob.IsError
                    && !_previewJob.IsCancelled
                    && !_previewJob.IsCompleted)
                {
                    // Recalcs progress bar, even if using previous job status
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
                            // Check if it's time to update status and do so, as needed.
                            DateTime nowTime = DateTime.Now;
                            if (nowTime.Subtract(lastCheckTime).TotalSeconds > MainConsts.PREVIEW_JOB_UPDATE_INTERVAL_IN_SEC)
                            {
                                _previewJob = UpdatePreviewJob(_previewJob.Id);
                                JobUpdated?.Invoke(this, _previewJob);

                                lastCheckTime = nowTime;
                            }
                        }
                    }
                }

                // Deal with error or cancel
                if (_previewJob.IsError)
                {
                    throw new ApplicationException("Server error");
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

            // Retrieve file from server, if we've made it this far
            // (download errors will throw from here).
            _previewFile = DownloadPreviewFile(_previewJob);
            FileDownloaded?.Invoke(this, _previewFile);

            try
            {
                // Create, instrument, and show preview form
                PreviewForm previewForm = new PreviewForm();
                previewForm.FormClosed += PreviewFormFormClosed;

                previewForm.SetPreviewFile(_previewJob, _previewFile);
                ShowModalForm(previewForm);
            }
            finally
            {
                // Get rid of temp dir preview file, no matter what
                if (_previewFile.Exists)
                {
                    _previewFile.Delete();
                }
            }
        }

        /// <summary>
        /// Gives user opportunity to save preview file someplace else.
        /// </summary>
        /// <param name="sender">Event source (form).</param>
        /// <param name="e">Form closed details.</param>
        public virtual void PreviewFormFormClosed(object sender, FormClosedEventArgs e)
        {
            if (ShowMessageBox($"Save preview file for project \"{_projectDetails.ProjectName}\", updated {_projectDetails.ProjectUpdated.ToString("u")}?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SaveFileDialog saveFile = new SaveFileDialog())
                {
                    string dateTimeText = _projectDetails.ProjectUpdated.ToString("yyyyMMdd'T'HHmmss'Z'");

                    saveFile.FileName = $"preview-{_previewJob.ProjectName}-{_previewJob.BookFormat}-{dateTimeText}.pdf";
                    saveFile.InitialDirectory = Environment.GetFolderPath(SpecialFolder.MyDocuments);
                    saveFile.Filter = "Adobe PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                    saveFile.DefaultExt = "pdf";
                    saveFile.AddExtension = true;
                    saveFile.OverwritePrompt = true;

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

        /// <summary>
        /// Downloads preview file from service to temp file.
        /// </summary>
        /// <param name="previewJob">Preview job (required).</param>
        /// <returns>Downloaded temp file.</returns>
        public virtual FileInfo DownloadPreviewFile(PreviewJob previewJob)
        {
            FileInfo downloadFile = new FileInfo(Path.Combine(Path.GetTempPath(), $"preview-{previewJob.Id}.pdf"));
            WebRequest webRequest = WebRequest.Create($"{MainConsts.DEFAULT_SERVER_URI}/PreviewFile/{previewJob.Id}");
            webRequest.Method = HttpMethod.Get.Method;

            using (Stream inputStream = webRequest.GetResponse().GetResponseStream())
            {
                using (FileStream outputStream = downloadFile.OpenWrite())
                {
                    inputStream.CopyTo(outputStream);
                }
            }

            return downloadFile;
        }

        /// <summary>
        /// Called when user cancels render process.
        /// 
        /// Locks on workflow instance, as this overwrites member _previewJob (no return from event handler).
        /// </summary>
        /// <param name="sender">Event source (cancel button).</param>
        /// <param name="e">Event args.</param>
        public virtual void ProgressFormCancelled(object sender, EventArgs e)
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

        /// <summary>
        /// Creates (starts) preview job on server.
        /// </summary>
        /// <param name="previewJob">Input preview job, with user settings.</param>
        /// <returns>Created preview job, with user settings, server-side status, and ID.</returns>
        public virtual PreviewJob CreatePreviewJob(PreviewJob previewJob)
        {
            WebRequest webRequest = WebRequest.Create($"{MainConsts.DEFAULT_SERVER_URI}/PreviewJobs");
            webRequest.Method = HttpMethod.Post.Method;
            webRequest.ContentType = MainConsts.APPLICATION_JSON_MIME_TYPE;

            using (StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(previewJob));
            }
            using (StreamReader streamReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            {
                return JsonConvert.DeserializeObject<PreviewJob>(streamReader.ReadToEnd());
            }
        }

        /// <summary>
        /// Update the preview job from the server, using its ID.
        /// </summary>
        /// <param name="jobId">Job ID (required).</param>
        /// <returns>Updated preview job, with user settings, server-side status, and ID.</returns>
        public virtual PreviewJob UpdatePreviewJob(string jobId)
        {
            WebRequest webRequest = WebRequest.Create($"{MainConsts.DEFAULT_SERVER_URI}/PreviewJobs/{jobId}");
            webRequest.Method = HttpMethod.Get.Method;

            using (StreamReader streamReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            {
                return JsonConvert.DeserializeObject<PreviewJob>(streamReader.ReadToEnd());
            }
        }

        /// <summary>
        /// Checks whether project name exists on server and retrieves last update time.
        /// </summary>
        /// <param name="projectName">Paratext project name (required).</param>
        /// <returns>Project details if found, null otherwise.</returns>
        public virtual ProjectDetails CheckProjectName(string projectName)
        {
            try
            {
                WebRequest webRequest = WebRequest.Create($"{MainConsts.DEFAULT_SERVER_URI}/ProjectDetails");
                webRequest.Method = HttpMethod.Get.Method;

                using (StreamReader streamReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
                {
                    List<ProjectDetails> allProjectDetails = JsonConvert.DeserializeObject<List<ProjectDetails>>(streamReader.ReadToEnd());

                    // It's possible there aren't any.
                    if (allProjectDetails.Count < 1)
                    {
                        ShowMessageBox($"Can't preview project: \"{projectName}\" (none present on server).\n\nPlease try again later or contact support.",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        return null;
                    }

                    // Find the one that matters to us.
                    ProjectDetails result = allProjectDetails
                        .FirstOrDefault(detailsItem => detailsItem.ProjectName.Equals(projectName, StringComparison.CurrentCultureIgnoreCase));

                    // Not found = error, done.
                    if (result == null)
                    {
                        // Sort and list projects we do have.
                        allProjectDetails.Sort((l, r) => l.ProjectName.CompareTo(r.ProjectName));
                        string availableProjects = string.Join(", ", allProjectDetails.Select(detailItem => $"\"{detailItem.ProjectName}\""));

                        ShowMessageBox($"Can't preview project: \"{projectName}\" (not present on server).\n\nPlease try again later or contact support (available projects: {availableProjects}).",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox($"Can't contact typesetting preview server.\n\nPlease check Internet connection and try again, or contact support (Details: {ex.Message}).",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        /// <summary>
        /// Overridable utility method to show forms modally.
        /// </summary>
        /// <param name="inputForm">Input form (required).</param>
        public virtual void ShowModalForm(System.Windows.Forms.Form inputForm)
        {
            Application.Run(inputForm);
        }

        /// <summary>
        /// Overridable utility method to show forms modelessly.
        /// </summary>
        /// <param name="inputForm">Input form (required).</param>
        public virtual void ShowModelessForm(System.Windows.Forms.Form inputForm)
        {
            inputForm.Show();
        }

        /// <summary>
        /// Overridable utility method to show message boxes.
        /// </summary>
        /// <param name="messageText">Message box text (required).</param>
        /// <param name="messageButtons">Message box buttons (required).</param>
        /// <param name="messageIcon">Message box icon (required).</param>
        /// <returns>Result from message box call (e.g., "Cancel").</returns>
        public virtual DialogResult ShowMessageBox(string messageText, MessageBoxButtons messageButtons, MessageBoxIcon messageIcon)
        {
            return MessageBox.Show(messageText, "Notice...", messageButtons, messageIcon);
        }
    }
}
