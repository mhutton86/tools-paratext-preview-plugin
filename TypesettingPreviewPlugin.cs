using AddInSideViews;
using System;
using System.AddIn;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using TptMain.Models;
using TptMain.Util;
using TptMain.Workflow;

namespace TptMain
{
    /// <summary>
    /// Typesetting preview plugin root class.
    /// </summary>
    [AddIn("Typesetting Preview Plugin", Description = "Provides printable typesetting preview.", Version = "1.0", Publisher = "Biblica")]
    [QualificationData(PluginMetaDataKeys.menuText, "Typesetting Preview")]
    [QualificationData(PluginMetaDataKeys.insertAfterMenuName, "Tools|")]
    [QualificationData(PluginMetaDataKeys.multipleInstances, CreateInstanceRule.always)]
    public class TypesettingPreviewPlugin : IParatextAddIn2
    {
        /// <summary>
        /// Most-recent project details observed by workflow. Will be null until workflow is underway.
        /// </summary>
        private ProjectDetails _projectDetails;

        /// <summary>
        /// Most-recent preview job observed by workflow. Will be null until workflow is underway.
        /// </summary>
        private PreviewJob _previewJob;

        /// <summary>
        /// Most-recent preview file observed by workflow. Will be null until workflow is underway (and near-complete).
        /// </summary>
        private FileInfo _previewFile;

        /// <summary>
        /// No-op, to fulfill IParatextAddIn2 contract.
        /// </summary>
        public Dictionary<string, IPluginDataFileMergeInfo> DataFileKeySpecifications => null;

        /// <summary>
        /// No-op, to fulfill IParatextAddIn2 contract.
        ///
        /// Should never by invoked when CreateInstanceRule.always setting in place (above).
        /// </summary>
        /// <param name="activeProjectName">Active Paratext project name.</param>
        public void Activate(string activeProjectName)
        {
        }

        /// <summary>
        /// Called when plugin is requested to shut down.
        ///
        /// Terminates process, since plugins are standalone processes (not in-process libraries).
        /// </summary>
        public void RequestShutdown()
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Entry point method.
        /// </summary>
        /// <param name="host">Host interface, providing access to Paratext services.</param>
        /// <param name="activeProjectName">Active Paratext project name.</param>
        public void Run(IHost host, string activeProjectName)
        {
            lock (this)
            {
                // track host & plugin reference for static error utilities
                HostUtil.Instance.Host = host;
                HostUtil.Instance.TypesettingPreviewPlugin = this;

                try
                {
                    // Create main thread & delegate
                    Application.EnableVisualStyles();
                    var uiThread = new Thread(() =>
                    {
                        try
                        {
                            // Create and instrument workflow
                            var previewWorkflow = new TypesettingPreviewWorkflow();

                            previewWorkflow.DetailsUpdated += (currWorkflow, projectDetails) =>
                            {
                                _projectDetails = projectDetails;
                            };
                            previewWorkflow.JobUpdated += (currWorkflow, previewJob) => { _previewJob = previewJob; };
                            previewWorkflow.FileDownloaded += (currWorkflow, previewFile) =>
                            {
                                _previewFile = previewFile;
                            };

                            // Execute workflow. Will not return until complete.
                            previewWorkflow.Run(host, activeProjectName);
                        }
                        catch (Exception ex)
                        {
                            // Log with different information, depending on what's been observed.
                            if (_previewFile != null
                                && _previewJob != null
                                && _projectDetails != null)
                            {
                                HostUtil.Instance.ReportError(
                                    $"Can't display preview file (file: {_previewFile.FullName}, job id: \"{_previewJob.Id}\", project: \"{_previewJob.ProjectName}\", updated: {_projectDetails.ProjectUpdated:u}).",
                                    ex);
                            }
                            else if (_previewJob != null
                                     && _projectDetails != null)
                            {
                                HostUtil.Instance.ReportError(
                                    $"Can't generate preview file (job id: \"{_previewJob.Id}\", project: \"{_previewJob.ProjectName}\", updated: {_projectDetails.ProjectUpdated:u}).",
                                    ex);
                            }
                            else if (_projectDetails != null)
                            {
                                HostUtil.Instance.ReportError(
                                    $"Can't get preview options (project: \"{_projectDetails.ProjectName}\", updated: {_projectDetails.ProjectUpdated:u}).",
                                    ex);
                            }
                            else
                            {
                                HostUtil.Instance.ReportError($"Can't execute workflow.", ex);
                            }
                        }
                        finally
                        {
                            // Exit process (terminate plugin) once complete, no matter what.
                            Environment.Exit(0);
                        }
                    })
                    { IsBackground = false };

                    // Execute main thread.
                    uiThread.SetApartmentState(ApartmentState.STA);
                    uiThread.Start();
                }
                catch (Exception ex)
                {
                    // Log any errors that make it this far and re-throw to give Paratext a heads-up.
                    HostUtil.Instance.ReportError(ex);
                    throw;
                }
            }
        }
    }
}