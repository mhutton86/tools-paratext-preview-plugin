using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tools_tpt_transformation_service.Models;

namespace tools_paratext_preview_plugin.Form
{
    public partial class PreviewForm : System.Windows.Forms.Form
    {
        private PreviewJob _previewJob;
        private FileInfo _previewFile;

        public PreviewForm()
        {
            InitializeComponent();
        }

        internal void SetPreviewFile(PreviewJob previewJob, FileInfo previewFile)
        {
            _previewJob = previewJob;
            _previewFile = previewFile;

            Text = $"Preview: {previewJob.ProjectName}, Format: {previewJob.BookFormat}, Font: {previewJob.FontSizeInPts}pts, Leading: {previewJob.FontLeadingInPts}pts";
            webPreview.Navigate(new Uri(_previewFile.FullName).AbsoluteUri);
        }
    }
}
