using System;
using System.Data;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.Clinical;
using Application.Common;

namespace BusinessProcess.Clinical
{
    /// <summary>
    /// 
    /// </summary>
    public class BRadiology : ProcessBase, IRadiology
    {
        /// <summary>
        /// Finds the name of the x ray by.
        /// </summary>
        /// <param name="SearchText">The search text.</param>
        /// <returns></returns>
        public DataTable FindXRayByName(string SearchText)
        {
            lock (this)
            {
                ClsObject theON = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@SearchText", SqlDbType.VarChar, SearchText);
                return (DataTable)theON.ReturnObject(ClsUtility.theParams, "pr_Radiology_GetXRayByName", ClsUtility.ObjectEnum.DataTable);
            }
        }

        /// <summary>
        /// Saves the x ray request.
        /// </summary>
        /// <param name="XRaysRequested">The x rays requested.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="PatientID">The patient identifier.</param>
        /// <param name="RequestedDate">The requested date.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string SaveXRayRequest(int PatientID, int LocationID, int ModuleID, int UserID, int RequestedBy, DateTime RequestedDate, DataTable XRaysRequested)
        {
            lock (this)
            {
                try
                {
                    System.Text.StringBuilder sbItems = new System.Text.StringBuilder("<root>");
                    foreach (DataRow row in XRaysRequested.Rows)
                    {

                        sbItems.Append("<xray>");
                        sbItems.Append("<xrayid>" + row["XRayID"].ToString() + "</xrayid>");
                        sbItems.Append("<requestnotes>" + row["RequestNotes"].ToString() + "</requestnotes>");

                        sbItems.Append("</xray>");
                    }
                    sbItems.Append("</root>");
                    ClsObject theON = new ClsObject();
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddExtendedParameters("@PatientID", SqlDbType.Int, PatientID);
                    ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, LocationID);
                    ClsUtility.AddExtendedParameters("@ModuleID", SqlDbType.Int, ModuleID);
                    ClsUtility.AddExtendedParameters("@UserID", SqlDbType.Int, UserID);
                    ClsUtility.AddExtendedParameters("@OrderedBy", SqlDbType.Int, RequestedBy);
                    ClsUtility.AddExtendedParameters("@OrderDate", SqlDbType.DateTime, RequestedDate);
                    ClsUtility.AddExtendedParameters("@XRayOrdered", SqlDbType.Xml, sbItems.ToString());
                    DataTable dt = (DataTable)theON.ReturnObject(ClsUtility.theParams, "pr_Radiology_SaveRequest",
                        ClsUtility.ObjectEnum.DataTable);
                    string r = "";

                    r = dt.Rows[0][0].ToString();

                    return r;

                }
                catch
                {
                    throw;
                }

            }
        }

        /// <summary>
        /// Updates the x ray request.
        /// </summary>
        /// <param name="XRaysUpdated">The x rays updated.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="PatientID">The patient identifier.</param>
        /// <param name="RequestedDate">The requested date.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public void UpdateXRayRequest(int OrderID, int UserID, int DoneBy, DateTime DateDone, DataTable XRaysUpdated)
        {
            lock (this)
            {
                try
                {

                    
                    System.Text.StringBuilder sbItems = new System.Text.StringBuilder("<root>");
                    foreach (DataRow row in XRaysUpdated.Rows)
                    {

                        sbItems.Append("<xray>");
                        sbItems.Append("<xrayid>" + row["XRayID"].ToString() + "</xrayid>");
                        sbItems.Append("<requestnotes>" + row["RequestNotes"].ToString() + "</requestnotes>");
                         sbItems.Append("<xraynotes>" + row["XRayNotes"].ToString() + "</xraynotes>");
                         sbItems.Append("<xraydate>" + row["XRayDate"].ToString() + "</xraydate>");
                         sbItems.Append("<rowstatus>" + row["RowStatus"].ToString() + "</rowstatus>");
                         sbItems.Append("<clinicalnotes>" + row["ClinicalNotes"].ToString() + "</clinicalnotes>");
                         sbItems.Append("<analysisdate>" + row["AnalysisDate"].ToString() + "</analysisdate>");
                        sbItems.Append("</xray>");
                    }
                    sbItems.Append("</root>");

                    ClsObject theON = new ClsObject();
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddExtendedParameters("@OrderID", SqlDbType.Int, OrderID);
                    ClsUtility.AddExtendedParameters("@UserID", SqlDbType.Int, UserID);
                    ClsUtility.AddExtendedParameters("@DoneBy", SqlDbType.Int, DoneBy);
                    ClsUtility.AddExtendedParameters("@DateDone", SqlDbType.DateTime, DateDone);
                    ClsUtility.AddExtendedParameters("@XRaysUpdated", SqlDbType.Xml, sbItems.ToString());
                    theON.ReturnObject(ClsUtility.theParams, "pr_Radiology_UpdateRequest", ClsUtility.ObjectEnum.ExecuteNonQuery);

                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Gets the x rays order details.
        /// </summary>
        /// <param name="OrderID">The order identifier.</param>
        /// <returns></returns>
        public DataTable GetXRaysOrderDetails(int OrderID)
        {
            lock (this)
            {
                ClsObject theON = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@OrderID", SqlDbType.Int, OrderID);
                return (DataTable)theON.ReturnObject(ClsUtility.theParams, "pr_Radiology_GetRequestDetails", ClsUtility.ObjectEnum.DataTable);
            }
        }

        /// <summary>
        /// Gets the x rays orders.
        /// </summary>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="PatientID">The patient identifier.</param>
        /// <param name="DateFrom">The date from.</param>
        /// <param name="DateTo">The date to.</param>
        /// <param name="Status">The status.</param>
        /// <returns></returns>
        public DataTable GetXRaysOrders(int LocationID, int? OrderID = null, int? PatientID = null, DateTime? DateFrom = null, DateTime? DateTo = null, string Status = "All")
        {
            lock (this)
            {
                try
                {
                    ClsObject theON = new ClsObject();
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, LocationID);
                    if (OrderID.HasValue)
                        ClsUtility.AddExtendedParameters("@OrderID", SqlDbType.Int, OrderID.Value);
                    if (PatientID.HasValue)
                        ClsUtility.AddExtendedParameters("@PatientID", SqlDbType.Int, PatientID.Value);
                    if (DateFrom.HasValue)
                        ClsUtility.AddExtendedParameters("@DateFrom", SqlDbType.Int, DateFrom.Value);
                    if (DateTo.HasValue)
                        ClsUtility.AddExtendedParameters("@DateTo", SqlDbType.DateTime, DateTo.Value);
                    ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                    ClsUtility.AddParameters("@ProcessStatus", SqlDbType.VarChar, Status);
                    return (DataTable)theON.ReturnObject(ClsUtility.theParams, "pr_Radiology_GetRequest", ClsUtility.ObjectEnum.DataTable);

                }
                catch
                {
                    throw;
                }
            }
        }




        public void UpdateXRayAnalysis(int OrderID, int UserID, int UpdatedBy, DateTime DateAnalysed, DataTable XRaysUpdated)
        {
            throw new NotImplementedException();
        }

        public void UpdateXRayResults(int OrderID, int UserID, int UpdatedBy, DateTime DateDone, DataTable XRaysUpdated)
        {
            lock (this)
            {
                try
                {


                    System.Text.StringBuilder sbItems = new System.Text.StringBuilder("<root>");

                    DataView dv = XRaysUpdated.DefaultView;
                    dv.RowFilter = "RowStatus = 'Modified'";
                    DataTable theDT = dv.ToTable();
                    foreach (DataRow row in theDT.Rows)
                    {

                        sbItems.Append("<xray>");
                        sbItems.Append("<xrayid>" + row["XRayID"].ToString() + "</xrayid>");
                        sbItems.Append("<id>" + row["ID"].ToString() + "</id>"); 
                        sbItems.Append("<xraynotes>" + row["XRayNotes"].ToString() + "</xraynotes>");                        
                        sbItems.Append("</xray>");
                    }
                    sbItems.Append("</root>");

                    ClsObject theON = new ClsObject();
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddExtendedParameters("@OrderID", SqlDbType.Int, OrderID);
                    ClsUtility.AddExtendedParameters("@UserID", SqlDbType.Int, UserID);
                    ClsUtility.AddExtendedParameters("@DoneBy", SqlDbType.Int, UpdatedBy);
                    ClsUtility.AddExtendedParameters("@DateDone", SqlDbType.DateTime, DateDone);
                    ClsUtility.AddExtendedParameters("@XRaysUpdated", SqlDbType.Xml, sbItems);
                    theON.ReturnObject(ClsUtility.theParams, "pr_Radiology_UpdateResults", ClsUtility.ObjectEnum.ExecuteNonQuery);

                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
