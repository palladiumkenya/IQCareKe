using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
namespace IQCare.Web.Admin
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DrugList : BasePage
    {
        /////////////////////////////////////////////////////////////////////
        // Code Written By   : Sanjay Rana
        // Written Date      : 25th July 2006
        // Modification Date : 
        // Description       : Drug List
        //
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        //
        
        protected void Page_Load(object sender, EventArgs e)
        {
            IDrugMst DrugManager;
            try
            {

                if (!IsPostBack)
                {
                    
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Customize Lists >> ";
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Drugs";

                    DrugManager = (IDrugMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDrugMst, BusinessProcess.Administration");
                    //pr_Admin_SelectDrug_Constella
                    DataSet theDS = DrugManager.GetDrug();
                    MakeDrugList(theDS);
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


        /// <summary>
        /// Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string url;
            url = "frmAdmin_Drug.aspx?name=Add";
            Response.Redirect(url);
        }

        #region "User Functions"
        //private void BindGrid(DataTable theDT)
        /// <summary>
        /// Binds the grid.
        /// </summary>
        private void BindGrid()
        {

            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "Drug_Pk";
            theCol0.DataField = "Drug_Pk";
            theCol0.ItemStyle.CssClass = "textstyle";
            //theCol0.ItemStyle.Width = 5;
            theCol0.ReadOnly = true;
            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "Trade Name";
            theCol2.ItemStyle.CssClass = "textstyle";
            theCol2.DataField = "TradeName";
            theCol2.SortExpression = "TradeName";                 ///"DrugName";
            //theCol2.ItemStyle.Width = 5;

            theCol2.ReadOnly = true;


            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "Generic Name";
            theCol3.ItemStyle.CssClass = "textstyle";
            theCol3.DataField = "DrugGeneric";
            theCol3.SortExpression = "DrugGeneric";
            //theCol3.ItemStyle.Width = 5;//11Mar08
            theCol3.ReadOnly = true;

            BoundField theCol4 = new BoundField();
            theCol4.HeaderText = "Drug Type";
            theCol4.ItemStyle.CssClass = "textstyle";
            theCol4.DataField = "DrugTypeName";
            theCol4.SortExpression = "DrugTypeName";
            //theCol4.ItemStyle.Width = 5;
            theCol4.ReadOnly = true;


            BoundField theCol5 = new BoundField();
            theCol5.HeaderText = "Generic Abbrevation";
            theCol5.ItemStyle.CssClass = "textstyle";
            theCol5.DataField = "GenericAbbv";
            theCol5.SortExpression = "GenericAbbv";                     //"GenericAbbrevation";
            //theCol5.ItemStyle.Width = 5;
            theCol5.ReadOnly = true;

            BoundField theCol6 = new BoundField();
            theCol6.HeaderText = "Status";
            theCol6.DataField = "Status";
            theCol6.ItemStyle.CssClass = "textstyle";
            theCol6.SortExpression = "Status";
            //theCol6.ItemStyle.Width = 5;
            theCol6.ReadOnly = true;

            ButtonField theBtn = new ButtonField();
            theBtn.ButtonType = ButtonType.Link;
            theBtn.CommandName = "Select";
            theBtn.HeaderStyle.CssClass = "textstylehidden";
            theBtn.ItemStyle.CssClass = "textstylehidden";

            grdMasterDrugs.Columns.Add(theCol0);
            //grdMasterDrugs.Columns.Add(theCol1);
            grdMasterDrugs.Columns.Add(theCol2);
            grdMasterDrugs.Columns.Add(theCol3);
            grdMasterDrugs.Columns.Add(theCol4);
            grdMasterDrugs.Columns.Add(theCol5);
            grdMasterDrugs.Columns.Add(theCol6);

            grdMasterDrugs.Columns.Add(theBtn);

            grdMasterDrugs.DataBind();
            grdMasterDrugs.Columns[0].Visible = false;


        }

        /// <summary>
        /// Makes the drug list.
        /// </summary>
        /// <param name="theDS">The ds.</param>
        private void MakeDrugList(DataSet theDS)
        {
            DataTable theDT = theDS.Tables[0];
            DataView theDV;//= new DataView();
            string theGeneric = "";
            string theGenericAbbv = "";            
            int DrugId = -1;

            DataTable theDT1 = new DataTable();
            theDT1.Columns.Add("Drug_Pk", System.Type.GetType("System.Int32"));
            theDT1.Columns.Add("DrugGeneric", System.Type.GetType("System.String"));
            theDT1.Columns.Add("DrugTypeName", System.Type.GetType("System.String"));
            theDT1.Columns.Add("GenericAbbv", System.Type.GetType("System.String"));
            theDT1.Columns.Add("TradeName", System.Type.GetType("System.String"));
            theDT1.Columns.Add("Status", System.Type.GetType("System.String"));

            DataView DV = new DataView(theDT);
            DV.Sort = "Drug_Pk asc";
            IQCareUtils theUtil = new IQCareUtils();
            theDT = theUtil.CreateTableFromDataView(DV);

            #region "fillTable"
            for (int i = 0; i < theDT.Rows.Count; i++)
            {
                if (Convert.ToInt32(theDT.Rows[i]["Drug_Pk"]) > 0)
                {
                    if (DrugId != Convert.ToInt32(theDT.Rows[i]["Drug_Pk"]))
                    {
                        DrugId = Convert.ToInt32(theDT.Rows[i]["Drug_pk"]);

                        theDV = new DataView(theDT);
                        theDV.RowFilter = "Drug_pk = " + DrugId;

                        if (theDV.Count > 0)
                        {
                            //theTradeName = Convert.ToString(theDV[0].Row["DrugName"]);
                            #region "Modified 18June2007 (1)"
                            for (int j = 0; j < theDV.Count; j++)
                            {
                                if (theGeneric.Trim() == "")
                                {
                                    theGeneric = Convert.ToString(theDV[j].Row["GenericName"]);
                                }
                                else
                                {
                                    if (theGeneric.Contains(Convert.ToString(theDV[j].Row["GenericName"])) == false)
                                        theGeneric = theGeneric + "/" + " " + Convert.ToString(theDV[j].Row["GenericName"]);
                                }
                                if (theGenericAbbv == "")
                                {
                                    theGenericAbbv = Convert.ToString(theDV[j].Row["GenericAbbrevation"]);
                                }
                                else
                                {
                                    theGenericAbbv = theGenericAbbv + "/" + " " + Convert.ToString(theDV[j].Row["GenericAbbrevation"]);

                                }

                            }
                            #endregion
                            DataRow theDR = theDT1.NewRow();
                            theDR["Drug_pk"] = Convert.ToInt32(theDT.Rows[i]["Drug_Pk"]);
                            theDR["DrugGeneric"] = theGeneric;
                            theDR["GenericAbbv"] = theGenericAbbv;
                            theDR["DrugTypeName"] = Convert.ToString(theDT.Rows[i]["DrugTypeName"]);
                            theDR["TradeName"] = Convert.ToString(theDT.Rows[i]["DrugName"]); ;
                            theDR["Status"] = Convert.ToString(theDT.Rows[i]["Status"]);
                            theDT1.Rows.Add(theDR);
                            theGeneric = "";
                            theGenericAbbv = "";

                        }
                    }
                }
                else
                {
                    DataRow theDR = theDT1.NewRow();
                    theDR["Drug_pk"] = Convert.ToInt32(theDT.Rows[i]["Drug_Pk"]);
                    theDR["DrugGeneric"] = theDT.Rows[i]["GenericName"];
                    theDR["GenericAbbv"] = theDT.Rows[i]["GenericAbbrevation"];
                    theDR["DrugTypeName"] = Convert.ToString(theDT.Rows[i]["DrugTypeName"]);
                    theDR["TradeName"] = Convert.ToString(theDT.Rows[0]["DrugName"]); ;
                    theDR["Status"] = Convert.ToString(theDT.Rows[i]["Status"]);
                    theDT1.Rows.Add(theDR);
                }

            }
            #endregion

            DV = new DataView(theDT1);
            DV.Sort = "Status,DrugGeneric asc";
            theDT1 = theUtil.CreateTableFromDataView(DV);

            if (ViewState["grdDataSource"] == null)
            {
                ViewState["grdDataSource"] = theDT1;
                ViewState["SortDirection"] = "Desc";
            }

            grdMasterDrugs.DataSource = theDT1;
            BindGrid();
        }

        #endregion
        /// <summary>
        /// Handles the Sorting event of the grdMasterDrugs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewSortEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the RowDataBound event of the grdMasterDrugs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void grdMasterDrugs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdMasterDrugs, "Select$" + e.Row.RowIndex.ToString()));
            }
        }


        /// <summary>
        /// Handles the SelectedIndexChanging event of the grdMasterDrugs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewSelectEventArgs"/> instance containing the event data.</param>
        protected void grdMasterDrugs_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int thePage = grdMasterDrugs.PageIndex;
            int thePageSize = grdMasterDrugs.PageSize;

            GridViewRow theRow = grdMasterDrugs.Rows[e.NewSelectedIndex];
            int theIndex = thePageSize * thePage + theRow.RowIndex;

            int DrugId = Convert.ToInt32(theRow.Cells[0].Text.ToString());
            string DrugType = theRow.Cells[3].Text.ToString();

            string DrugAbbv = theRow.Cells[4].Text.ToString();
            string GenericName = theRow.Cells[2].Text.ToString();

            string Status = theRow.Cells[5].Text.ToString();

            string theUrl = string.Format("{0}&DrugId={1}&DrugType={2}&Generic={3}&Status={4}", "frmAdmin_Drug.aspx?name=Edit", DrugId, DrugType, GenericName, Status);
            Response.Redirect(theUrl);
        }
        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmAdmin_PMTCT_CustomItems.aspx");
        }
    }
}