using AddInSideViews;
using System;
using System.AddIn;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using tools_paratext_preview_plugin.Util;
using tools_paratext_preview_plugin.Workflow;

namespace tools_paratext_preview_plugin
{
    /*
     * Positions the launch for the Translation Validation Plugin in the Main Tools drop down in Paratext.
     */
    [AddIn("Typesetting Preview Plugin", Description = "Provides printable typesetting preview.", Version = "1.0", Publisher = "Biblica")]
    [QualificationData(PluginMetaDataKeys.menuText, "Typesetting Preview")]
    [QualificationData(PluginMetaDataKeys.insertAfterMenuName, "Tools|")]
    [QualificationData(PluginMetaDataKeys.multipleInstances, CreateInstanceRule.always)]
    public class TypesettingPreviewPlugin : IParatextAddIn2
    {
        public Dictionary<string, IPluginDataFileMergeInfo> DataFileKeySpecifications
        {
            get { return null; }
        }

        public void Activate(string activeProjectName)
        {
        }

        public void RequestShutdown()
        {
            Environment.Exit(0);
        }

        public void Run(IHost host, string activeProjectName)
        {
            lock (this)
            {

                ErrorUtil.Host = host;
                ErrorUtil.TranslationValidationPlugin = this;

                try
                {
                    Application.EnableVisualStyles();
                    Thread uiThread = new Thread(() =>
                    {
                        try
                        {
                            TypesettingPreviewWorkflow previewWorkflow = new TypesettingPreviewWorkflow();
                            previewWorkflow.Run(host, activeProjectName);
                        }
                        catch (Exception ex)
                        {
                            ErrorUtil.ReportError("Can't run workflow", ex);
                        }
                        finally
                        {
                            Environment.Exit(0);
                        }
                    });

                    uiThread.IsBackground = false;
                    uiThread.SetApartmentState(ApartmentState.STA);
                    uiThread.Start();
                }
                catch (Exception ex)
                {
                    ErrorUtil.ReportError(ex);
                    throw;
                }
            }
        }
    }
}
