using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tools_paratext_preview_plugin.Form
{
    public partial class ResultForm : System.Windows.Forms.Form
    {
        public ResultForm()
        {
            InitializeComponent();

            webBrowser1.Navigate("file://E:/Projects/Work/Biblica/DT369/tools-paratext-preview-plugin/Resources/preview.pdf");
        }
    }
}
