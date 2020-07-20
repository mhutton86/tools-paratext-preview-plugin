using AddInSideViews;
using Paratext.Data.ProjectSettingsAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Windows.Forms;
using TptMain.Form;
using TptMain.Models;
using TptMain.Util;
using static System.Environment;

namespace TptMain.Workflow
{
    /// <summary>
    /// Main typesetting preview workflow.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentlySynchronizedField")]
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
        /// A flag to determine if an archive will be downloaded with a preview.
        /// </summary>
        private Boolean _isArchive;

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
        /// TypesettingPreviewWorkflow ctor.
        /// </summary>
        public TypesettingPreviewWorkflow()
        {
            // Use the TLS 1.2 protocol for HTTPS requests.
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        // Get footnote caller sequence
        //public ParatextProjectSettings() = Paratext.Data.ProjectSettingsAccess.ProjectSettings;

        /// <summary>
        /// Entry point method.
        /// </summary>
        /// <param name="host">Host interface, providing Paratext services (required).</param>
        /// <param name="activeProjectName">Active Paratext project name (required).</param>
        public virtual void Run(IHost host, string activeProjectName)
        {
            _ = host ?? throw new ArgumentNullException(nameof(host));
            _ = activeProjectName ?? throw new ArgumentNullException(nameof(activeProjectName));

#if DEBUG
            // Provided because plugins are separate processes that may only be attached to,
            // once instantiated (can't run Paratext and automatically attach, as with shared libraries).
            ShowMessageBox($"Attach debugger now to PID {Process.GetCurrentProcess().Id}, if needed!",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif

            try
            {
                // Ensures the active project is available on the server.
                _projectDetails = CheckProjectName(activeProjectName);
                DetailsUpdated?.Invoke(this, _projectDetails);

                // Create & show setup form to user to get preview input.
                var setupForm = CreateSetupForm();
                setupForm.SetProjectDetails(_projectDetails);
                setupForm.User = host.UserName;

                // Enable the setup form's custom footnote option based on the availability of footnotes.
                var footnotesDefined = IsFootnoteCallerSequenceDefined(activeProjectName);
                setupForm.SetCustomFootnotesEnabled(footnotesDefined);

                ShowModalForm(setupForm);
                if (setupForm.IsCancelled)
                {
                    return;
                }

                _isArchive = setupForm.IsArchive;

                // Create, instrument, and show progress form.
                var progressForm = CreateProgressForm();
                progressForm.Cancelled += OnProgressFormCancelled;

                ShowModelessForm(progressForm);

                try
                {
                    // Create preview job on server and start monitoring.
                    _previewJob = CreatePreviewJob(setupForm.PreviewJob);
                    JobUpdated?.Invoke(this, _previewJob);

                    // Update preview form with initial status.
                    progressForm.SetStatus(_previewJob);
                    var lastCheckTime = DateTime.Now;
                    const int threadSleepInMs = (int)(1000f / MainConsts.PROGRESS_FORM_UPDATE_RATE_IN_FPS);

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
                            if (_previewJob.IsError
                                || _previewJob.IsCancelled
                                || _previewJob.IsCompleted)
                            {
                                continue;
                            }

                            // Check if it's time to update status and do so, as needed.
                            var nowTime = DateTime.Now;
                            if (!(nowTime.Subtract(lastCheckTime).TotalSeconds >
                                  MainConsts.PREVIEW_JOB_UPDATE_INTERVAL_IN_SEC))
                            {
                                continue;
                            }

                            _previewJob = UpdatePreviewJob(_previewJob.Id);
                            JobUpdated?.Invoke(this, _previewJob);

                            lastCheckTime = nowTime;
                        }
                    }

                    // Deal with error or cancel
                    if (_previewJob.IsCancelled)
                    {
                        return;
                    }
                    else if (_previewJob.IsError)
                    {
                        throw new WorkflowException($"A server error occurred: {_previewJob.ErrorMessage}");
                    }
                }
                finally
                {
                    HideModelessForm(progressForm);
                }

                // Retrieve file from server, if we've made it this far
                // _isArchive if flag is true preview job will download with the typesetting files;
                // else just the PDF will download.
                // (download errors will throw from here).
                _previewFile = DownloadPreviewFile(_previewJob, _isArchive);
                FileDownloaded?.Invoke(this, _previewFile);
                
            }
            catch (WorkflowException)
            {
                throw;
            }
            catch (IOException ex)
            {
                throw new WorkflowException("Error: Can't read or write data. Please contact support.", ex);
            }
            catch (Exception ex)
            {
                throw new WorkflowException("Error: Can't generate preview. Please contact support.", ex);
            }
        }

        /// <summary>
        /// Kicks off the dialog for the user to save the typesetting preview file.
        /// </summary>
        public virtual void StartPreviewSaveDialog()
        {
            if (ShowMessageBox($"Save preview file for project \"{_projectDetails.ProjectName}\", updated {_projectDetails.ProjectUpdated:u}?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var saveFile = new SaveFileDialog())
                {
                    var dateTimeText = _projectDetails.ProjectUpdated.ToString(MainConsts.DEFAULT_OUTPUT_FILE_NAME_FORMAT);
                    saveFile.InitialDirectory = Environment.GetFolderPath(SpecialFolder.MyDocuments);
                    saveFile.AddExtension = true;
                    saveFile.OverwritePrompt = true;

                    if (_isArchive)
                    {
                        saveFile.FileName = $"preview-{_previewJob.ProjectName}-{_previewJob.BookFormat}-{dateTimeText}.zip";
                        saveFile.Filter = "Zip file (*.zip)|*.zip|All files (*.*)|*.*";
                        saveFile.DefaultExt = "zip";                        
                    }
                    else
                    {
                        saveFile.FileName = $"preview-{_previewJob.ProjectName}-{_previewJob.BookFormat}-{dateTimeText}.pdf";
                        saveFile.Filter = "Adobe PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                        saveFile.DefaultExt = "pdf";
                    }

                    if (saveFile.ShowDialog() == DialogResult.OK)
                    {
                        using (var outputStream = saveFile.OpenFile())
                        {
                            using (var inputStream = _previewFile.OpenRead())
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
        /// The temp file can either be a .zip if isArchive is true or a PDF if false.
        /// </summary>
        /// <param name="previewJob">Preview job (required).</param>
        /// <returns>Downloaded temp file.</returns>
        public virtual FileInfo DownloadPreviewFile(PreviewJob previewJob, bool isArchive)
        {
            FileInfo downloadFile;
            if (isArchive)
            {
              downloadFile = new FileInfo(Path.Combine(Path.GetTempPath(), $"preview-{previewJob.Id}.zip"));
            }
            else
            {
              downloadFile = new FileInfo(Path.Combine(Path.GetTempPath(), $"preview-{previewJob.Id}.pdf"));
            }
            
            var webRequest = WebRequest.Create($"{MainConsts.DEFAULT_SERVER_URI}/PreviewFile/{previewJob.Id}?archive={isArchive}");
            webRequest.Method = HttpMethod.Get.Method;
            webRequest.Timeout = MainConsts.DEFAULT_REQUEST_TIMEOUT_IN_MS;

            using (var inputStream = webRequest.GetResponse().GetResponseStream())
            {
                using (var outputStream = downloadFile.OpenWrite())
                {
                    inputStream?.CopyTo(outputStream);
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
        public virtual void OnProgressFormCancelled(object sender, EventArgs e)
        {
            if (_previewJob == null)
            {
                return;
            }

            lock (this)
            {
                var webRequest = WebRequest.Create($"{MainConsts.DEFAULT_SERVER_URI}/PreviewJobs/{_previewJob.Id}");
                webRequest.Method = HttpMethod.Delete.Method;
                webRequest.Timeout = MainConsts.DEFAULT_REQUEST_TIMEOUT_IN_MS;

                using (var streamReader = new StreamReader(webRequest.GetResponse()
                                                               .GetResponseStream()
                    ?? throw new InvalidOperationException("Can't open response stream")))
                {
                    _previewJob = JsonConvert.DeserializeObject<PreviewJob>(streamReader.ReadToEnd());
                }
            }
        }

        /// <summary>
        /// Creates and starts preview job on server.
        /// </summary>
        /// <param name="previewJob">Input preview job, with user settings.</param>
        /// <returns>Created preview job, with user settings, server-side status, and ID.</returns>
        public virtual PreviewJob CreatePreviewJob(PreviewJob previewJob)
        {
            var webRequest = WebRequest.Create($"{MainConsts.DEFAULT_SERVER_URI}/PreviewJobs");
            webRequest.Method = HttpMethod.Post.Method;
            webRequest.Timeout = MainConsts.DEFAULT_REQUEST_TIMEOUT_IN_MS;
            webRequest.ContentType = MainConsts.APPLICATION_JSON_MIME_TYPE;

            using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(previewJob));
            }
            using (var streamReader = new StreamReader(webRequest.GetResponse().GetResponseStream()
                ?? throw new InvalidOperationException("Can't open response stream")))
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
            var webRequest = WebRequest.Create($"{MainConsts.DEFAULT_SERVER_URI}/PreviewJobs/{jobId}");
            webRequest.Method = HttpMethod.Get.Method;
            webRequest.Timeout = MainConsts.DEFAULT_REQUEST_TIMEOUT_IN_MS;

            using (var streamReader = new StreamReader(webRequest.GetResponse().GetResponseStream()
                        ?? throw new InvalidOperationException("Can't open response stream")))
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
                var webRequest = WebRequest.Create($"{MainConsts.DEFAULT_SERVER_URI}/ProjectDetails");
                webRequest.Method = HttpMethod.Get.Method;
                webRequest.Timeout = MainConsts.DEFAULT_REQUEST_TIMEOUT_IN_MS;

                using (var streamReader = new StreamReader(webRequest
                                              .GetResponse()
                                              .GetResponseStream())
                    ?? throw new InvalidOperationException("Can't open response stream"))
                {
                    var allProjectDetails = JsonConvert.DeserializeObject<List<ProjectDetails>>(streamReader.ReadToEnd());

                    // It's possible there aren't any.
                    if (allProjectDetails.Count < 1)
                    {
                        throw new WorkflowException($"Can't preview project: \"{projectName}\" (none present on server).\n\nPlease try again later or contact support.");
                    }

                    // Find the one that matters to us.
                    var result = allProjectDetails
                        .FirstOrDefault(detailsItem => detailsItem.ProjectName.Equals(projectName, StringComparison.CurrentCultureIgnoreCase));

                    // Not found = error, done.
                    if (result == null)
                    {
                        // Sort and list projects we do have.
                        allProjectDetails.Sort((l, r) => string.Compare(l.ProjectName, r.ProjectName, StringComparison.Ordinal));
                        var availableProjects = string.Join(", ", allProjectDetails.Select(detailItem => $"\"{detailItem.ProjectName}\""));

                        throw new WorkflowException($"Can't preview project: \"{projectName}\" (not present on server).\n\nPlease try again later or contact support (available projects: {availableProjects}).");
                    }

                    return result;
                }
            }
            catch (WorkflowException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new WorkflowException($"Can't contact typesetting preview server.\n\nPlease check Internet connection and try again, or contact support (Details: {ex.Message}).", ex);
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
        /// Overridable utility method to hide modeless forms.
        /// </summary>
        /// <param name="inputForm">Input form (required).</param>
        public virtual void HideModelessForm(System.Windows.Forms.Form inputForm)
        {
            inputForm.Hide();
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

        /// <summary>
        /// Overridable utility method to create setup form.
        /// </summary>
        /// <returns>Setup form.</returns>
        public virtual SetupForm CreateSetupForm()
        {
            return new SetupForm();
        }

        /// <summary>
        /// Overridable utility method to create progress form.
        /// </summary>
        /// <returns>Progress form.</returns>
        public virtual ProgressForm CreateProgressForm()
        {
            return new ProgressForm();
        }

        /// <summary>
        /// TODO documenting me.
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public virtual bool IsFootnoteCallerSequenceDefined(string projectName)
        {
            // get the project's directory
            var projectDirectory = HostUtil.Instance.GetParatextProjectDirectory(projectName);

            // get the project's footnote caller sequence, knowing the project directory
            var footnotes = ParatextProjectHelper.GetFootnoteCallerSequence(projectDirectory);

            return footnotes != null && footnotes.Length > 0;
        }
    }
}