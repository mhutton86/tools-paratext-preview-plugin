using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tools_tpt_transformation_service.Models;

namespace tools_paratext_preview_plugin.Form
{
    public partial class SetupForm : System.Windows.Forms.Form
    {
        private ProjectDetails _projectDetails;

        public bool IsCreating { get; set; }

        public SetupForm()
        {
            InitializeComponent();
        }

        private void SetupForm_Load(object sender, EventArgs e)
        {

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            IsCreating = true;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void rdoTextOptionsDefaults_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoTextOptionsDefaults.Checked)
            {
                ControlTextOptions(false);
            }
        }

        private void rdoTextOptionsCustom_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoTextOptionsCustom.Checked)
            {
                ControlTextOptions(true);
            }
        }

        private void ControlTextOptions(bool isEnabled)
        {
            lblFontSize.Enabled = isEnabled;
            nudFontSize.Enabled = isEnabled;
            lblFontSizeUnits.Enabled = isEnabled;

            lblFontLeading.Enabled = isEnabled;
            nudFontLeading.Enabled = isEnabled;
            lblFontLeadingUnits.Enabled = isEnabled;
        }

        private void rdoPageOptionsDefaults_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPageOptionsDefaults.Checked)
            {
                ControlPageOptions(false);
            }
        }

        private void rdoPageOptionsCustom_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPageOptionsCustom.Checked)
            {
                ControlPageOptions(true);
            }
        }

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

        public float? FontSizeInPts
        {
            get { return rdoTextOptionsDefaults.Checked ? (float?)null : Convert.ToSingle(nudFontSize.Value); }
        }

        public float? FontLeadingInPts
        {
            get { return rdoTextOptionsDefaults.Checked ? (float?)null : Convert.ToSingle(nudFontLeading.Value); }
        }

        public float? PageHeightInPts
        {
            get { return rdoPageOptionsDefaults.Checked ? (float?)null : Convert.ToSingle(nudPageHeight.Value); }
        }

        public float? PageWidthInPts
        {
            get { return rdoPageOptionsDefaults.Checked ? (float?)null : Convert.ToSingle(nudPageWidth.Value); }
        }

        public float? PageHeaderInPts
        {
            get { return rdoPageOptionsDefaults.Checked ? (float?)null : Convert.ToSingle(nudPageHeader.Value); }
        }

        public BookFormat BookFormat
        {
            get { return rdoLayoutCav.Checked ? BookFormat.cav : BookFormat.tbotb; }
        }

        public string ProjectName
        {
            get { return lblProjectNameText.Text; }
        }

        internal void SetProjectDetails(ProjectDetails projectDetails)
        {
            _projectDetails = projectDetails;

            lblProjectNameText.Text = _projectDetails.ProjectName;
            lblProjectUpdatedText.Text = _projectDetails.ProjectUpdated.ToString("u");
        }
    }
}
