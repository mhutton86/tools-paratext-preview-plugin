using AddInSideViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tools_paratext_preview_plugin.util
{
    public class ErrorUtil
    {
        static private TypesettingPreviewPlugin translationValidationPlugin;

        static private IHost host;

        public static TypesettingPreviewPlugin TranslationValidationPlugin { get => translationValidationPlugin; set => translationValidationPlugin = value; }
        public static IHost Host { get => host; set => host = value; }

        public static void ReportError(Exception ex)
        {
            reportError(null, ex);
        }

        public static void reportError(string prefix, Exception ex)
        {
            string text = (prefix ?? "") + Environment.NewLine
                + ex.Message + Environment.NewLine
                + ex.StackTrace + Environment.NewLine;

            MessageBox.Show(text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (host != null)
            {
                host.WriteLineToLog(translationValidationPlugin, $"Error: {text}");
            }
        }
    }
}
