/*
Copyright © 2022 by Biblica, Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using TptMain.Models;
using TptMain.Project;
using TptMain.Properties;
using TptMain.Text;
using TptMain.Util;

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
        /// Provides project setting & metadata access.
        /// </summary>
        private ProjectManager _projectManager;

        /// <summary>
        /// Active project name.
        /// </summary>
        private string _activeProjectName;

        /// <summary>
        /// The list of selected books to check
        /// </summary>
        private BookNameItem[] _selectedBooks;

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
        public virtual bool IsArchive => cbDownloadTypesettingFiles.Checked;

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

            // Set starting form view based on user status and starting layout
            SetAdminView(ProjectName);
            grpLayout_Changed(null, null);
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
            cbLocalizeFootnotes.Enabled = footnotesDefined;
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

            _previewJob.User = User;
            _previewJob.BibleSelectionParams ??= new BibleSelectionParams();
            _previewJob.BibleSelectionParams.ProjectName = ProjectName;
            _previewJob.BibleSelectionParams.IncludeAncillary = IncludeAncillary;
            _previewJob.BibleSelectionParams.SelectedBooks = SelectedBooks;
            _previewJob.TypesettingParams ??= new TypesettingParams();
            _previewJob.TypesettingParams.BookFormat = BookFormat;
            _previewJob.TypesettingParams.FontSizeInPts = FontSizeInPts;
            _previewJob.TypesettingParams.FontLeadingInPts = FontLeadingInPts;
            _previewJob.TypesettingParams.PageHeightInPts = PageHeightInPts;
            _previewJob.TypesettingParams.PageWidthInPts = PageWidthInPts;
            _previewJob.TypesettingParams.PageHeaderInPts = PageHeaderInPts;
            _previewJob.TypesettingParams.UseCustomFootnotes = UseCustomFootnotes;
            _previewJob.TypesettingParams.UseHyphenation = UseHyphenation;
            _previewJob.TypesettingParams.UseProjectFont = UseProjectFont;
            _previewJob.TypesettingParams.IncludeFootnotes = IncludeFootnotes;
            _previewJob.TypesettingParams.IncludeHeadings = IncludeHeadings;
            _previewJob.TypesettingParams.IncludeIntros = IncludeIntros;
            _previewJob.TypesettingParams.IncludeAcrosticPoetry = IncludeAcrosticPoetry;
            _previewJob.TypesettingParams.IncludeChapterNumbers = IncludeChapterNumbers;
            _previewJob.TypesettingParams.IncludeParallelPassages = IncludeParallelPassages;
            _previewJob.TypesettingParams.IncludeVerseNumbers = IncludeVerseNumbers;

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
        public bool UseCustomFootnotes => cbLocalizeFootnotes.Checked;

        /// <summary>
        /// Whether to apply hyphenation to the preview
        /// </summary>
        public bool UseHyphenation => cbHyphenate.Checked;

        /// <summary>
        /// Determines whether the project font will be used when generating the preview.
        /// </summary>
        public bool UseProjectFont => cbUseProjectFonts.Checked;

        /// <summary>
        /// Determines whether the preview should include Footnotes.
        ///</summary>
        public bool IncludeFootnotes => cbFootnotes.Checked;

        /// <summary>
        /// Determines whether the preview should include Headings.
        ///</summary>
        public bool IncludeHeadings => cbHeadings.Checked;

        /// <summary>
        /// Determines whether the preview should include Intros.
        ///</summary>
        public bool IncludeIntros => cbIntros.Checked;

        /// <summary>
        /// Determines whether the preview should include Acrostic Poetry.
        ///</summary>
        public bool IncludeAcrosticPoetry => cbAcrostic.Checked;

        /// <summary>
        /// Determines whether the preview should include Chapter Numbers.
        ///</summary>
        public bool IncludeChapterNumbers => cbChapterNumbers.Checked;

        /// <summary>
        /// Determines whether the preview should include Parallel Passages.
        ///</summary>
        public bool IncludeParallelPassages => cbParallelPassages.Checked;

        /// <summary>
        /// Determines whether the preview should include Verse Numbers.
        ///</summary>
        public bool IncludeVerseNumbers => cbVerseNumbers.Checked;

        /// <summary>
        /// Determines whether the preview should include Ancillary material.
        ///</summary>
        public bool IncludeAncillary => cbIncludeAncillary.Checked;

        /// <summary>
        /// Defines which books should be included in the preview.
        /// </summary>
        public string SelectedBooks => DetermineSelectedBooks();

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
        /// Update form fields based on the selected layout.
        /// </summary>
        /// <param name="bookFormat">The selected layout.</param>
        private void UpdateFieldsByLayout(BookFormat bookFormat)
        {
            switch (bookFormat)
            {
                case BookFormat.tbotb:
                    {
                        cbHeadings.Checked = false;
                        cbFootnotes.Checked = false;
                        cbChapterNumbers.Checked = false;
                        cbVerseNumbers.Checked = false;
                        cbParallelPassages.Checked = false;

                        break;
                    }
                case BookFormat.cav:
                    {
                        cbHeadings.Checked = true;
                        cbFootnotes.Checked = true;
                        cbChapterNumbers.Checked = true;
                        cbVerseNumbers.Checked = true;
                        cbParallelPassages.Checked = true;

                        break;
                    }
            }
        }

        /// <summary>
        /// Set preview job options based on selected layout
        /// https://docs.google.com/spreadsheets/d/1wXMY_M8Dts8ATNt_autcU4MrtMl9LIAPOKvzA3w8eAI/edit?skip_itp2_check=true#gid=0
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">event</param>
        private void grpLayout_Changed(object sender, EventArgs e)
        {
            var bookLayout = BookFormat.cav;
            if (rdoLayoutTbotb.Checked)
            {
                bookLayout = BookFormat.tbotb;
            }

            UpdateFieldsByLayout(bookLayout);
        }

        /// <summary>
        /// This method returns which books should be included with the preview.
        /// </summary>
        /// <returns></returns>
        private string DetermineSelectedBooks()
        {
            if (rbFullBible.Checked)
            {
                return null;
            }

            if (rbNewTestament.Checked)
            {
                // send the books of the new testament
                return String.Join(",", MainConsts.NEW_TESTAMENT_BOOKS);
            }

            // Return the list of books specified by the user
            var selectedBooksString = BookSelection.stringFromSelectedBooks(_selectedBooks);
            return selectedBooksString.Trim().Replace(" ", "");
        }

        /// <summary>
        /// Opens a link to the support URL from the plugin
        /// </summary>
        /// <param name="sender">The control that sent this event</param>
        /// <param name="e">The event information that triggered this call</param>
        private void contactSupportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Call the Process.Start method to open the default browser
            //with a URL:
            Process.Start(MainConsts.SUPPORT_URL);
        }

        /// <summary>
        /// Set up to support selecting a different project when we enable that. Right now the Plugin API
        /// does not allow for getting a list of projects.
        /// </summary>
        /// <param name="activeProjectName">Allows for setting the current project to work against</param>
        public virtual void SetActiveProject(string activeProjectName)
        {
            _activeProjectName = activeProjectName;
            _projectManager = new ProjectManager(HostUtil.Instance.Host, _activeProjectName);
            SetCurrentBookDefault();
        }


        /// <summary>
        /// Sets the defaults for the current book, including the name and chapter counts
        /// </summary>
        private void SetCurrentBookDefault()
        {
            var versificationName = HostUtil.Instance.Host.GetProjectVersificationName(_activeProjectName);
            BookUtil.RefToBcv(HostUtil.Instance.Host.GetCurrentRef(versificationName),
                out var runBookNum, out _, out _);

            // check that the current book by ID is available first
            if (!_projectManager.BookNamesByNum.ContainsKey(runBookNum))
            {
                var currentBook = BookUtil.BookIdsByNum[runBookNum];

                // let the user know they have not set the book's abbreviation, shortname, or longname
                throw new Exception(
                    $"The Book '{currentBook.BookCode}' has not had its Book Names set: abbreviation, short name, or long name. Please set these before continuing.");
            }

            // set the current book name
            tbCustomBookSet.Text = _projectManager.BookNamesByNum[runBookNum].BookCode;
            _selectedBooks = new[] { _projectManager.BookNamesByNum[runBookNum] };
        }


        /// <summary>
        /// Bring up the choose books form to decide which books to generate a preview of.
        /// </summary>
        /// <param name="sender">The control that sent this event</param>
        /// <param name="e">The event information that triggered this call</param>
        private void chooseBooksButton_Click(object sender, EventArgs e)
        {
            // bring up book selection dialog, use current selection to initialize
            using (var form = new BookSelection(_projectManager, _selectedBooks))
            {
                form.StartPosition = FormStartPosition.CenterParent;

                var result = form.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    // update which books were selected
                    _selectedBooks = form.GetSelected();
                    var selectedBooksString = BookSelection.truncatedStringFromSelectedBooks(_selectedBooks);
                    tbCustomBookSet.Text = selectedBooksString;
                }
            }

            rbCustom.Checked = true;
        }
    }
}
