
namespace Entities.Billing
{
    /// <summary>
    /// Bill Statuses
    /// </summary>
    public enum BillStatus
    {
        /// <summary>
        /// All
        /// </summary>
        All = 0,
        /// <summary>
        /// The open bills. Not fully settle
        /// </summary>
        Open = 1,
        /// <summary>
        /// The closed. Fully settled
        /// </summary>
        Closed = 2,
        /// <summary>
        /// The voided bills
        /// </summary>
        Voided = 3
    }
    /// <summary>
    /// 
    /// </summary>
    public enum BillPaymentOptions
    {
        /// <summary>
        /// The full bill
        /// </summary>
        FullBill = 1,
        /// <summary>
        /// The select item
        /// </summary>
        SelectItem = 2
    }




    /// <summary>
    /// 
    /// </summary>

    /// <summary>
    /// Types of receipts
    /// </summary>
    public enum ReceiptType
    {
        /// <summary>
        /// The bill payment in whatever mode
        /// </summary>
        BillPayment = 1,
        /// <summary>
        /// The bill payment reversal
        /// </summary>
        BillPaymentReversal = 2,
        /// <summary>
        /// The deposit transaction  new deposit
        /// </summary>
        NewDeposit = 3,
        /// <summary>
        /// The deposit refund
        /// </summary>
        DepositRefund = 4,

    }
    /// <summary>
    /// Transaction Types allowable on deposits  only
    /// </summary>
    public enum DepositTransactionType
    {
        /// <summary>
        /// Making a deposit
        /// </summary>
        MakeDeposit = 1,
        /// <summary>
        /// The settlement. Using the deposit to clear bills
        /// </summary>
        Settlement = 2,
        /// <summary>
        /// Return available deposit to the client
        /// </summary>
        ReturnDeposit = 3
    }
    
}
