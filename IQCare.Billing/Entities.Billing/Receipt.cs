using System;

namespace Entities.Billing
{
    [Serializable]
    public class Receipt
    {
        int _receiptId = -1;
        ReceiptType _receiptType = ReceiptType.BillPayment;
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id
        {
            get
            {
                return _receiptId;
            }
            set
            {
                _receiptId = value;
            }
        }
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public int PatientId { get; set; }
        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>
        /// The transaction identifier.
        /// </value>
        public int TransactionId { get; set; }
        /// <summary>
        /// Gets or sets the type of the receipt.
        /// </summary>
        /// <value>
        /// The type of the receipt.
        /// </value>
        public ReceiptType ReceiptType
        {
            get
            {
                return _receiptType;
            }
            set
            {
                _receiptType = value;
            }
        }
        /// <summary>
        /// Gets the name of the receipt type.
        /// </summary>
        /// <value>
        /// The name of the receipt type.
        /// </value>
        public string ReceiptTypeName
        {
            get
            {
                string _name = "";
                switch (_receiptType)
                {
                    case Billing.ReceiptType.BillPayment:
                        _name = "Bill Payment";
                        break;
                    case Billing.ReceiptType.BillPaymentReversal:
                        _name = "Bill Payment Reversal";
                        break;
                    case Billing.ReceiptType.DepositRefund:
                        _name = "Refund Deposit";
                        break;
                    case Billing.ReceiptType.NewDeposit:
                        _name = "New Deposit";
                        break;
                }
                return _name;
            }
        }
        /// <summary>
        /// Gets or sets the receipt date.
        /// </summary>
        /// <value>
        /// The receipt date.
        /// </value>
        public DateTime ReceiptDate { get; set; }
        /// <summary>
        /// Gets or sets the receipt number.
        /// </summary>
        /// <value>
        /// The receipt number.
        /// </value>
        public string ReceiptNumber { get; set; }
        /// <summary>
        /// Gets or sets the receipt data.
        /// </summary>
        /// <value>
        /// The receipt data.
        /// </value>
        public string ReceiptData { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the print count.
        /// </summary>
        /// <value>
        /// The print count.
        /// </value>
        public int PrintCount { get; set; }
    }
}
