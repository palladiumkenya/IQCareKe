using System.Collections.Generic;
using System.Data;
using Entities.Lab;
using System;

namespace Interface.Laboratory
{
    public interface ILabRepo
    {
        List<LabOrder> GetAll(Entities.Common.IFilter orderFilters);
    }
    /// <summary>
    /// Lab Request Interface
    /// </summary>
    public interface ILabRequest
    {
        /// <summary>
        /// Finds the name of the lab by.
        /// </summary>
        /// <param name="SearchText">The search text.</param>
        /// <param name="IncludeDepartment">The include department.</param>
        /// <param name="ExcludeDepartment">The exclude department.</param>
        /// <returns></returns>
        DataTable FindLabByName(string SearchText, bool excludeGroup=false);

        /// <summary>
        /// Gets the lab order.
        /// </summary>
        /// <param name="LabOrderId">The lab order identifier.</param>
        /// <returns></returns>
       // DataSet GetLabOrder(int LabOrderId);

        /// <summary>
        /// Gets the lab order.
        /// </summary>
        /// <param name="LabOrderId">The lab order identifier.</param>
        /// <param name="PatientId">The patient identifier.</param>
        /// <returns></returns>
       LabOrder GetLabOrder(int locationId,int LabOrderId);
       /// <summary>
       /// Gets the lab orders.
       /// </summary>
       /// <param name="patientId">The patient identifier.</param>
       /// <returns></returns>
       List<LabOrder> GetLabOrders(int locationId,int? patientId);
       /// <summary>
       /// Gets the lab orders.
       /// </summary>
       /// <param name="locationId">The location identifier.</param>
       /// <param name="orderStatus">The order status.</param>
       /// <param name="dateFrom">The date from.</param>
       /// <param name="dateTo">The date to.</param>
       /// <returns></returns>
       List<LabOrder> GetLabOrders(int locationId, string orderStatus,DateTime? dateFrom = null, DateTime? dateTo = null);
       /// <summary>
       /// Gets the ordered tests.
       /// </summary>
       /// <param name="orderId">The order identifier.</param>
       /// <returns></returns>
       List<LabOrderTest> GetOrderedTests(int orderId, int? labOrderTestId= null);
        /// <summary>
        /// Gets the lab test parameter result.
        /// </summary>
        /// <param name="LabTestOrderId">The lab test order identifier.</param>
        /// <returns></returns>
        List<LabTestParameterResult> GetLabTestParameterResult(int LabTestOrderId);

        /// <summary>
        /// Gets the lab test parameter result.
        /// </summary>
        /// <param name="LabTestOrderId">The lab test order identifier.</param>
        /// <param name="LabTestId">The lab test identifier.</param>
        /// <returns></returns>
       // DataTable GetLabTestParameterResult(int LabTestOrderId, int LabTestId);

        /// <summary>
        /// Saves the lab order.
        /// </summary>
        /// <param name="labOrder">The lab order.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="LocationId">The location identifier.</param>
        /// <returns></returns>
     //   DataSet SaveLabOrder(LabOrder labOrder, int UserId, int LocationId);

        /// <summary>
        /// Saves the lab order.
        /// </summary>
        /// <param name="labOrder">The lab order.</param>
        /// <returns></returns>
        LabOrder SaveLabOrder(LabOrder labOrder, int UserId, int LocationId);

        /// <summary>
        /// Saves the lab results.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <param name="LabTestOrderId">The lab test order identifier.</param>
        /// <param name="ResultNotes">The result notes.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        int SaveLabResults(List<LabTestParameterResult> results, int LabTestOrderId, string ResultNotes, int userId,int ResultBy,
            DateTime ResultDate);
    }
}