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
using System.Configuration;
using System.Web;

    namespace IQCare.FormBuilder
    {
    public partial class frmSplFormLink : Form
    {
        DataSet objDsFormModuleLinkDetails = new DataSet();
       // int ModuleId;
        public frmSplFormLink()
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
                objDsFormModuleLinkDetails = objFormModuleLink.GetFormModuleLinkDetail(0);
                if (objDsFormModuleLinkDetails.Tables[1].Rows.Count > 0)
                {
                    BindFunctions theBind = new BindFunctions();
                    theBind.Win_BindCombo(cmbModuleName, objDsFormModuleLinkDetails.Tables[0], "ModuleName", "ModuleID");
                    theBind.Win_BindCheckListBox(chklistFormName, objDsFormModuleLinkDetails.Tables[1], "FeatureName", "FeatureID");
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
                 if (cmbModuleName.SelectedText == "Select")
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

                 if (list.Count == 0)
                 {
                    
                     list.Add(-1);
                     //IQCareWindowMsgBox.ShowWindow("SelectFormName", this);
                     //return;

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
            
             if (cmbModuleName.SelectedIndex==0)
             {
                 
                 return;

             }
             if (cmbModuleName.SelectedText == "Select") 
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
                 IFormModuleLink objFormModuleLink;
                // IManageForms objFormDetail;
           
                 objFormModuleLink = (IFormModuleLink)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFormModuleLink, BusinessProcess.FormBuilder");
                 objDsFormModuleLinkDetails = objFormModuleLink.GetFormModuleLinkDetail(intModuleId);
                 if (objDsFormModuleLinkDetails.Tables[1].Rows.Count > 0)
                 {
                     BindFunctions theBind = new BindFunctions();
                     chklistFormName.DataSource=null;
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

     }  

    }