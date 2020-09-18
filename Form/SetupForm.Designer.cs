namespace TptMain.Form
{
    partial class SetupForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupForm));
            this.grpLayout = new System.Windows.Forms.GroupBox();
            this.rdoLayoutTbotb = new System.Windows.Forms.RadioButton();
            this.rdoLayoutCav = new System.Windows.Forms.RadioButton();
            this.rdoTextOptionsDefaults = new System.Windows.Forms.RadioButton();
            this.rdoTextOptionsCustom = new System.Windows.Forms.RadioButton();
            this.grpTextOptions = new System.Windows.Forms.GroupBox();
            this.nudFontLeading = new System.Windows.Forms.NumericUpDown();
            this.nudFontSize = new System.Windows.Forms.NumericUpDown();
            this.lblFontLeadingUnits = new System.Windows.Forms.Label();
            this.lblFontSizeUnits = new System.Windows.Forms.Label();
            this.lblFontLeading = new System.Windows.Forms.Label();
            this.lblFontSize = new System.Windows.Forms.Label();
            this.grpPageOptions = new System.Windows.Forms.GroupBox();
            this.nudPageHeader = new System.Windows.Forms.NumericUpDown();
            this.nudPageHeight = new System.Windows.Forms.NumericUpDown();
            this.nudPageWidth = new System.Windows.Forms.NumericUpDown();
            this.lblPageHeaderUnits = new System.Windows.Forms.Label();
            this.lblPageHeightUnits = new System.Windows.Forms.Label();
            this.lblPageWidthUnits = new System.Windows.Forms.Label();
            this.lblPageHeader = new System.Windows.Forms.Label();
            this.lblPageHeight = new System.Windows.Forms.Label();
            this.lblPageWidth = new System.Windows.Forms.Label();
            this.rdoPageOptionsCustom = new System.Windows.Forms.RadioButton();
            this.rdoPageOptionsDefaults = new System.Windows.Forms.RadioButton();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblProjectUpdatedText = new System.Windows.Forms.Label();
            this.lblProjectUpdated = new System.Windows.Forms.Label();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.grpProject = new System.Windows.Forms.GroupBox();
            this.lblProjectNameText = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.advancedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onDownloadTypesettingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCustomFootnotesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useProjectFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.licenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Copyright = new System.Windows.Forms.Label();
            this.lblVersions = new System.Windows.Forms.Label();
            this.grpLayout.SuspendLayout();
            this.grpTextOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontLeading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).BeginInit();
            this.grpPageOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPageHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPageHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPageWidth)).BeginInit();
            this.grpProject.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpLayout
            // 
            this.grpLayout.Controls.Add(this.rdoLayoutTbotb);
            this.grpLayout.Controls.Add(this.rdoLayoutCav);
            this.grpLayout.Location = new System.Drawing.Point(12, 113);
            this.grpLayout.Name = "grpLayout";
            this.grpLayout.Size = new System.Drawing.Size(348, 84);
            this.grpLayout.TabIndex = 11;
            this.grpLayout.TabStop = false;
            this.grpLayout.Text = "Layout";
            //  
            // rdoLayoutTbotb
            // 
            this.rdoLayoutTbotb.AutoSize = true;
            this.rdoLayoutTbotb.Location = new System.Drawing.Point(19, 51);
            this.rdoLayoutTbotb.Name = "rdoLayoutTbotb";
            this.rdoLayoutTbotb.Size = new System.Drawing.Size(181, 17);
            this.rdoLayoutTbotb.TabIndex = 10;
            this.rdoLayoutTbotb.Text = " The Books of the Bible (TBOTB)";
            this.rdoLayoutTbotb.UseVisualStyleBackColor = true;
            // 
            // rdoLayoutCav
            // 
            this.rdoLayoutCav.AutoSize = true;
            this.rdoLayoutCav.Checked = true;
            this.rdoLayoutCav.Location = new System.Drawing.Point(19, 28);
            this.rdoLayoutCav.Name = "rdoLayoutCav";
            this.rdoLayoutCav.Size = new System.Drawing.Size(146, 17);
            this.rdoLayoutCav.TabIndex = 9;
            this.rdoLayoutCav.TabStop = true;
            this.rdoLayoutCav.Text = " Chapter and Verse (CAV)";
            this.rdoLayoutCav.UseVisualStyleBackColor = true;
            // 
            // rdoTextOptionsDefaults
            // 
            this.rdoTextOptionsDefaults.AutoSize = true;
            this.rdoTextOptionsDefaults.Checked = true;
            this.rdoTextOptionsDefaults.Location = new System.Drawing.Point(19, 28);
            this.rdoTextOptionsDefaults.Name = "rdoTextOptionsDefaults";
            this.rdoTextOptionsDefaults.Size = new System.Drawing.Size(59, 17);
            this.rdoTextOptionsDefaults.TabIndex = 10;
            this.rdoTextOptionsDefaults.TabStop = true;
            this.rdoTextOptionsDefaults.Text = "Default";
            this.rdoTextOptionsDefaults.UseVisualStyleBackColor = true;
            this.rdoTextOptionsDefaults.CheckedChanged += new System.EventHandler(this.RdoTextOptionsDefaults_CheckedChanged);
            // 
            // rdoTextOptionsCustom
            // 
            this.rdoTextOptionsCustom.AutoSize = true;
            this.rdoTextOptionsCustom.Location = new System.Drawing.Point(19, 51);
            this.rdoTextOptionsCustom.Name = "rdoTextOptionsCustom";
            this.rdoTextOptionsCustom.Size = new System.Drawing.Size(60, 17);
            this.rdoTextOptionsCustom.TabIndex = 11;
            this.rdoTextOptionsCustom.Text = "Custom";
            this.rdoTextOptionsCustom.UseVisualStyleBackColor = true;
            this.rdoTextOptionsCustom.CheckedChanged += new System.EventHandler(this.RdoTextOptionsCustom_CheckedChanged);
            // 
            // grpTextOptions
            // 
            this.grpTextOptions.Controls.Add(this.nudFontLeading);
            this.grpTextOptions.Controls.Add(this.nudFontSize);
            this.grpTextOptions.Controls.Add(this.lblFontLeadingUnits);
            this.grpTextOptions.Controls.Add(this.lblFontSizeUnits);
            this.grpTextOptions.Controls.Add(this.lblFontLeading);
            this.grpTextOptions.Controls.Add(this.lblFontSize);
            this.grpTextOptions.Controls.Add(this.rdoTextOptionsCustom);
            this.grpTextOptions.Controls.Add(this.rdoTextOptionsDefaults);
            this.grpTextOptions.Location = new System.Drawing.Point(12, 207);
            this.grpTextOptions.Name = "grpTextOptions";
            this.grpTextOptions.Size = new System.Drawing.Size(348, 124);
            this.grpTextOptions.TabIndex = 12;
            this.grpTextOptions.TabStop = false;
            this.grpTextOptions.Text = "Text Options";
            // 
            // nudFontLeading
            // 
            this.nudFontLeading.DecimalPlaces = 1;
            this.nudFontLeading.Enabled = false;
            this.nudFontLeading.Location = new System.Drawing.Point(202, 79);
            this.nudFontLeading.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFontLeading.Name = "nudFontLeading";
            this.nudFontLeading.Size = new System.Drawing.Size(102, 20);
            this.nudFontLeading.TabIndex = 25;
            this.nudFontLeading.ThousandsSeparator = true;
            this.nudFontLeading.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            // 
            // nudFontSize
            // 
            this.nudFontSize.DecimalPlaces = 1;
            this.nudFontSize.Enabled = false;
            this.nudFontSize.Location = new System.Drawing.Point(202, 50);
            this.nudFontSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFontSize.Name = "nudFontSize";
            this.nudFontSize.Size = new System.Drawing.Size(102, 20);
            this.nudFontSize.TabIndex = 24;
            this.nudFontSize.ThousandsSeparator = true;
            this.nudFontSize.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // lblFontLeadingUnits
            // 
            this.lblFontLeadingUnits.AutoSize = true;
            this.lblFontLeadingUnits.BackColor = System.Drawing.Color.Transparent;
            this.lblFontLeadingUnits.Enabled = false;
            this.lblFontLeadingUnits.Location = new System.Drawing.Point(310, 81);
            this.lblFontLeadingUnits.Name = "lblFontLeadingUnits";
            this.lblFontLeadingUnits.Size = new System.Drawing.Size(16, 13);
            this.lblFontLeadingUnits.TabIndex = 17;
            this.lblFontLeadingUnits.Text = "pt";
            this.lblFontLeadingUnits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFontSizeUnits
            // 
            this.lblFontSizeUnits.AutoSize = true;
            this.lblFontSizeUnits.BackColor = System.Drawing.Color.Transparent;
            this.lblFontSizeUnits.Enabled = false;
            this.lblFontSizeUnits.Location = new System.Drawing.Point(310, 53);
            this.lblFontSizeUnits.Name = "lblFontSizeUnits";
            this.lblFontSizeUnits.Size = new System.Drawing.Size(16, 13);
            this.lblFontSizeUnits.TabIndex = 16;
            this.lblFontSizeUnits.Text = "pt";
            this.lblFontSizeUnits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFontLeading
            // 
            this.lblFontLeading.AutoSize = true;
            this.lblFontLeading.BackColor = System.Drawing.Color.Transparent;
            this.lblFontLeading.Enabled = false;
            this.lblFontLeading.Location = new System.Drawing.Point(148, 81);
            this.lblFontLeading.Name = "lblFontLeading";
            this.lblFontLeading.Size = new System.Drawing.Size(48, 13);
            this.lblFontLeading.TabIndex = 15;
            this.lblFontLeading.Text = "Leading:";
            this.lblFontLeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFontSize
            // 
            this.lblFontSize.AutoSize = true;
            this.lblFontSize.BackColor = System.Drawing.Color.Transparent;
            this.lblFontSize.Enabled = false;
            this.lblFontSize.Location = new System.Drawing.Point(144, 53);
            this.lblFontSize.Name = "lblFontSize";
            this.lblFontSize.Size = new System.Drawing.Size(52, 13);
            this.lblFontSize.TabIndex = 14;
            this.lblFontSize.Text = "Font size:";
            this.lblFontSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpPageOptions
            // 
            this.grpPageOptions.Controls.Add(this.nudPageHeader);
            this.grpPageOptions.Controls.Add(this.nudPageHeight);
            this.grpPageOptions.Controls.Add(this.nudPageWidth);
            this.grpPageOptions.Controls.Add(this.lblPageHeaderUnits);
            this.grpPageOptions.Controls.Add(this.lblPageHeightUnits);
            this.grpPageOptions.Controls.Add(this.lblPageWidthUnits);
            this.grpPageOptions.Controls.Add(this.lblPageHeader);
            this.grpPageOptions.Controls.Add(this.lblPageHeight);
            this.grpPageOptions.Controls.Add(this.lblPageWidth);
            this.grpPageOptions.Controls.Add(this.rdoPageOptionsCustom);
            this.grpPageOptions.Controls.Add(this.rdoPageOptionsDefaults);
            this.grpPageOptions.Location = new System.Drawing.Point(12, 338);
            this.grpPageOptions.Name = "grpPageOptions";
            this.grpPageOptions.Size = new System.Drawing.Size(348, 158);
            this.grpPageOptions.TabIndex = 16;
            this.grpPageOptions.TabStop = false;
            this.grpPageOptions.Text = "Page Options";
            // 
            // nudPageHeader
            // 
            this.nudPageHeader.DecimalPlaces = 1;
            this.nudPageHeader.Enabled = false;
            this.nudPageHeader.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudPageHeader.Location = new System.Drawing.Point(202, 112);
            this.nudPageHeader.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudPageHeader.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPageHeader.Name = "nudPageHeader";
            this.nudPageHeader.Size = new System.Drawing.Size(102, 20);
            this.nudPageHeader.TabIndex = 23;
            this.nudPageHeader.ThousandsSeparator = true;
            this.nudPageHeader.Value = new decimal(new int[] {
            18,
            0,
            0,
            0});
            // 
            // nudPageHeight
            // 
            this.nudPageHeight.DecimalPlaces = 1;
            this.nudPageHeight.Enabled = false;
            this.nudPageHeight.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudPageHeight.Location = new System.Drawing.Point(202, 81);
            this.nudPageHeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudPageHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPageHeight.Name = "nudPageHeight";
            this.nudPageHeight.Size = new System.Drawing.Size(102, 20);
            this.nudPageHeight.TabIndex = 22;
            this.nudPageHeight.ThousandsSeparator = true;
            this.nudPageHeight.Value = new decimal(new int[] {
            612,
            0,
            0,
            0});
            // 
            // nudPageWidth
            // 
            this.nudPageWidth.DecimalPlaces = 1;
            this.nudPageWidth.Enabled = false;
            this.nudPageWidth.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudPageWidth.Location = new System.Drawing.Point(202, 51);
            this.nudPageWidth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudPageWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPageWidth.Name = "nudPageWidth";
            this.nudPageWidth.Size = new System.Drawing.Size(102, 20);
            this.nudPageWidth.TabIndex = 21;
            this.nudPageWidth.ThousandsSeparator = true;
            this.nudPageWidth.Value = new decimal(new int[] {
            396,
            0,
            0,
            0});
            // 
            // lblPageHeaderUnits
            // 
            this.lblPageHeaderUnits.AutoSize = true;
            this.lblPageHeaderUnits.BackColor = System.Drawing.Color.Transparent;
            this.lblPageHeaderUnits.Enabled = false;
            this.lblPageHeaderUnits.Location = new System.Drawing.Point(310, 114);
            this.lblPageHeaderUnits.Name = "lblPageHeaderUnits";
            this.lblPageHeaderUnits.Size = new System.Drawing.Size(16, 13);
            this.lblPageHeaderUnits.TabIndex = 20;
            this.lblPageHeaderUnits.Text = "pt";
            this.lblPageHeaderUnits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPageHeightUnits
            // 
            this.lblPageHeightUnits.AutoSize = true;
            this.lblPageHeightUnits.BackColor = System.Drawing.Color.Transparent;
            this.lblPageHeightUnits.Enabled = false;
            this.lblPageHeightUnits.Location = new System.Drawing.Point(310, 83);
            this.lblPageHeightUnits.Name = "lblPageHeightUnits";
            this.lblPageHeightUnits.Size = new System.Drawing.Size(16, 13);
            this.lblPageHeightUnits.TabIndex = 19;
            this.lblPageHeightUnits.Text = "pt";
            this.lblPageHeightUnits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPageWidthUnits
            // 
            this.lblPageWidthUnits.AutoSize = true;
            this.lblPageWidthUnits.BackColor = System.Drawing.Color.Transparent;
            this.lblPageWidthUnits.Enabled = false;
            this.lblPageWidthUnits.Location = new System.Drawing.Point(310, 53);
            this.lblPageWidthUnits.Name = "lblPageWidthUnits";
            this.lblPageWidthUnits.Size = new System.Drawing.Size(16, 13);
            this.lblPageWidthUnits.TabIndex = 18;
            this.lblPageWidthUnits.Text = "pt";
            this.lblPageWidthUnits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPageHeader
            // 
            this.lblPageHeader.AutoSize = true;
            this.lblPageHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblPageHeader.Enabled = false;
            this.lblPageHeader.Location = new System.Drawing.Point(151, 114);
            this.lblPageHeader.Name = "lblPageHeader";
            this.lblPageHeader.Size = new System.Drawing.Size(45, 13);
            this.lblPageHeader.TabIndex = 17;
            this.lblPageHeader.Text = "Header:";
            this.lblPageHeader.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPageHeight
            // 
            this.lblPageHeight.AutoSize = true;
            this.lblPageHeight.BackColor = System.Drawing.Color.Transparent;
            this.lblPageHeight.Enabled = false;
            this.lblPageHeight.Location = new System.Drawing.Point(155, 83);
            this.lblPageHeight.Name = "lblPageHeight";
            this.lblPageHeight.Size = new System.Drawing.Size(41, 13);
            this.lblPageHeight.TabIndex = 15;
            this.lblPageHeight.Text = "Height:";
            this.lblPageHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPageWidth
            // 
            this.lblPageWidth.AutoSize = true;
            this.lblPageWidth.BackColor = System.Drawing.Color.Transparent;
            this.lblPageWidth.Enabled = false;
            this.lblPageWidth.Location = new System.Drawing.Point(158, 53);
            this.lblPageWidth.Name = "lblPageWidth";
            this.lblPageWidth.Size = new System.Drawing.Size(38, 13);
            this.lblPageWidth.TabIndex = 14;
            this.lblPageWidth.Text = "Width:";
            this.lblPageWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rdoPageOptionsCustom
            // 
            this.rdoPageOptionsCustom.AutoSize = true;
            this.rdoPageOptionsCustom.Location = new System.Drawing.Point(19, 51);
            this.rdoPageOptionsCustom.Name = "rdoPageOptionsCustom";
            this.rdoPageOptionsCustom.Size = new System.Drawing.Size(60, 17);
            this.rdoPageOptionsCustom.TabIndex = 11;
            this.rdoPageOptionsCustom.Text = "Custom";
            this.rdoPageOptionsCustom.UseVisualStyleBackColor = true;
            this.rdoPageOptionsCustom.CheckedChanged += new System.EventHandler(this.RdoPageOptionsCustom_CheckedChanged);
            // 
            // rdoPageOptionsDefaults
            // 
            this.rdoPageOptionsDefaults.AutoSize = true;
            this.rdoPageOptionsDefaults.Checked = true;
            this.rdoPageOptionsDefaults.Location = new System.Drawing.Point(19, 28);
            this.rdoPageOptionsDefaults.Name = "rdoPageOptionsDefaults";
            this.rdoPageOptionsDefaults.Size = new System.Drawing.Size(59, 17);
            this.rdoPageOptionsDefaults.TabIndex = 10;
            this.rdoPageOptionsDefaults.TabStop = true;
            this.rdoPageOptionsDefaults.Text = "Default";
            this.rdoPageOptionsDefaults.UseVisualStyleBackColor = true;
            this.rdoPageOptionsDefaults.CheckedChanged += new System.EventHandler(this.RdoPageOptionsDefaults_CheckedChanged);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(204, 505);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 17;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.BtnCreate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(286, 505);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // lblProjectUpdatedText
            // 
            this.lblProjectUpdatedText.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProjectUpdatedText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProjectUpdatedText.Location = new System.Drawing.Point(184, 47);
            this.lblProjectUpdatedText.Name = "lblProjectUpdatedText";
            this.lblProjectUpdatedText.Size = new System.Drawing.Size(145, 22);
            this.lblProjectUpdatedText.TabIndex = 8;
            this.lblProjectUpdatedText.Text = "None";
            this.lblProjectUpdatedText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProjectUpdated
            // 
            this.lblProjectUpdated.AutoSize = true;
            this.lblProjectUpdated.Location = new System.Drawing.Point(181, 27);
            this.lblProjectUpdated.Name = "lblProjectUpdated";
            this.lblProjectUpdated.Size = new System.Drawing.Size(123, 13);
            this.lblProjectUpdated.TabIndex = 7;
            this.lblProjectUpdated.Text = "Last Updated on Server:";
            // 
            // lblProjectName
            // 
            this.lblProjectName.AutoSize = true;
            this.lblProjectName.Location = new System.Drawing.Point(16, 27);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(38, 13);
            this.lblProjectName.TabIndex = 5;
            this.lblProjectName.Text = "Name:";
            // 
            // grpProject
            // 
            this.grpProject.Controls.Add(this.lblProjectNameText);
            this.grpProject.Controls.Add(this.lblProjectUpdatedText);
            this.grpProject.Controls.Add(this.lblProjectUpdated);
            this.grpProject.Controls.Add(this.lblProjectName);
            this.grpProject.Location = new System.Drawing.Point(12, 22);
            this.grpProject.Name = "grpProject";
            this.grpProject.Size = new System.Drawing.Size(348, 87);
            this.grpProject.TabIndex = 10;
            this.grpProject.TabStop = false;
            this.grpProject.Text = "Project";
            // 
            // lblProjectNameText
            // 
            this.lblProjectNameText.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProjectNameText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProjectNameText.Location = new System.Drawing.Point(19, 47);
            this.lblProjectNameText.Name = "lblProjectNameText";
            this.lblProjectNameText.Size = new System.Drawing.Size(145, 22);
            this.lblProjectNameText.TabIndex = 9;
            this.lblProjectNameText.Text = "None";
            this.lblProjectNameText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.advancedToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(372, 24);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // advancedToolStripMenuItem
            // 
            this.advancedToolStripMenuItem.Checked = true;
            this.advancedToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.advancedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onDownloadTypesettingMenuItem,
            this.addCustomFootnotesToolStripMenuItem,
            this.useProjectFontToolStripMenuItem});
            this.advancedToolStripMenuItem.Name = "advancedToolStripMenuItem";
            this.advancedToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.advancedToolStripMenuItem.Text = "Advanced";
            // 
            // onDownloadTypesettingMenuItem
            // 
            this.onDownloadTypesettingMenuItem.Name = "onDownloadTypesettingMenuItem";
            this.onDownloadTypesettingMenuItem.Size = new System.Drawing.Size(217, 22);
            this.onDownloadTypesettingMenuItem.Text = "Download Typesetting Files";
            this.onDownloadTypesettingMenuItem.Click += new System.EventHandler(this.OnAdvancedMenuItemClick);
            // 
            // addCustomFootnotesToolStripMenuItem
            // 
            this.addCustomFootnotesToolStripMenuItem.Name = "addCustomFootnotesToolStripMenuItem";
            this.addCustomFootnotesToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.addCustomFootnotesToolStripMenuItem.Text = "Localized Footnotes";
            this.addCustomFootnotesToolStripMenuItem.Click += new System.EventHandler(this.OnAdvancedMenuItemClick);
            // 
            // useProjectFontToolStripMenuItem
            // 
            this.useProjectFontToolStripMenuItem.Name = "useProjectFontToolStripMenuItem";
            this.useProjectFontToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.useProjectFontToolStripMenuItem.Text = "Use Project Font";
            this.useProjectFontToolStripMenuItem.Click += new System.EventHandler(this.OnAdvancedMenuItemClick);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.licenseToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // licenseToolStripMenuItem
            // 
            this.licenseToolStripMenuItem.Name = "licenseToolStripMenuItem";
            this.licenseToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.licenseToolStripMenuItem.Text = "License";
            this.licenseToolStripMenuItem.Click += new System.EventHandler(this.licenseToolStripMenuItem_Click);
            // 
            // Copyright
            // 
            this.Copyright.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Copyright.AutoSize = true;
            this.Copyright.Location = new System.Drawing.Point(9, 505);
            this.Copyright.Name = "Copyright";
            this.Copyright.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.Copyright.Size = new System.Drawing.Size(101, 23);
            this.Copyright.TabIndex = 20;
            this.Copyright.Text = "© 2020 Biblica, Inc.";
            // 
            // lblVersions
            // 
            this.lblVersions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersions.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblVersions.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblVersions.Location = new System.Drawing.Point(9, 528);
            this.lblVersions.Name = "lblVersions";
            this.lblVersions.Size = new System.Drawing.Size(363, 23);
            this.lblVersions.TabIndex = 21;
            this.lblVersions.Text = "UI version: X, Server version: Y";
            this.lblVersions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SetupForm
            // 
            this.AcceptButton = this.btnCreate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(372, 554);
            this.ControlBox = false;
            this.Controls.Add(this.lblVersions);
            this.Controls.Add(this.Copyright);
            this.Controls.Add(this.grpProject);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.grpPageOptions);
            this.Controls.Add(this.grpTextOptions);
            this.Controls.Add(this.grpLayout);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SetupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Print Preview...";
            this.grpLayout.ResumeLayout(false);
            this.grpLayout.PerformLayout();
            this.grpTextOptions.ResumeLayout(false);
            this.grpTextOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontLeading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).EndInit();
            this.grpPageOptions.ResumeLayout(false);
            this.grpPageOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPageHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPageHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPageWidth)).EndInit();
            this.grpProject.ResumeLayout(false);
            this.grpProject.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox grpLayout;
        private System.Windows.Forms.RadioButton rdoLayoutTbotb;
        private System.Windows.Forms.RadioButton rdoLayoutCav;
        private System.Windows.Forms.RadioButton rdoTextOptionsDefaults;
        private System.Windows.Forms.RadioButton rdoTextOptionsCustom;
        private System.Windows.Forms.GroupBox grpTextOptions;
        private System.Windows.Forms.Label lblFontLeading;
        private System.Windows.Forms.Label lblFontSize;
        private System.Windows.Forms.GroupBox grpPageOptions;
        private System.Windows.Forms.Label lblPageHeader;
        private System.Windows.Forms.Label lblPageHeight;
        private System.Windows.Forms.Label lblPageWidth;
        private System.Windows.Forms.RadioButton rdoPageOptionsCustom;
        private System.Windows.Forms.RadioButton rdoPageOptionsDefaults;
        private System.Windows.Forms.NumericUpDown nudFontLeading;
        private System.Windows.Forms.NumericUpDown nudFontSize;
        private System.Windows.Forms.Label lblFontLeadingUnits;
        private System.Windows.Forms.Label lblFontSizeUnits;
        private System.Windows.Forms.NumericUpDown nudPageHeader;
        private System.Windows.Forms.NumericUpDown nudPageHeight;
        private System.Windows.Forms.NumericUpDown nudPageWidth;
        private System.Windows.Forms.Label lblPageHeaderUnits;
        private System.Windows.Forms.Label lblPageHeightUnits;
        private System.Windows.Forms.Label lblPageWidthUnits;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblProjectUpdatedText;
        private System.Windows.Forms.Label lblProjectUpdated;
        private System.Windows.Forms.Label lblProjectName;
        private System.Windows.Forms.GroupBox grpProject;
        private System.Windows.Forms.Label lblProjectNameText;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem advancedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onDownloadTypesettingMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addCustomFootnotesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem licenseToolStripMenuItem;
        private System.Windows.Forms.Label Copyright;
        private System.Windows.Forms.Label lblVersions;
        private System.Windows.Forms.ToolStripMenuItem useProjectFontToolStripMenuItem;
    }
}