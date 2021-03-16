using AddInSideViews;
using System;
using System.IO;
using System.Windows.Forms;
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

        private const string ADMIN_ROLE = "Administrator";

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
        /// Method to determine if the current user is an administrator or not. This loads the ProjectUserAccess.xml file from 
        /// the project and compares the users there against the current user name from IHost.
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns>True, if the current user is an Admin for the given project</returns>
        public bool isCurrentUserAdmin(string projectName)
        {
            // using this method requires including ImportManager
            //string rolename = Paratext.Data.ScrText.Permissions.GetRole();


            if (projectName == null || projectName.Length < 1)
            {
                throw new ArgumentNullException(nameof(projectName));
            }

            FileManager fileManager = new FileManager(_host, projectName);

            using Stream reader = new FileStream(Path.Combine(fileManager.ProjectDir.FullName, "ProjectUserAccess.xml"), FileMode.Open);
            ProjectUserAccess projectUserAccess = ProjectUserAccess.LoadFromXML(reader);

            foreach (User user in projectUserAccess.Users)
            {
                if (user.UserName.Equals(_host.UserName) && user.Role.Equals(ADMIN_ROLE))
                {
                    // Bail as soon as we find a match
                    return true;
                }
            }
            return false;
        }
    }
}