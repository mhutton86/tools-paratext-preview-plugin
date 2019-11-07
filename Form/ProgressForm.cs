using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tools_paratext_preview_plugin.Util;
using tools_tpt_transformation_service.Models;

namespace tools_paratext_preview_plugin.Form
{
    public partial class ProgressForm : System.Windows.Forms.Form
    {
        public event EventHandler Cancelled;

        public ProgressForm()
        {
            InitializeComponent();
        }

        internal void SetStatus(PreviewJob workJob)
        {
            DateTime nowTime = DateTime.UtcNow;

            if (workJob.IsStarted)
            {
                TimeSpan timeSpan = DateTime.UtcNow.Subtract(workJob.DateStarted ?? nowTime);
                lblElapsedTime.Text = GetElapsedTime(timeSpan);

                int runTimeInSec = (int)timeSpan.TotalSeconds;
                if (runTimeInSec > MainConsts.TARGET_PREVIEW_JOB_TIME_IN_SEC)
                {
                    lblStatusText.Text = "Rendering preview (taking longer than expected)...";
                    pbrStatus.Style = ProgressBarStyle.Marquee;
                }
                else
                {
                    lblStatusText.Text = "Rendering preview...";
                    pbrStatus.Style = ProgressBarStyle.Continuous;
                    pbrStatus.Value = (int)(((float)runTimeInSec / (float)MainConsts.TARGET_PREVIEW_JOB_TIME_IN_SEC) * 100f);
                }
            }
            else
            {
                lblElapsedTime.Text = GetElapsedTime(DateTime.UtcNow.Subtract(workJob.DateSubmitted ?? nowTime));
                lblStatusText.Text = "Enqueued (please wait)...";
                pbrStatus.Style = ProgressBarStyle.Marquee;
            }
        }

        private string GetElapsedTime(TimeSpan timeSpan)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(string.Format("{0:D2}", (int)timeSpan.TotalMinutes));
            stringBuilder.Append(((timeSpan.Seconds % 2) == 0) ? " " : ":");
            stringBuilder.Append(string.Format("{0:D2}", timeSpan.Seconds));

            return stringBuilder.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancelled?.Invoke(sender, e);
        }
    }
}
