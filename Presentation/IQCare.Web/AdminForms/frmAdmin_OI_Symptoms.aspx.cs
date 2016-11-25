using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;

namespace IQCare.Web.Admin
{
    public partial class OISymptoms : BasePage
    {
        private TreeNode ICDCode = new TreeNode();
        private TreeNode ICDSubBlockCode = new TreeNode();
        private TreeNode ICDBlockCode = new TreeNode();
        private TreeNode ICDChapterCode = new TreeNode();

        protected void Page_Load(object sender, EventArgs e)
        {
            HeadingDefination();
            SetValues();
            if (!IsPostBack)
            {
                if (Request.QueryString["SelectedId"] != null)
                {
                    GetData();
                    BindModuleList();
                    GetTreeViewData();
                    MsgBuilder theBuilder = new MsgBuilder();
                    if (Convert.ToInt32(Request.QueryString["Fid"]) == 31)
                    {
                        theBuilder.DataElements["Name"] = "Disease";
                        if (Convert.ToInt32(Request.QueryString["SelectedId"]) > 0)
                            theBuilder.DataElements["Name"] = "Disease";
                        IQCareMsgBox.ShowConfirm("CustomSaveRecord", theBuilder, btnSave);
                    }
                    else if (Convert.ToInt32(Request.QueryString["Fid"]) == 32)
                    {
                        theBuilder.DataElements["Name"] = "Symptom";
                        if (Convert.ToInt32(Request.QueryString["SelectedId"]) > 0)
                            theBuilder.DataElements["Name"] = "Symptom";
                        IQCareMsgBox.ShowConfirm("CustomSaveRecord", theBuilder, btnSave);
                    }
                }
                else
                {
                    BindList();
                    BindModuleList();
                    MsgBuilder theBuilder = new MsgBuilder();
                    if (Convert.ToInt32(Request.QueryString["Fid"]) == 31)
                    {
                        theBuilder.DataElements["Name"] = "Disease";
                        if (Convert.ToInt32(Request.QueryString["SelectedId"]) > 0)
                            theBuilder.DataElements["Name"] = "Disease";
                        IQCareMsgBox.ShowConfirm("CustomSaveRecord", theBuilder, btnSave);
                    }
                    else if (Convert.ToInt32(Request.QueryString["Fid"]) == 32)
                    {
                        theBuilder.DataElements["Name"] = "Symptom";
                        if (Convert.ToInt32(Request.QueryString["SelectedId"]) > 0)
                            theBuilder.DataElements["Name"] = "Symptom";
                        IQCareMsgBox.ShowConfirm("CustomSaveRecord", theBuilder, btnSave);
                    }
                }
            }
        }

        private void SetValues()
        {
            ViewState["TableName"] = Request.QueryString["TableName"].ToString();
            ViewState["CategoryId"] = Request.QueryString["CategoryId"].ToString();
            ViewState["ListName"] = Request.QueryString["LstName"].ToString();
            ViewState["FID"] = Request.QueryString["Fid"].ToString();
            ViewState["Update"] = Request.QueryString["Upd"].ToString();
            if (Request.QueryString["CCID"] != null)
            {
                ViewState["CCID"] = Request.QueryString["CCID"].ToString();
            }
            else { ViewState["CCID"] = "0"; }
            if (Request.QueryString["ModId"].ToString() != "")
            {
                ViewState["ModuleId"] = Convert.ToInt32(Request.QueryString["ModId"]);
            }
        }

        private void BindModuleList()
        {
            BindFunctions BindManager = new BindFunctions();
            IFacilitySetup FacilityManager = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");
            DataSet theDSFacility = FacilityManager.GetModuleName();
            DataTable DT = theDSFacility.Tables[0];
            BindManager.BindCheckedList(cblModuleName, DT, "modulename", "moduleid");
        }

        private void HeadingDefination()
        {
            if (Convert.ToInt32(Request.QueryString["Fid"]) == 31)
            {
                lblHeader.Text = "OI or AIDS Defining Illness";
                lblName.Text = "OI or AIDS Defining Illness";
            }
            else if (Convert.ToInt32(Request.QueryString["Fid"]) == 32)
            {
                lblHeader.Text = "Presenting Complaints";
                lblName.Text = "Presenting Complaints";
            }
        }

        private void GetData()
        {
            ICustomList CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
            DataTable theDT = CustomManager.GetCustomMasterDetails(Convert.ToString(Request.QueryString["TableName"]), Convert.ToInt32(Request.QueryString["SelectedId"]), Convert.ToInt32(Session["SystemId"]));
            txtName.Text = theDT.Rows[0]["Name"].ToString();
            txtSeqNo.Text = theDT.Rows[0]["SRNO"].ToString();

            if (theDT.Rows[0]["DeleteFlag"].ToString() != "")
            {
                if (Convert.ToBoolean(Convert.ToInt32(theDT.Rows[0]["DeleteFlag"])) == true)
                {
                    ddStatus.SelectedValue = "1";
                }
            }
            if (Convert.ToInt32(Request.QueryString["Fid"]) == 31)
            {
                ddType.SelectedValue = "1";
            }
            else if (Convert.ToInt32(Request.QueryString["Fid"]) == 32)
            {
                ddType.SelectedValue = "0";
            }
        }

        private Boolean FieldValidation()
        {
            DataTable dtretout = new DataTable();
            ICustomList CustomManager;
            CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList,BusinessProcess.Administration");
            if (txtName.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = lblName.Text;
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtName.Focus();
                return false;
            }
            if (txtSeqNo.Text.Trim() == "" && ddStatus.SelectedValue != "1")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Priority";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtSeqNo.Focus();
                return false;
            }
            if (txtSeqNo.Text == "0")
            {
                IQCareMsgBox.Show("ChkPriority", this);
                return false;
            }
            if (TVICD10.SelectedValue == "")
            {
                IQCareMsgBox.Show("SelectICDCode", this);
                return false;
            }
            

            return true;
        }

        private void GetlnkModuleData(int Id, int DiseaseFlag, DataTable theDT)
        {
            foreach (DataRow theDR in theDT.Rows)
            {
                for (int i = 0; i < cblModuleName.Items.Count; i++)
                {
                    if (Convert.ToString(cblModuleName.Items[i].Value) == Convert.ToString(theDR[2].ToString()))
                    {
                        cblModuleName.Items[i].Selected = true;
                    }
                }
            }
        }

        private void GetTreeViewData()
        {
            int Id = Convert.ToInt32(Request.QueryString["SelectedId"]);
            int DiseaseFlag = 0;
            if (Convert.ToString(Request.QueryString["TableName"]) == "HivDisease")
            {
                DiseaseFlag = 1;
            }
            else if (Convert.ToString(Request.QueryString["TableName"]) == "Symptom")
            {
                DiseaseFlag = 0;
            }
            ICustomList CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
            DataSet theDS = CustomManager.GetICDData(Id, DiseaseFlag);
            if (theDS.Tables[0].Rows.Count > 0)
            {
                if (theDS.Tables[0].Rows[0][0] != DBNull.Value && theDS.Tables[2].Rows[0][0] != DBNull.Value && theDS.Tables[3].Rows[0][0] != DBNull.Value)
                {
                    ViewState["ICDCodeId"] = theDS.Tables[0].Rows[0][0];
                    GetSelectedBindICDCodes(Convert.ToInt32(theDS.Tables[0].Rows[0][0]), 2, Convert.ToInt32(theDS.Tables[2].Rows[0][0]), Convert.ToInt32(theDS.Tables[3].Rows[0][0]));
                }
                else
                {
                    BindList();
                }
                if (theDS.Tables[4].Rows.Count > 0)
                {
                    if (theDS.Tables[4].Rows[0][0] != DBNull.Value)
                    {
                        GetlnkModuleData(Id, DiseaseFlag, theDS.Tables[4]);
                    }
                }
            }
        }

        private void GetSelectedBindICDCodes(int SelectedValue, int Current_level, int BlockId, int ChapterId)
        {
            try
            {
                int level_0 = 0;
                int level_1 = 1;
                ICustomList CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
                DataSet theDSList = CustomManager.GetICDList();
                foreach (DataRow theDR1 in theDSList.Tables[0].Rows)
                {
                    ICDChapterCode = new TreeNode();
                    ICDChapterCode.Text = Convert.ToString(theDR1["ChapterName"]);
                    ICDChapterCode.Value = Convert.ToString(theDR1["ChapterCode"]);
                    foreach (DataRow theDR2 in theDSList.Tables[1].Rows)
                    {
                        if (Convert.ToInt32(theDR1["ChapterId"]) == Convert.ToInt32(theDR2["ChapterId"]) && ChapterId == Convert.ToInt32(theDR2["ChapterId"]) && level_0 == 0)
                        {
                            ICDBlockCode = new TreeNode();
                            ICDBlockCode.Text = theDR2["BlockCode"] + " " + theDR2["BlockName"];
                            ICDBlockCode.Value = theDR2["BlockId"].ToString();
                            foreach (DataRow theDR3 in theDSList.Tables[2].Rows)
                            {
                                if (Convert.ToInt32(ICDBlockCode.Value) == Convert.ToInt32(theDR3["BlockId"]) && BlockId == Convert.ToInt32(ICDBlockCode.Value) && level_1 == 1)
                                {
                                    ICDSubBlockCode = new TreeNode();
                                    ICDSubBlockCode.Text = theDR3["SubBlockCode"] + " " + theDR3["SubBlockName"];
                                    ICDSubBlockCode.Value = Convert.ToString(theDR3["SubBlockId"]);
                                    foreach (DataRow theDR4 in theDSList.Tables[3].Rows)
                                    {
                                        ICDCode = new TreeNode();
                                        ICDCode.Text = theDR4["ICDCode"] + " " + theDR4["ICDCodeName"];
                                        ICDCode.Value = Convert.ToString(theDR4["ID"]);
                                        if (Convert.ToInt32(ICDSubBlockCode.Value) == Convert.ToInt32(theDR4["SubBlockId"]) && SelectedValue == Convert.ToInt32(ICDCode.Value) && Current_level == 2)
                                        {
                                            ICDCode.Selected = true;
                                            ICDSubBlockCode.ChildNodes.Add(ICDCode);
                                        }
                                    }
                                    ICDBlockCode.ChildNodes.Add(ICDSubBlockCode);
                                }
                            }
                            ICDChapterCode.ChildNodes.Add(ICDBlockCode);
                        }
                    }
                    TVICD10.Nodes.Add(ICDChapterCode);
                }
                TVICD10.SelectedNodeStyle.Font.Underline = true;
                TVICD10.SelectedNodeStyle.ForeColor = System.Drawing.Color.Red;
                TVICD10.ShowLines = true;
                TVICD10.NodeIndent = 5;
                TVICD10.ExpandAll();
            }
            catch { }
            finally { }
        }

        private void SelectedBindICDCodes(int SelectedValue, int Current_level, int BlockId, int ChapterId)
        {
            try
            {
                int level_0 = 0;
                int level_1 = 1;
                ICustomList CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
                DataSet theDSList = CustomManager.GetICDList();
                foreach (DataRow theDR1 in theDSList.Tables[0].Rows)
                {
                    ICDChapterCode = new TreeNode();
                    ICDChapterCode.Text = Convert.ToString(theDR1["ChapterName"]);
                    ICDChapterCode.Value = Convert.ToString(theDR1["ChapterCode"]);
                    foreach (DataRow theDR2 in theDSList.Tables[1].Rows)
                    {
                        if (Convert.ToInt32(theDR1["ChapterId"]) == Convert.ToInt32(theDR2["ChapterId"]) && ChapterId == Convert.ToInt32(theDR2["ChapterId"]) && level_0 == 0)
                        {
                            ICDBlockCode = new TreeNode();
                            ICDBlockCode.Text = theDR2["BlockCode"] + " " + theDR2["BlockName"];
                            ICDBlockCode.Value = theDR2["BlockId"].ToString();
                            foreach (DataRow theDR3 in theDSList.Tables[2].Rows)
                            {
                                if (Convert.ToInt32(ICDBlockCode.Value) == Convert.ToInt32(theDR3["BlockId"]) && BlockId == Convert.ToInt32(ICDBlockCode.Value) && level_1 == 1)
                                {
                                    ICDSubBlockCode = new TreeNode();
                                    ICDSubBlockCode.Text = theDR3["SubBlockCode"] + " " + theDR3["SubBlockName"];
                                    ICDSubBlockCode.Value = Convert.ToString(theDR3["SubBlockId"]);
                                    foreach (DataRow theDR4 in theDSList.Tables[3].Rows)
                                    {
                                        if (Convert.ToInt32(ICDSubBlockCode.Value) == Convert.ToInt32(theDR4["SubBlockId"]) && SelectedValue == Convert.ToInt32(ICDSubBlockCode.Value) && Current_level == 2)
                                        {
                                            ICDCode = new TreeNode();
                                            ICDCode.Text = theDR4["ICDCode"] + " " + theDR4["ICDCodeName"];
                                            ICDCode.Value = Convert.ToString(theDR4["ID"]);
                                            ICDSubBlockCode.ChildNodes.Add(ICDCode);
                                        }
                                    }
                                    ICDBlockCode.ChildNodes.Add(ICDSubBlockCode);
                                }
                            }
                            ICDChapterCode.ChildNodes.Add(ICDBlockCode);
                        }
                    }
                    TVICD10.Nodes.Add(ICDChapterCode);
                }
                TVICD10.ShowLines = true;
                TVICD10.NodeIndent = 5;
                TVICD10.ExpandAll();
            }
            catch { }
            finally { }

           
        }

        private void SelectedBindSubBlock(int SelectedValue, int Current_level, int ChapterId)
        {
            try
            {
                int level_0 = 0;
                ICustomList CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
                DataSet theDSList = CustomManager.GetICDList();
                foreach (DataRow theDR1 in theDSList.Tables[0].Rows)
                {
                    ICDChapterCode = new TreeNode();
                    ICDChapterCode.Text = Convert.ToString(theDR1["ChapterName"]);
                    ICDChapterCode.Value = Convert.ToString(theDR1["ChapterCode"]);
                    foreach (DataRow theDR2 in theDSList.Tables[1].Rows)
                    {
                        if (Convert.ToInt32(theDR1["ChapterId"]) == Convert.ToInt32(theDR2["ChapterId"]) && ChapterId == Convert.ToInt32(theDR2["ChapterId"]) && level_0 == 0)
                        {
                            ICDBlockCode = new TreeNode();
                            ICDBlockCode.Text = theDR2["BlockCode"] + " " + theDR2["BlockName"];
                            ICDBlockCode.Value = theDR2["BlockId"].ToString();
                            foreach (DataRow theDR3 in theDSList.Tables[2].Rows)
                            {
                                if (Convert.ToInt32(ICDBlockCode.Value) == Convert.ToInt32(theDR3["BlockId"]) && SelectedValue == Convert.ToInt32(ICDBlockCode.Value) && Current_level == 1)
                                {
                                    ICDSubBlockCode = new TreeNode();
                                    ICDSubBlockCode.Text = theDR3["SubBlockCode"] + " " + theDR3["SubBlockName"];
                                    ICDSubBlockCode.Value = Convert.ToString(theDR3["SubBlockId"]);
                                    ICDBlockCode.ChildNodes.Add(ICDSubBlockCode);
                                }
                            }
                            ICDChapterCode.ChildNodes.Add(ICDBlockCode);
                            ICDChapterCode.Expand();
                        }
                    }
                    TVICD10.Nodes.Add(ICDChapterCode);
                }
                TVICD10.ShowLines = true;
                TVICD10.NodeIndent = 5;
                TVICD10.ExpandAll();
            }
            catch { }
            finally { }

           
        }

        private void SelectedBindBlock(int depth, int level)
        {
            try
            {
                ICustomList CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
                DataSet theDSList = CustomManager.GetICDList();
                foreach (DataRow theDR1 in theDSList.Tables[0].Rows)
                {
                    ICDChapterCode = new TreeNode();
                    ICDChapterCode.Text = Convert.ToString(theDR1["ChapterName"]);
                    ICDChapterCode.Value = Convert.ToString(theDR1["ChapterCode"]);
                    foreach (DataRow theDR2 in theDSList.Tables[1].Rows)
                    {
                        if (depth == Convert.ToInt32(theDR1["ChapterId"]))
                        {
                            if (Convert.ToInt32(theDR1["ChapterId"]) == Convert.ToInt32(theDR2["ChapterId"]) && level == 0)
                            {
                                ICDBlockCode = new TreeNode();
                                ICDBlockCode.Text = theDR2["BlockCode"] + " " + theDR2["BlockName"];
                                ICDBlockCode.Value = theDR2["BlockId"].ToString();
                                ICDChapterCode.ChildNodes.Add(ICDBlockCode);
                            }
                            ICDChapterCode.Expand();
                        }
                    }
                    TVICD10.Nodes.Add(ICDChapterCode);
                }
                TVICD10.ShowLines = true;
                TVICD10.NodeIndent = 5;
                TVICD10.ExpandAll();
            }
            catch { }
            finally { }
        }

        private void BindList()
        {
            try
            {
                ICustomList CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
                DataSet theDSList = CustomManager.GetICDList();
                foreach (DataRow theDR1 in theDSList.Tables[0].Rows)
                {
                    ICDChapterCode = new TreeNode();
                    ICDChapterCode.Text = Convert.ToString(theDR1["ChapterName"]);
                    ICDChapterCode.Value = Convert.ToString(theDR1["ChapterCode"]);
                   
                    TVICD10.Nodes.Add(ICDChapterCode);
                }
                TVICD10.ShowLines = true;
                TVICD10.NodeIndent = 5;
                TVICD10.CollapseAll();
            }
            catch { }
            finally { }
        }

        private Hashtable Disease_SymptomDetails(string Id, int DiseaseFlag)
        {
            Hashtable Disease_SymptomDetails = new Hashtable();
            Disease_SymptomDetails.Add("TableName", Request.QueryString["TableName"]);
            Disease_SymptomDetails.Add("ListName", Request.QueryString["LstName"]);
            Disease_SymptomDetails.Add("Name", txtName.Text);
            Disease_SymptomDetails.Add("Code", Request.QueryString["Fid"]);
            Disease_SymptomDetails.Add("Stage", "");
            if (Id != null && DiseaseFlag == 0 && Convert.ToString(TVICD10.SelectedValue) != Convert.ToString(ViewState["ICDCodeId"]))
            {
                Disease_SymptomDetails.Add("Validate", "1");
                Disease_SymptomDetails.Add("ICDCode", TVICD10.SelectedValue);
            }
            else if (Id != null && DiseaseFlag == 1 && Convert.ToString(TVICD10.SelectedValue) != Convert.ToString(ViewState["ICDCodeId"]))
            {
                Disease_SymptomDetails.Add("Validate", "1");
                Disease_SymptomDetails.Add("ICDCode", TVICD10.SelectedValue);
            }
            else
            {
                Disease_SymptomDetails.Add("Validate", "0");
                Disease_SymptomDetails.Add("ICDCode", TVICD10.SelectedValue);
            }
            Disease_SymptomDetails.Add("Status", ddStatus.SelectedValue);
            Disease_SymptomDetails.Add("Sequence", txtSeqNo.Text);
            Disease_SymptomDetails.Add("Category", Request.QueryString["CategoryId"]);
            Disease_SymptomDetails.Add("UserId", Session["AppUserId"]);
            Disease_SymptomDetails.Add("SystemId", Session["SystemId"]);
            Disease_SymptomDetails.Add("CountryID", Request.QueryString["CCID"]);
            return Disease_SymptomDetails;
        }

        protected void TVICD10_SelectedNodeChanged(object sender, EventArgs e)
        {
            object SelectedValue = ((TreeView)sender).SelectedValue;
            if (((TreeView)sender).SelectedNode.Depth == 0)
            {
                TVICD10.Nodes.Clear();
                SelectedBindBlock(Convert.ToInt32(SelectedValue), 0);
                TVICD10.SelectedNodeStyle.Font.Underline = true;
            }
            else if (((TreeView)sender).SelectedNode.Depth == 1)
            {
                ICustomList CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
                DataSet theDSList = CustomManager.GetICDList();
                DataView theDV = new DataView(theDSList.Tables[1]);
                theDV.RowFilter = "BlockId=" + SelectedValue.ToString();
                DataTable theDT = theDV.ToTable();
                TVICD10.Nodes.Clear();
                SelectedBindSubBlock(Convert.ToInt32(SelectedValue), 1, Convert.ToInt32(theDT.Rows[0][1]));
                TVICD10.SelectedNodeStyle.Font.Underline = true;
            }
            else if (((TreeView)sender).SelectedNode.Depth == 2)
            {
                ICustomList CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
                DataSet theDSList = CustomManager.GetICDList();
                DataView theDV = new DataView(theDSList.Tables[2]);
                theDV.RowFilter = "SubBlockId=" + SelectedValue.ToString();
                DataTable theDT = theDV.ToTable();
                theDV = new DataView(theDSList.Tables[1]);
                theDV.RowFilter = "BlockId=" + Convert.ToString(theDT.Rows[0][0]);
                DataTable theDT1 = theDV.ToTable();
                TVICD10.Nodes.Clear();
                SelectedBindICDCodes(Convert.ToInt32(SelectedValue), 2, Convert.ToInt32(theDT.Rows[0][0]), Convert.ToInt32(theDT1.Rows[0][1]));
                TVICD10.SelectedNodeStyle.Font.Underline = true;
                TVICD10.SelectedNodeStyle.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected ArrayList ICDCodeforModule()
        {
            ArrayList ICDCodeforModule = new ArrayList();
            for (int i = 0; i < cblModuleName.Items.Count; i++)
            {
                if (cblModuleName.Items[i].Selected)
                {
                    ICDCodeforModule.Add(cblModuleName.Items[i].Value);
                }
            }
            return ICDCodeforModule;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (FieldValidation() == false)
            {
                return;
            }
            String Id = Convert.ToString(Request.QueryString["SelectedId"]);
            int DiseaseFlag = 0;
            if (Convert.ToString(Request.QueryString["TableName"]) == "HivDisease")
            {
                DiseaseFlag = 1;
            }
            else if (Convert.ToString(Request.QueryString["TableName"]) == "Symptom")
            {
                DiseaseFlag = 0;
            }
            Hashtable theHT = Disease_SymptomDetails(Id, DiseaseFlag);
            ArrayList theAL = ICDCodeforModule();
            ICustomList CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
            if (Id == null)
            {
                DataTable RowsAffected = CustomManager.SaveICDCodeRecord(theHT, theAL);
                string Url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&CCID={6}&ModId={7}", "frmAdmin_CustomList.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString(), ViewState["CCID"].ToString(), ViewState["ModuleId"].ToString());
                Response.Redirect(Url);
            }
            else
            {
                int RowsAffected = CustomManager.UpdateICDCodeRecord(Id, theHT, theAL);
                string Url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&CCID={6}&ModId={7}", "frmAdmin_CustomList.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString(), ViewState["CCID"].ToString(), ViewState["ModuleId"].ToString());
                Response.Redirect(Url);
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            String Id = Convert.ToString(Request.QueryString["SelectedId"]);
            if (Id == null)
            {
                string Url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&CCID={6}&ModId={7}", "frmAdmin_CustomList.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString(), ViewState["CCID"].ToString(), ViewState["ModuleId"].ToString());
                Response.Redirect(Url);
            }
            else
            {
                string Url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&CCID={6}&ModId={7}", "frmAdmin_CustomList.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString(), ViewState["CCID"].ToString(), ViewState["ModuleId"].ToString());
                Response.Redirect(Url);
            }
        }
    }
}