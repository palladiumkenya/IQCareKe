using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Application.Common;
using Interface.FormBuilder;
using Application.Presentation;

namespace IQCare.FormBuilder
{
    public partial class frmMasterList : Form
    {
        public frmMasterList()
        {
            InitializeComponent();
        }

        private void frmMasterList_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);


           
        }

        }
}
