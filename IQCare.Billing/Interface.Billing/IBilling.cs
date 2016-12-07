using System;
using System.Collections.Generic;
using System.Data;
using Entities.Billing;
using Entities.Common;

namespace Interface.Billing
{
   
    /// <summary>
    /// 
    /// </summary>
    public interface IBilling
    {
             
        ///// <summary>    
        /// Gets the open bilss.
        /// </summary>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="DateFrom">The date from.</param>
        /// <param name="DateTo">The date to.</param>
        /// <returns></returns>
        DataTable GetPatientWithUnpaidItems(int locationId, DateTime? dateFrom, DateTime? dateTo);
        /// <summary>
        /// Gets all billable items.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="ItemTypeID">The item type identifier.</param>
        /// <param name="ExcludeItemTypeID">The exclude item type identifier.</param>
        /// <param name="PriceDate">The price date.</param>
        /// <param name="WithPriceOnly">if set to <c>true</c> [with price only].</param>
        /// <returns></returns>
       // DataTable GetAllBillableItems(String filter, int? ItemTypeID = null, int? ExcludeItemTypeID = null, DateTime? PriceDate=null, bool? WithPriceOnly =true);      
        /// <summary>
        /// Gets the price list.
        /// </summary>
        /// <param name="itemType">Type of the item.</param>
        /// <returns></returns>
        DataTable GetPriceList(int itemType);
        /// <summary>
        /// Gets the price list.
        /// </summary>
        /// <param name="ItemTypeID">The item type identifier.</param>
        /// <param name="ItemName">Name of the item.</param>
        /// <param name="WithPriceOnly">if set to <c>true</c> [with price only].</param>
        /// <returns></returns>
        ResultSet<SaleItem> GetPriceList(int? itemTypeId, DateTime? priceDate = null,string ItemName = "", bool WithPriceOnly = false, Pager page = null);
        /// <summary>
        /// Gets the price list XML.
        /// </summary>
        /// <param name="facilityName">Name of the facility.</param>
        /// <param name="UserName">Name of the user.</param>
        /// <returns></returns>
        String GetPriceListXML(string facilityName, string userName,DateTime? priceDate = null);
        /// <summary>
        /// Saves the price list.
        /// </summary>
        /// <param name="dtPriceList">The dt price list.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        int SavePriceList(DataTable dtPriceList, int userId);
        /// <summary>
        /// Saves the price list.
        /// </summary>
        /// <param name="itemList">The item list.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        int SavePriceList(List<SaleItem> itemList, int userId);
        /// <summary>
        /// Saves the patient bill payments.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="paymentInfo">The payment information.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns></returns>
        Receipt SavePatientBillPayments(int patientId, BillPaymentInfo paymentInfo,  int userId);

        Price GetItemPrice(int itemId, int itemTypeId, DateTime? priceDate = null);

        #region Reports
        /// <summary>
        /// Gets the reciept.
        /// </summary>
        /// <param name="TransactionID">The transaction identifier.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <param name="receiptType">Type of the receipt.</param>
        /// <returns></returns>
        //DataSet GetReceipt(int TransactionID, int locationID, ReceiptType receiptType = ReceiptType.BillPayment);

        /// <summary>
        /// Gets the receipt.
        /// </summary>
        /// <param name="ReceiptNumber">The receipt number.</param>
        /// <returns></returns>
        Receipt GetReceipt(string receiptNumber);
        /// <summary>
        /// Generates the receipt.
        /// </summary>
        /// <param name="TransactionId">The transaction identifier.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="?">The ?.</param>
        /// <returns></returns>
        Receipt GenerateReceipt(int transactionId, int userId, ReceiptType receiptType);
        

        /// <summary>
        /// Gets the billing report.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <param name="queryName">Name of the query.</param>
        /// <returns></returns>
        DataSet GetBillingReport(DateTime fromDate, DateTime toDate, int locationId, string queryName, bool HasPatientData=false);
        #endregion

        #region PaidItems
        /// <summary>
        /// Determines whether [is visit type paid] [the specified visittype identifier].
        /// </summary>
        /// <param name="visittypeID">The visittype identifier.</param>
        /// <param name="patientID">The patient identifier.</param>
        /// <returns></returns>
        DataTable isVisitTypePaid(int visittypeID, int patientID);
        /// <summary>
        /// Gets the paid labs.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <returns></returns>
        DataTable GetPaidLabs(int patientID);
        /// <summary>
        /// Gets the paid drugs.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <returns></returns>
        DataTable GetPaidDrugs(int patientID);  
        #endregion    
      
        #region Reversal
        /// <summary>
        /// Requests the bill reversal.
        /// </summary>
        /// <param name="TransactionID">The transaction identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="ReversalReason">The reversal reason.</param>
        /// <param name="RequestDate">The request date.</param>
        /// <param name="ItemToReverse">The item to reverse.</param>
        void RequestTransactionReversal(int TransactionID, int UserID, string ReversalReason, DateTime RequestDate, List<int> ItemToReverse = null);
        /// <summary>
        /// Approves the bill reversal.
        /// </summary>
        /// <param name="ReversalID">The reversal identifier.</param>
        /// <param name="TransactionID">The transaction identifier.</param>
        /// <param name="Approved">if set to <c>true</c> [approved].</param>
        /// <param name="ApproverID">The approver identifier.</param>
        /// <param name="ApprovalReason">The approval reason.</param>
        /// <param name="ApprovalDate">The approval date.</param>
        /// <param name="RefundCash">if set to <c>true</c> [refund cash].</param>
        Receipt ApproveRejectTransactionReversal(int ReversalID, int TransactionID, bool Approved, int ApproverID, string ApprovalReason, DateTime ApprovalDate, bool RefundCash=false);
        /// <summary>
        /// Itemses to be reversed.
        /// </summary>
        /// <param name="ReversalID">The reversal identifier.</param>
        /// <returns></returns>
        DataTable ItemsToBeReversed(int ReversalID);
        /// <summary>
        /// Gets the reversal requests.
        /// </summary>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="ReversalReference">The reversal reference.</param>
        /// <param name="FilterOption">The filter option.</param>
        /// <param name="PatientID">The patient identifier.</param>
        /// <returns></returns>
        DataTable GetReversalRequests(int LocationID, string ReversalReference = "", string FilterOption = "ALL", int? PatientID=null);

        /// <summary>
        /// Refunds the cash.
        /// </summary>
        /// <param name="ReversalId">The reversal identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        Receipt RefundCash(int ReversalId, int UserID);
        #endregion

        /// <summary>
        /// Saves the patient payable items.
        /// </summary>
        /// <param name="billitems">The billitems.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns></returns>
        int SavePatientPayableItems(DataTable billitems, int userId);
        /// <summary>
        /// Gets the patient un billed items.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="LocationID">The location identifier.</param>
        /// <returns></returns>
        DataTable GetPatientUnBilledItems(int patientId, int locationId);
        /// <summary>
        /// Generates the bill.
        /// </summary>
        /// <param name="billItems">The bill items.</param>
        /// <param name="PatientID">The patient identifier.</param>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns></returns>
        string GenerateBill(DataTable billItems, int patientId,int locationId, int userId);
        /// <summary>
        /// Gets the patient bill by status.
        /// </summary>
        /// <param name="PatientID">The patient identifier.</param>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="billStatus">The bill status.</param>
        /// <returns></returns>
        DataTable GetPatientBillByStatus(int patientId, int locationId, BillStatus billStatus);
        /// <summary>
        /// Gets the bill items.
        /// </summary>
        /// <param name="BillID">The bill identifier.</param>
        /// <returns></returns>
        DataTable GetBillItems(int billId);
        /// <summary>
        /// Gets the bill transactions.
        /// </summary>
        /// <param name="BillID">The bill identifier.</param>
        /// <returns></returns>
        DataTable GetBillTransactions(int billId);
        /// <summary>
        /// Cancels the bill.
        /// </summary>
        /// <param name="BillID">The bill identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        void CancelBill(int billId, int userId);
        /// <summary>
        /// Gets the bill details.
        /// </summary>
        /// <param name="BillID">The bill identifier.</param>
        /// <param name="PatientID">The patient identifier.</param>
        /// <returns></returns>
        DataTable GetBillDetails(int billId);
        /// <summary>
        /// Removes the item from bill.
        /// </summary>
        /// <param name="billItemID">The bill item identifier.</param>
        /// <param name="BillID">The bill identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        void RemoveItemFromBill(int billItemId, int billId, int userId);
        /// <summary>
        /// Removes the item from bill.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="UserID">The user identifier.</param>
        void RemoveItemFromBill(List<BillItem> items, int userId);
      /// <summary>
        /// Gets the invoice.
        /// </summary>
        /// <param name="BillID">The bill identifier.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <param name="patientID">The patient identifier.</param>
        /// <returns></returns>
        DataSet GetInvoice(int billId, int locationId, int patientId);
        /// <summary>
        /// Gets the item price.
        /// </summary>
        /// <param name="ItemID">The item identifier.</param>
        /// <param name="ItemTypeID">The item type identifier.</param>
        /// <param name="BillingDate">The billing date.</param>
        /// <returns></returns>
        double GetItemPrice(int itemId, int itemTypeid, DateTime billingDate);

        #region Deposits
        /// <summary>
        /// Gets the patient deposit.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <returns></returns>
        DataTable GetPatientDeposit(int patientID, int locationID);

        /// <summary>
        /// Gets the patient deposit transactions.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <returns></returns>
        DataTable GetPatientDepositTransactions(int patientId, int locationId);

        /// <summary>
        /// Executes the deposit transaction.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="transactionType">Type of the transaction.</param>
        /// <param name="clsObject">The CLS object.</param>
        /// <returns></returns>
        Receipt ExecuteDepositTransaction(int patientId, int locationId, int userId, decimal amount, DepositTransactionType transactionType, object clsObject=null);
        #endregion
      //  DataSet getSalesSummary(DateTime fromDate, DateTime toDate, int locationID);
        /// <summary>
        /// Gets the payment methods.
        /// </summary>
        /// <param name="filterName">Name of the filter.</param>
        /// <returns></returns>
        List<PaymentMethod> GetPaymentMethods(string filterName = "");
        /// <summary>
        /// Saves the payment method.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="UserID">The user identifier.</param>
        void SavePaymentMethod(PaymentMethod p, int userId);
        /// <summary>
        /// Gets the exemption reasons.
        /// </summary>
        /// <param name="filterName">Name of the filter.</param>
        /// <returns></returns>
        List<KeyValuePair<int, string>> GetExemptionReasons(string filterName = "");
        /// <summary>
        /// Gets or sets the print price list XSL.
        /// </summary>
        /// <value>
        /// The print price list XSL.
        /// </value>
       // string PrintPriceListXSL { get;  }
        DataSet getCashiersTransactionSummary(int userId);
        /// <summary>
        /// Gets the outstanding bill.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <returns></returns>
        DataTable getOutstandingBill(int patientId);
       
        #region QuickPanel
        /// <summary>
        /// Issues the item.
        /// </summary>
        /// <param name="Items">The items.</param>
        /// <param name="PatientID">The patient identifier.</param>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns></returns>
        int IssueItem(List<IssuableItem> items, int patientId, int locationId, int userId);
        /// <summary>
        /// Removes the issued item.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="itemIssueID">The item issue identifier.</param>
        void RemoveIssuedItem(int userId, int itemIssueId);
        /// <summary>
        /// Gets the patients items issued by user identifier.
        /// </summary>
        /// <param name="PatientID">The patient identifier.</param>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="ItemIssueDate">The item issue date.</param>
        /// <returns></returns>
        List<IssuableItem> GetPatientsItemsIssuedByUserID(int patientId, int locationId, int userId, DateTime? itemIssueDate, int? itemTypeId = null);
        #endregion

        #region DiscountPlans
        List<DiscountPlan> GetDiscountPlans(int? discountId= null, int? paymentTypeId = null, DateTime? discountDate = null,  bool active = true);
        /// <summary>
        /// Adds the discount plan.
        /// </summary>
        /// <param name="plan">The plan.</param>
        /// <param name="UserID">The user identifier.</param>
        void AddDiscountPlan(DiscountPlan plan, int userId);
        /// <summary>
        /// Updates the discount plan.
        /// </summary>
        /// <param name="plan">The plan.</param>
        /// <param name="UserID">The user identifier.</param>
        void UpdateDiscountPlan(DiscountPlan plan, int userId);
        #endregion

        List<ItemType> GetBillingType();
    }

    
}
