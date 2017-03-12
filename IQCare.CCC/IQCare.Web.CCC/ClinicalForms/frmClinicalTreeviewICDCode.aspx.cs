using System;
using System.Data;
using System.Configuration;
using System.Collections;
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



    public partial class TreeviewICDCode : System.Web.UI.Page
    {

        TreeNode ICDCode10 = new TreeNode();
        TreeNode ICDSubBlockCode = new TreeNode();
        TreeNode ICDBlockCode = new TreeNode();
        TreeNode ICDChapterCode = new TreeNode();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Array a = Request.QueryString["param"].Replace('"', ' ').Split('%');
                Hpother.Value = ((string[])(a))[0];
                HpNodeId.Value = ((string[])(a))[1];
                string value = Convert.ToString(Request.QueryString["ParamValue"]);
                if (value != "")
                {
                    GetBindList(value);
                }
                else
                {
                    BindList();
                }
            }
        }
        private void GetBindList(string value)
        {

            try
            {
                ICustomList CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
                DataSet theDSList = CustomManager.GetICDList();
                DataView theDV = new DataView(theDSList.Tables[3]);
                theDV.RowFilter = "CodeName IN( '" + value + "')";
                theDV.ToTable();

                DataSet theDS = CustomManager.GetICDData(Convert.ToInt32(theDV.ToTable().Rows[0][0]), 2);
                if (theDS.Tables[0].Rows.Count > 0)
                {
                    if (theDS.Tables[0].Rows[0][0] != DBNull.Value && theDS.Tables[2].Rows[0][0] != DBNull.Value && theDS.Tables[3].Rows[0][0] != DBNull.Value)
                    {
                        ViewState["ICDCodeId"] = theDS.Tables[0].Rows[0][0];
                        GetSelectedBindICDCodes(Convert.ToInt32(theDS.Tables[0].Rows[0][0]), 2, Convert.ToInt32(theDS.Tables[2].Rows[0][0]), Convert.ToInt32(theDS.Tables[3].Rows[0][0]));
                    }
                }

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
                //TVICD10.CollapseAll();

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
                                            ICDCode10.Value = Convert.ToString(theDR4["ID"] + "-" + theDR4["ICDCode"] + " " + theDR4["ICDCodeName"]);
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
                                        ICDCode10 = new TreeNode();
                                        ICDCode10.Text = theDR4["ICDCode"] + " " + theDR4["ICDCodeName"];
                                        ICDCode10.Value = Convert.ToString(theDR4["ID"] + "-" + theDR4["ICDCode"] + " " + theDR4["ICDCodeName"]);
                                        //ICDCode10.Value = Convert.ToString(theDR4["ID"]);
                                        if (Convert.ToInt32(ICDSubBlockCode.Value) == Convert.ToInt32(theDR4["SubBlockId"]) && SelectedValue == Convert.ToInt32(theDR4["ID"]) && Current_level == 2)
                                        {
                                            ICDCode10.Selected = true;
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
                TVICD10.SelectedNodeStyle.Font.Underline = true;
                TVICD10.SelectedNodeStyle.ForeColor = System.Drawing.Color.Red;
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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //HpNodeValue.Value = TVICD10.SelectedNode.Text;
        }
    }
}