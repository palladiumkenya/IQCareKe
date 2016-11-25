using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Application.Common;
using Interface.FormBuilder;
using Application.Presentation;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

    namespace IQCare.FormBuilder
    { 
    partial class frmHomePage : Form
    {     
    Int32 theGridRowNo = 0;
    bool bCellClick;
    string Flag;
    int ID = 0;
    public frmHomePage()
    {
         InitializeComponent();
      
    }

    #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion
    private void frmHomePage_Load(object sender, EventArgs e)
    {
     try
        {
               //set css begin
                clsCssStyle theStyle = new clsCssStyle();
                theStyle.setStyle(this);
                //set css end
                bCellClick = false;
                IHomePageConfiguration objTechArea = (IHomePageConfiguration)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BHomePageConfiguration,BusinessProcess.FormBuilder");
                DataSet dsTechAreaDetails = new DataSet();
                dsTechAreaDetails = objTechArea.GetTechnicalArea();
                BindFunctions theBind = new BindFunctions();
                theBind.Win_BindCombo(cmbTechnicalArea, dsTechAreaDetails.Tables[1], "ModuleName", "ModuleID");
                cmbHomePageType.SelectedIndex = 0;
                DataSet dsIndicatorQuery = new DataSet();
                dsIndicatorQuery = objTechArea.GetIndicatorQueryResult(GblIQCare.iHomePageId);
                if (dsIndicatorQuery.Tables.Count > 1)
                {
                    if (dsIndicatorQuery.Tables[1].Rows.Count > 0)
                    {
                        txtSectionTitle.Text = dsIndicatorQuery.Tables[1].Rows[0]["FacilityHomeTitle"].ToString();
                        cmbHomePageType.Text = dsIndicatorQuery.Tables[1].Rows[0]["Feature"].ToString();
                        cmbTechnicalArea.SelectedValue = dsIndicatorQuery.Tables[1].Rows[0]["Module"].ToString();
                        ID = Convert.ToInt32(dsIndicatorQuery.Tables[1].Rows[0]["ID"].ToString());
                    }
                }
                DataColumn theColumn = dsIndicatorQuery.Tables[0].Columns["Indicator"];
                dsIndicatorQuery.Tables[0].Constraints.Add("Pk", theColumn, true);
                if (GblIQCare.iHomePageId != 0)
                {
                    cmbTechnicalArea.Enabled = false;
                    cmbHomePageType.Enabled = false;
                }
                BindGrid(dsIndicatorQuery.Tables[0]);
                }

        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
            return;
        }

    }
    private void BindGrid(DataTable theDT)
    {
                dgwQueryDetails.DataSource = null;
                dgwQueryDetails.AutoGenerateColumns = false;
                dgwQueryDetails.Columns.Clear();
           
                DataGridViewTextBoxColumn theColumnId = new DataGridViewTextBoxColumn();
                theColumnId.HeaderText = "Id";
                theColumnId.DataPropertyName = "Id";
                theColumnId.Width = 10;
                theColumnId.Visible = false;

                DataGridViewTextBoxColumn theColumnHomePageId = new DataGridViewTextBoxColumn();
                theColumnHomePageId.HeaderText = "HomePageId";
                theColumnHomePageId.DataPropertyName = "HomePageId";
                theColumnHomePageId.Width = 500;
                theColumnHomePageId.Visible = false;

                DataGridViewTextBoxColumn theColumnIndicator = new DataGridViewTextBoxColumn();
                theColumnIndicator.HeaderText = "Indicator";
                theColumnIndicator.DataPropertyName = "Indicator";
                theColumnIndicator.Width = 500;

                DataGridViewImageColumn theColumnQuery = new DataGridViewImageColumn();
                theColumnQuery.HeaderText = "Query";
                theColumnQuery.Name = "Query";
                theColumnQuery.DataPropertyName = "Query";
                theColumnQuery.Width = 300;
                
                DataGridViewTextBoxColumn theColumnSeq = new DataGridViewTextBoxColumn();
                theColumnSeq.HeaderText = "Seq";
                theColumnSeq.DataPropertyName = "Seq";
                theColumnSeq.Width = 10;
                theColumnSeq.Visible = false;

                DataGridViewTextBoxColumn theColumnDeleteFlag = new DataGridViewTextBoxColumn();
                theColumnDeleteFlag.HeaderText = "DeleteFlag";
                theColumnDeleteFlag.DataPropertyName = "DeleteFlag";
                theColumnDeleteFlag.Width = 10;
                theColumnDeleteFlag.Visible = false;

                DataGridViewTextBoxColumn theColumnUserId = new DataGridViewTextBoxColumn();
                theColumnUserId.HeaderText = "UserId";
                theColumnUserId.DataPropertyName = "UserId";
                theColumnUserId.Width = 10;
                theColumnUserId.Visible = false;

                dgwQueryDetails.Columns.Add(theColumnId);
                dgwQueryDetails.Columns.Add(theColumnHomePageId);
                dgwQueryDetails.Columns.Add(theColumnIndicator);
                dgwQueryDetails.Columns.Add(theColumnQuery);
                dgwQueryDetails.Columns.Add(theColumnSeq);
                dgwQueryDetails.Columns.Add(theColumnDeleteFlag);
                dgwQueryDetails.Columns.Add(theColumnUserId);

                dgwQueryDetails.DataSource = theDT;
                dgwQueryDetails.Columns[0].Visible = false;
                dgwQueryDetails.Columns[1].Visible = false;
                dgwQueryDetails.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgwQueryDetails.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
    }
    private void btnCancel_Click(object sender, EventArgs e)
    {
            Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmHomePageList, IQCare.FormBuilder"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 2;
            this.Close();
            theForm.Show();
    }
    private void dgwQueryDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
            Image imgQuery;

                    if (e.ColumnIndex != -1 && e.RowIndex != -1)
                    {
                        if (dgwQueryDetails.Columns[e.ColumnIndex].Name == "Query")
                        {
                            dgwQueryDetails.Rows[e.RowIndex].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleCenter; 

                            if(Convert.ToString(dgwQueryDetails.Rows[e.RowIndex].Cells[3].Value)=="")
                              imgQuery = Image.FromFile(GblIQCare.GetPath() + "\\redrule.bmp"); 
                           
                            else 
                             imgQuery = Image.FromFile(GblIQCare.GetPath() + "\\brule.bmp");
                            
                            e.Value = imgQuery;
                        }
                   }
    }
    private void frmHomePage_Activated(object sender, EventArgs e)
    {
                if(bCellClick == true)
                   dgwQueryDetails.Rows[theGridRowNo].Cells[3].Value = GblIQCare.Query;
       }
    private void btnDown_Click(object sender, EventArgs e)
    {   
                    try
                    {
                    if(dgwQueryDetails.Rows.Count <= 1)
                    {
                        return;
                    }
                    else if (dgwQueryDetails.CurrentRow.Index >= 0)
                    {
                        Int32 iPos = dgwQueryDetails.CurrentRow.Index;
                        DataTable theDT = (DataTable)dgwQueryDetails.DataSource;
                        DataRow theDR = theDT.NewRow();
                        theDR.ItemArray = theDT.Rows[dgwQueryDetails.CurrentRow.Index].ItemArray;
                        theDT.Rows[dgwQueryDetails.CurrentRow.Index].Delete();
                        theDT.Rows.InsertAt(theDR, iPos + 2);
                        theDT.AcceptChanges(); 
                    }
                    }
                    catch (Exception err)
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["MessageText"] = err.Message.ToString();
                        IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                        return;
                     }  
        }
    private void btnRemove_Click(object sender, EventArgs e)
    { 
            try
            {
                int Row = 0;
                IHomePageConfiguration objTechArea = (IHomePageConfiguration)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BHomePageConfiguration,BusinessProcess.FormBuilder");
                if (dgwQueryDetails.Rows.Count <= 1)
                {
                    return;
                }
                else if (dgwQueryDetails.Rows[dgwQueryDetails.CurrentRow.Index].Cells[0].Value == DBNull.Value)
                {
                    dgwQueryDetails.Rows.Remove(dgwQueryDetails.CurrentRow);
                }
                else if (dgwQueryDetails.Rows[dgwQueryDetails.CurrentRow.Index].Cells[0].Value != DBNull.Value)
                {
                    foreach (DataGridViewRow r in dgwQueryDetails.Rows)
                    {
                        if (r.Cells[2].Selected == true)
                        {
                            string Indicator = Convert.ToString(r.Cells[2].Value);
                            int ID = Convert.ToInt32(r.Cells[0].Value);
                            if (ID != 0)
                            {
                                DialogResult dlgRes;
                                dlgRes = IQCareWindowMsgBox.ShowWindowConfirm("DeleteIndicator", this);
                                if (dlgRes == DialogResult.Yes)
                                { 
                                    int CheckIndicator = objTechArea.DeleteIndicator(ID, 2);
                                    if (CheckIndicator != 0)
                                    {
                                       Row = objTechArea.DeleteIndicator(ID, 1);
                                    }
                                    else
                                    {
                                        IQCareWindowMsgBox.ShowWindow("UnDeleteIndicator", this); 
                                    }
                                }
                                 frmHomePage_Load(sender, e);
                            }
                        }
                    }
                }
                else
                {
                    IQCareWindowMsgBox.ShowWindow("PMTCTNotDeletePredefine", this);
                }

                }

            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                return;
            }       
         
        }
    private void btnSave_Click(object sender, EventArgs e)
    {
         try
            {
                    if (Validation() == false)
                    {
                        return;
                    }
                    DataTable dtFeature;
                    dtFeature = clsCommon.CreateTableMstFeature();
                    DataRow theDRFeature;
                    int iFormId;
                    theDRFeature = dtFeature.NewRow();

                    string FName = cmbHomePageType.Text.Trim() + "-" + cmbTechnicalArea.Text.Trim();
                   
                    if(GblIQCare.iHomePageId != 0)
                    {
                        iFormId = GblIQCare.iHomePageId;
                        Flag = PMTCTConstants.strUpdate;
                    }
                    else
                    {
                        iFormId = 0;
                        Flag = PMTCTConstants.strInsert;

                    }
                    if (dgwQueryDetails.Rows.Count <= 1)
                    {
                        IQCareWindowMsgBox.ShowWindow("IndicatorMandatory", this);
                        return;
                    }
                    theDRFeature["FeatureId"] = iFormId;
                    theDRFeature["FeatureName"] = cmbHomePageType.Text + "-" + cmbTechnicalArea.Text;  
                    theDRFeature["UserId"] = GblIQCare.AppUserId;
                    theDRFeature["ModuleId"] = cmbTechnicalArea.SelectedValue;
                    dtFeature.Rows.Add(theDRFeature);

                    DataTable dtMstHomePage = new DataTable();
                    DataColumn theDT;
                      
                    theDT = new DataColumn();
                    theDT.DataType = Type.GetType("System.Int32");
                    theDT.ColumnName = "Id";
                    dtMstHomePage.Columns.Add(theDT);
                  
                    theDT = new DataColumn();
                    theDT.DataType = Type.GetType("System.String");
                    theDT.ColumnName = "Name";
                    dtMstHomePage.Columns.Add(theDT);

                    theDT = new DataColumn();
                    theDT.DataType = Type.GetType("System.Int32");
                    theDT.ColumnName = "FeatureId";
                    dtMstHomePage.Columns.Add(theDT);

                    theDT = new DataColumn();
                    theDT.DataType = Type.GetType("System.Int32");
                    theDT.ColumnName = "UserId";
                    dtMstHomePage.Columns.Add(theDT);

                    DataRow theDRHomePage;
                    
                    theDRHomePage = dtMstHomePage.NewRow();
                    if (GblIQCare.iHomePageId != 0)
                    {
                        theDRHomePage["Id"] = ID;
                    }
                   
                    theDRHomePage["Name"] = txtSectionTitle.Text;
                    theDRHomePage["FeatureId"] = iFormId;
                    theDRHomePage["UserId"] = GblIQCare.AppUserId;

                    dtMstHomePage.Rows.Add(theDRHomePage);
                    DataTable dtlHomePage;

                    dtlHomePage = (DataTable)dgwQueryDetails.DataSource;

                    DataTable dtDtHomePage;

                    dtDtHomePage = dtlHomePage.Copy();
                    DataSet dsSaveIndicatorQuery = new DataSet();
                    dsSaveIndicatorQuery.Tables.Add(dtDtHomePage);
                    dsSaveIndicatorQuery.Tables.Add(dtFeature);
                    dsSaveIndicatorQuery.Tables.Add(dtMstHomePage);
                    foreach (DataRow theDR in dtDtHomePage.Rows)
                       {
                        if (theDR["Indicator"].ToString() == "")
                           {
                             IQCareWindowMsgBox.ShowWindow("BlankIndicator", this);
                             return;
                            }
                        }
                    
                    IHomePageConfiguration objTechArea = (IHomePageConfiguration)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BHomePageConfiguration,BusinessProcess.FormBuilder");
                    int iSave = (int)objTechArea.SaveHomePageIndicator(dsSaveIndicatorQuery, Flag);
                    btnCancel_Click(sender, e);
                 
            }

        catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                return;
            }
            
          
        }
    private Boolean Validation()
        {
                    StringBuilder theErrMsg = new StringBuilder();
                    theErrMsg.Append("Please ");
                    DataSet dsSaveIndicatorQuery = new DataSet();
                    dsSaveIndicatorQuery.Tables.Clear();

                   if (txtSectionTitle.Text.Trim() == "")
                   {
                      theErrMsg.Append("Enter Section Title.");
                    }
                    else if(cmbHomePageType.SelectedIndex == 0)
                    {
                      theErrMsg.Append("Select Home Page Type.");
                    }
                    else if (cmbTechnicalArea.SelectedIndex == 0)
                    {
                      theErrMsg.Append("Select Service.");
                    }
                    if (theErrMsg.Length > 8)
                    {
                      MsgBuilder theBuilder = new MsgBuilder();
                      theBuilder.DataElements["MessageText"] = theErrMsg.ToString();
                      IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                      return false;
                    }

                    return true;
        }
    private void btnUp_Click(object sender, EventArgs e)
            {
                try
               {
                   if (dgwQueryDetails.Rows.Count <= 1)
                   {
                       return;
                   }
                   else if (dgwQueryDetails.CurrentRow.Index > 0)
                   {
                       Int32 iPos = dgwQueryDetails.CurrentRow.Index;
                       DataTable theDT = (DataTable)dgwQueryDetails.DataSource;
                       DataRow theDR = theDT.NewRow();
                       theDR.ItemArray = theDT.Rows[dgwQueryDetails.CurrentRow.Index].ItemArray;
                       theDT.Rows[dgwQueryDetails.CurrentRow.Index].Delete();
                       theDT.Rows.InsertAt(theDR, iPos - 1);
                       theDT.AcceptChanges();
                   }
               }
               catch (Exception err)
               {
                   MsgBuilder theBuilder = new MsgBuilder();
                   theBuilder.DataElements["MessageText"] = err.Message.ToString();
                   IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                   return;
               }  
            }
    private void dgwQueryDetails_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
                if (dgwQueryDetails.Rows[e.RowIndex].Cells[4].Value == DBNull.Value )
                   dgwQueryDetails.Rows[e.RowIndex].Cells[4].Value = dgwQueryDetails.Rows.Count - 1;
        }
    private void dgwQueryDetails_CellClick(object sender, DataGridViewCellEventArgs e)
         {  
            try
                    {
                       if (e.ColumnIndex != -1 && e.RowIndex > -1)
                        {
                           if (dgwQueryDetails.Columns[e.ColumnIndex].Index == 3)
                            {
                                bCellClick = true;
                                theGridRowNo = e.RowIndex;
                                GblIQCare.Query = dgwQueryDetails.Rows[theGridRowNo].Cells[3].Value.ToString();
                                
                                Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmQuery, IQCare.FormBuilder"));
                                if (cmbHomePageType.SelectedItem.ToString().Contains("Patient Home"))
                                {
                                    GblIQCare.blnIsPatientHomePage = true;
                                }
                                else
                                {
                                    GblIQCare.blnIsPatientHomePage = false;
                                }
                               
                                theForm.Left = 0;
                                theForm.Top = 0;
                                theForm.MdiParent = this.MdiParent;
                                theForm.Show();
                            }
                        else
                        {
                            bCellClick = false;
                            GblIQCare.Query = "";
                        }
                       
                    }    
                    }

                catch (Exception err)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = err.Message.ToString();
                    IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                    return;
                }
                
        }
    private void dgwQueryDetails_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = e.Exception.Message.ToString();
                IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                return;
    }

    }
    }
