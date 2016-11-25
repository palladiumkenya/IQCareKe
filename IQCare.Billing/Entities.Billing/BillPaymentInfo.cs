using System;
using System.Collections.Generic;

namespace Entities.Billing
{
    /// <summary>
    /// Complete bill payment information. Contains all the details necessary to execute payment for a given bill id
    /// </summary>
    /// 
    [Serializable]
    public class BillPaymentInfo : Discount<BillPaymentInfo>
    {
        /// <summary>
        /// The bill identifier
        /// </summary>
        public int BillId;
        /// <summary>
        /// The location identifier
        /// </summary>
        public int LocationId;
        /// <summary>
        /// The payment mode
        /// </summary>
        public PaymentMethod PaymentMode;
        /// <summary>
        /// The chosen discount plan
        /// </summary>
        public DiscountPlan ChosenDiscountPlan;
        /// <summary>
        /// The reference number
        /// </summary>
        public string ReferenceNumber;
        /// <summary>
        /// The amount
        /// </summary>
        public double Amount;
        /// <summary>
        /// The amount payable
        /// </summary>
        public double AmountPayable
        {
            get
            {
                return Amount * (1.0D - CalculatedDiscount);
            }
        }
        /// <summary>
        /// The tendered amount
        /// </summary>
        public double TenderedAmount;
        /// <summary>
        /// The deposit
        /// </summary>
        public Boolean Deposit;
        /// <summary>
        /// The print receipt
        /// </summary>
        public Boolean PrintReceipt = true;
        /// <summary>
        /// The items to pay
        /// </summary>
        public List<BillItem> ItemsToPay;
        string _justification = "";
        /// <summary>
        /// Gets or sets the pay mode justification.
        /// </summary>
        /// <value>
        /// The pay mode justification.
        /// </value>
        public string Narrative
        {
            get { return _justification; }
            set { _justification = value; }
        }
        /// <summary>
        /// Gets the calculated discount.
        /// </summary>
        /// <value>
        /// The calculated discount.
        /// </value>
        public double CalculatedDiscount
        {
            get
            {
                return CalculateDiscount(this, 0, DateTime.Now);
            }
        }
        /// <summary>
        /// Calculates the discount.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="PatientID">The patient identifier.</param>
        /// <param name="DiscountDate">The discount date.</param>
        /// <returns></returns>
        protected override double CalculateDiscount(BillPaymentInfo t, int PatientId, DateTime DiscountDate)
        {
            if (t.ChosenDiscountPlan != null)
            {
                DiscountPlan Plan = (t.ChosenDiscountPlan);
                if (Plan.Active && Plan.StartDate <= DiscountDate && Plan.EndDate >= DiscountDate)
                {
                    //  AmountPayable = Amount * (1.0M - Plan.Rate);
                    return Plan.Rate;
                }
            }
            // this.AmountPayable = this.Amount;
            return base.CalculateDiscount(t, PatientId, DiscountDate);
        }

    }
}
