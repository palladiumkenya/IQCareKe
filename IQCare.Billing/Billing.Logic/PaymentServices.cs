using System.Collections.Generic;
using System.Linq;
using Application.Presentation;
using Entities.Billing;
using Interface.Billing;
using IQCare.Web.UILogic;

namespace IQCare.Billing.Logic
{
    public class PaymentServices
    {
        IBilling mgr = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");
        /// <summary>
        /// Gets the credit payment method.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns></returns>
       public List<PaymentMethod> GetCreditPaymentMethod(CurrentSession session)
        {
          return  mgr.GetPaymentMethods("").Where(pm=> pm.Credit).ToList();
        }
    }
}
