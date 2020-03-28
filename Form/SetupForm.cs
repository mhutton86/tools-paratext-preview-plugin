using System;
using TptMain.Models;

namespace TptMain.Form
{
    /// <summary>
    /// Setup form that captures user settings for a preview job.
    /// </summary>
    public partial class SetupForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Project details, from server.
        /// </summary>
        private ProjectDetails _projectDetails;

        /// <summary>
        /// Preview job, created here.
        /// </summary>
        private PreviewJob _previewJob;

        /// <summary>
        /// Preview job, created here.
        /// </summary>
        public virtual PreviewJob PreviewJob => _previewJob;

        /// <summary>
        /// True if user wants to create preview (clicked "Create"), false otherwise.
        /// </summary>
        public virtual bool IsCancelled { get; set; }

        /// <summary>
        /// User setter/getter.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Basic ctor.
        /// </summary>
        public SetupForm()
        {
            InitializeComponent();
            _previewJob = new PreviewJob();
        }

        /// <summary>
        /// Create button handler.
        /// </summary>
        /// <param name="sender">Event source (button).</param>
        /// <param name="e">Event args.</param>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            PopulatePreviewJob();
            Close();
        }

        /// <summary>
        /// Cancel button handler.
        /// </summary>
        /// <param name="sender">Event source (button).</param>
        /// <param name="e">Event args.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            IsCancelled = true;

            PopulatePreviewJob();
            Close();
        }

        /// <summary>
        /// Default text options handler. Disables text options fields, when checked.
        /// </summary>
        /// <param name="sender">Event source (radio button).</param>
        /// <param name="e">Event args.</param>
        private void rdoTextOptionsDefaults_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoTextOptionsDefaults.Checked)
            {
                ControlTextOptions(false);
            }
        }

        /// <summary>
        /// Custom text options handler. Enables text options fields, when checked.
        /// </summary>
        /// <param name="sender">Event source (radio button).</param>
        /// <param name="e">Event args.</param>
        private void rdoTextOptionsCustom_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoTextOptionsCustom.Checked)
            {
                ControlTextOptions(true);
            }
        }

        /// <summary>
        /// Enables/disabled text options.
        /// </summary>
        /// <param name="isEnabled">True to enable text options, false otherwise.</param>
        private void ControlTextOptions(bool isEnabled)
        {
            lblFontSize.Enabled = isEnabled;
            nudFontSize.Enabled = isEnabled;
            lblFontSizeUnits.Enabled = isEnabled;

            lblFontLeading.Enabled = isEnabled;
            nudFontLeading.Enabled = isEnabled;
            lblFontLeadingUnits.Enabled = isEnabled;
        }

        /// <summary>
        /// Fill out preview job with user settings.
        /// </summary>
        private void PopulatePreviewJob()
        {
            _previewJob.ProjectName = ProjectName;
            _previewJob.User = User;
            _previewJob.BookFormat = BookFormat;
            _previewJob.FontSizeInPts = FontSizeInPts;
            _previewJob.FontLeadingInPts = FontLeadingInPts;
            _previewJob.PageHeightInPts = PageHeightInPts;
            _previewJob.PageWidthInPts = PageWidthInPts;
            _previewJob.PageHeaderInPts = PageHeaderInPts;
        }

        /// <summary>
        /// Default page options handler. Disables page options fields, when checked.
        /// </summary>
        /// <param name="sender">Event source (radio button).</param>
        /// <param name="e">Event args.</param>
        private void rdoPageOptionsDefaults_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPageOptionsDefaults.Checked)
            {
                ControlPageOptions(false);
            }
        }

        /// <summary>
        /// Custom page options handler. Enables page options fields, when checked.
        /// </summary>
        /// <param name="sender">Event source (radio button).</param>
        /// <param name="e">Event args.</param>
        private void rdoPageOptionsCustom_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPageOptionsCustom.Checked)
            {
                ControlPageOptions(true);
            }
        }

        /// <summary>
        /// Enables/disabled page options.
        /// </summary>
        /// <param name="isEnabled">True to enable page options, false otherwise.</param>
        private void ControlPageOptions(bool isEnabled)
        {
            lblPageHeight.Enabled = isEnabled;
            nudPageHeight.Enabled = isEnabled;
            lblPageHeightUnits.Enabled = isEnabled;

            lblPageWidth.Enabled = isEnabled;
            nudPageWidth.Enabled = isEnabled;
            lblPageWidthUnits.Enabled = isEnabled;

            lblPageHeader.Enabled = isEnabled;
            nudPageHeader.Enabled = isEnabled;
            lblPageHeaderUnits.Enabled = isEnabled;
        }

        /// <summary>
        /// Font size in points if custom text options enabled, null otherwise.
        /// </summary>
        public float? FontSizeInPts => rdoTextOptionsDefaults.Checked ? (float?)null : Convert.ToSingle(nudFontSize.Value);

        /// <summary>
        /// Font leading in points if custom text options enabled, null otherwise.
        /// </summary>
        public float? FontLeadingInPts => rdoTextOptionsDefaults.Checked ? (float?)null : Convert.ToSingle(nudFontLeading.Value);

        /// <summary>
        /// Page height in points if custom page options enabled, null otherwise.
        /// </summary>
        public float? PageHeightInPts => rdoPageOptionsDefaults.Checked ? (float?)null : Convert.ToSingle(nudPageHeight.Value);

        /// <summary>
        /// Page width in points if custom page options enabled, null otherwise.
        /// </summary>
        public float? PageWidthInPts => rdoPageOptionsDefaults.Checked ? (float?)null : Convert.ToSingle(nudPageWidth.Value);

        /// <summary>
        /// Page header in points if custom page options enabled, null otherwise.
        /// </summary>
        public float? PageHeaderInPts => rdoPageOptionsDefaults.Checked ? (float?)null : Convert.ToSingle(nudPageHeader.Value);

        /// <summary>
        /// Book format accessor.
        /// </summary>
        public BookFormat BookFormat => rdoLayoutCav.Checked ? BookFormat.cav : BookFormat.tbotb;

        /// <summary>
        /// Project name accessor.
        /// </summary>
        public string ProjectName => lblProjectNameText.Text;

        /// <summary>
        /// Sets server-side project details and populates related labels.
        /// </summary>
        /// <param name="projectDetails">Project details (required).</param>
        public virtual void SetProjectDetails(ProjectDetails projectDetails)
        {
            _projectDetails = projectDetails;

            lblProjectNameText.Text = _projectDetails.ProjectName;
            lblProjectUpdatedText.Text = _projectDetails.ProjectUpdated.ToString("u");
        }
    }
}
