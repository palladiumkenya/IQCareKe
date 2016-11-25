using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Application.Common;
using Application.Presentation;

namespace IQCare.FormBuilder
{
    public partial class frmManageFBTab : Form
    {
        DataTable dtTab;
        private DataTable _dtTabPos = new DataTable();
        public DataTable dtTabPos
        {
            get
            {
                return _dtTabPos;
            }
            set
            {
                _dtTabPos = value;
            }
        }

        public frmManageFBTab()
        {
            InitializeComponent();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int i = this.lstTabPos.SelectedIndex;

            if (i < this.lstTabPos.Items.Count && i >= 0)
            {
                //set deleteflag for section to 1
                dtTab.Rows[i]["DeleteFlag"] = 1;

                //swap position only if items are more than 1
                if (this.lstTabPos.Items.Count > 1)
                {
                    //move other section upward to accomodate the place of deleted section
                    for (int x = i + 1; x <= dtTab.Rows.Count - 1; x++)
                    {
                        string strSwapSecName;
                        string strSwapSecDeleteFlag;
                        string strSwapSecId;
                        strSwapSecName = dtTab.Rows[x - 1]["TabName"].ToString();
                        strSwapSecDeleteFlag = dtTab.Rows[x - 1]["DeleteFlag"].ToString();
                        strSwapSecId = dtTab.Rows[x - 1]["TabId"].ToString();
                        dtTab.Rows[x - 1]["TabName"] = dtTab.Rows[x]["TabName"];
                        dtTab.Rows[x - 1]["DeleteFlag"] = dtTab.Rows[x]["DeleteFlag"];
                        dtTab.Rows[x - 1]["TabId"] = dtTab.Rows[x]["TabId"];
                        dtTab.Rows[x]["TabName"] = strSwapSecName;
                        dtTab.Rows[x]["DeleteFlag"] = strSwapSecDeleteFlag;
                        dtTab.Rows[x]["TabId"] = (strSwapSecId != "") ? strSwapSecId : "-1";
                    }
                }

                this.lstTabPos.Items.RemoveAt(i);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            frmFormBuilder.dtManageTabPos = dtTab;
            this.Close();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int i = this.lstTabPos.SelectedIndex;
            object o = this.lstTabPos.SelectedItem;

            if (i > 0)
            {

                //column value swapped up
                dtTab.Rows[i]["TabName"] = dtTab.Rows[i - 1]["TabName"];
                dtTab.Rows[i - 1]["TabName"] = lstTabPos.Items[i];

                this.lstTabPos.Items.RemoveAt(i);
                this.lstTabPos.Items.Insert(i - 1, o);
                this.lstTabPos.SelectedIndex = i - 1;
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int i = this.lstTabPos.SelectedIndex;
            object o = this.lstTabPos.SelectedItem;

            if (i < this.lstTabPos.Items.Count - 1 && i >= 0)
            {
                //column value swapped Down
                dtTab.Rows[i]["TabName"] = dtTab.Rows[i + 1]["TabName"];
                dtTab.Rows[i + 1]["TabName"] = lstTabPos.Items[i];

                this.lstTabPos.Items.RemoveAt(i);
                this.lstTabPos.Items.Insert(i + 1, o);
                this.lstTabPos.SelectedIndex = i + 1;
            }
        }

        private void frmManageFBTab_Load(object sender, EventArgs e)
        {
            dtTab = dtTabPos;
            DataView dvTab = dtTab.DefaultView;
            dvTab.Sort = "TopPos asc";
            dtTab = dvTab.ToTable();
            lstTabPos.Items.Clear();

            for (int i = 0; i < dtTab.Rows.Count; i++)
            {
                lstTabPos.Items.Add(dtTab.Rows[i]["TabName"]);
            }
            //set css begin
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            //set css end
        }

       
    }
}
