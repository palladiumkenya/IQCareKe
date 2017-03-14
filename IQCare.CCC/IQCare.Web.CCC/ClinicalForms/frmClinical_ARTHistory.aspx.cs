using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
using Interface.Clinical;

/// /////////////////////////////////////////////////////////////////////
// Code Written By   : Jayant Kumar Das
// Written Date      : 15 August 2012
// Description       : ART History for Kenya Blue Card
//
/// /////////////////////////////////////////////////////////////////
/// IQCare.Web.ClinicalForms.frmClinical_ARTHistory
/// 
namespace IQCare.Web.Clinical
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ARTHistory : System.Web.UI.Page
    {
        /// <summary>
        /// The p identifier
        /// </summary>
        int PId;
        /// <summary>
        /// The selected purpose identifier
        /// </summary>
        public int SelectedPurposeId;
        /// <summary>
        /// The selected regimen
        /// </summary>
        public string SelectedRegimen, SelectedLastused;
        /// <summary>
        /// The strmultiselect
        /// </summary>
        string strmultiselect;
        /// <summary>
        /// The sb values
        /// </summary>
        StringBuilder sbValues;
        /// <summary>
        /// The arl
        /// </summary>
        ArrayList arl= new ArrayList();
        /// <summary>
        /// The pat identifier
        /// </summary>
        int PatID;
        /// <summary>
        /// The sb parameter
        /// </summary>
        StringBuilder sbParameter = new StringBuilder();
        /// <summary>
        /// The table name
        /// </summary>
        String TableName = "";
        /// <summary>
        /// The icount
        /// </summary>
        int icount;
        /// <summary>
        /// Gets the art history data.
        /// </summary>
        protected void GetARTHistoryData()
        {
            DataSet theDSARTHistory = new DataSet();

            IPriorArtHivCare ARTHistoryMgr = (IPriorArtHivCare)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPriorArtHivCare, BusinessProcess.Clinical");
            theDSARTHistory = ARTHistoryMgr.GetARTHistoryData(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["PatientVisitId"]), Convert.ToInt32(Session["AppLocationId"]));

            if (Convert.ToInt32(theDSARTHistory.Tables[0].Rows[0]["VisitId"]) > 0)
            {
                Session["PatientVisitId"] = Convert.ToInt32(theDSARTHistory.Tables[0].Rows[0]["VisitId"]);

                txtTransferInDate.Value = String.Format("{0:dd-MMM-yyyy}", theDSARTHistory.Tables[1].Rows[0]["ARTTransferInDate"]);
                //dddistrict.SelectedValue = Convert.ToString(theDSARTHistory.Tables[1].Rows[0]["FromDistrict"]);
                txtdistrict.Text = theDSARTHistory.Tables[1].Rows[0]["FromDistrict"].ToString();
                txtFacility.Text = theDSARTHistory.Tables[1].Rows[0]["ARTTransferInFrom"].ToString();
                //ddfacility.SelectedValue = Convert.ToString(theDSARTHistory.Tables[1].Rows[0]["ARTTransferInFrom"]);
                txtDateARTStarted.Value = String.Format("{0:dd-MMM-yyyy}", theDSARTHistory.Tables[3].Rows[0]["ARTStartDate"]);
                txtHIVConfirmHIVPosDate.Value = String.Format("{0:dd-MMM-yyyy}", theDSARTHistory.Tables[3].Rows[0]["ConfirmHIVPosDate"]);
                string dateEnrolledInCare = String.Format("{0:dd-MMM-yyyy}", theDSARTHistory.Tables[6].Rows[0]["EnrollmentDate"]);
                if (theDSARTHistory.Tables[3].Rows[0]["DateEnrolledInCare"] != DBNull.Value)
                {
                    dateEnrolledInCare = String.Format("{0:dd-MMM-yyyy}", theDSARTHistory.Tables[3].Rows[0]["DateEnrolledInCare"]);
                }
                txtEnrolledinHIVCare.Value = dateEnrolledInCare;
//                DateEnrolledInCare

                txtWhere.Text = theDSARTHistory.Tables[2].Rows[0]["HIVCareWhere"].ToString();
                if (Convert.ToInt32(theDSARTHistory.Tables[2].Rows[0]["PriorART"]) == 1)
                {
                    rbtnknownYes.Checked = true;
                    btnRegimen.Enabled = true;
                    txtRegimen.Enabled = true;
                }
                else if (Convert.ToInt32(theDSARTHistory.Tables[2].Rows[0]["PriorART"]) == 0)
                {
                    rbtnknownNo.Checked = true;
                    btnRegimen.Enabled = false;
                    txtRegimen.Enabled = false;
                }

                if (theDSARTHistory.Tables[4].Rows.Count > 0)
                {
                    ddlWHOStage.Text = theDSARTHistory.Tables[4].Rows[0]["WHOStage"].ToString();
                }
                txtAreaAllergy.Value = theDSARTHistory.Tables[5].Rows[0]["DrugAllergies"].ToString();

               // txtEnrolledinHIVCare.Value = String.Format("{0:dd-MMM-yyyy}", theDSARTHistory.Tables[6].Rows[0]["RegistrationDate"]);

                GrdPriorART.Columns.Clear();
                BindGrid();
                GrdPriorART.DataSource = theDSARTHistory.Tables[7];
                GrdPriorART.DataBind();
                ViewState["GridData"] = theDSARTHistory.Tables[7];
            }
            else { txtEnrolledinHIVCare.Value = String.Format("{0:dd-MMM-yyyy}", theDSARTHistory.Tables[1].Rows[0]["EnrollmentDate"]); }
        }
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
           
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "ART History";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "ART History";

            //todo
            PutCustomControl();

            if (!IsPostBack)
            {
                Authenticate();
                FillDropDown();
                Init_ARTHistory();
                GetARTHistoryData();
            }
            else
            {
                if ((DataTable)Application["AddRegimen"] != null)
                {
                    DataTable theDT = (DataTable)Application["AddRegimen"];
                    ViewState["SelectedData"] = theDT;
                    string theStr = FillRegimen(theDT);
                    txtRegimen.Text = theStr;
                    Application.Remove("AddRegimen");
                }
            }
            PId = Convert.ToInt32(Session["PatientId"]);
            FillOldData(PId);


            txtTransferInDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');");
            txtTransferInDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");
            txtDateARTStarted.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');");
            txtDateARTStarted.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");

            txtHIVConfirmHIVPosDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');");
            txtHIVConfirmHIVPosDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");

            txtEnrolledinHIVCare.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');");
            txtEnrolledinHIVCare.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");


        }

        //todo
        /// <summary>
        /// Puts the custom control.
        /// </summary>
        private void PutCustomControl()
        {
            ICustomFields CustomFields;
            CustomFieldClinical theCustomField = new CustomFieldClinical();
            try
            {

                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields,BusinessProcess.Administration");
                DataSet theDS = CustomFields.GetCustomFieldListforAForm(Convert.ToInt32(ApplicationAccess.ARTHistory));
                if (theDS.Tables[0].Rows.Count != 0)
                {
                    theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "AHistory");
                    //ViewState["CustomFieldsDS"] = theDS;
                    //pnlCustomList.Visible = true;
                }
                //theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "PAHCare");
                ViewState["CustomFieldsDS"] = theDS;
                pnlCustomList.Visible = true;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
            finally
            {
                CustomFields = null;
            }

        }

        /// <summary>
        /// Fills the regimen.
        /// </summary>
        /// <param name="theDT">The dt.</param>
        /// <returns></returns>
        private string FillRegimen(DataTable theDT)
        {
            string theRegimen = "";
            DataView theDV = new DataView();
            if (theDT.Rows.Count != 0)
            {
                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    DataTable theFilDT = (DataTable)ViewState["MasterARVData"];
                    if (theFilDT.Rows.Count > 0)
                    {
                        theDV = new DataView(theFilDT);
                        theDV.RowFilter = "DrugId = " + theDT.Rows[i]["DrugId"];
                        if (theDV.Count > 0)
                        {
                            for (int j = 0; j < theDV.Count; j++)
                            {
                                if (theRegimen == "")
                                {
                                    theRegimen = theDV[j]["Abbr"].ToString();
                                }
                                else
                                {
                                    theRegimen = theRegimen + "/" + theDV[j]["Abbr"].ToString();
                                }
                            }
                        }
                    }
                    theRegimen = theRegimen.Trim();

                }
            }
            return theRegimen;
        }
        /// <summary>
        /// Fills the drop down.
        /// </summary>
        protected void FillDropDown()
        {
            BindFunctions theBndMgr = new BindFunctions();
            DataSet theDS = new DataSet();
            theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            IQCareUtils theUtils = new IQCareUtils();

            //if (theDS.Tables["Mst_District"] != null)
            //{
            //    DataView theDV = new DataView(theDS.Tables["Mst_District"]);
            //    theDV.RowFilter = "DeleteFlag=0 and Systemid=1";
            //    if (theDV.Table != null)
            //    {
            //        DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            //        theBndMgr.BindCombo(dddistrict, theDT, "Name", "ID");
            //    }
            //}
            //if (theDS.Tables["Mst_LPTF"] != null)
            //{
            //    DataView theDV = new DataView(theDS.Tables["Mst_LPTF"]);
            //    theDV.RowFilter = "DeleteFlag=0";
            //    if (theDV.Table != null)
            //    {
            //        DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            //        theBndMgr.BindCombo(ddfacility, theDT, "Name", "ID");
            //    }
            //}
            if (theDS.Tables["Mst_Decode"] != null)
            {
                DataView theDV = new DataView(theDS.Tables["Mst_Decode"]);
                theDV.RowFilter = "DeleteFlag=0 and CodeId=33";
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    theBndMgr.BindCombo(ddlpurpose, theDT, "Name", "ID");
                }
            }

            if (theDS.Tables["Mst_Decode"] != null)
            {
                DataView theDV = new DataView(theDS.Tables["Mst_Decode"]);
                theDV.RowFilter = "(DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1) and CodeId=22";
                theDV.Sort = "SRNo";
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    theBndMgr.BindCombo(ddlWHOStage, theDT, "Name", "ID");
                }
            }
        }
        /// <summary>
        /// Init_s the art history.
        /// </summary>
        protected void Init_ARTHistory()
        {

            IPatientARTCare ARTHistoryMgr = (IPatientARTCare)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientARTCare, BusinessProcess.Clinical");
            DataSet theDS = ARTHistoryMgr.GetPatientARTCare(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["AppLocationId"]));
            ViewState["MasterARVData"] = theDS.Tables[1];

        }
        /// <summary>
        /// Binds the grid.
        /// </summary>
        private void BindGrid()
        {
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "VisitId";
            theCol0.DataField = "VisitId";
            theCol0.ItemStyle.CssClass = "textstyle";
            GrdPriorART.Columns.Add(theCol0);

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Patientid";
            theCol1.DataField = "ptn_pk";
            theCol1.ItemStyle.CssClass = "textstyle";
            GrdPriorART.Columns.Add(theCol1);

            BoundField theCol6 = new BoundField();
            theCol6.HeaderText = "PurposeId";
            theCol6.DataField = "PurposeId";
            GrdPriorART.Columns.Add(theCol6);

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "Purpose";
            theCol2.DataField = "Purpose";
            theCol2.SortExpression = "Purpose";
            theCol2.ReadOnly = true;
            GrdPriorART.Columns.Add(theCol2);

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "Regimen";
            theCol3.DataField = "Regimen";
            theCol3.SortExpression = "Regimen";
            theCol3.ReadOnly = true;
            GrdPriorART.Columns.Add(theCol3);

            BoundField theCol4 = new BoundField();
            theCol4.HeaderText = "Regimen Last Used";
            theCol4.DataField = "RegLastUsed";
            theCol4.DataFormatString = "{0:dd-MMM-yyyy}";
            theCol4.SortExpression = "RegLastUsed";
            theCol4.ReadOnly = true;
            GrdPriorART.Columns.Add(theCol4);

            CommandField theCol5 = new CommandField();
            theCol5.ButtonType = ButtonType.Link;
            theCol5.DeleteText = "<img src='../Images/del.gif' alt='Delete this' border='0' />";
            theCol5.ShowDeleteButton = true;
            GrdPriorART.Columns.Add(theCol5);

            GrdPriorART.Columns[0].Visible = false;
            GrdPriorART.Columns[1].Visible = false;
            GrdPriorART.Columns[2].Visible = false;
        }
        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        private void Refresh()
        {
            ddlpurpose.SelectedIndex = -1;
            txtRegimen.Text = "";
            txtLastUsed.Text = "";

        }
        /// <summary>
        /// Handles the Click event of the btnAddPriorART control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnAddPriorART_Click(object sender, EventArgs e)
        {
            if (ValidateLastUsed() == false)
            {
                return;
            }
            int VisitId = Convert.ToInt32(Session["PatientVisitId"]) > 0 ? Convert.ToInt32(Session["PatientVisitId"]) : 0;
            DataTable theDT = new DataTable();
            if ((DataTable)ViewState["GridData"] == null)
            {
                theDT.Columns.Add("ptn_pk", typeof(Int32));
                theDT.Columns.Add("VisitId", typeof(Int32));
                theDT.Columns.Add("PurposeId", typeof(Int32));
                theDT.Columns.Add("Purpose", typeof(string));
                theDT.Columns.Add("Regimen", typeof(string));
                theDT.Columns.Add("RegLastUsed", typeof(string));
                DataRow theDR = theDT.NewRow();
                theDR["ptn_pk"] = Session["PatientId"];
                theDR["VisitId"] = VisitId;
                theDR["Purpose"] = ddlpurpose.SelectedItem.Text;
                theDR["Regimen"] = txtRegimen.Text;
                theDR["RegLastUsed"] = txtLastUsed.Text;
                theDR["PurposeId"] = ddlpurpose.SelectedValue;
                theDT.Rows.Add(theDR);
                GrdPriorART.Columns.Clear();
                BindGrid();
                Refresh();
                GrdPriorART.DataSource = theDT;
                GrdPriorART.DataBind();
                ViewState["GridData"] = theDT;
            }
            else
            {

                theDT = (DataTable)ViewState["GridData"];
                if (Convert.ToInt32(ViewState["UpdateFlag"]) == 1)
                {
                    DataRow[] rows = theDT.Select("PurposeId=" + ViewState["SelectedPurposeId"] + " and Regimen='" + ViewState["SelectedRegimen"] + "' and RegLastUsed='" + ViewState["SelectedLastused"] + "'");
                    for (int i = 0; i < rows.Length; i++)
                    {
                        rows[i]["ptn_pk"] = Session["PatientId"];
                        rows[i]["VisitId"] = VisitId;
                        rows[i]["PurposeId"] = ddlpurpose.SelectedValue;
                        rows[i]["Purpose"] = ddlpurpose.SelectedItem.Text;
                        rows[i]["Regimen"] = txtRegimen.Text;
                        rows[i]["RegLastUsed"] = txtLastUsed.Text;
                        theDT.AcceptChanges();
                    }
                    GrdPriorART.Columns.Clear();
                    BindGrid();
                    Refresh();
                    GrdPriorART.DataSource = theDT;
                    GrdPriorART.DataBind();
                    ViewState["GridData"] = theDT;
                    ViewState["UpdateFlag"] = "0";
                }
                else
                {
                    DataRow theDR = theDT.NewRow();
                    theDR["ptn_pk"] = Session["PatientId"];
                    theDR["VisitId"] = VisitId;
                    theDR["Purpose"] = ddlpurpose.SelectedItem.Text;
                    theDR["Regimen"] = txtRegimen.Text;
                    theDR["RegLastUsed"] = txtLastUsed.Text;
                    theDR["PurposeId"] = ddlpurpose.SelectedValue;
                    theDT.Rows.Add(theDR);
                    GrdPriorART.Columns.Clear();
                    BindGrid();
                    Refresh();
                    GrdPriorART.DataSource = theDT;
                    GrdPriorART.DataBind();
                    ViewState["GridData"] = theDT;



                }
            }

        }
        /// <summary>
        /// Handles the Click event of the btnRegimen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnRegimen_Click(object sender, EventArgs e)
        {
            string theScript;
            Application.Add("MasterData", ViewState["MasterARVData"]);
            Application.Add("SelectedDrug", (DataTable)ViewState["SelectedData"]);
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.open('../Pharmacy/frmDrugSelector.aspx?DrugType=37&btnreg=" + btnRegimen.ID + "' ,'DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
            theScript += "</script>\n";
            //Page.ClientScript.RegisterStartupScript(this.GetType(),"DrgPopup", theScript);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DrgPopup", theScript);
        }
        /// <summary>
        /// Handles the RowDataBound event of the GrdPriorART control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void GrdPriorART_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.Cells[0].Text.ToString() != "0")
                    {
                        e.Row.Cells[i].Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                        e.Row.Cells[i].Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                        e.Row.Cells[i].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(GrdPriorART, "Select$" + e.Row.RowIndex.ToString()));
                    }
                    if (e.Row.Cells[5].Text.ToString() == "01-Jan-1900")
                    {
                        e.Row.Cells[5].Text = "";
                    }

                }
            }
        }
        /// <summary>
        /// Handles the SelectedIndexChanging event of the GrdPriorART control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewSelectEventArgs"/> instance containing the event data.</param>
        protected void GrdPriorART_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int thePage = GrdPriorART.PageIndex;
            int thePageSize = GrdPriorART.PageSize;
            GridViewRow theRow = GrdPriorART.Rows[e.NewSelectedIndex];
            int theIndex = thePageSize * thePage + theRow.RowIndex;
            System.Data.DataTable theDT = new System.Data.DataTable();
            theDT = ((DataTable)ViewState["GridData"]);
            int r = theIndex;
            if (theDT.Rows.Count > 0)
            {
                ViewState["UpdateFlag"] = 1;
                ddlpurpose.SelectedValue = theDT.Rows[r]["PurposeId"].ToString();
                ViewState["SelectedPurposeId"] = Convert.ToInt32(theDT.Rows[r]["PurposeId"]);
                txtRegimen.Text = theDT.Rows[r]["Regimen"].ToString();
                ViewState["SelectedRegimen"] = theDT.Rows[r]["Regimen"].ToString();

                txtLastUsed.Text = String.Format("{0:dd-MMM-yyyy}", theDT.Rows[r]["RegLastUsed"]);
                ViewState["SelectedLastused"] = theDT.Rows[r]["RegLastUsed"].ToString();

                DataTable dtMaster = (DataTable)ViewState["MasterARVData"];
                string[] thetransStrRegimen = txtRegimen.Text.Split(new Char[] { '/' });
                DataView theARVTransDV = new DataView(dtMaster);
                if (txtRegimen.Text != "")
                {
                    ViewState["SelectedData"] = OldRegimenList(thetransStrRegimen, theARVTransDV);
                }

            }
        }
        /// <summary>
        /// Olds the regimen list.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="theDV">The dv.</param>
        /// <returns></returns>
        private DataTable OldRegimenList(string[] str, DataView theDV)
        {
            DataTable theDT = CreateSelectedTable();
            foreach (string reg in str)
            {
                theDV.RowFilter = "Abbr Like '" + reg + "%'";
                if (theDV.Count > 0)
                {
                    DataRow theDR = theDT.NewRow();
                    theDR[0] = theDV[0][0];
                    theDR[1] = theDV[0][1];
                    theDR[2] = theDV[0][2];
                    theDR[3] = theDV[0][3];
                    theDR[4] = theDV[0][4];

                    DataRow theTempeDR;
                    theTempeDR = theDT.Rows.Find(theDV[0][0]);
                    if (theTempeDR == null)
                    {
                        theDT.Rows.Add(theDR);
                    }
                }
            }
            return theDT;

        }
        /// <summary>
        /// Creates the selected table.
        /// </summary>
        /// <returns></returns>
        private DataTable CreateSelectedTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugName", System.Type.GetType("System.String"));
            theDT.Columns.Add("Generic", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugTypeID", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("Abbr", System.Type.GetType("System.String"));
            theDT.PrimaryKey = new DataColumn[] { theDT.Columns[0] };
            return theDT;
        }
        /// <summary>
        /// Hts the specified qlty flag.
        /// </summary>
        /// <param name="qltyFlag">The qlty flag.</param>
        /// <returns></returns>
        protected Hashtable HT(int qltyFlag)
        {

            Hashtable theHT = new Hashtable();
            try
            {
                theHT.Add("PatientId", Session["PatientId"]);
                theHT.Add("VisitId", Session["PatientVisitId"]);
                theHT.Add("LocationId", Session["AppLocationId"]);
                theHT.Add("TransferInDate", txtTransferInDate.Value);
                //theHT.Add("dddistrict", dddistrict.SelectedValue);
                theHT.Add("dddistrict", txtdistrict.Text);
                theHT.Add("ddfacility", txtFacility.Text);
                //theHT.Add("ddfacility", ddfacility.SelectedValue);
                theHT.Add("DateARTStarted", txtDateARTStarted.Value);
                int priorART = rbtnknownYes.Checked ? 1 : rbtnknownNo.Checked ? 0 : 9;
                theHT.Add("priorART", priorART);
                theHT.Add("ConfirmHIVPosDate", txtHIVConfirmHIVPosDate.Value);
                theHT.Add("Where", txtWhere.Text);
                theHT.Add("EnrolledinHIVCare", txtEnrolledinHIVCare.Value);
                theHT.Add("WHOStage", ddlWHOStage.SelectedValue);
                theHT.Add("AreaAllergy", txtAreaAllergy.Value);
                theHT.Add("UserID", Session["AppUserId"].ToString());
                theHT.Add("ModuleId", Convert.ToInt32(Session["TechnicalAreaId"]));

                //todo
                theHT.Add("visitdate", System.DateTime.Now.ToString("dd-MMM-yyyy"));

                if (qltyFlag == 1)
                {
                    theHT.Add("qltyFlag", "1");
                }
                else
                {
                    theHT.Add("qltyFlag", "0");
                }


            }
            catch (Exception err)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theMsg, this);
            }
            return theHT;

        }
        /// <summary>
        /// Saves the cancel.
        /// </summary>
        private void SaveCancel()
        {

            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";

            script += "ans=window.confirm('ART History Form saved successfully. Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            if (ViewState["Redirect"] == null || ViewState["Redirect"].ToString() == "0")
            {
                script += "window.location.href='frmPatient_Home.aspx';\n";
            }
            else
            {
                script += "window.location.href='frmPatient_History.aspx?sts=" + 0 + "';\n";
            }
            script += "}\n";
            script += "else \n";
            script += "{\n";
            script += "window.location.href='frmClinical_ARTHistory.aspx';\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }
        /// <summary>
        /// Authenticates this instance.
        /// </summary>
        private void Authenticate()
        {
            AuthenticationManager Authentication = new AuthenticationManager();
            /***************** Check For User Rights ****************/
            if (Authentication.HasFunctionRight(ApplicationAccess.ARTHistory, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
            {
                btnPrint.Enabled = false;
            }
            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                if (Authentication.HasFunctionRight(ApplicationAccess.ARTHistory, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Enabled = false;
                    btncomplete.Enabled = false;
                }
            }

            else
            {
                if (Authentication.HasFunctionRight(ApplicationAccess.ARTHistory, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    string theUrl = "";
                    theUrl = string.Format("{0}?sts={1}", "frmPatient_History.aspx", Session["HIVPatientStatus"].ToString());
                    Response.Redirect(theUrl);
                }
                else if (Authentication.HasFunctionRight(ApplicationAccess.ARTHistory, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Enabled = false;
                    btncomplete.Enabled = false;
                }
                if (Convert.ToString(Session["HIVPatientStatus"]) == "1")
                {
                    btnsave.Enabled = false;
                    btncomplete.Enabled = false;
                }
                //Privilages for Care End
                if (Convert.ToString(Session["CareEndFlag"]) == "1")
                {
                    btnsave.Enabled = true;
                    btncomplete.Enabled = true;
                }
            }
        }
        /// <summary>
        /// Validates the last used.
        /// </summary>
        /// <returns></returns>
        private Boolean ValidateLastUsed()
        {
            IQCareUtils theUtil = new IQCareUtils();
            if (Convert.ToDateTime(Application["AppCurrentDate"]) < Convert.ToDateTime(theUtil.MakeDate(txtLastUsed.Text)))
            {
                IQCareMsgBox.Show("CmpLastUsed", this);
                txtLastUsed.Focus();
                return false;
            }
            else if (ddlpurpose.SelectedValue == "0")
            {
                IQCareMsgBox.Show("ChkPurpose", this);
                txtLastUsed.Focus();
                return false;
            }
            return true;
        }
        /// <summary>
        /// Fields the validation.
        /// </summary>
        /// <returns></returns>
        private Boolean fieldValidation()
        {
            IQCareUtils theUtil = new IQCareUtils();

            int priorART = rbtnknownYes.Checked == true ? 1 : rbtnknownNo.Checked == true ? 0 : 9;
            if (priorART == 9)
            {
                IQCareMsgBox.Show("ChkPriorART", this);
                return false;
            }

            else if (Convert.ToDateTime(Application["AppCurrentDate"]) < Convert.ToDateTime(theUtil.MakeDate(txtTransferInDate.Value)))
            {
                IQCareMsgBox.Show("CmpTransfInDate", this);
                txtTransferInDate.Focus();
                return false;
            }

            else if (Convert.ToDateTime(Application["AppCurrentDate"]) < Convert.ToDateTime(theUtil.MakeDate(txtDateARTStarted.Value)))
            {
                IQCareMsgBox.Show("CmpStartART", this);
                txtDateARTStarted.Focus();
                return false;
            }

            else if (Convert.ToDateTime(Application["AppCurrentDate"]) < Convert.ToDateTime(theUtil.MakeDate(txtHIVConfirmHIVPosDate.Value)))
            {
                IQCareMsgBox.Show("CmpConHIVPositive", this);
                txtHIVConfirmHIVPosDate.Focus();
                return false;
            }
            else if (priorART == 1)
            {
                if (GrdPriorART.Rows.Count == 0)
                {
                    IQCareMsgBox.Show("ChkGrid", this);
                    txtHIVConfirmHIVPosDate.Focus();
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Handles the Click event of the btnsave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (fieldValidation() == false)
            {
                return;
            }
            Hashtable theHT = HT(0);
            DataTable theDT = new DataTable();
            if ((DataTable)ViewState["GridData"] == null)
            {
                theDT.Columns.Add("ptn_pk", typeof(Int32));
                theDT.Columns.Add("VisitId", typeof(Int32));
                theDT.Columns.Add("PurposeId", typeof(Int32));
                theDT.Columns.Add("Purpose", typeof(string));
                theDT.Columns.Add("Regimen", typeof(string));
                theDT.Columns.Add("RegLastUsed", typeof(string));
            }
            else { theDT = (DataTable)ViewState["GridData"]; }


            //todo
            DataTable theCustomDataDT = null;
            if ((Convert.ToInt32(Session["PatientVisitId"]) > 0))
            {
                //  visitPK = Convert.ToInt32(Session["PatientVisitId"]);

                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.ARTHistory, (DataSet)ViewState["CustomFieldsDS"]);

            }
            else
            {
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.ARTHistory, (DataSet)ViewState["CustomFieldsDS"]);

            }

            //todo
            IPriorArtHivCare ARTHistoryMgr = (IPriorArtHivCare)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPriorArtHivCare, BusinessProcess.Clinical");
            DataTable DT = ARTHistoryMgr.Save_Update_ARVHistory(theHT, theDT, theCustomDataDT);
            Session["Redirect"] = "0";
            if (Convert.ToInt32(DT.Rows[0]["VisitId"]) > 0)
            {
                SaveCancel();
            }

        }
        /// <summary>
        /// Handles the Click event of the btncomplete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btncomplete_Click(object sender, EventArgs e)
        {
            Hashtable theHT = HT(1);
            DataTable theDT = new DataTable();
            if ((DataTable)ViewState["GridData"] == null)
            {
                theDT.Columns.Add("ptn_pk", typeof(Int32));
                theDT.Columns.Add("VisitId", typeof(Int32));
                theDT.Columns.Add("PurposeId", typeof(Int32));
                theDT.Columns.Add("Purpose", typeof(string));
                theDT.Columns.Add("Regimen", typeof(string));
                theDT.Columns.Add("RegLastUsed", typeof(string));
            }
            else { theDT = (DataTable)ViewState["GridData"]; }


            //todo
            DataTable theCustomDataDT = null;
            if ((Convert.ToInt32(Session["PatientVisitId"]) > 0))
            {
                //  visitPK = Convert.ToInt32(Session["PatientVisitId"]);

                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.ARTHistory, (DataSet)ViewState["CustomFieldsDS"]);

            }
            else
            {
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.ARTHistory, (DataSet)ViewState["CustomFieldsDS"]);

            }



            IPriorArtHivCare ARTHistoryMgr = (IPriorArtHivCare)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPriorArtHivCare, BusinessProcess.Clinical");
            DataTable DT = ARTHistoryMgr.Save_Update_ARVHistory(theHT, theDT, theCustomDataDT);
            Session["Redirect"] = "0";
            if (Convert.ToInt32(DT.Rows[0]["VisitId"]) > 0)
            {
                SaveCancel();
            }
        }
        /// <summary>
        /// Handles the Click event of the btnback control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnback_Click(object sender, EventArgs e)
        {
            string theUrl = "";
            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                theUrl = string.Format("{0}?PatientId={1}", "frmPatient_Home.aspx", Session["PatientId"].ToString());
            }
            else if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                theUrl = string.Format("{0}", "frmPatient_History.aspx");

            }

            Response.Redirect(theUrl);
        }
        /// <summary>
        /// Handles the Click event of the btnPrint control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Handles the RowDeleting event of the GrdPriorART control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewDeleteEventArgs"/> instance containing the event data.</param>
        protected void GrdPriorART_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            System.Data.DataTable theDT = new System.Data.DataTable();
            theDT = ((DataTable)ViewState["GridData"]);
            int r = Convert.ToInt32(e.RowIndex.ToString());

            int Id = -1;
            try
            {
                if (theDT.Rows.Count > 0)
                {

                    if (theDT.Rows[r].HasErrors == false)
                    {
                        if ((theDT.Rows[r]["PurposeId"] != null) && (theDT.Rows[r]["PurposeId"] != DBNull.Value))
                        {
                            if (theDT.Rows[r]["PurposeId"].ToString() != "")
                            {
                                Id = Convert.ToInt32(theDT.Rows[r]["PurposeId"]);
                                theDT.Rows[r].Delete();
                                theDT.AcceptChanges();
                                ViewState["GridData"] = theDT;
                                GrdPriorART.Columns.Clear();
                                BindGrid();
                                Refresh();
                                GrdPriorART.DataSource = (DataTable)ViewState["GridData"];
                                GrdPriorART.DataBind();
                                IQCareMsgBox.Show("DeleteSuccess", this);
                            }
                        }
                    }



                    if (((DataTable)ViewState["GridData"]).Rows.Count == 0)
                        btnAddPriorART.Enabled = false;
                    else
                        btnAddPriorART.Enabled = true;
                }
                else
                {
                    GrdPriorART.Visible = false;
                    //IQCareMsgBox.Show("DeleteSuccess", this);
                    Refresh();
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        /// <summary>
        /// Updates the custom fields values.
        /// </summary>
        private void UpdateCustomFieldsValues()
        {
            GenerateCustomFieldsValues(pnlCustomList);
            string sqlstr = string.Empty;
            PatID = Convert.ToInt32(Request.QueryString["patientid"]);
            string sqlselect;
            string strdelete;
            Int32 visitID = 0;
            DateTime visitdate = System.DateTime.Now;
            ICustomFields CustomFields;
            //  if (txtvisitDate.Text.ToString() != "")

            visitdate = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy"));

            if (ViewState["VisitID_add"] != null)
                visitID = Convert.ToInt32(ViewState["VisitID_add"]);

            if (sbValues.ToString().Trim() != "")
            {
                if (ViewState["CustomFieldsData"] != null)
                {
                    sbValues = sbValues.Remove(0, 1);
                    sqlstr = "UPDATE dtl_CustomField_" + TableName.ToString().Replace("-", "_") + " SET ";
                    sqlstr += sbValues.ToString() + " where Ptn_pk= " + PatID.ToString() + " and Visit_pk=" + visitID.ToString();
                }
                else
                {
                    sqlstr = "INSERT INTO dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "(ptn_pk,LocationID,Visit_pk,Visit_Date " + sbParameter.ToString() + " )";
                    sqlstr += " VALUES(" + PatID.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'" + sbValues.ToString() + ")";
                    ViewState["CustomFieldsData"] = 1;
                }


                try
                {
                    CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                    icount = CustomFields.SaveCustomFieldValues(sqlstr.ToString());
                    if (icount == -1)
                    {
                        return;
                    }
                }
                catch
                {
                }
                finally
                {
                    CustomFields = null;
                }
            }
            if (strmultiselect.ToString() != "")
            {
                string[] FieldValues = strmultiselect.Split(new char[] { '^' });
                if (arl.Count != 0)
                {
                    int p = 0;
                    foreach (object obj in arl)
                    {
                        sqlselect = "";
                        strdelete = "";
                        if (obj.ToString() != "")
                        {
                            try
                            {
                                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                                strdelete = "DELETE from [" + obj.ToString() + "] where ptn_pk= " + PatID.ToString() + " and LocationID=" + Session["AppLocationID"] + " and visit_pk=" + visitID;
                                icount = CustomFields.SaveCustomFieldValues(strdelete.ToString());

                                if (FieldValues[p].ToString() != "")
                                {
                                    string[] mValues = FieldValues[p].Split(new char[] { ',' });

                                    foreach (string str in mValues)
                                    {
                                        if (str.ToString() != "")
                                        {
                                            string strtab = "dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "_";
                                            Int32 ispos = Convert.ToInt32(strtab.Length);
                                            Int32 iepos = Convert.ToInt32(obj.ToString().Length) - ispos;

                                            sqlselect = "INSERT INTO [" + obj.ToString() + "](ptn_pk,LocationID,visit_pk,visit_Date, [" + obj.ToString().Substring(ispos, iepos) + "])";
                                            sqlselect += " VALUES (" + PatID.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'," + str.ToString() + ")";



                                            icount = CustomFields.SaveCustomFieldValues(sqlselect.ToString());
                                            if (icount == -1)
                                            {
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                            catch
                            {
                            }
                            finally
                            {
                                CustomFields = null;
                            }
                        }
                        p += 1;
                    }
                }
            }
        }

        /// <summary>
        /// Inserts the custom fields values.
        /// </summary>
        private void InsertCustomFieldsValues()
        {
            GenerateCustomFieldsValues(pnlCustomList);
            string sqlstr = string.Empty;
            string sqlselect;
            Int32 visitID = 0;
            DateTime visitdate = System.DateTime.Now;
            PatID = Convert.ToInt32(Request.QueryString["patientid"]);
            ICustomFields CustomFields;
            if (ViewState["VisitID_add"] != null)
                visitID = Convert.ToInt32(ViewState["VisitID_add"]);
            //  if (txtvisitDate.Text.ToString() != "")
            visitdate = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy"));

            if (sbValues.ToString().Trim() != "")
            {
                sqlstr = "INSERT INTO dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "(ptn_pk,LocationID,Visit_pk,Visit_Date " + sbParameter.ToString() + " )";
                sqlstr += " VALUES(" + PatID.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'" + sbValues.ToString() + ")";

                try
                {
                    CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                    icount = CustomFields.SaveCustomFieldValues(sqlstr.ToString());
                    if (icount == -1)
                    {
                        return;
                    }
                }
                catch
                {
                }
                finally
                {
                    CustomFields = null;
                }
            }
            if (strmultiselect.ToString() != "")
            {
                string[] FieldValues = strmultiselect.Split(new char[] { '^' });
                if (arl.Count != 0)
                {
                    int p = 0;
                    foreach (object obj in arl)
                    {
                        sqlselect = "";
                        if (obj.ToString() != "")
                        {
                            if (FieldValues[p].ToString() != "")
                            {
                                string[] mValues = FieldValues[p].Split(new char[] { ',' });
                                foreach (string str in mValues)
                                {
                                    if (str.ToString() != "")
                                    {
                                        string strtab = "dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "_";
                                        Int32 ispos = Convert.ToInt32(strtab.Length);
                                        Int32 iepos = Convert.ToInt32(obj.ToString().Length) - ispos;
                                        sqlselect = "INSERT INTO [" + obj.ToString() + "](ptn_pk,LocationID,Visit_pk,Visit_Date, [" + obj.ToString().Substring(ispos, iepos) + "])";
                                        sqlselect += " VALUES (" + PatID.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'," + str.ToString() + ")";
                                        try
                                        {
                                            CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                                            icount = CustomFields.SaveCustomFieldValues(sqlselect.ToString());
                                            if (icount == -1)
                                            {
                                                return;
                                            }

                                        }
                                        catch
                                        {
                                        }
                                        finally
                                        {
                                            CustomFields = null;
                                        }
                                    }
                                }
                            }
                        }
                        p += 1;
                    }
                }
            }
        }

        /// <summary>
        /// Generates the custom fields values.
        /// </summary>
        /// <param name="Cntrl">The CNTRL.</param>
        private void GenerateCustomFieldsValues(Control Cntrl)
        {
            string pnlName = Cntrl.ID;
            sbValues = new StringBuilder();
            strmultiselect = string.Empty;
            string strfName = string.Empty;
            Boolean radioflag = false;

            Int32 stpos = 0;
            Int32 enpos = 0;
            if (ViewState["CustomFieldsData"] != null)
            {
                foreach (Control x in Cntrl.Controls)
                {
                    if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                    {
                        if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "TXT")
                        {
                            strfName = pnlName.ToUpper() + "TXT";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = x.ID.Substring(stpos, enpos).ToString();
                            if (((TextBox)x).Text != "")
                            {
                                sbValues.Append(",[" + strfName + "] = '" + ((TextBox)x).Text.ToString() + "'");
                            }
                            else
                            {
                                sbValues.Append(",[" + strfName + "] = ' " + "'");
                            }
                        }
                        else if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "NUM")
                        {
                            strfName = pnlName.ToUpper() + "NUM";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = x.ID.Substring(stpos, enpos).ToString();
                            if (((TextBox)x).Text != "")
                            {
                                sbValues.Append(",[" + strfName + "]=" + ((TextBox)x).Text.ToString());
                            }
                            else
                            {
                                sbValues.Append("," + strfName + "=Null");
                            }

                        }
                        else if (x.ID.Substring(0, 15).ToString().ToUpper() == pnlName.ToUpper() + "DT")
                        {
                            strfName = pnlName.ToUpper() + "DT";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = x.ID.Substring(stpos, enpos).ToString();
                            if (((TextBox)x).Text != "")
                            {
                                sbValues.Append(",[" + strfName + "]='" + Convert.ToDateTime(((TextBox)x).Text.ToString()) + "'");
                            }
                            else
                            {
                                sbValues.Append(",[" + strfName + "]=" + "Null");
                            }
                        }
                    }
                    if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                    {
                        if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO1")
                        {
                            radioflag = false;
                            strfName = pnlName.ToUpper() + "RADIO1";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = x.ID.Substring(stpos, enpos).ToString();
                            if (((HtmlInputRadioButton)x).Checked == true)
                            {
                                sbValues.Append(",[" + strfName + "]=" + "1");
                                radioflag = true;
                            }
                        }
                        if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO2")
                        {
                            strfName = pnlName.ToUpper() + "RADIO2";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = x.ID.Substring(stpos, enpos).ToString();
                            if (((HtmlInputRadioButton)x).Checked == true)
                            {
                                sbValues.Append(",[" + strfName + "]=" + "0");
                                radioflag = true;
                            }
                            if (radioflag == false)
                            {
                                sbValues.Append(",[" + strfName + "]=" + "Null");
                            }
                        }

                    }
                    if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                    {
                        if (x.ID.Substring(0, 23).ToString().ToUpper() == pnlName.ToUpper() + "SELECTLIST")
                        {
                            strfName = pnlName.ToUpper() + "SELECTLIST";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = x.ID.Substring(stpos, enpos).ToString();
                            if (((DropDownList)x).SelectedValue != "0")
                            {
                                sbValues.Append(",[" + strfName + "] = " + ((DropDownList)x).SelectedValue.ToString() + " ");
                            }
                            else
                            {
                                sbValues.Append(",[" + strfName + "] =  " + "0");
                            }

                        }
                    }

                }
            }

            if (ViewState["CustomFieldsData"] == null)
            {
                foreach (Control x in Cntrl.Controls)
                {
                    if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                    {
                        if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "TXT")
                        {
                            if (((TextBox)x).Text != "")
                            {
                                sbValues.Append(",'" + ((TextBox)x).Text.ToString() + "'");
                            }
                            else
                            {
                                sbValues.Append(",' " + "'");
                            }
                        }
                        else if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "NUM")
                        {
                            if (((TextBox)x).Text != "")
                            {
                                sbValues.Append("," + ((TextBox)x).Text.ToString());
                            }
                            else
                            {
                                sbValues.Append(",0");
                            }

                        }
                        else if (x.ID.Substring(0, 15).ToString().ToUpper() == pnlName.ToUpper() + "DT")
                        {
                            if (((TextBox)x).Text != "")
                            {
                                sbValues.Append(",'" + Convert.ToDateTime(((TextBox)x).Text.ToString()) + "'");
                            }
                            else
                            {
                                sbValues.Append(",Null");
                            }
                        }
                    }
                    if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                    {
                        if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO1")
                        {
                            radioflag = false;
                            if (((HtmlInputRadioButton)x).Checked == true)
                            {
                                sbValues.Append(",1");
                                radioflag = true;
                            }
                        }
                        if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO2")
                        {
                            if (((HtmlInputRadioButton)x).Checked == true)
                            {
                                sbValues.Append(",0");
                                radioflag = true;
                            }
                            if (radioflag == false)
                            {
                                sbValues.Append(",Null");
                            }
                        }
                    }
                    if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                    {
                        if (x.ID.Substring(0, 23).ToString().ToUpper() == pnlName.ToUpper() + "SELECTLIST")
                        {
                            if (((DropDownList)x).SelectedValue != "0")
                            {
                                sbValues.Append("," + ((DropDownList)x).SelectedValue.ToString() + " ");
                            }
                            else
                            {
                                sbValues.Append(", " + "0");
                            }

                        }
                    }
                }
            }
            if (ViewState["CustomFieldsMulti"] != null || ViewState["CustomFieldsMulti"] == null)
            {
                foreach (Control x in Cntrl.Controls)
                {
                    if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBoxList))
                    {

                        if (x.ID.Substring(0, 28).ToString().ToUpper() == pnlName.ToUpper() + "MULTISELECTLIST")
                        {

                            foreach (ListItem li in ((CheckBoxList)x).Items)
                            {
                                if (Convert.ToInt32(li.Selected) == 1)
                                {
                                    strmultiselect += " " + li.Value.ToString() + ",";
                                }
                            }
                            strmultiselect += "^";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Fills the old data.
        /// </summary>
        /// <param name="PatID">The pat identifier.</param>
        private void FillOldData(Int32 PatID)
        {
            DataSet dsvalues = null;
            ICustomFields CustomFields;

            try
            {
                DataSet theCustomFields = (DataSet)ViewState["CustomFieldsDS"];
                string theTblName = "";
                if (theCustomFields.Tables[0].Rows.Count > 0)
                    theTblName = theCustomFields.Tables[0].Rows[0]["FeatureName"].ToString().Replace(" ", "_");
                string theColName = "";
                foreach (DataRow theDR in theCustomFields.Tables[0].Rows)
                {
                    if (theDR["ControlId"].ToString() != "9")
                    {
                        if (theColName == "")
                            theColName = theDR["Label"].ToString();
                        else
                            theColName = theColName + "," + theDR["Label"].ToString();
                    }
                }

                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");

                //todo AMit extra
                int visitPKval = Convert.ToInt32(Session["PatientVisitId"]);

                dsvalues = CustomFields.GetCustomFieldValues("dtl_CustomField_" + theTblName.ToString().Replace("-", "_"), theColName, Convert.ToInt32(PatID.ToString()), 0, visitPKval, 0, 0, Convert.ToInt32(ApplicationAccess.ARTHistory));
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomManager.FillCustomFieldData(theCustomFields, dsvalues, pnlCustomList, "AHistory");
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
            finally
            {
                CustomFields = null;
            }
        }
    }
}