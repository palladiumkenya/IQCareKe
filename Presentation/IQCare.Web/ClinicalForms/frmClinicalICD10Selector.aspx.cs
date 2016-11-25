using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using Application.Common;
using Application.Presentation;
using Interface.Administration;
using Interface.Security;
namespace IQCare.Web.Clinical
{


    public partial class ICD10Selector : System.Web.UI.Page
    {
        TreeNode ICDCode10 = new TreeNode();
        TreeNode ICDSubBlockCode = new TreeNode();
        TreeNode ICDBlockCode = new TreeNode();
        TreeNode ICDChapterCode = new TreeNode();
        public DataTable DTICD10;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindList();
                hdnvalue.Value = Request.QueryString["Param"];
               
                lstSelectedICD10.DataSource = ((DataTable)Session["SelectedICD" + Convert.ToInt32(hdnvalue.Value) + ""]);
                lstSelectedICD10.DataTextField = "Name";
                lstSelectedICD10.DataBind();
                //}
            }


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
                //CollapseAll();
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
                                ICDBlockCode.Value = theDR2["BlockId"] + "^" + theDR2["BlockCode"] + " " + theDR2["BlockName"];//theDR2["BlockId"].ToString();
                                ICDChapterCode.ChildNodes.Add(ICDBlockCode);
                            }
                            ICDChapterCode.Expand();
                        }
                    }
                    TVICD10.Nodes.Add(ICDChapterCode);
                }
                TVICD10.ShowLines = true;
                TVICD10.NodeIndent = 5;
                TVICD10.SelectedNodeStyle.Font.Underline = true;
                TVICD10.SelectedNodeStyle.ForeColor = System.Drawing.Color.Red;
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
                                    ICDSubBlockCode.Value = theDR3["SubBlockId"] + "^" + theDR3["SubBlockCode"] + " " + theDR3["SubBlockName"];// Convert.ToString(theDR3["SubBlockId"]);
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
                TVICD10.SelectedNodeStyle.Font.Underline = true;
                TVICD10.SelectedNodeStyle.ForeColor = System.Drawing.Color.Red;
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
                                            ICDCode10 = new TreeNode();
                                            ICDCode10.Text = theDR4["ICDCode"] + " " + theDR4["ICDCodeName"];
                                            ICDCode10.Value = theDR4["ID"] + "^" + theDR4["ICDCode"] + " " + theDR4["ICDCodeName"];// Convert.ToString(theDR4["ID"] + "-" + theDR4["ICDCode"] + " " + theDR4["ICDCodeName"]);
                                            ICDSubBlockCode.ChildNodes.Add(ICDCode10);

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
                TVICD10.SelectedNodeStyle.Font.Underline = true;
                TVICD10.SelectedNodeStyle.ForeColor = System.Drawing.Color.Red;
                TVICD10.ExpandAll();
            }
            catch { }

            finally { }
        }
        protected void TVICD10_SelectedNodeChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;
            Object SelectedValue = ((TreeView)sender).SelectedValue;
            if (((TreeView)sender).SelectedNode.Depth == 0)
            {
                TVICD10.Nodes.Clear();
                SelectedBindBlock(Convert.ToInt32(SelectedValue), 0);
                //TVICD10.SelectedNodeStyle.Font.Underline = true;
            }
            else if (((TreeView)sender).SelectedNode.Depth == 1)
            {
                ICustomList CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
                DataSet theDSList = CustomManager.GetICDList();
                DataView theDV = new DataView(theDSList.Tables[1]);
                String[] Id = SelectedValue.ToString().Split('^');
                txtvalue.Text = SelectedValue.ToString() + "^" + ((TreeView)sender).SelectedNode.Depth;
                theDV.RowFilter = "BlockId=" + Id[0].ToString();
                DataTable theDT = theDV.ToTable();
                TVICD10.Nodes.Clear();
                //txtvalue.Text = theDT.Rows[0]["BlockId"] + "^" + theDT.Rows[0]["BlockCode"] + " " + theDT.Rows[0]["BlockName"] + "^" + "1";
                SelectedBindSubBlock(Convert.ToInt32(Id[0]), 1, Convert.ToInt32(theDT.Rows[0][1]));
                txtvalue.Text = theDT.Rows[0]["BlockId"] + "^" + theDT.Rows[0]["BlockCode"] + " " + theDT.Rows[0]["BlockName"] + "^" + "1";
                //TVICD10.SelectedNodeStyle.Font.Underline = true;
            }
            else if (((TreeView)sender).SelectedNode.Depth == 2)
            {
                ICustomList CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
                DataSet theDSList = CustomManager.GetICDList();
                DataView theDV = new DataView(theDSList.Tables[2]);
                String[] Id = SelectedValue.ToString().Split('^');
                theDV.RowFilter = "SubBlockId=" + Id[0].ToString();
                DataTable theDT = theDV.ToTable();
                theDV = new DataView(theDSList.Tables[1]);
                theDV.RowFilter = "BlockId=" + Convert.ToString(theDT.Rows[0][0]);
                DataTable theDT1 = theDV.ToTable();
                TVICD10.Nodes.Clear();
                SelectedBindICDCodes(Convert.ToInt32(Id[0]), 2, Convert.ToInt32(theDT.Rows[0][0]), Convert.ToInt32(theDT1.Rows[0][1]));
                txtvalue.Text = theDT.Rows[0]["SubBlockId"] + "^" + theDT.Rows[0]["SubBlockCode"] + " " + theDT.Rows[0]["SubBlockName"] + "^" + "2";

                //txtvalue.Text = theDT.Rows[0]["SubBlockId"] + "^" + theDT.Rows[0]["SubBlockCode"] + " " + theDT.Rows[0]["SubBlockName"] + "^"+"2";
                //TVICD10.SelectedNodeStyle.Font.Underline = true;
                //TVICD10.SelectedNodeStyle.ForeColor = System.Drawing.Color.Red;
            }
            else if (((TreeView)sender).SelectedNode.Depth == 3)
            {
                txtvalue.Text = SelectedValue.ToString() + "^" + ((TreeView)sender).SelectedNode.Depth;
                //TVICD10.SelectedNodeStyle.Font.Underline = true;
                //TVICD10.SelectedNodeStyle.ForeColor = System.Drawing.Color.Red;
            }
            if (txtvalue.Text != "")
            {
                btnAdd.Enabled = true;
            }
        }
        private DataTable CreateColumntheDTICD10()
        {

            DataTable theDTICD10 = new DataTable();
            theDTICD10.Columns.Add("FieldId", typeof(int));
            theDTICD10.Columns.Add("BlockId", typeof(int));
            theDTICD10.Columns.Add("SubBlockId", typeof(int));
            theDTICD10.Columns.Add("Id", typeof(int));
            theDTICD10.Columns.Add("CodeId", typeof(string));
            theDTICD10.Columns.Add("Name", typeof(string));
            return theDTICD10;
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string[] Name = Convert.ToString(txtvalue.Text).Replace("'", " ").Split('^');
            if (Name[0] != "")
            {
                if (((DataTable)Session["SelectedICD" + Convert.ToInt32(hdnvalue.Value) + ""]).Rows.Count > 0)
                {
                    DataRow[] row = ((DataTable)Session["SelectedICD" + Convert.ToInt32(hdnvalue.Value) + ""]).Select("Name='" + Name[1] + "'");
                    if (row.Length > 0)
                    {
                        IQCareMsgBox.Show("DuplicateICD10", this);
                        return;
                    }


                }
                int number = Convert.ToInt32(Name[2]);
                switch (number)
                {
                    case 1:
                        ((DataTable)Session["SelectedICD" + Convert.ToInt32(hdnvalue.Value) + ""]).Rows.Add(Convert.ToInt32(hdnvalue.Value), Name[0], null, null, "%" + Name[0] + "%" + 0 + "%" + 0 + "%" + 0 + "", Name[1]);
                        ((DataTable)Session["SelectedICD" + Convert.ToInt32(hdnvalue.Value) + ""]).AcceptChanges();
                        break;
                    case 2:
                        ((DataTable)Session["SelectedICD" + Convert.ToInt32(hdnvalue.Value) + ""]).Rows.Add(Convert.ToInt32(hdnvalue.Value), null, Name[0], null, "%" + 0 + "%" + Name[0] + "%" + 0 + "%" + 0 + "", Name[1]);
                        ((DataTable)Session["SelectedICD" + Convert.ToInt32(hdnvalue.Value) + ""]).AcceptChanges();
                        break;
                    case 3:
                        ((DataTable)Session["SelectedICD" + Convert.ToInt32(hdnvalue.Value) + ""]).Rows.Add(Convert.ToInt32(hdnvalue.Value), null, null, Name[0], "%" + 0 + "%" + 0 + "%" + Name[0] + "%" + 0 + "", Name[1]);
                        ((DataTable)Session["SelectedICD" + Convert.ToInt32(hdnvalue.Value) + ""]).AcceptChanges();
                        break;
                }
                lstSelectedICD10.DataSource = ((DataTable)Session["SelectedICD" + Convert.ToInt32(hdnvalue.Value) + ""]);
                lstSelectedICD10.DataTextField = "Name";
                lstSelectedICD10.DataBind();
            }
            txtvalue.Text = "";
            btnAdd.Enabled = false;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "btnSubmit_Click", "<script>window.opener.GetControl();closeMe();</script>");
        }
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            DataRow[] rows = ((DataTable)Session["SelectedICD" + Convert.ToInt32(hdnvalue.Value) + ""]).Select("Name='" + lstSelectedICD10.SelectedItem + "'");
            ((DataTable)Session["SelectedICD" + Convert.ToInt32(hdnvalue.Value) + ""]).Rows.Remove(rows[0]);
            ((DataTable)Session["SelectedICD" + Convert.ToInt32(hdnvalue.Value) + ""]).AcceptChanges();
            lstSelectedICD10.DataSource = ((DataTable)Session["SelectedICD" + Convert.ToInt32(hdnvalue.Value) + ""]);
            lstSelectedICD10.DataTextField = "Name";
            lstSelectedICD10.DataBind();
        }
    }
}