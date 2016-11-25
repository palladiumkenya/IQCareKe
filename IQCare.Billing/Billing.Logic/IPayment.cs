using System;
using System.Web.UI.WebControls;
using Entities.Billing;

namespace IQCare.Billing.Logic
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPayment
    {
        /// <summary>
        /// Gets or sets the amount due.
        /// </summary>
        /// <value>
        /// The amount due.
        /// </value>
        Double AmountDue { get; set; }
        /// <summary>
        /// Gets or sets the bill amount.
        /// </summary>
        /// <value>
        /// The bill amount.
        /// </value>
        Double BillAmount { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [allow partial payment].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow partial payment]; otherwise, <c>false</c>.
        /// </value>
        Double AmountToPay { get; set; }
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        int PatientId { get; set; }
        /// <summary>
        /// Gets or sets the client script.
        /// </summary>
        /// <value>
        /// The client script.
        /// </value>
        string ClientScript { get; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        int UserId { get; set; }
        /// <summary>
        /// Gets or sets the bill identifier.
        /// </summary>
        /// <value>
        /// The bill identifier.
        /// </value>
        int BillId { get; set; }
        /// <summary>
        /// Gets or sets the bill location identifier.
        /// </summary>
        /// <value>
        /// The bill location identifier.
        /// </value>
        int BillLocationId { get; set; }
        /// <summary>
        /// Gets or sets the item for pay.
        /// </summary>
        /// <value>
        /// The item for pay.
        /// </value>
        Delegate ItemForPay { get; set; }
        /// <summary>
        /// Gets or sets the available discount plans.
        /// </summary>
        /// <value>
        /// The available discount plans.
        /// </value>
        DiscountPlan SelectedDiscountPlan { get; set; }
        /// <summary>
        /// Gets or sets the payment mode.
        /// </summary>
        /// <value>
        /// The payment mode.
        /// </value>
        PaymentMethod PaymentMode { get; set; }
        /// <summary>
        /// Gets or sets the payment option.
        /// </summary>
        /// <value>
        /// The payment option.
        /// </value>
        BillPaymentOptions BillPayOption { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has transaction.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has transaction; otherwise, <c>false</c>.
        /// </value>
        bool HasTransaction { get; set; }
        /// <summary>
        /// Rebinds this instance.
        /// </summary>
        void Rebind();
        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear();
        /// <summary>
        /// Computes this instance.
        /// </summary>
        void Compute();
        /// <summary>
        /// Validates the specified bill payment information.
        /// </summary>
        /// <param name="billPaymentInfo">The bill payment information.</param>
        /// <returns></returns>
        void Validate();
        /// <summary>
        /// Occurs when [cancel compute].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when compute balance is canceled.")]
        [System.ComponentModel.Bindable(true)]
        event CommandEventHandler CancelCompute;
        /// <summary>
        /// Occurs when [pay complete].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when finish payment events completes.")]
        [System.ComponentModel.Bindable(true)]
        event CommandEventHandler PayComplete;
        /// <summary>
        /// Occurs when [notify command].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when a notifcation need to be sent.")]
        [System.ComponentModel.Bindable(true)]
        event CommandEventHandler NotifyCommand;
        /// <summary>
        /// Occurs when [error occurred].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when an error occurs.")]
        [System.ComponentModel.Bindable(true)]
        event CommandEventHandler ErrorOccurred;
        /// <summary>
        /// Occurs when [execute payment].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised to execute payment.")]
        [System.ComponentModel.Bindable(true)]
        event CommandEventHandler ExecutePayment;
    }
   
   
}
