using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;
using Interface.Administration;

namespace IQCare.Web.Admin
{
    public partial class PMTCTCustomItems : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["PatientId"] = 0;

                BindGrid();
            }
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Customize Lists";
        }

        private void BindGrid()
        {
            string filePath = Server.MapPath("~/XMLFiles/customizelist.xml");
            DataSet dscustomiseList = new DataSet();
            dscustomiseList.ReadXml(filePath);

            DataView theDV = new DataView(dscustomiseList.Tables[0]);
            DataTable theXMLDT = new DataTable();
            IFacilitySetup FacilityManager = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");
            DataSet theDSFacility = FacilityManager.GetModuleName();
            DataTable theDTPubModule = (DataTable)Session["AppModule"];
            DataTable theDTModule = theDSFacility.Tables[1];

            string theModList = "";
            foreach (DataRow theDR in theDTModule.Rows)
            {
                if (theModList == "")
                    theModList = theDR["ModuleId"].ToString();
                else
                    theModList = theModList + "," + theDR["ModuleId"].ToString();
            }
            IQCareUtils theUtil = new IQCareUtils();
            theDV.RowFilter = "ModuleId in (0," + theModList + ")";
            if (theDV.Count > 0)
            {
                theDV.RowFilter = "(SystemId=" + Session["SystemId"].ToString() + " or SystemId=0)";
                theXMLDT = theUtil.CreateTableFromDataView(theDV);
            }
            ICustomList CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
            DataTable theDT = CustomManager.GetCustomFieldList(Convert.ToInt32(Session["SystemId"]), Convert.ToInt32(Session["AppLocationId"]));
            if (theDT != null && theDT.Rows.Count > 0)
            {
                theXMLDT.Merge(theDT);
            }

            #region "ParentNode"

            TreeNode root = new TreeNode();
            TreeNode theChildRoot;
            TreeNode theMRoot;
            tvcustomlist.LeafNodeStyle.Font.Underline = true;
            tvcustomlist.LeafNodeStyle.ForeColor = System.Drawing.Color.Blue;
            bool flagroot = true;
            string stralpha = string.Empty;
            AuthenticationManager Authentication = new AuthenticationManager();
            root.Text = "List";
            if (flagroot)
            {
                root.Expand();
                flagroot = false;
            }
            else
            {
                root.Collapse();
            }
            tvcustomlist.Nodes.Add(root);

            #endregion "ParentNode"

            #region "Universal Node"

            theMRoot = new TreeNode();
            theMRoot.Text = "Universal";
            theMRoot.Value = "0";
            theMRoot.Target = "_blank";
            theMRoot.Expand();
            root.ChildNodes.Add(theMRoot);

            theDV = new DataView(theXMLDT);
            theDV.RowFilter = "ModuleId = 0";
            theDV.Sort = "ListName asc";
            DataTable objTable = theDV.ToTable();
            foreach (DataRow theUniDR in objTable.Rows)
            {
                string url = string.Empty;
                theChildRoot = new TreeNode();
                theChildRoot.Text = theUniDR["ListName"].ToString();

                theChildRoot.Target = "";
                if ((theUniDR["FeatureID"].ToString() != "0") && (Authentication.HasFeatureRight(Convert.ToInt32(theUniDR["FeatureID"].ToString()), theXMLDT) == false))
                {
                    theChildRoot.ImageUrl = "~/Images/lock.jpg";
                    theChildRoot.ImageToolTip = "You are Not Authorized to Access this Functionality";
                    theChildRoot.SelectAction = TreeNodeSelectAction.None;
                }
                else
                {
                    //url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&CCID={6}", theUniDR["FormName"].ToString(), theUniDR["TableName"].ToString(), theUniDR["CategoryId"].ToString(), theUniDR["ListName"].ToString(), theUniDR["FeatureID"].ToString(), theUniDR["Update"].ToString(), theUniDR["CountryID"].ToString());
                    url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&CCID={6}&ModId={7}", theUniDR["FormName"].ToString(), theUniDR["TableName"].ToString(), theUniDR["CategoryId"].ToString(), theUniDR["ListName"].ToString(), theUniDR["FeatureID"].ToString(), theUniDR["Update"].ToString(), theUniDR["CountryID"].ToString(), "0");
                    theChildRoot.NavigateUrl = url;
                }
                theMRoot.ChildNodes.Add(theChildRoot);
            }

            #endregion "Universal Node"

            #region "Module Nodes"

            //////////////////////////////////////////////////////////////////////
            string thePubModList = "";
            DataTable objPubTable;
            foreach (DataRow thepubDR in theDTPubModule.Rows)
            {
                if (thePubModList == "")
                    thePubModList = thepubDR["ModuleId"].ToString();
                else
                    thePubModList = thePubModList + "," + thepubDR["ModuleId"].ToString();
            }
            DataView thepubDV = new DataView(theXMLDT);
            thepubDV.RowFilter = "ModuleId in(" + thePubModList.ToString() + ")";
            thepubDV.Sort = "ListName asc";
            objPubTable = thepubDV.ToTable();
            ////////////////////////////////////////////////////////////
            foreach (DataRow theModRow in theDTModule.Rows)
            {
                theDV = new DataView(theXMLDT);
                theDV.RowFilter = "ModuleId =" + theModRow["ModuleId"].ToString();
                theDV.Sort = "ListName asc";
                objTable = theDV.ToTable();

                if (objTable.Rows.Count > 0)
                {
                    theMRoot = new TreeNode();
                    theMRoot.Text = theModRow["ModuleName"].ToString();
                    theMRoot.Value = theModRow["ModuleId"].ToString();
                    theMRoot.Target = "_blank";
                    root.ChildNodes.Add(theMRoot);

                    foreach (DataRow theUniDR in objTable.Rows)
                    {
                        string url = string.Empty;
                        theChildRoot = new TreeNode();
                        theChildRoot.Text = theUniDR["ListName"].ToString();

                        theChildRoot.Target = "";

                        if ((theUniDR["ModuleID"].ToString() != "0") && (Authentication.HasModuleRight(Convert.ToInt32(theUniDR["ModuleID"].ToString()), objPubTable) == false))
                        {
                            theChildRoot.ImageUrl = "~/Images/lock.jpg";
                            theChildRoot.ImageToolTip = "You are Not Authorized to Access this Functionality";
                            theChildRoot.SelectAction = TreeNodeSelectAction.None;
                        }
                        else
                        {
                            //url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&CCID={6}", theUniDR["FormName"].ToString(), theUniDR["TableName"].ToString(), theUniDR["CategoryId"].ToString(), theUniDR["ListName"].ToString(), theUniDR["FeatureID"].ToString(), theUniDR["Update"].ToString(), theUniDR["CountryID"].ToString());
                            url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&CCID={6}&ModId={7}", theUniDR["FormName"].ToString(), theUniDR["TableName"].ToString(), theUniDR["CategoryId"].ToString(), theUniDR["ListName"].ToString(), theUniDR["FeatureID"].ToString(), theUniDR["Update"].ToString(), theUniDR["CountryID"].ToString(), theUniDR["ModuleID"].ToString());
                            theChildRoot.NavigateUrl = url;
                        }
                        theMRoot.ChildNodes.Add(theChildRoot);
                    }
                }
            }

            #endregion "Module Nodes"
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../frmFacilityHome.aspx");
        }
    }
}