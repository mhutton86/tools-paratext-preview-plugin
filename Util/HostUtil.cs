using AddInSideViews;
using Paratext.Data;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using TvpMain.Import;
using TvpMain.Project;

namespace TptMain.Util
{
    /// <summary>
    /// Process-wide error utilities.
    /// </summary>
    public class HostUtil
    {
        /// <summary>
        /// Private singleton instance.
        /// </summary>
        private static readonly HostUtil _instance = new HostUtil();

        /// <summary>
        /// Administrator role name
        /// </summary>
        private const string ADMIN_ROLE = "Administrator";

        /// <summary>
        /// Indicates whether ParatextData has been initialized.
        ///
        /// Note: Uses int because Interlocked.CompareExchange doesn't work with bool.
        /// </summary>
        private int _isParatextDataInit = 0;

        /// <summary>
        /// Event used to track paratext data initialization complete.
        /// </summary>
        private readonly CountdownEvent _paratextDataSetupEvent = new CountdownEvent(1);

        /// <summary>
        /// Thread-safe singleton instance.
        /// </summary>
        public static HostUtil Instance => _instance;

        /// <summary>
        /// Global reference to plugin, to route logging.
        /// </summary>
        private TypesettingPreviewPlugin _typesettingPreviewPlugin;

        /// <summary>
        /// Global reference to host interface, providing Paratext services including logging.
        /// </summary>
        private IHost _host;

        /// <summary>
        /// Property for assignment from plugin entry method.
        /// </summary>
        public TypesettingPreviewPlugin TypesettingPreviewPlugin { set => _typesettingPreviewPlugin = value; }

        /// <summary>
        /// Property for assignment from plugin entry method.
        /// </summary>
        public IHost Host { set => _host = value; }

        /// <summary>
        /// Reports exception to log and message box w/prefix text.
        ///
        /// Either prefixText (or) ex must be non-null.
        /// </summary>
        /// <param name="prefixText">Prefix text (optional, may be null; default used when null).</param>
        /// <param name="ex">Exception (optional, may be null).</param>
        public void ReportError(string prefixText, Exception ex)
        {
            if (prefixText == null && ex == null)
            {
                throw new ArgumentNullException("prefixText (or) ex must be non-null");
            }

            var messageText = (prefixText ?? "Error: Please contact support.")
                + (ex == null ? string.Empty
                    : Environment.NewLine + Environment.NewLine
                    + "Details: " + ex + Environment.NewLine);

            MessageBox.Show(messageText, "Notice...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            LogLine($"Error: {messageText}", true);
        }

        /// <summary>
        /// Log text to Paratext's app log and the console.
        /// </summary>
        /// <param name="inputText">Input text (required).</param>
        /// <param name="isError">Error flag.</param>
        public void LogLine(string inputText, bool isError)
        {
            (isError ? Console.Error : Console.Out).WriteLine(inputText);
            _host?.WriteLineToLog(_typesettingPreviewPlugin, inputText);
        }

        /// <summary>
        /// This function will get the absolute path for a given paratext project.
        /// </summary>
        /// <param name="projectName">A Paratext project's shortname, we want the directory of EG: "usNIVv3"</param>
        /// <returns>An absolute path to a given Paratext project.</returns>
        public DirectoryInfo GetParatextProjectDirectory(string projectName)
        {
            // validate inputs
            _ = projectName ?? throw new ArgumentNullException(nameof(projectName));

            // We're using the host's figure path to determine project directory, 
            // as the Paratext AddinViews doesn't appear to provide a function to do this
            var figurePath = _host.GetFigurePath(projectName, false);

            // If the non-local figure path is unavailable, get the local figure path
            if (figurePath == null)
            {
                figurePath = _host.GetFigurePath(projectName, true);

                if (figurePath == null)
                {
                    throw new Exception("We couldn't find the project path for " + projectName);
                }

                // return the project directory from the local figure directory. EG: usNIV11/local/figure/ -> usNIV11
                return Directory.GetParent(Directory.GetParent(figurePath).FullName);
            }

            // return the project directory from the non-local figure directory. EG: usNIV11/figure/ -> usNIV11
            return Directory.GetParent(figurePath);
        }

        /// <summary>
        /// Method to determine if the current user is an administrator or not.
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns>True, if the current user is an Admin for the given project</returns>
        public bool isCurrentUserAdmin(string projectName)
        {
            ImportManager importManager = new ImportManager(Instance._host, projectName);

            // using this method requires including ImportManager
            string rolename = importManager.ProjectScrText.Permissions.GetRole().ToString();
            return rolename.Equals(ADMIN_ROLE);

        }

        /// <summary>
        /// Set up the ParatextData libraries for project input/output.
        ///
        /// Will block until initialization complete, which takes at least a few seconds
        /// on typical systems and may scale per the number of projects.
        /// 
        /// Thread safe, may be called repeatedly
        /// </summary>
        /// <param name="isToBlock">True to block until initialization complete, false otherwise.</param>
        public void InitParatextData(bool isToBlock)
        {
            if (Interlocked.CompareExchange(ref _isParatextDataInit, 1, 0) == 0)
            {
                System.Threading.Tasks.Task.Run(() =>
                {
                    try
                    {
                        var executingAssembly = Assembly.GetExecutingAssembly();
                        var assemblyPath = Path.GetDirectoryName(executingAssembly.Location);
                        if (assemblyPath == null)
                        {
                            throw new InvalidOperationException(
                                $"plugin assembly in unexpected location: {executingAssembly.Location}");
                        }

                        var assemblyDir = new DirectoryInfo(assemblyPath);
                        if (assemblyDir.Parent?.Parent == null)
                        {
                            throw new InvalidOperationException(
                                $"plugin directory in unexpected location: {assemblyDir.FullName}");
                        }

                        // fall back on plugin working dir, if paratext.exe not found
                        var paratextDir = assemblyDir.Parent.Parent;
                        PtxUtils.Platform.BaseDirectory =
                            File.Exists(Path.Combine(paratextDir.FullName, "Paratext.exe"))
                                ? paratextDir.FullName : assemblyPath;
                        ParatextData.Initialize();

#if DEBUG
                        ReportNonFatalParatextDataErrors();
#endif
                    }
                    catch (Exception ex)
                    {
                        ReportError("Can't initialize ParatextData", true, ex);
                    }
                    finally
                    {
                        _paratextDataSetupEvent.Signal();
                    }
                });
            }

            if (isToBlock)
            {
                _paratextDataSetupEvent.Wait();
            }
        }

        /// <summary>
        /// Reports non-fatal ParatextData initialization errors.
        /// </summary>
        public void ReportNonFatalParatextDataErrors()
        {
            var errorText = string.Join(Environment.NewLine,
                ScrTextCollection.ErrorMessages.Select(messageItem => $"Project: {messageItem.ProjectName}, type: {messageItem.ProjecType}, reason: {messageItem.Reason}, exception: {messageItem.Exception}."));
            if (!string.IsNullOrWhiteSpace(errorText))
            {
                ReportError("There were non-fatal initialization errors (performance may be impacted)."
                            + Environment.NewLine + Environment.NewLine
                            + errorText, false, null);
            }
        }

        /// <summary>
        /// Reports exception to log and message box w/prefix text.
        /// </summary>
        /// <param name="prefixText">Prefix text (optional, may be null; default used when null).</param>
        /// <param name="includeStackTrace">True to include stack trace, false otherwise.</param>
        /// <param name="ex">Exception (optional, may be null).</param>
        public void ReportError(string prefixText, bool includeStackTrace, Exception ex)
        {
            string messageText = null;
            if (ex == null)
            {
                messageText = (prefixText ?? "Error: Please contact support");
            }
            else
            {
                if (includeStackTrace)
                {
                    messageText = (prefixText ?? "Error: Please contact support.")
                                  + Environment.NewLine + Environment.NewLine
                                  + "Details: " + ex.ToString() + Environment.NewLine;
                }
                else
                {
                    messageText = (prefixText ?? "Error: Please contact support")
                                  + $" (Details: {ex.Message}).";
                }
            }

            MessageBox.Show(messageText, "Notice...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            LogLine(messageText, true);
        }
    }
}