using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Interface.Clinical;
using Application.Presentation;
using System.Data;
using System.Configuration;
using Application.Common;

namespace IQCare.Web.Clinical
{

    /// <summary>
    /// 
    /// </summary>
    public partial class PatientWaitingList : System.Web.UI.Page
    {

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (IsPostBack) return;
            Session["dtWaitingList"]=null;
            Session["WLTechnicalArea"]=null;
            Session["WLTechnicalAreaName"] = null;
            Session["WLPatientID"] = 0;
            String pID = Session["PatientId"].ToString();
            if (pID == "0")
                pID = Request.QueryString["PID"];
            if (Session["PatientInformation"] == null)
            {
                IPatientHome PatientManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
                //System.Data.DataSet theDS = PatientManager.GetPatientDetails(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["SystemId"]));
                System.Data.DataSet theDS = PatientManager.GetPatientDetails(Convert.ToInt32(pID), Convert.ToInt32(Session["SystemId"]), Convert.ToInt32(Session["TechnicalAreaId"]));
                PatientManager = null;
                Session["PatientInformation"] = theDS.Tables[0];
            }
            BindQueues();
            DataTable dtPatientInfo = (DataTable)Session["PatientInformation"];
            if (dtPatientInfo != null)
            {
               
              
                lblname.Text = String.Format("{0}, {1}", dtPatientInfo.Rows[0]["LastName"], dtPatientInfo.Rows[0]["FirstName"]);

                lblIQnumber.Text = dtPatientInfo.Rows[0]["IQNumber"].ToString();
                if (Request.QueryString["srvNm"] != null)
                {
                    lblTechnicalArea.Text = Request.QueryString["srvNm"];
                    Session["WLTechnicalArea"] = Request.QueryString["mod"];
                    Session["WLTechnicalAreaName"] = Request.QueryString["srvNm"];
                    Session["WLPatientID"] = Request.QueryString["PID"];
                }
                else
                {
                    lblTechnicalArea.Text = Session["TechnicalAreaName"].ToString();
                    Session["WLTechnicalArea"] = Session["TechnicalAreaId"];
                    Session["WLTechnicalAreaName"] = Session["TechnicalAreaName"];
                    Session["WLPatientID"] = HttpContext.Current.Session["PatientId"];
                }

                LoadPatientsWaitList(Convert.ToInt32(HttpContext.Current.Session["WLPatientID"]));
            }

          


            

        }
        void BindQueues()
        {
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("~\\XMLFiles\\AllMasters.con"));

            DataView theDV = new DataView();
            DataTable theDT = new DataTable();

            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "DeleteFlag=0 and ListName='Waiting List' and (SystemID=0 or SystemID=" + Session["SystemId"] + ")";
            if (theDV.Table != null)
            {
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCombo(ddWList, theDT, "Name", "ID");
                theDV.Dispose();
                theDT.Clear();
            }

        }
        /// <summary>
        /// Loads the patients wait list.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        private void LoadPatientsWaitList(int patientID)
        {
            IPatientHome PManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            System.Data.DataTable theDt = PManager.GetPatientWaitList(patientID);
           
                grdWaitingList.DataSource = theDt;
                Session["dtWaitingList"] = theDt;
                grdWaitingList.DataBind();

        }
        /// <summary>
        /// Handles the RowDeleting event of the grdWaitingList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewDeleteEventArgs"/> instance containing the event data.</param>
        protected void grdWaitingList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            DataTable theDT = (DataTable)Session["dtWaitingList"];

            DataRow rowDelete = theDT.Rows[e.RowIndex];

            if (Convert.ToInt32(rowDelete["Persisted"]) == 1)
            {
                rowDelete["RowStatus"] = "3";
                rowDelete.AcceptChanges();
            }
            else
            {
                theDT.Rows.RemoveAt(e.RowIndex);
            }
            BindWaitingGrid();
        }

        /// <summary>
        /// Binds the waiting grid.
        /// </summary>
        void BindWaitingGrid()
        {
            DataTable theMainDT = (DataTable)Session["dtWaitingList"];
            DataView dv = theMainDT.DefaultView;
            dv.RowFilter = "RowStatus = '0'";
            DataTable theDT = dv.ToTable();
          
                grdWaitingList.DataSource = theDT;
                grdWaitingList.DataBind();
         
            
        }


        /// <summary>
        /// Handles the Sorting event of the grdWaitingList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewSortEventArgs"/> instance containing the event data.</param>
        protected void grdWaitingList_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        /// <summary>
        /// Handles the RowDataBound event of the grdWaitingList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void grdWaitingList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        /// <summary>
        /// Gets the revist HRS allowance.
        /// </summary>
        /// <value>
        /// The revist HRS allowance.
        /// </value>
        int RevistHrsAllowance
        {
            get
            {
                int hrs = 24;
                if (ConfigurationManager.AppSettings["RevisitHoursAllowance"] == null)
                    return hrs;
                int.TryParse(ConfigurationManager.AppSettings.Get("RevisitHoursAllowance").ToLower(), out hrs);
                return hrs;
            }
        }
        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        int UserId
        {
            get
            {
                return Convert.ToInt32(Session["AppUserId"]);
            }
        }
        /// <summary>
        /// Gets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        int LocationId
        {
            get
            {
                return Convert.ToInt32(Session["AppLocationId"]);
            }
        }
        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            try
            {
                //Revist Date.
                DateTime now = DateTime.Now;
                //Entry point
                int moduleID = Convert.ToInt32(Session["WLTechnicalArea"]);

                int patientID = Convert.ToInt32(HttpContext.Current.Session["WLPatientID"]);
                //UserID
                IPatientHome pMGR;
                pMGR = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
                pMGR.SavePatientRevisit(now, this.RevistHrsAllowance, moduleID, this.UserId, patientID, this.LocationId);

                
            }
            catch //(Exception err)
            {

               // MsgBuilder theBuilder = new MsgBuilder();
               // theBuilder.DataElements["MessageText"] = err.Message.ToString();
               // IQCareMsgBox.Show("#C1", theBuilder, this);
            }
            try
            {
                IPatientHome WListManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");

                WListManager.SavePatientWaitList((DataTable)Session["dtWaitingList"], 
                    Convert.ToInt32(Session["WLTechnicalArea"]), 
                    Convert.ToInt32(base.Session["AppUserId"]), 
                    Convert.ToInt32(HttpContext.Current.Session["WLPatientID"]));

                IQCareMsgBox.Show("Successfully saved ", "!", "", this);
            }
            catch (Exception ex)
            {
                IQCareMsgBox.Show("Error Occurred " + ex.Message, "!", "", this);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {

           DataRow[] foundRows;
            //validate waiting list item selected
           if (ddWList.SelectedItem.Text.Trim() == "Select")
           {
               ddWList.BorderColor = System.Drawing.Color.Red;
               ddWList.BackColor = System.Drawing.Color.Orange;
               return;
           }
           else
           {
               ddWList.BorderColor = System.Drawing.Color.Black;
               ddWList.BackColor = System.Drawing.Color.White;
           }

                 
           
          
            DataTable theDT = new DataTable();
            theDT = (DataTable)Session["dtWaitingList"];
            foundRows = theDT.Select(String.Format("ListID='{0}' and ModuleID='{1}' and RowStatus=0", ddWList.SelectedItem.Value, Session["WLTechnicalArea"]));
            
                if (foundRows.Length < 1)
                {
                    // a new list item is added since its not there

                   
                    DataRow theDR = theDT.NewRow();

                    theDR["ListName"] = ddWList.SelectedItem.Text;
                    theDR["ModuleName"] = Session["WLTechnicalAreaName"].ToString();
                    theDR["ModuleID"] = int.Parse(Session["WLTechnicalArea"].ToString());
                    theDR["AddedBy"] = Session["AppUserName"].ToString();
                    theDR["ListID"] = ddWList.SelectedItem.Value;
                    theDR["Priority"] = ddPriority.SelectedItem.Value;
                    theDR["RowStatus"] = 0;
                    theDR["Persisted"] = 0;
                    
                  
                    

                    theDT.Rows.Add(theDR);

                    Session["dtWaitingList"] = theDT;
                    BindWaitingGrid();
                }
                else
                {
                    IQCareMsgBox.Show("Patient is already in " + ddWList.SelectedItem.Text + " waiting list", "!", "", this);
                    
                    return;
                }
            
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the grdWaitingList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void grdWaitingList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
