using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;

namespace IQCare.Web.Admin
{
    public partial class TBRegimenGenericList : System.Web.UI.Page
    {
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string url;
            url = "frmAdmin_TBRegimenGeneric.aspx?name=Add";
            Response.Redirect(url);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmAdmin_PMTCT_CustomItems.aspx");
        }

        protected void grdMasterDrugs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdMasterDrugs, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        protected void grdMasterDrugs_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int thePage = grdMasterDrugs.PageIndex;
            int thePageSize = grdMasterDrugs.PageSize;

            GridViewRow theRow = grdMasterDrugs.Rows[e.NewSelectedIndex];
            int theIndex = thePageSize * thePage + theRow.RowIndex;

            int RegimenId = Convert.ToInt32(theRow.Cells[0].Text.ToString());

            //string theUrl = string.Format("{0}&DrugId={1}&DrugType={2}&Generic={3}", "frmAdmin_Drug.aspx?name=Edit" , DrugId, DrugType,GenericName);
            string theUrl = string.Format("{0}&RegimenId={1}", "frmAdmin_TBRegimenGeneric.aspx?name=Edit", RegimenId);
            Response.Redirect(theUrl);
        }

        protected void grdMasterDrugs_Sorting(object sender, GridViewSortEventArgs e)
        {
            IQCareUtils clsUtil = new IQCareUtils();
            DataView theDV;
            if (ViewState["SortDirection"].ToString() == "Asc")
            {
                theDV = clsUtil.GridSort((DataTable)ViewState["grdDataSource"], e.SortExpression, ViewState["SortDirection"].ToString());
                ViewState["SortDirection"] = "Desc";
            }
            else
            {
                theDV = clsUtil.GridSort((DataTable)ViewState["grdDataSource"], e.SortExpression, ViewState["SortDirection"].ToString());
                ViewState["SortDirection"] = "Asc";
            }
            grdMasterDrugs.Columns.Clear();
            grdMasterDrugs.DataSource = theDV;
            BindGrid();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            IDrugMst DrugManager;
            try
            {
                if (!IsPostBack)
                {
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Customize Lists";
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = " >> TB Regimen";
                    DrugManager = (IDrugMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDrugMst, BusinessProcess.Administration");
                    DataSet theDS = DrugManager.GetAllTBRegimenGeneric();
                    MakeRegimenGenericList(theDS);//11Mar08
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
            finally
            {
                DrugManager = null;
            }
        }
        //private void BindGrid(DataTable theDT)
        private void BindGrid()
        {
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "RegimenID";
            theCol0.DataField = "ID";
            theCol0.ItemStyle.CssClass = "textstyle";
            theCol0.ReadOnly = true;

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Priority";
            theCol1.ItemStyle.CssClass = "textstyle";
            theCol1.DataField = "SRNo";
            theCol1.SortExpression = "SRNo";
            theCol1.ReadOnly = true;

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "TB Regimen Name";
            theCol2.ItemStyle.CssClass = "textstyle";
            theCol2.DataField = "Name";
            theCol2.SortExpression = "Name";
            theCol2.ReadOnly = true;

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "TB Regimen";
            theCol3.ItemStyle.CssClass = "textstyle";
            theCol3.DataField = "GenericName";
            theCol3.SortExpression = "GenericName";
            theCol3.ReadOnly = true;

            BoundField theCol4 = new BoundField();
            theCol4.HeaderText = "Treatment Time";
            theCol4.DataField = "TreatmentTime";
            theCol4.ItemStyle.CssClass = "textstyle";
            theCol4.SortExpression = "TreatmentTime";
            theCol4.ReadOnly = true;

            BoundField theCol5 = new BoundField();
            theCol5.HeaderText = "Status";
            theCol5.DataField = "Status";
            theCol5.ItemStyle.CssClass = "textstyle";
            theCol5.SortExpression = "Status";
            theCol5.ReadOnly = true;

            ButtonField theBtn = new ButtonField();
            theBtn.ButtonType = ButtonType.Link;
            theBtn.CommandName = "Select";
            theBtn.HeaderStyle.CssClass = "textstylehidden";
            theBtn.ItemStyle.CssClass = "textstylehidden";

            grdMasterDrugs.Columns.Add(theCol0);
            grdMasterDrugs.Columns.Add(theCol1);
            grdMasterDrugs.Columns.Add(theCol2);
            grdMasterDrugs.Columns.Add(theCol3);
            grdMasterDrugs.Columns.Add(theCol4);
            grdMasterDrugs.Columns.Add(theCol5);

            grdMasterDrugs.Columns.Add(theBtn);

            grdMasterDrugs.DataBind();
            grdMasterDrugs.Columns[0].Visible = false;
        }

        private void MakeRegimenGenericList(DataSet theDS)
        {
            DataTable theDT = theDS.Tables[0];
            DataView theDV;//= new DataView();

            int RegimenId = -1;
            string GenericID = string.Empty;
            string GenericName = string.Empty;

            DataTable theDT1 = new DataTable();
            theDT1.Columns.Add("ID", System.Type.GetType("System.Int32"));
            theDT1.Columns.Add("Name", System.Type.GetType("System.String"));
            theDT1.Columns.Add("TreatmentTime", System.Type.GetType("System.Int32"));
            theDT1.Columns.Add("SRNo", System.Type.GetType("System.String"));
            theDT1.Columns.Add("UserID", System.Type.GetType("System.Int32"));
            theDT1.Columns.Add("GenericID", System.Type.GetType("System.String"));
            theDT1.Columns.Add("GenericName", System.Type.GetType("System.String"));
            theDT1.Columns.Add("Status", System.Type.GetType("System.String"));

            DataView DV = new DataView(theDT);
            //DV.Sort = "GenericID asc";
            IQCareUtils theUtil = new IQCareUtils();
            theDT = theUtil.CreateTableFromDataView(DV);

            for (int i = 0; i < theDT.Rows.Count; i++)
            {
                if (Convert.ToInt32(theDT.Rows[i]["ID"]) > 0)
                {
                    if (RegimenId != Convert.ToInt32(theDT.Rows[i]["ID"]))
                    {
                        RegimenId = Convert.ToInt32(theDT.Rows[i]["ID"]);

                        theDV = new DataView(theDT);
                        theDV.RowFilter = "ID = " + RegimenId;

                        if (theDV.Count > 0)
                        {
                            for (int j = 0; j < theDV.Count; j++)
                            {
                                if (GenericID.Trim() == "")
                                {
                                    GenericID = Convert.ToString(theDV[j].Row["GenericID"]);
                                }
                                else
                                {
                                    if (GenericID.Contains(Convert.ToString(theDV[j].Row["GenericID"])) == false)
                                        GenericID = GenericID + "/" + " " + Convert.ToString(theDV[j].Row["GenericID"]);
                                }

                                if (GenericName.Trim() == "")
                                {
                                    GenericName = Convert.ToString(theDV[j].Row["GenericName"]);
                                }
                                else
                                {
                                    if (GenericName.Contains(Convert.ToString(theDV[j].Row["GenericName"])) == false)
                                        GenericName = GenericName + "/" + " " + Convert.ToString(theDV[j].Row["GenericName"]);
                                }
                            }

                            DataRow theDR = theDT1.NewRow();
                            theDR["ID"] = Convert.ToInt32(theDT.Rows[i]["ID"]);
                            theDR["Name"] = Convert.ToString(theDT.Rows[i]["Name"]);
                            theDR["TreatmentTime"] = Convert.ToInt32(theDT.Rows[i]["TreatmentTime"]);
                            theDR["UserID"] = Convert.ToInt32(theDT.Rows[i]["UserID"]);
                            theDR["SRNo"] = Convert.ToString(theDT.Rows[i]["SRNo"]);
                            theDR["GenericID"] = GenericID;
                            theDR["GenericName"] = GenericName;
                            theDR["Status"] = Convert.ToString(theDT.Rows[i]["Status"]);
                            theDT1.Rows.Add(theDR);
                            GenericID = "";
                            GenericName = "";
                        }
                    }
                }
                else
                {
                    DataRow theDR = theDT1.NewRow();
                    theDR["ID"] = Convert.ToInt32(theDT.Rows[i]["ID"]);
                    theDR["Name"] = Convert.ToString(theDT.Rows[i]["Name"]);
                    theDR["TreatmentTime"] = Convert.ToInt32(theDT.Rows[i]["TreatmentTime"]);
                    theDR["UserID"] = Convert.ToInt32(theDT.Rows[i]["UserID"]);
                    theDR["SRNo"] = Convert.ToString(theDT.Rows[i]["SRNo"]);
                    theDR["GenericID"] = Convert.ToString(theDT.Rows[0]["GenericID"]); ;
                    theDR["GenericName"] = Convert.ToString(theDT.Rows[i]["GenericName"]);
                    theDR["Status"] = Convert.ToString(theDT.Rows[i]["Status"]);
                    theDT1.Rows.Add(theDR);
                }
            }

            DV = new DataView(theDT1);
            DV.Sort = "Status asc";
            theDT1 = theUtil.CreateTableFromDataView(DV);

            if (ViewState["grdDataSource"] == null)
            {
                ViewState["grdDataSource"] = theDT1;
                ViewState["SortDirection"] = "Desc";
            }

            grdMasterDrugs.DataSource = theDT1;
            BindGrid();
        }
    }
}