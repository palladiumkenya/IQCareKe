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
using Interface.FormBuilder;

    namespace IQCare.FormBuilder
    {
    public partial class frmOrderForms : Form
    {
        DataTable dtForms;
        frmFormBuilder objFrmBuilder = new frmFormBuilder();
        DataSet objDsFormDetails = new DataSet();
        public frmOrderForms()
        {
            InitializeComponent();
        }

        private void frmOrderForms_Load(object sender, EventArgs e)
        {
            BindFunctions objBindControls = new BindFunctions();
            DataSet dsModule;
            DataTable dtAddAll;
            IManageForms objManageForms;
            objManageForms = (IManageForms)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BManageForms,BusinessProcess.FormBuilder");
            dsModule = objManageForms.GetPublishedModuleList();
            dtAddAll = dsModule.Tables[0];
            DataRow theDR = dtAddAll.NewRow();
            theDR["ModuleName"] = "Select";
            theDR["ModuleId"] = 0;
            dtAddAll.Rows.InsertAt(theDR, 0);
            objBindControls.Win_BindCombo(cmbTechArea, dtAddAll, "ModuleName", "ModuleId");
            cmbTechArea.SelectedIndex = 0;
            //set css begin
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
           //set css end
        }

        private void cmbTechArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstForms.Items.Clear();
            if (cmbTechArea.SelectedIndex.ToString() != "0")
            {
                BindList();
            }
        }
        /// <summary>
        /// This function is used to bind grid with database
        /// </summary>
        /// <param name="FieldName"></param>
        public void BindList()
        {
            try
            {
                IManageForms objFormDetail;
                objFormDetail = (IManageForms)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BManageForms,BusinessProcess.FormBuilder");
                objDsFormDetails = objFormDetail.GetFormDetail("1", (cmbTechArea.SelectedValue == null || cmbTechArea.SelectedIndex == 0) ? "0" : cmbTechArea.SelectedValue.ToString(), Convert.ToInt16(GblIQCare.AppCountryId));

                DataView dvForm = objDsFormDetails.Tables[0].DefaultView;
                dvForm.Sort = "seq asc";
                dtForms = dvForm.ToTable();
                lstForms.Items.Clear();
                for (int i = 0; i < dtForms.Rows.Count; i++)
                {
                    lstForms.Items.Add(dtForms.Rows[i]["FormName"]);

                }

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }
        private void btnDown_Click(object sender, EventArgs e)
        {
            int i = this.lstForms.SelectedIndex;
            object o = this.lstForms.SelectedItem;

            if (i < this.lstForms.Items.Count - 1 && i >= 0)
            {

                dtForms.Rows[i]["FormName"] = dtForms.Rows[i + 1]["FormName"];
                dtForms.Rows[i + 1]["FormName"] = lstForms.Items[i];
                this.lstForms.Items.RemoveAt(i);
                this.lstForms.Items.Insert(i + 1, o);
                this.lstForms.SelectedIndex = i + 1;
            }
    }
      
        private void btnUp_Click(object sender, EventArgs e)
        {
            int i = this.lstForms.SelectedIndex;
            object o = this.lstForms.SelectedItem;

            if (i > 0)
            {

                //column value swapped up
                dtForms.Rows[i]["FormName"] = dtForms.Rows[i - 1]["FormName"];
                dtForms.Rows[i - 1]["FormName"] = lstForms.Items[i];
                this.lstForms.Items.RemoveAt(i);
                this.lstForms.Items.Insert(i - 1, o);
                this.lstForms.SelectedIndex = i - 1;
            }
            
        }
      
      

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            ////frmFormBuilder.dtManageSectionPos = dtSections;

            DataTable dtMstFeature = new DataTable();
            dtMstFeature = clsCommon.CreateTableMstFeature();
            DataRow row_Form;
            int iSeq = 1;
            for (int iRow = 0; iRow < lstForms.Items.Count; iRow++)
            {
                row_Form = dtMstFeature.NewRow();
                row_Form["FeatureName"] = dtForms.Rows[iRow]["FormName"].ToString();
                row_Form["FeatureId"] = dtForms.Rows[iRow]["FormId"].ToString();
                row_Form["ModuleId"] = cmbTechArea.SelectedValue.ToString();
                dtMstFeature.Rows.Add(row_Form);
                dtMstFeature.Rows[iRow]["seq"] = iSeq;

                iSeq += 1;
            }

            IFormBuilder objFormBuilder;
            objFormBuilder = (IFormBuilder)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFormBuilder,BusinessProcess.FormBuilder");
            int res = objFormBuilder.UpdateFormDetailSeq(dtMstFeature);

            this.Close();
        }

       

       
    }
    }
