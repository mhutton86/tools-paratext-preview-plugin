using AddInSideViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TptMain.Util
{
    /// <summary>
    /// Process-wide error utilities.
    /// </summary>
    public class HostUtil
    {
        private static readonly HostUtil _instance = new HostUtil();

        public static HostUtil Instance
        {
            get => _instance;
        }

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
        /// Reports exception to log and message box w/o prefix text.
        /// </summary>
        /// <param name="ex"></param>
        public void ReportError(Exception ex)
        {
            ReportError(null, ex);
        }

        /// <summary>
        /// Reports exception to log and message box w/prefix text.
        /// </summary>
        /// <param name="prefixText">Prefix text (optional, may be null; default used when null).</param>
        /// <param name="ex">Exception (required).</param>
        public void ReportError(string prefixText, Exception ex)
        {
            string messageText = (prefixText ?? "Error: Please contact support.")
                + Environment.NewLine + Environment.NewLine
                + "Details: " + ex.ToString() + Environment.NewLine;
            MessageBox.Show(messageText, "Notice...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            LogLine($"Error: {messageText}", true);
        }

        public void LogLine(String inputText, bool isError)
        {
            (isError ? Console.Error : Console.Out).WriteLine(inputText);
            if (_host != null)
            {
                _host.WriteLineToLog(_typesettingPreviewPlugin, inputText);
            }
        }
    }
}
