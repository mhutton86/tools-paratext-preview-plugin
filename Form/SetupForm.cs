using System;
using System.Globalization;
using System.Windows.Forms;
using TptMain.Models;
using TptMain.Util;
using Paratext.Data.ProjectSettingsAccess;
using TptMain.Properties;
using System.Reflection;
using System.Drawing;

namespace TptMain.Form
{
    /// <summary>
    /// Setup form that captures user settings for a preview job.
    /// </summary>
    public partial class SetupForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Used to give extra margin if advanced section isn't shown
        /// </summary>
        public static int HEIGHT_MARGIN = 10;

        /// <summary>
        /// Project details, from server.
        /// </summary>
        private ProjectDetails _projectDetails;

        /// <summary>
        /// Server's status.
        /// </summary>
        private ServerStatus _serverStatus;

        /// <summary>
        /// Preview job, created here.
        /// </summary>
        private readonly PreviewJob _previewJob;

        /// <summary>
        /// Preview job, created here.
        /// </summary>
        public virtual PreviewJob PreviewJob => _previewJob;

        /// <summary>
        /// True if user wants to create preview (clicked "Create"), false otherwise.
        /// </summary>
        public virtual bool IsCancelled { get; protected set; }

        /// <summary>
        /// True if a user wants typesetting files downloaded with a preview, 
        /// false if just a PDF is wanted.
        /// </summary>
        public virtual bool IsArchive { get; protected set; }
   
        /// <summary>
        /// User setter/getter.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Basic ctor.
        /// </summary>
        public SetupForm(ProjectDetails projectDetails)
        {
            InitializeComponent();

            SetProjectDetails(projectDetails);

            Copyright.Text = MainConsts.COPYRIGHT;
            _previewJob = new PreviewJob();

            SetupFieldControl(nudFontSize, MainConsts.FontSizeSettings);
            SetupFieldControl(nudFontLeading, MainConsts.FontLeadingSettings);
            SetupFieldControl(nudPageHeight, MainConsts.PageHeightSettings);
            SetupFieldControl(nudPageWidth, MainConsts.PageWidthSettings);
            SetupFieldControl(nudPageHeader, MainConsts.PageHeaderSettings);

            // set up all tool-tips

            toolTip.SetToolTip(grpLayout, MainConsts.LAYOUT_TOOLTIP);
            toolTip.SetToolTip(rdoLayoutCav, MainConsts.LAYOUT_CAV_TOOLTIP);
            toolTip.SetToolTip(rdoLayoutTbotb, MainConsts.LAYOUT_BTOTB_TOOLTIP);

            toolTip.SetToolTip(grpBookRange, MainConsts.BOOK_RANGE);
            toolTip.SetToolTip(rbFullBible, MainConsts.BOOK_RANGE_FULL);
            toolTip.SetToolTip(rbNewTestament, MainConsts.BOOK_RANGE_NT);
            toolTip.SetToolTip(rbCustom, MainConsts.BOOK_RANGE_CUSTOM);
            toolTip.SetToolTip(tbCustomBookSet, MainConsts.BOOK_RANGE_CUSTOM);
            toolTip.SetToolTip(cbIncludeAncillary, MainConsts.BOOK_RANGE_ANCILLARY);

            toolTip.SetToolTip(grpTextOptions, MainConsts.TEXT_OPTS);
            toolTip.SetToolTip(nudFontSize, MainConsts.TEXT_FONT);
            toolTip.SetToolTip(nudFontLeading, MainConsts.TEXT_LEAD);

            toolTip.SetToolTip(grpPageOptions, MainConsts.PAGE_OPTS);
            toolTip.SetToolTip(nudPageWidth, MainConsts.PAGE_WIDTH);
            toolTip.SetToolTip(nudPageHeight, MainConsts.PAGE_HEIGHT);
            toolTip.SetToolTip(nudPageHeader, MainConsts.PAGE_HEADER);

            toolTip.SetToolTip(cbHyphenate, MainConsts.HYPHENATE);
            toolTip.SetToolTip(cbLocalizeFootnotes, MainConsts.LOCALIZE_FOOTNOTES);

            toolTip.SetToolTip(gbInclusions, MainConsts.INCLUSIONS);
            toolTip.SetToolTip(cbIntros, MainConsts.INCLUDE_INTRO);
            toolTip.SetToolTip(cbHeadings, MainConsts.INCLUDE_HEADINGS);
            toolTip.SetToolTip(cbFootnotes, MainConsts.INCLUDE_FOOTNOTES);
            toolTip.SetToolTip(cbChapterNumbers, MainConsts.INCLUDE_CHAPTER_NUMS);
            toolTip.SetToolTip(cbVerseNumbers, MainConsts.INCLUDE_VERSE_NUMS);
            toolTip.SetToolTip(cbParallelPassages, MainConsts.INCLUDE_PARALLEL);
            toolTip.SetToolTip(cbAcrostic, MainConsts.INCLUDE_ACROSTIC);

            toolTip.SetToolTip(cbDownloadTypesettingFiles, MainConsts.DOWNLOAD_TYPESETTING);
            toolTip.SetToolTip(cbUseProjectFonts, MainConsts.USE_PROJECT_FONTS);

            SetAdminView(ProjectName);

        }

        /// <summary>
        /// Change the form view, showing or hiding advanced view, based on administrator role.
        /// This was separated out to allow for testing this functionality without initializing Paratext
        /// </summary>
        /// <param name="projectName"></param>
        public virtual void SetAdminView(string projectName)
        {
            // resize, hiding advanced panel if we aren't an admin
            if (HostUtil.Instance.isCurrentUserAdmin(ProjectName))
            {
                gbAdvanced.Visible = true;
            }
            else
            {
                gbAdvanced.Visible = false;
                this.MinimumSize = new Size(this.Width, (this.Height - gbAdvanced.Height) + HEIGHT_MARGIN);
                this.Height = (this.Height - gbAdvanced.Height) + HEIGHT_MARGIN;
                gbAdvanced.SendToBack();
            }
        }

        /// <summary>
        /// Sets up a field control with min, max, and default values.
        /// </summary>
        /// <param name="fieldControl">Field control (required).</param>
        /// <param name="fieldSettings">Field settings (required).</param>
        private void SetupFieldControl(
            NumericUpDown fieldControl,
            MainConsts.PreviewSetting fieldSettings)
        {
            fieldControl.Minimum = (decimal)fieldSettings.MinValue;
            fieldControl.Maximum = (decimal)fieldSettings.MaxValue;
            fieldControl.Value = (decimal)fieldSettings.DefaultValue;
        }

        /// <summary>
        /// Create button handler.
        /// </summary>
        /// <param name="sender">Event source (button).</param>
        /// <param name="e">Event args.</param>
        private void BtnCreate_Click(object sender, EventArgs e)
        {
            if (PopulatePreviewJob())
            {
                Close();
            }
        }

        /// <summary>
        /// Cancel button handler.
        /// </summary>
        /// <param name="sender">Event source (button).</param>
        /// <param name="e">Event args.</param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            IsCancelled = true;
            Close();
        }

        /// <summary>
        /// Enable or disable custom footnotes option
        /// </summary>
        /// <param name="footnotesDefined"></param>
        internal void SetCustomFootnotesEnabled(bool footnotesDefined)
        {
            cbFootnotes.Enabled = footnotesDefined;
        }

        /// <summary>
        /// Check user settings and fill out preview job.
        /// </summary>
        private bool PopulatePreviewJob()
        {
            if (!CheckFieldValue("font size", FontSizeInPts,
                !nudFontSize.Enabled,
                MainConsts.FontSizeSettings))
            {
                return false;
            }

            if (!CheckFieldValue("font leading", FontLeadingInPts,
                !nudFontLeading.Enabled,
                MainConsts.FontLeadingSettings))
            {
                return false;
            }

            if (!CheckFieldValue("page height", PageHeightInPts,
                !nudPageHeight.Enabled,
                MainConsts.PageHeightSettings))
            {
                return false;
            }

            if (!CheckFieldValue("page width", PageWidthInPts,
                !nudPageWidth.Enabled,
                MainConsts.PageWidthSettings))
            {
                return false;
            }

            if (!CheckFieldValue("page header", PageHeaderInPts,
                !nudPageHeader.Enabled,
                MainConsts.PageHeaderSettings))
            {
                return false;
            }
         
            _previewJob.ProjectName = ProjectName;
            _previewJob.User = User;
            _previewJob.BookFormat = BookFormat;
            _previewJob.FontSizeInPts = FontSizeInPts;
            _previewJob.FontLeadingInPts = FontLeadingInPts;
            _previewJob.PageHeightInPts = PageHeightInPts;
            _previewJob.PageWidthInPts = PageWidthInPts;
            _previewJob.PageHeaderInPts = PageHeaderInPts;
            _previewJob.UseCustomFootnotes = UseCustomFootnotes;
            _previewJob.UseProjectFont = UseProjectFont;

            return true;
        }

        /// <summary>
        /// Checks whether a nullable value is set and between a min and max
        /// and shows a warning box on failure.
        /// </summary>
        /// <param name="fieldName">Field name (required).</param>
        /// <param name="inputValue">Input value (optional, may be null).</param>
        /// <param name="isNullOk">True to allow null values, false otherwise.</param>
        /// <param name="fieldSetting">Field settings (min/max/default; required).</param>
        /// <returns>True if value is null or between min and max, false otherwise.</returns>
        private static bool CheckFieldValue(
            string fieldName,
            float? inputValue,
            bool isNullOk,
            MainConsts.PreviewSetting fieldSetting)
        {
            if ((inputValue == null && isNullOk)
                || (inputValue >= fieldSetting.MinValue && inputValue <= fieldSetting.MaxValue))
            {
                return true;
            }

            MessageBox.Show($"Invalid {fieldName}: {inputValue ?? 0}pt  (minimum: {fieldSetting.MinValue}pt, maximum: {fieldSetting.MaxValue}pt, default: {fieldSetting.DefaultValue}pt).",
                "Notice...",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        /// <summary>
        /// Enables/disabled page options.
        /// </summary>
        /// <param name="isEnabled">True to enable page options, false otherwise.</param>
        private void ControlPageOptions(bool isEnabled)
        {
            ToggleFieldControl(nudPageHeight, lblPageHeight, lblPageHeightUnits,
                MainConsts.PageHeightSettings, isEnabled);
            ToggleFieldControl(nudPageWidth, lblPageWidth, lblPageWidthUnits,
                MainConsts.PageWidthSettings, isEnabled);
            ToggleFieldControl(nudPageHeader, lblPageHeader, lblPageHeaderUnits,
                MainConsts.PageHeaderSettings, isEnabled);
        }

        /// <summary>
        /// Enable or disable a numeric up/down control and a pair of labels,
        /// resetting default values as needed.
        /// </summary>
        /// <param name="fieldControl">Field control (required).</param>
        /// <param name="fieldLabel">Field control primary label (required).</param>
        /// <param name="unitsLabel">Field control units label (required).</param>
        /// <param name="fieldSettings">Field settings (required).</param>
        /// <param name="isEnabled">True to enable, false to disable.</param>
        private void ToggleFieldControl(
            NumericUpDown fieldControl,
            Control fieldLabel,
            Control unitsLabel,
            MainConsts.PreviewSetting fieldSettings,
            bool isEnabled)
        {
            // set control enabled
            fieldControl.Enabled = isEnabled;
            fieldLabel.Enabled = isEnabled;
            unitsLabel.Enabled = isEnabled;

            // reset value
            fieldControl.Value = isEnabled
                                 && !string.IsNullOrWhiteSpace(fieldControl.Text)
                ? fieldControl.Value
                : (decimal)fieldSettings.DefaultValue;

            // copy value to text (not always aligned)
            fieldControl.Text = fieldControl.Value.ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Font size in points if custom text options enabled, null otherwise.
        /// </summary>
        public float? FontSizeInPts => string.IsNullOrWhiteSpace(nudFontSize.Text)
                ? (float?)null
                : Convert.ToSingle(nudFontSize.Value);

        /// <summary>
        /// Font leading in points if custom text options enabled, null otherwise.
        /// </summary>
        public float? FontLeadingInPts => string.IsNullOrWhiteSpace(nudFontLeading.Text)
                ? (float?)null
                : Convert.ToSingle(nudFontLeading.Value);

        /// <summary>
        /// Page height in points if custom page options enabled, null otherwise.
        /// </summary>
        public float? PageHeightInPts => string.IsNullOrWhiteSpace(nudPageHeight.Text)
                ? (float?)null
                : Convert.ToSingle(nudPageHeight.Value);

        /// <summary>
        /// Page width in points if custom page options enabled, null otherwise.
        /// </summary>
        public float? PageWidthInPts => string.IsNullOrWhiteSpace(nudPageWidth.Text)
                ? (float?)null
                : Convert.ToSingle(nudPageWidth.Value);

        /// <summary>
        /// Page header in points if custom page options enabled, null otherwise.
        /// </summary>
        public float? PageHeaderInPts => string.IsNullOrWhiteSpace(nudPageHeader.Text)
                ? (float?)null
                : Convert.ToSingle(nudPageHeader.Value);

        /// <summary>
        /// Book format accessor.
        /// </summary>
        public BookFormat BookFormat => rdoLayoutCav.Checked ? BookFormat.cav : BookFormat.tbotb;

        /// <summary>
        /// Use custom footnote accessor.
        /// </summary>
        public bool UseCustomFootnotes => cbFootnotes.Checked;

        /// <summary>
        /// Determines whether the project font will be used when generating the preview.
        /// </summary>
        public bool UseProjectFont => cbUseProjectFonts.Checked;

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
            lblProjectUpdatedText.Text = _projectDetails.ProjectUpdated.ToShortDateString()
                                         + " " + _projectDetails.ProjectUpdated.ToShortTimeString();
        }

        /// <summary>
        /// This function sets the server status and populates the appropriate labels.
        /// </summary>
        /// <param name="serverStatus">Server's status (required).</param>
        public virtual void SetServerStatus(ServerStatus serverStatus)
        {
            // validate input
            _ = serverStatus ?? throw new ArgumentNullException(nameof(serverStatus));

            _serverStatus = serverStatus;

            // get the UI version
            var uiVersion = Assembly.GetExecutingAssembly()?.GetName()?.Version;

            lblVersions.Text = $"UI version: {uiVersion}, Server version: {serverStatus.Version}";
        }

        /// <summary>
        /// This method controls what happens when an item in the "Advanced" menu is clicked.
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">event</param>
        private void OnAdvancedMenuItemClick(object sender, EventArgs e)
        {
            // When invoked by a ToolStripMenuItem, flip the state of that menu item
            if (sender is ToolStripMenuItem menuItem)
            {
                menuItem.Checked = !menuItem.Checked;
                menuItem.CheckState = menuItem.Checked
                    ? CheckState.Checked : CheckState.Unchecked;
            }
        }

        /// <summary>
        /// Show EULA dialog
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">event</param>
        private void licenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string pluginName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            string formTitle = $"{pluginName} - End User License Agreement";

            LicenseForm eulaForm = new LicenseForm();
            eulaForm.FormType = LicenseForm.FormTypes.Info;
            eulaForm.FormTitle = formTitle;
            eulaForm.LicenseText = Resources.TPT_EULA;
            eulaForm.OnDismiss = () => eulaForm.Close();
            eulaForm.Show();
        }

        /// <summary>
        /// Show text box when allowing custom book ranges to be entered
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">event</param>
        private void rbCustom_CheckedChanged(object sender, EventArgs e)
        {
            tbCustomBookSet.Enabled = rbCustom.Checked;
        }

        /// <summary>
        /// Uncheck some items when selecting TBOTB based on
        /// https://docs.google.com/spreadsheets/d/1wXMY_M8Dts8ATNt_autcU4MrtMl9LIAPOKvzA3w8eAI/edit?skip_itp2_check=true#gid=0
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">event</param>
        private void rdoLayoutTbotb_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoLayoutTbotb.Checked) {
                cbHeadings.Checked = false;
                cbFootnotes.Checked = false;
                cbChapterNumbers.Checked = false;
                cbVerseNumbers.Checked = false;
                cbParallelPassages.Checked = false;
            }
        }

        /// <summary>
        /// Check some items when selecting CAV based on
        /// https://docs.google.com/spreadsheets/d/1wXMY_M8Dts8ATNt_autcU4MrtMl9LIAPOKvzA3w8eAI/edit?skip_itp2_check=true#gid=0
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">event</param>
        private void rdoLayoutCav_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoLayoutCav.Checked)
            {
                cbHeadings.Checked = true;
                cbFootnotes.Checked = true;
                cbChapterNumbers.Checked = true;
                cbVerseNumbers.Checked = true;
                cbParallelPassages.Checked = true;
            }

        }
    }
}
