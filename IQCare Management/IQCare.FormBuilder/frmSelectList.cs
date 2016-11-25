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
using Interface.FormBuilder;
using Application.Presentation;
namespace IQCare.FormBuilder
{
    public partial class frmSelectList : Form
    {
        Form theForm;
        public frmSelectList()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strSelectValue = txtEnterValue.Text;
            if (strSelectValue != "")
            {
                int intStringfind = lstSelectList.FindStringExact(strSelectValue, 0);
                if (intStringfind != -1)
                {
                    IQCareWindowMsgBox.ShowWindow("PMTCTSelectlistduplicate", this);
                }
                if (intStringfind == -1)
                {
                    lstSelectList.Items.Add(strSelectValue.Trim());
                }
                txtEnterValue.Text = "";

            }
            txtEnterValue.Focus();

        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int i = this.lstSelectList.SelectedIndex;
            object o = this.lstSelectList.SelectedItem;

            if (lstSelectList.Items.Count > 0)
            {
                if (i < this.lstSelectList.Items.Count - 1)
                {
                    if (i != -1)
                    {
                        this.lstSelectList.Items.RemoveAt(i);
                        this.lstSelectList.Items.Insert(i + 1, o);
                        this.lstSelectList.SelectedIndex = i + 1;
                    }
                }
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int i = this.lstSelectList.SelectedIndex;
            object o = this.lstSelectList.SelectedItem;

            if (i > 0)
            {
                this.lstSelectList.Items.RemoveAt(i);
                this.lstSelectList.Items.Insert(i - 1, o);
                this.lstSelectList.SelectedIndex = i - 1;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int i = this.lstSelectList.SelectedIndex;
            if (i >= 0)
            {
                if (i < this.lstSelectList.Items.Count)
                {
                    this.lstSelectList.Items.RemoveAt(i);
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string strSelectValue = string.Empty;
            if (lstSelectList.Items.Count > 0)
            {
                for (int i = 0; i < lstSelectList.Items.Count; i++)
                {
                    if (strSelectValue == "")
                    {
                        strSelectValue = lstSelectList.Items[i].ToString();
                    }
                    else
                    {
                        strSelectValue = strSelectValue + ";" + lstSelectList.Items[i].ToString();
                    }
                }
                if (GblIQCare.objhashSelectList.Contains(GblIQCare.gblRowIndex))
                {
                    GblIQCare.objhashSelectList[GblIQCare.gblRowIndex] = strSelectValue;
                }

                GblIQCare.hashTblSelectList.Clear();
                GblIQCare.strRetainSelectList = strSelectValue;
                GblIQCare.strRetainSelectField = GblIQCare.strSelectFieldName;
                GblIQCare.hashTblSelectList.Add(GblIQCare.strSelectFieldName, strSelectValue);
                GblIQCare.blnFieldDetailsChange = true;
                GblIQCare.blhselectlistChange = true;
                this.Close();
            }
            else
            {
                IQCareWindowMsgBox.ShowWindow("PMTCTSelectlistvalidate", this);
            }
        }

        private void frmSelectList_Load(object sender, EventArgs e)
        {
            btnconditionalfield.Enabled = true;
            if (GblIQCare.strgblPredefined.ToString() == "P")
            {
                pnlselectlist.Enabled = false;
            }
            else
            {
                pnlselectlist.Enabled = true;
            }
            if (GblIQCare.strControlReferenceId == "SELECTLIST_TEXTBOX")
            {
                btnconditionalfield.Enabled = false;
            }
            //if (GblIQCare.strYesNo == "Yes No")
            //{
            //    lstSelectList.Items.Add("Yes");
            //    lstSelectList.Items.Add("No");
            //    pnlselectlist.Enabled = false;
            //}
            //else
            //{
            //    pnlselectlist.Enabled = true;
            //    lstSelectList.Items.Clear();

            //}

            //disable all controls on form when form mode is 1 i.e. readonly
            if (GblIQCare.iFormMode.ToString() == "1")
            {
                pnlselectlist.Enabled = false;
            }

            if (GblIQCare.iConditionalbtn.ToString() == "1")
            {
                btnconditionalfield.Enabled = false;
            }
            else
            {

                btnconditionalfield.Enabled = true;
                GblIQCare.iConditionalbtn = 0;
            }

            //set css begin
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            //set css end

            if (GblIQCare.strSelectList == "frmFieldDetails")
            {
                if (GblIQCare.objhashSelectList.Contains(GblIQCare.gblRowIndex))
                {
                    char[] splitter1 = { ';' };
                    if (GblIQCare.objhashSelectList[GblIQCare.gblRowIndex].ToString() != "")
                    {
                        string strselect = GblIQCare.objhashSelectList[GblIQCare.gblRowIndex].ToString();
                        string[] arrListValue1 = strselect.Split(splitter1);
                        for (int i = 0; i < arrListValue1.Length; i++)
                        {
                            lstSelectList.Items.Add(arrListValue1[i].ToString().Trim());
                        }
                    }
                }
            }
            else
            {
                if (GblIQCare.objhashSelectList.Contains(GblIQCare.gblRowIndex))
                {
                    char[] splitter1 = { ';' };
                    if (GblIQCare.objhashSelectList[GblIQCare.gblRowIndex].ToString() != "")
                    {
                        string strselect = GblIQCare.objhashSelectList[GblIQCare.gblRowIndex].ToString();
                        string[] arrListValue1 = strselect.Split(splitter1);
                        for (int i = 0; i < arrListValue1.Length; i++)
                        {
                            lstSelectList.Items.Add(arrListValue1[i].ToString().Trim());
                        }
                    }
                }
            }

            /////Written by Rajketu//////////////  
            if (GblIQCare.strYesNo == "Yes No" || GblIQCare.strYesNo == "6")
            {
                lstSelectList.Items.Clear();
                lstSelectList.Items.Add("Yes");
                lstSelectList.Items.Add("No");
                pnlselectlist.Enabled = false;
            }

            /////Written by Rajketu////////////// 
            else if (GblIQCare.strYesNo == "Disease/Symptoms" || GblIQCare.strYesNo == "15")
            {
                DataSet XMLDS = new DataSet();
                XMLDS.ReadXml(GblIQCare.GetXMLPath() + "\\AllMasters.con");
                BindFunctions theBindManager = new BindFunctions();
                DataView theDV = new DataView(XMLDS.Tables["VWDiseaseSymptom"]);
                theDV.RowFilter = "(DeleteFlag =0 or DeleteFlag is null)";
                theDV.Sort = "Name1 ASC";
                DataTable theDT = theDV.ToTable();
                theBindManager.Win_BindListBox(lstSelectList, theDT, "Name1", "Id");
                btnconditionalfield.Enabled = false;
                btnDelete.Enabled = false;
                btnSubmit.Enabled = false;
                btnAdd.Enabled = false;
                txtEnterValue.Enabled = false;
            }

        }

        private void frmSelectList_FormClosed(object sender, FormClosedEventArgs e)
        {
            GblIQCare.objHashtbl.Clear();
        }

        private void btnconditionalfield_Click(object sender, EventArgs e)
        {
            if (GblIQCare.iManageCareEnded == 0 || GblIQCare.iManageCareEnded == 2)
            {
                theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmConditionalDisplay, IQCare.FormBuilder"));
            }
            else
            {
                theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmCareEndConditionalDisplay, IQCare.FormBuilder"));
                GblIQCare.strCareEndcon = "1";
            }
            theForm.Left = 50;
            theForm.Top = 100;
            theForm.Show();
            this.Close();

        }

        private void FieldList()
        {
            string strSelectValue = string.Empty;
            if (lstSelectList.Items.Count > 0)
            {
                for (int i = 0; i < lstSelectList.Items.Count; i++)
                {
                    if (strSelectValue == "")
                    {
                        strSelectValue = lstSelectList.Items[i].ToString();
                    }
                    else
                    {
                        strSelectValue = strSelectValue + ";" + lstSelectList.Items[i].ToString();
                    }
                }
                if (GblIQCare.objhashSelectList.Contains(GblIQCare.gblRowIndex))
                {
                    GblIQCare.objhashSelectList[GblIQCare.gblRowIndex] = strSelectValue;
                    GblIQCare.strSelectListstr = strSelectValue;
                }

                GblIQCare.hashTblSelectList.Clear();
                GblIQCare.strRetainSelectList = strSelectValue;
                GblIQCare.strRetainSelectField = GblIQCare.strSelectFieldName;
                GblIQCare.hashTblSelectList.Add(GblIQCare.strSelectFieldName, strSelectValue);
                GblIQCare.blnFieldDetailsChange = true;
                GblIQCare.blhselectlistChange = true;
                //this.Close();
            }
            else
            {
                IQCareWindowMsgBox.ShowWindow("PMTCTSelectlistvalidate", this);
            }
        }

       

        private void txtEnterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ("_\\/!@#$&%^*<>?`~\";'[]{}".IndexOf(e.KeyChar.ToString()) < 0)
                return;
            e.Handled = true;
        }

    }
}
