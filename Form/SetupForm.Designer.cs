/*
Copyright © 2021 by Biblica, Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using TptMain.Util;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupForm));
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
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblProjectUpdatedText = new System.Windows.Forms.Label();
            this.lblProjectUpdated = new System.Windows.Forms.Label();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.grpProject = new System.Windows.Forms.GroupBox();
            this.lblProjectNameText = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contactSupportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.licenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Copyright = new System.Windows.Forms.Label();
            this.lblVersions = new System.Windows.Forms.Label();
            this.grpBookRange = new System.Windows.Forms.GroupBox();
            this.cbIncludeAncillary = new System.Windows.Forms.CheckBox();
            this.tbCustomBookSet = new System.Windows.Forms.TextBox();
            this.rbCustom = new System.Windows.Forms.RadioButton();
            this.rbNewTestament = new System.Windows.Forms.RadioButton();
            this.rbFullBible = new System.Windows.Forms.RadioButton();
            this.gbAdvanced = new System.Windows.Forms.GroupBox();
            this.gbInclusions = new System.Windows.Forms.GroupBox();
            this.cbAcrostic = new System.Windows.Forms.CheckBox();
            this.cbParallelPassages = new System.Windows.Forms.CheckBox();
            this.cbVerseNumbers = new System.Windows.Forms.CheckBox();
            this.cbChapterNumbers = new System.Windows.Forms.CheckBox();
            this.cbHeadings = new System.Windows.Forms.CheckBox();
            this.cbFootnotes = new System.Windows.Forms.CheckBox();
            this.cbIntros = new System.Windows.Forms.CheckBox();
            this.cbDownloadTypesettingFiles = new System.Windows.Forms.CheckBox();
            this.cbUseProjectFonts = new System.Windows.Forms.CheckBox();
            this.cbLocalizeFootnotes = new System.Windows.Forms.CheckBox();
            this.cbHyphenate = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.rdoLayoutCav = new System.Windows.Forms.RadioButton();
            this.rdoLayoutTbotb = new System.Windows.Forms.RadioButton();
            this.grpLayout = new System.Windows.Forms.GroupBox();
            this.chooseBookButton = new System.Windows.Forms.Button();
            this.grpTextOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontLeading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).BeginInit();
            this.grpPageOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPageHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPageHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPageWidth)).BeginInit();
            this.grpProject.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.grpBookRange.SuspendLayout();
            this.gbAdvanced.SuspendLayout();
            this.gbInclusions.SuspendLayout();
            this.grpLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpTextOptions
            // 
            this.grpTextOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTextOptions.Controls.Add(this.nudFontLeading);
            this.grpTextOptions.Controls.Add(this.nudFontSize);
            this.grpTextOptions.Controls.Add(this.lblFontLeadingUnits);
            this.grpTextOptions.Controls.Add(this.lblFontSizeUnits);
            this.grpTextOptions.Controls.Add(this.lblFontLeading);
            this.grpTextOptions.Controls.Add(this.lblFontSize);
            this.grpTextOptions.Location = new System.Drawing.Point(391, 113);
            this.grpTextOptions.Name = "grpTextOptions";
            this.grpTextOptions.Size = new System.Drawing.Size(208, 84);
            this.grpTextOptions.TabIndex = 12;
            this.grpTextOptions.TabStop = false;
            this.grpTextOptions.Text = "Text";
            // 
            // nudFontLeading
            // 
            this.nudFontLeading.DecimalPlaces = 1;
            this.nudFontLeading.Location = new System.Drawing.Point(64, 45);
            this.nudFontLeading.Maximum = new decimal(new int[] {
            28,
            0,
            0,
            0});
            this.nudFontLeading.Minimum = new decimal(new int[] {
            6,
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
            this.nudFontSize.Location = new System.Drawing.Point(64, 19);
            this.nudFontSize.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.nudFontSize.Minimum = new decimal(new int[] {
            6,
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
            this.lblFontLeadingUnits.Location = new System.Drawing.Point(172, 47);
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
            this.lblFontSizeUnits.Location = new System.Drawing.Point(172, 21);
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
            this.lblFontLeading.Location = new System.Drawing.Point(10, 47);
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
            this.lblFontSize.Location = new System.Drawing.Point(6, 21);
            this.lblFontSize.Name = "lblFontSize";
            this.lblFontSize.Size = new System.Drawing.Size(52, 13);
            this.lblFontSize.TabIndex = 14;
            this.lblFontSize.Text = "Font size:";
            this.lblFontSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpPageOptions
            // 
            this.grpPageOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPageOptions.Controls.Add(this.nudPageHeader);
            this.grpPageOptions.Controls.Add(this.nudPageHeight);
            this.grpPageOptions.Controls.Add(this.nudPageWidth);
            this.grpPageOptions.Controls.Add(this.lblPageHeaderUnits);
            this.grpPageOptions.Controls.Add(this.lblPageHeightUnits);
            this.grpPageOptions.Controls.Add(this.lblPageWidthUnits);
            this.grpPageOptions.Controls.Add(this.lblPageHeader);
            this.grpPageOptions.Controls.Add(this.lblPageHeight);
            this.grpPageOptions.Controls.Add(this.lblPageWidth);
            this.grpPageOptions.Location = new System.Drawing.Point(391, 203);
            this.grpPageOptions.Name = "grpPageOptions";
            this.grpPageOptions.Size = new System.Drawing.Size(208, 139);
            this.grpPageOptions.TabIndex = 16;
            this.grpPageOptions.TabStop = false;
            this.grpPageOptions.Text = "Page";
            // 
            // nudPageHeader
            // 
            this.nudPageHeader.DecimalPlaces = 1;
            this.nudPageHeader.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudPageHeader.Location = new System.Drawing.Point(64, 71);
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
            this.nudPageHeight.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudPageHeight.Location = new System.Drawing.Point(64, 45);
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
            this.nudPageWidth.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudPageWidth.Location = new System.Drawing.Point(64, 19);
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
            this.lblPageHeaderUnits.Location = new System.Drawing.Point(172, 73);
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
            this.lblPageHeightUnits.Location = new System.Drawing.Point(172, 47);
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
            this.lblPageWidthUnits.Location = new System.Drawing.Point(172, 21);
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
            this.lblPageHeader.Location = new System.Drawing.Point(13, 73);
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
            this.lblPageHeight.Location = new System.Drawing.Point(17, 47);
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
            this.lblPageWidth.Location = new System.Drawing.Point(20, 21);
            this.lblPageWidth.Name = "lblPageWidth";
            this.lblPageWidth.Size = new System.Drawing.Size(38, 13);
            this.lblPageWidth.TabIndex = 14;
            this.lblPageWidth.Text = "Width:";
            this.lblPageWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Location = new System.Drawing.Point(438, 587);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 17;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.BtnCreate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(519, 587);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // lblProjectUpdatedText
            // 
            this.lblProjectUpdatedText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProjectUpdatedText.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProjectUpdatedText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProjectUpdatedText.Location = new System.Drawing.Point(379, 47);
            this.lblProjectUpdatedText.Name = "lblProjectUpdatedText";
            this.lblProjectUpdatedText.Size = new System.Drawing.Size(188, 22);
            this.lblProjectUpdatedText.TabIndex = 8;
            this.lblProjectUpdatedText.Text = "None";
            this.lblProjectUpdatedText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProjectUpdated
            // 
            this.lblProjectUpdated.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProjectUpdated.AutoSize = true;
            this.lblProjectUpdated.Location = new System.Drawing.Point(376, 27);
            this.lblProjectUpdated.Name = "lblProjectUpdated";
            this.lblProjectUpdated.Size = new System.Drawing.Size(156, 13);
            this.lblProjectUpdated.TabIndex = 7;
            this.lblProjectUpdated.Text = "Last Updated on Server (GMT):";
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
            this.grpProject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpProject.Controls.Add(this.lblProjectNameText);
            this.grpProject.Controls.Add(this.lblProjectUpdatedText);
            this.grpProject.Controls.Add(this.lblProjectUpdated);
            this.grpProject.Controls.Add(this.lblProjectName);
            this.grpProject.Location = new System.Drawing.Point(12, 22);
            this.grpProject.Name = "grpProject";
            this.grpProject.Size = new System.Drawing.Size(587, 87);
            this.grpProject.TabIndex = 10;
            this.grpProject.TabStop = false;
            this.grpProject.Text = "Project";
            // 
            // lblProjectNameText
            // 
            this.lblProjectNameText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProjectNameText.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProjectNameText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProjectNameText.Location = new System.Drawing.Point(19, 47);
            this.lblProjectNameText.Name = "lblProjectNameText";
            this.lblProjectNameText.Size = new System.Drawing.Size(347, 22);
            this.lblProjectNameText.TabIndex = 9;
            this.lblProjectNameText.Text = "None";
            this.lblProjectNameText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(606, 24);
            this.menuStrip.TabIndex = 19;
            this.menuStrip.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contactSupportToolStripMenuItem,
            this.licenseToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // contactSupportToolStripMenuItem
            // 
            this.contactSupportToolStripMenuItem.Name = "contactSupportToolStripMenuItem";
            this.contactSupportToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.contactSupportToolStripMenuItem.Text = "Contact Support";
            this.contactSupportToolStripMenuItem.Click += new System.EventHandler(this.contactSupportToolStripMenuItem_Click);
            // 
            // licenseToolStripMenuItem
            // 
            this.licenseToolStripMenuItem.Name = "licenseToolStripMenuItem";
            this.licenseToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.licenseToolStripMenuItem.Text = "License";
            this.licenseToolStripMenuItem.Click += new System.EventHandler(this.licenseToolStripMenuItem_Click);
            // 
            // Copyright
            // 
            this.Copyright.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Copyright.AutoSize = true;
            this.Copyright.Location = new System.Drawing.Point(12, 587);
            this.Copyright.Name = "Copyright";
            this.Copyright.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.Copyright.Size = new System.Drawing.Size(0, 23);
            this.Copyright.TabIndex = 20;
            // 
            // lblVersions
            // 
            this.lblVersions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblVersions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersions.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblVersions.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblVersions.Location = new System.Drawing.Point(148, 587);
            this.lblVersions.Name = "lblVersions";
            this.lblVersions.Size = new System.Drawing.Size(224, 23);
            this.lblVersions.TabIndex = 21;
            this.lblVersions.Text = "UI version: X, Server version: Y";
            this.lblVersions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpBookRange
            // 
            this.grpBookRange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBookRange.Controls.Add(this.chooseBookButton);
            this.grpBookRange.Controls.Add(this.cbIncludeAncillary);
            this.grpBookRange.Controls.Add(this.tbCustomBookSet);
            this.grpBookRange.Controls.Add(this.rbCustom);
            this.grpBookRange.Controls.Add(this.rbNewTestament);
            this.grpBookRange.Controls.Add(this.rbFullBible);
            this.grpBookRange.Location = new System.Drawing.Point(13, 203);
            this.grpBookRange.Name = "grpBookRange";
            this.grpBookRange.Size = new System.Drawing.Size(365, 139);
            this.grpBookRange.TabIndex = 22;
            this.grpBookRange.TabStop = false;
            this.grpBookRange.Text = "Book Range";
            // 
            // cbIncludeAncillary
            // 
            this.cbIncludeAncillary.AutoSize = true;
            this.cbIncludeAncillary.Location = new System.Drawing.Point(18, 114);
            this.cbIncludeAncillary.Name = "cbIncludeAncillary";
            this.cbIncludeAncillary.Size = new System.Drawing.Size(143, 17);
            this.cbIncludeAncillary.TabIndex = 4;
            this.cbIncludeAncillary.Text = "Include Ancillary Material";
            this.cbIncludeAncillary.UseVisualStyleBackColor = true;
            // 
            // tbCustomBookSet
            // 
            this.tbCustomBookSet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCustomBookSet.Enabled = false;
            this.tbCustomBookSet.Location = new System.Drawing.Point(39, 88);
            this.tbCustomBookSet.Name = "tbCustomBookSet";
            this.tbCustomBookSet.Size = new System.Drawing.Size(220, 20);
            this.tbCustomBookSet.TabIndex = 3;
            // 
            // rbCustom
            // 
            this.rbCustom.AutoSize = true;
            this.rbCustom.Location = new System.Drawing.Point(18, 65);
            this.rbCustom.Name = "rbCustom";
            this.rbCustom.Size = new System.Drawing.Size(60, 17);
            this.rbCustom.TabIndex = 2;
            this.rbCustom.Text = "Custom";
            this.rbCustom.UseVisualStyleBackColor = true;
            // 
            // rbNewTestament
            // 
            this.rbNewTestament.AutoSize = true;
            this.rbNewTestament.Location = new System.Drawing.Point(18, 42);
            this.rbNewTestament.Name = "rbNewTestament";
            this.rbNewTestament.Size = new System.Drawing.Size(100, 17);
            this.rbNewTestament.TabIndex = 1;
            this.rbNewTestament.Text = "New Testament";
            this.rbNewTestament.UseVisualStyleBackColor = true;
            // 
            // rbFullBible
            // 
            this.rbFullBible.AutoSize = true;
            this.rbFullBible.Checked = true;
            this.rbFullBible.Location = new System.Drawing.Point(18, 19);
            this.rbFullBible.Name = "rbFullBible";
            this.rbFullBible.Size = new System.Drawing.Size(67, 17);
            this.rbFullBible.TabIndex = 0;
            this.rbFullBible.TabStop = true;
            this.rbFullBible.Text = "Full Bible";
            this.rbFullBible.UseVisualStyleBackColor = true;
            // 
            // gbAdvanced
            // 
            this.gbAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbAdvanced.Controls.Add(this.gbInclusions);
            this.gbAdvanced.Controls.Add(this.cbDownloadTypesettingFiles);
            this.gbAdvanced.Controls.Add(this.cbUseProjectFonts);
            this.gbAdvanced.Controls.Add(this.cbLocalizeFootnotes);
            this.gbAdvanced.Controls.Add(this.cbHyphenate);
            this.gbAdvanced.Location = new System.Drawing.Point(13, 348);
            this.gbAdvanced.Name = "gbAdvanced";
            this.gbAdvanced.Size = new System.Drawing.Size(586, 211);
            this.gbAdvanced.TabIndex = 23;
            this.gbAdvanced.TabStop = false;
            this.gbAdvanced.Text = "Advanced";
            // 
            // gbInclusions
            // 
            this.gbInclusions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbInclusions.Controls.Add(this.cbAcrostic);
            this.gbInclusions.Controls.Add(this.cbParallelPassages);
            this.gbInclusions.Controls.Add(this.cbVerseNumbers);
            this.gbInclusions.Controls.Add(this.cbChapterNumbers);
            this.gbInclusions.Controls.Add(this.cbHeadings);
            this.gbInclusions.Controls.Add(this.cbFootnotes);
            this.gbInclusions.Controls.Add(this.cbIntros);
            this.gbInclusions.Location = new System.Drawing.Point(18, 68);
            this.gbInclusions.Name = "gbInclusions";
            this.gbInclusions.Size = new System.Drawing.Size(562, 112);
            this.gbInclusions.TabIndex = 8;
            this.gbInclusions.TabStop = false;
            this.gbInclusions.Text = "Inclusions";
            // 
            // cbAcrostic
            // 
            this.cbAcrostic.AutoSize = true;
            this.cbAcrostic.Location = new System.Drawing.Point(230, 65);
            this.cbAcrostic.Name = "cbAcrostic";
            this.cbAcrostic.Size = new System.Drawing.Size(97, 17);
            this.cbAcrostic.TabIndex = 11;
            this.cbAcrostic.Text = "Acrostic Poetry";
            this.cbAcrostic.UseVisualStyleBackColor = true;
            // 
            // cbParallelPassages
            // 
            this.cbParallelPassages.AutoSize = true;
            this.cbParallelPassages.Checked = true;
            this.cbParallelPassages.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbParallelPassages.Location = new System.Drawing.Point(230, 42);
            this.cbParallelPassages.Name = "cbParallelPassages";
            this.cbParallelPassages.Size = new System.Drawing.Size(109, 17);
            this.cbParallelPassages.TabIndex = 10;
            this.cbParallelPassages.Text = "Parallel Passages";
            this.cbParallelPassages.UseVisualStyleBackColor = true;
            // 
            // cbVerseNumbers
            // 
            this.cbVerseNumbers.AutoSize = true;
            this.cbVerseNumbers.Checked = true;
            this.cbVerseNumbers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbVerseNumbers.Location = new System.Drawing.Point(230, 19);
            this.cbVerseNumbers.Name = "cbVerseNumbers";
            this.cbVerseNumbers.Size = new System.Drawing.Size(98, 17);
            this.cbVerseNumbers.TabIndex = 9;
            this.cbVerseNumbers.Text = "Verse Numbers";
            this.cbVerseNumbers.UseVisualStyleBackColor = true;
            // 
            // cbChapterNumbers
            // 
            this.cbChapterNumbers.AutoSize = true;
            this.cbChapterNumbers.Checked = true;
            this.cbChapterNumbers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbChapterNumbers.Location = new System.Drawing.Point(21, 89);
            this.cbChapterNumbers.Name = "cbChapterNumbers";
            this.cbChapterNumbers.Size = new System.Drawing.Size(108, 17);
            this.cbChapterNumbers.TabIndex = 8;
            this.cbChapterNumbers.Text = "Chapter Numbers";
            this.cbChapterNumbers.UseVisualStyleBackColor = true;
            // 
            // cbHeadings
            // 
            this.cbHeadings.AutoSize = true;
            this.cbHeadings.Checked = true;
            this.cbHeadings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbHeadings.Location = new System.Drawing.Point(21, 42);
            this.cbHeadings.Name = "cbHeadings";
            this.cbHeadings.Size = new System.Drawing.Size(71, 17);
            this.cbHeadings.TabIndex = 5;
            this.cbHeadings.Text = "Headings";
            this.cbHeadings.UseVisualStyleBackColor = true;
            // 
            // cbFootnotes
            // 
            this.cbFootnotes.AutoSize = true;
            this.cbFootnotes.Checked = true;
            this.cbFootnotes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFootnotes.Location = new System.Drawing.Point(21, 65);
            this.cbFootnotes.Name = "cbFootnotes";
            this.cbFootnotes.Size = new System.Drawing.Size(73, 17);
            this.cbFootnotes.TabIndex = 7;
            this.cbFootnotes.Text = "Footnotes";
            this.cbFootnotes.UseVisualStyleBackColor = true;
            // 
            // cbIntros
            // 
            this.cbIntros.AutoSize = true;
            this.cbIntros.Checked = true;
            this.cbIntros.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIntros.Location = new System.Drawing.Point(21, 19);
            this.cbIntros.Name = "cbIntros";
            this.cbIntros.Size = new System.Drawing.Size(52, 17);
            this.cbIntros.TabIndex = 4;
            this.cbIntros.Text = "Intros";
            this.cbIntros.UseVisualStyleBackColor = true;
            // 
            // cbDownloadTypesettingFiles
            // 
            this.cbDownloadTypesettingFiles.AutoSize = true;
            this.cbDownloadTypesettingFiles.Location = new System.Drawing.Point(248, 186);
            this.cbDownloadTypesettingFiles.Name = "cbDownloadTypesettingFiles";
            this.cbDownloadTypesettingFiles.Size = new System.Drawing.Size(156, 17);
            this.cbDownloadTypesettingFiles.TabIndex = 3;
            this.cbDownloadTypesettingFiles.Text = "Download Typesetting Files";
            this.cbDownloadTypesettingFiles.UseVisualStyleBackColor = true;
            // 
            // cbUseProjectFonts
            // 
            this.cbUseProjectFonts.AutoSize = true;
            this.cbUseProjectFonts.Checked = true;
            this.cbUseProjectFonts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUseProjectFonts.Location = new System.Drawing.Point(249, 20);
            this.cbUseProjectFonts.Name = "cbUseProjectFonts";
            this.cbUseProjectFonts.Size = new System.Drawing.Size(110, 17);
            this.cbUseProjectFonts.TabIndex = 2;
            this.cbUseProjectFonts.Text = "Use Project Fonts";
            this.cbUseProjectFonts.UseVisualStyleBackColor = true;
            // 
            // cbLocalizeFootnotes
            // 
            this.cbLocalizeFootnotes.AutoSize = true;
            this.cbLocalizeFootnotes.Location = new System.Drawing.Point(18, 44);
            this.cbLocalizeFootnotes.Name = "cbLocalizeFootnotes";
            this.cbLocalizeFootnotes.Size = new System.Drawing.Size(115, 17);
            this.cbLocalizeFootnotes.TabIndex = 1;
            this.cbLocalizeFootnotes.Text = "Localize Footnotes";
            this.cbLocalizeFootnotes.UseVisualStyleBackColor = true;
            // 
            // cbHyphenate
            // 
            this.cbHyphenate.AutoSize = true;
            this.cbHyphenate.Location = new System.Drawing.Point(18, 20);
            this.cbHyphenate.Name = "cbHyphenate";
            this.cbHyphenate.Size = new System.Drawing.Size(78, 17);
            this.cbHyphenate.TabIndex = 0;
            this.cbHyphenate.Text = "Hyphenate";
            this.cbHyphenate.UseVisualStyleBackColor = true;
            // 
            // rdoLayoutCav
            // 
            this.rdoLayoutCav.AutoSize = true;
            this.rdoLayoutCav.Checked = true;
            this.rdoLayoutCav.Location = new System.Drawing.Point(19, 19);
            this.rdoLayoutCav.Name = "rdoLayoutCav";
            this.rdoLayoutCav.Size = new System.Drawing.Size(146, 17);
            this.rdoLayoutCav.TabIndex = 9;
            this.rdoLayoutCav.TabStop = true;
            this.rdoLayoutCav.Text = " Chapter and Verse (CAV)";
            this.rdoLayoutCav.UseVisualStyleBackColor = true;
            this.rdoLayoutCav.CheckedChanged += new System.EventHandler(this.grpLayout_Changed);
            // 
            // rdoLayoutTbotb
            // 
            this.rdoLayoutTbotb.AutoSize = true;
            this.rdoLayoutTbotb.Location = new System.Drawing.Point(19, 42);
            this.rdoLayoutTbotb.Name = "rdoLayoutTbotb";
            this.rdoLayoutTbotb.Size = new System.Drawing.Size(181, 17);
            this.rdoLayoutTbotb.TabIndex = 10;
            this.rdoLayoutTbotb.Text = " The Books of the Bible (TBOTB)";
            this.rdoLayoutTbotb.UseVisualStyleBackColor = true;
            this.rdoLayoutTbotb.CheckedChanged += new System.EventHandler(this.grpLayout_Changed);
            // 
            // grpLayout
            // 
            this.grpLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpLayout.Controls.Add(this.rdoLayoutTbotb);
            this.grpLayout.Controls.Add(this.rdoLayoutCav);
            this.grpLayout.Location = new System.Drawing.Point(12, 113);
            this.grpLayout.Name = "grpLayout";
            this.grpLayout.Size = new System.Drawing.Size(366, 84);
            this.grpLayout.TabIndex = 11;
            this.grpLayout.TabStop = false;
            this.grpLayout.Text = "Layout";
            // 
            // chooseBookButton
            // 
            this.chooseBookButton.Location = new System.Drawing.Point(265, 88);
            this.chooseBookButton.Name = "chooseBookButton";
            this.chooseBookButton.Size = new System.Drawing.Size(94, 20);
            this.chooseBookButton.TabIndex = 5;
            this.chooseBookButton.Text = "Choose...";
            this.chooseBookButton.UseVisualStyleBackColor = true;
            this.chooseBookButton.Click += new System.EventHandler(this.chooseBooksButton_Click);
            // 
            // SetupForm
            // 
            this.AcceptButton = this.btnCreate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(606, 619);
            this.ControlBox = false;
            this.Controls.Add(this.gbAdvanced);
            this.Controls.Add(this.grpBookRange);
            this.Controls.Add(this.lblVersions);
            this.Controls.Add(this.Copyright);
            this.Controls.Add(this.grpProject);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.grpPageOptions);
            this.Controls.Add(this.grpTextOptions);
            this.Controls.Add(this.grpLayout);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "SetupForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Print Preview...";
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
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.grpBookRange.ResumeLayout(false);
            this.grpBookRange.PerformLayout();
            this.gbAdvanced.ResumeLayout(false);
            this.gbAdvanced.PerformLayout();
            this.gbInclusions.ResumeLayout(false);
            this.gbInclusions.PerformLayout();
            this.grpLayout.ResumeLayout(false);
            this.grpLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox grpTextOptions;
        private System.Windows.Forms.Label lblFontLeading;
        private System.Windows.Forms.Label lblFontSize;
        private System.Windows.Forms.GroupBox grpPageOptions;
        private System.Windows.Forms.Label lblPageHeader;
        private System.Windows.Forms.Label lblPageHeight;
        private System.Windows.Forms.Label lblPageWidth;
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
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem licenseToolStripMenuItem;
        private System.Windows.Forms.Label Copyright;
        private System.Windows.Forms.Label lblVersions;
        private System.Windows.Forms.GroupBox grpBookRange;
        private System.Windows.Forms.TextBox tbCustomBookSet;
        private System.Windows.Forms.RadioButton rbCustom;
        private System.Windows.Forms.RadioButton rbNewTestament;
        private System.Windows.Forms.RadioButton rbFullBible;
        private System.Windows.Forms.GroupBox gbAdvanced;
        private System.Windows.Forms.CheckBox cbIncludeAncillary;
        private System.Windows.Forms.CheckBox cbHeadings;
        private System.Windows.Forms.CheckBox cbIntros;
        private System.Windows.Forms.CheckBox cbDownloadTypesettingFiles;
        private System.Windows.Forms.CheckBox cbUseProjectFonts;
        private System.Windows.Forms.CheckBox cbLocalizeFootnotes;
        private System.Windows.Forms.CheckBox cbHyphenate;
        private System.Windows.Forms.GroupBox gbInclusions;
        private System.Windows.Forms.CheckBox cbAcrostic;
        private System.Windows.Forms.CheckBox cbParallelPassages;
        private System.Windows.Forms.CheckBox cbVerseNumbers;
        private System.Windows.Forms.CheckBox cbChapterNumbers;
        private System.Windows.Forms.CheckBox cbFootnotes;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem contactSupportToolStripMenuItem;
        private System.Windows.Forms.RadioButton rdoLayoutCav;
        private System.Windows.Forms.RadioButton rdoLayoutTbotb;
        private System.Windows.Forms.GroupBox grpLayout;
        private System.Windows.Forms.Button chooseBookButton;
    }
}