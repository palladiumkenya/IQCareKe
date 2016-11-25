using System;
using System.Collections;
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
    public partial class frmManageSection : Form
    {
        DataTable dtSections;
        UserCtrlFormBuilder objFrmBuilder = new UserCtrlFormBuilder();
      //  frmFormBuilder objFrmBuilder = new frmFormBuilder();
        private  DataTable _dtManageSectionPos = new DataTable();
        public  DataTable dtManageSectionPos
        {
            get
            {
                return _dtManageSectionPos;
            }
            set
            {
                _dtManageSectionPos = value;
            }
        }

        frmPatientRegistrationFormBuilder objPRegFrmBuilder = new frmPatientRegistrationFormBuilder();
        
        public frmManageSection()
        {
            InitializeComponent();
          

        }

        private void frmManageSection_Load(object sender, EventArgs e)
        {
            if (GblIQCare.iFormBuilderId == 126)
            {
                dtSections = frmPatientRegistrationFormBuilder.dtManageSectionPos;
                DataView dvSection = dtSections.DefaultView;
                dvSection.Sort = "TopPos asc";
                dtSections = dvSection.ToTable();
                lstSection.Items.Clear();
            }
            else
            {
                
              //  dtSections = frmFormBuilder.dtManageSectionPos;
                dtSections = dtManageSectionPos;
                DataView dvSection = dtSections.DefaultView;
                dvSection.Sort = "TopPos asc";
                dtSections = dvSection.ToTable();
                lstSection.Items.Clear();
            }
           
          for (int i = 0; i < dtSections.Rows.Count; i++)
            {
                lstSection.Items.Add(dtSections.Rows[i]["SectionName"]);
            }
            //set css begin
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            //set css end
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int i = this.lstSection.SelectedIndex;
            object o = this.lstSection.SelectedItem;

            if (i < this.lstSection.Items.Count - 1 && i>=0)
            {
                //column value swapped Down
                dtSections.Rows[i]["SectionName"] = dtSections.Rows[i + 1]["SectionName"];
                dtSections.Rows[i + 1]["SectionName"] = lstSection.Items[i];

                this.lstSection.Items.RemoveAt(i);
                this.lstSection.Items.Insert(i + 1, o);
                this.lstSection.SelectedIndex = i + 1;
            }
    }
      
        private void btnUp_Click(object sender, EventArgs e)
        {
             int i = this.lstSection.SelectedIndex;
            object o = this.lstSection.SelectedItem;

            if (i > 0)
            {

                //column value swapped up
                dtSections.Rows[i]["SectionName"] = dtSections.Rows[i - 1]["SectionName"];
                dtSections.Rows[i - 1]["SectionName"] = lstSection.Items[i];

                this.lstSection.Items.RemoveAt(i);
                this.lstSection.Items.Insert(i - 1, o);
                this.lstSection.SelectedIndex = i - 1;
            }
            
        }
      
        private void btnRemove_Click(object sender, EventArgs e)
        {
            int i = this.lstSection.SelectedIndex;

            if (i < this.lstSection.Items.Count && i >= 0)
            {
                //set deleteflag for section to 1
                dtSections.Rows[i]["DeleteFlag"] = 1;

                //swap position only if items are more than 1
                if (this.lstSection.Items.Count > 1)
                {
                    //move other section upward to accomodate the place of deleted section
                    for (int x = i + 1; x <= dtSections.Rows.Count - 1; x++)
                    {
                        string strSwapSecName;
                        string strSwapSecDeleteFlag;
                        string strSwapSecId;
                        strSwapSecName = dtSections.Rows[x-1]["SectionName"].ToString();
                        strSwapSecDeleteFlag = dtSections.Rows[x-1]["DeleteFlag"].ToString();
                        strSwapSecId = dtSections.Rows[x-1]["SectionId"].ToString();
                        dtSections.Rows[x - 1]["SectionName"] = dtSections.Rows[x]["SectionName"];
                        dtSections.Rows[x - 1]["DeleteFlag"] = dtSections.Rows[x]["DeleteFlag"];
                        dtSections.Rows[x - 1]["SectionId"] = dtSections.Rows[x]["SectionId"];
                        dtSections.Rows[x]["SectionName"] = strSwapSecName;
                        dtSections.Rows[x]["DeleteFlag"] = strSwapSecDeleteFlag;
                        dtSections.Rows[x]["SectionId"] = (strSwapSecId!="")?strSwapSecId:"0";
                    }
                }
                
                this.lstSection.Items.RemoveAt(i);
            }
            
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (GblIQCare.iFormBuilderId == 126)
            {
                frmPatientRegistrationFormBuilder.dtManageSectionPos = dtSections;
                this.Close();
            }
            else
            {
                //frmFormBuilder.dtManageSectionPos = dtSections;
                UserCtrlFormBuilder.dtManageSectionPos = dtSections;
                this.Close();
            }
        }
    }
    }
