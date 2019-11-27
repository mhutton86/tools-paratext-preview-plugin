using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TptMain.Util;
using TptMain.Models;

namespace TptMain.Form
{
    /// <summary>
    /// Progress form.
    /// </summary>
    public partial class ProgressForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Cancel event handler, for use by workflow.
        /// </summary>
        public event EventHandler Cancelled;

        /// <summary>
        /// Preview job, with project information and user settings.
        /// </summary>
        private PreviewJob _previewJob;

        /// <summary>
        /// Basic ctor.
        /// </summary>
        public ProgressForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Updates with new preview job instance and recaclulates progress bar, as needed.
        /// 
        /// Notes:
        /// - If job is still enqued (not being rendered), progress bar will be indeterminate with appropriate label text.
        /// - If job has started (being rendered) and has been running for <90sec, progress bar will start and animate with a target time of 90sec, as there is no server-side progress information.
        /// - If job has started and has been running for >=90sec, progress bar will go back to indeterminate with different label text.
        /// </summary>
        /// <param name="previewJob"></param>
        public virtual void SetStatus(PreviewJob previewJob)
        {
            _previewJob = previewJob;
            DateTime nowTime = DateTime.UtcNow;

            if (_previewJob.IsStarted)
            {
                TimeSpan timeSpan = DateTime.UtcNow.Subtract(_previewJob.DateStarted ?? nowTime);
                lblElapsedTime.Text = GetElapsedTime(timeSpan);

                int runTimeInSec = (int)timeSpan.TotalSeconds;
                if (runTimeInSec >= MainConsts.TARGET_PREVIEW_JOB_TIME_IN_SEC)
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
                lblElapsedTime.Text = GetElapsedTime(DateTime.UtcNow.Subtract(_previewJob.DateSubmitted ?? nowTime));
                lblStatusText.Text = "Preview enqueued (please wait)...";
                pbrStatus.Style = ProgressBarStyle.Marquee;
            }
        }

        /// <summary>
        /// Helper method that reports time as "MM:SS" text form TimeSpan, showing the ":" only on odd seconds.
        /// </summary>
        /// <param name="timeSpan">Input time span (required).</param>
        /// <returns>Reported time text.</returns>
        private string GetElapsedTime(TimeSpan timeSpan)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(string.Format("{0:D2}", (int)timeSpan.TotalMinutes));
            stringBuilder.Append(((timeSpan.Seconds % 2) == 0) ? " " : ":");
            stringBuilder.Append(string.Format("{0:D2}", timeSpan.Seconds));

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Cancel event forwarder.
        /// </summary>
        /// <param name="sender">Event source (button).</param>
        /// <param name="e">Event args.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancelled?.Invoke(sender, e);
        }
    }
}
