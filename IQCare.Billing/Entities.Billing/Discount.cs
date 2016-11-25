using System;

namespace Entities.Billing
{
    [Serializable]
    public class Discount<T>
    {

        /// <summary>
        /// Calculates the discount.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="PatientID">The patient identifier.</param>
        /// <param name="DiscountDate">The discount date.</param>
        /// <returns></returns>
        protected virtual double CalculateDiscount(T t, int PatientId, DateTime DiscountDate)
        {

            return 0.00d;
        }

    }
}
