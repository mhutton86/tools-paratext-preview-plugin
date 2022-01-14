/*
Copyright © 2021 by Biblica, Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Text;
using System.Windows.Forms;
using TptMain.Models;
using TptMain.Util;

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
        /// Updates with new preview job instance and recalculates progress bar, as needed.
        ///
        /// Notes:
        /// - If job is still enqued (not being rendered), progress bar will be indeterminate with appropriate label text.
        /// - If job has started (being rendered) and has been running for &lt; the max time, progress bar will start and animate with a target time of 90sec, as there is no server-side progress information.
        /// - If job has started and has been running for &gt;= the max time, progress bar will go back to indeterminate with different label text.
        /// </summary>
        /// <param name="previewJob"></param>
        public virtual void SetStatus(PreviewJob previewJob)
        {
            _previewJob = previewJob ?? throw new ArgumentNullException(nameof(previewJob));
            Text = $"Project: \"{_previewJob.BibleSelectionParams.ProjectName}\", Format: {_previewJob.TypesettingParams.BookFormat}, Font: {_previewJob.TypesettingParams.FontSizeInPts}pt, Leading: {_previewJob.TypesettingParams.FontLeadingInPts}pt";

            var nowTime = DateTime.UtcNow;
            if (_previewJob.IsStarted)
            {
                var timeSpan = DateTime.UtcNow.Subtract(_previewJob.DateStarted ?? nowTime);
                lblElapsedTime.Text = GetElapsedTime(timeSpan);

                var runTimeInSec = (int)timeSpan.TotalSeconds;
                if (runTimeInSec >= MainConsts.TARGET_PREVIEW_JOB_TIME_IN_SEC)
                {
                    lblStatusText.Text = "Rendering preview (taking longer than expected)...";
                    pbrStatus.Style = ProgressBarStyle.Marquee;
                }
                else
                {
                    lblStatusText.Text = "Rendering preview...";
                    pbrStatus.Style = ProgressBarStyle.Continuous;
                    pbrStatus.Value = Math.Min(
                        Math.Abs((int)((runTimeInSec / (float)MainConsts.TARGET_PREVIEW_JOB_TIME_IN_SEC) * 100f)),
                        (int) pbrStatus.Maximum);
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
        private static string GetElapsedTime(TimeSpan timeSpan)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"{(int)timeSpan.TotalMinutes:D2}");
            stringBuilder.Append(((timeSpan.Seconds % 2) == 0) ? " " : ":");
            stringBuilder.Append($"{timeSpan.Seconds:D2}");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Cancel event forwarder.
        /// </summary>
        /// <param name="sender">Event source (button).</param>
        /// <param name="e">Event args.</param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Cancelled?.Invoke(sender, e);
        }
    }
}