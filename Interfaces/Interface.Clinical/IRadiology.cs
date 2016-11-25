using System;
using System.Data;

namespace Interface.Clinical
{
    /// <summary>
    /// 
    /// </summary>
  public  interface IRadiology
    {

        /// <summary>
        /// Finds the name of the x ray by.
        /// </summary>
        /// <param name="SearchText">The search text.</param>
        /// <returns></returns>
      DataTable FindXRayByName(string SearchText);

      /// <summary>
      /// Saves the x ray request.
      /// </summary>
      /// <param name="XRaysRequested">The x rays requested.</param>
      /// <param name="UserID">The user identifier.</param>
      /// <param name="PatientID">The patient identifier.</param>
      /// <param name="RequestedDate">The requested date.</param>
      /// <returns></returns>
      string SaveXRayRequest(int PatientID, int LocationID, int ModuleID, int UserID, int RequestedBy, DateTime RequestedDate, DataTable XRaysRequested);
        
      /// <summary>
      /// Updates the x ray request.
      /// </summary>
      /// <param name="XRaysUpdated">The x rays updated.</param>
      /// <param name="UserID">The user identifier.</param>
      /// <param name="PatientID">The patient identifier.</param>
      /// <param name="RequestedDate">The requested date.</param>
      /// <returns></returns>
      void UpdateXRayRequest(int OrderID, int UserID, int UpdatedBy, DateTime DateUpdated, DataTable XRaysUpdated);

      void UpdateXRayResults(int OrderID, int UserID, int UpdatedBy, DateTime DateDone, DataTable XRaysUpdated);

      void UpdateXRayAnalysis(int OrderID, int UserID, int UpdatedBy, DateTime DateAnalysed, DataTable XRaysUpdated);

      DataTable GetXRaysOrders(int LocationID, int? OrderID = null, int? PatientID = null, DateTime? DateFrom =null, DateTime? DateTo = null, string Status = "All");

      DataTable GetXRaysOrderDetails(int OrderID);

    }
}
