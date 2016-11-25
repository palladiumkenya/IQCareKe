using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Application.Common;

namespace IQCare.SCM
{
    public partial class frmConfigureDonermaster : Form
    {
        public frmConfigureDonermaster()
        {
            InitializeComponent();
        }

        private void frmConfigureDonermaster_Load(object sender, EventArgs e)
        {
            dateTimePicker2.CustomFormat = "dd-MMM-yyyy";
            dateTimePicker1.CustomFormat = "dd-MMM-yyyy";

            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

       
    }
}
