using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using Application.Common;
using Interface.FormBuilder;
using Application.Presentation;

namespace IQCare.FormBuilder
{
    public partial class frmFormModuleLink : Form
    {
        DataSet objDsFormModuleLinkDetails = new DataSet();
        int FlagValidate = 1;
        public frmFormModuleLink()
        {
            InitializeComponent();
        }

        private void frmModulelink_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            BindData();
        }

        public void SaveUpdateFormModuleLinkDetail()
        {

        }
        public void BindData()
        {
            try
            {

                IFormModuleLink objFormModuleLink = (IFormModuleLink)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFormModuleLink, BusinessProcess.FormBuilder");
                objDsFormModuleLinkDetails = objFormModuleLink.FormModuleLinking(0, Convert.ToInt32(GblIQCare.AppCountryId));
                if (objDsFormModuleLinkDetails.Tables[1].Rows.Count > 0)
                {

                    BindFunctions theBind = new BindFunctions();
                    theBind.Win_BindCombo(cmbModuleName, objDsFormModuleLinkDetails.Tables[0], "ModuleName", "ModuleID");
                    theBind.Win_BindCheckListBox(chklistFormName, objDsFormModuleLinkDetails.Tables[1], "FeatureName", "FeatureID");
                    chklistFormName.SelectedValueChanged += new EventHandler(chklistFormName_SelectedValueChanged);
                    chklistFormName.DoubleClick += new EventHandler(chklistFormName_DoubleClick);

                }

            }
            catch (Exception err)
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList list = new ArrayList();
                Int32 intModuleId = 0;
                IFormModuleLink objFormModuleLink = (IFormModuleLink)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFormModuleLink, BusinessProcess.FormBuilder");
                //if (cmbModuleName.SelectedText == "Select")
                if (cmbModuleName.Text == "Select")
                {
                    IQCareWindowMsgBox.ShowWindow("SelectTechnicalArea", this);
                    return;
                }
                else
                {
                    intModuleId = Convert.ToInt32(cmbModuleName.SelectedValue);
                }


                for (int i = 0; i < chklistFormName.Items.Count; i++)
                {
                    if (chklistFormName.GetItemChecked(i) == true)
                    {
                        list.Add((((System.Data.DataRowView)(chklistFormName.Items[i])).Row.ItemArray[0]).ToString());
                    }

                }
                if (FlagValidate == 0)
                {
                    if (list.Count == 0)
                    {
                        list.Add(-1);
                        IQCareWindowMsgBox.ShowWindow("SelectFormName", this);
                        return;
                    }
                }
                BindFunctions BindMgr = new BindFunctions();
                objFormModuleLink.SaveUpdateFormModuleLinkDetail(intModuleId, list, GblIQCare.AppUserId);
                IQCareWindowMsgBox.ShowWindow("SaveFormlink", this);
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void cmbModuleName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 intModuleId = 0;

            if (cmbModuleName.SelectedIndex == 0)
            {

                return;

            }
            if (cmbModuleName.Text == "Select")
            {
                IQCareWindowMsgBox.ShowWindow("SelectTechnicalArea", this);
                return;

            }
            else
            {
                intModuleId = Convert.ToInt32(cmbModuleName.SelectedValue);

            }

            try
            {
                IFormModuleLink   objFormModuleLink = (IFormModuleLink)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFormModuleLink, BusinessProcess.FormBuilder");
                objDsFormModuleLinkDetails = objFormModuleLink.FormModuleLinking(intModuleId, Convert.ToInt32(GblIQCare.AppCountryId));
                if (objDsFormModuleLinkDetails.Tables[2].Rows[0][0].ToString() == "0")
                {
                    FlagValidate = 0;
                }
                else
                {
                    FlagValidate = 1;
                }
                if (objDsFormModuleLinkDetails.Tables[1].Rows.Count > 0)
                {
                    BindFunctions theBind = new BindFunctions();
                    chklistFormName.DataSource = null;
                    theBind.Win_BindCheckListBox(chklistFormName, objDsFormModuleLinkDetails.Tables[1], "FeatureName", "FeatureID");
                    for (int i = 0; i < objDsFormModuleLinkDetails.Tables[1].Rows.Count; i++)
                    {
                        if (objDsFormModuleLinkDetails.Tables[1].Rows[i]["Selected"].ToString() == "True")
                        {
                            this.chklistFormName.SetItemChecked(i, true);
                        }
                    }
                }

            }
            catch (Exception err)
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }

        }

        void chklistFormName_DoubleClick(object sender, EventArgs e)
        {
            if (!object.Equals(((System.Windows.Forms.CheckedListBox)(sender)).SelectedItem, null))
            {
                object[] array = ((System.Data.DataRowView)(((System.Windows.Forms.CheckedListBox)(sender)).SelectedItem)).Row.ItemArray;
                if (array[3].ToString().ToLower() == "true")
                {
                    int index = ((System.Windows.Forms.CheckedListBox)(sender)).SelectedIndex;
                    if (!((System.Windows.Forms.CheckedListBox)(sender)).GetItemChecked(index))
                    {
                        this.chklistFormName.SetItemChecked(index, true);
                    }
                }
            }
        }

        void chklistFormName_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!object.Equals(((System.Windows.Forms.CheckedListBox)(sender)).SelectedItem, null))
            {
                object[] array = ((System.Data.DataRowView)(((System.Windows.Forms.CheckedListBox)(sender)).SelectedItem)).Row.ItemArray;
                if (array[3].ToString().ToLower() == "true")
                {
                    int index = ((System.Windows.Forms.CheckedListBox)(sender)).SelectedIndex;
                    if (!((System.Windows.Forms.CheckedListBox)(sender)).GetItemChecked(index))
                    {
                        this.chklistFormName.SetItemChecked(index, true);
                    }
                }
            }
        }


    }

}