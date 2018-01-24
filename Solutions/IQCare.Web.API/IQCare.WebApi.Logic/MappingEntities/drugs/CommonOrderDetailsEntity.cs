using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.WebApi.Logic.MappingEntities.drugs
{
   public class COMMON_ORDER_DETAILS
    {
        public COMMON_ORDER_DETAILS()
        {
            PLACER_ORDER_NUMBER=new PLACER_ORDER_NUMBER();
            ORDERING_PHYSICIAN=new ORDERING_PHYSICIAN();
        }
        public string ORDER_CONTROL { get; set; }
        public PLACER_ORDER_NUMBER PLACER_ORDER_NUMBER { get; set; }
        public string ORDER_STATUS { get; set; }
        public ORDERING_PHYSICIAN ORDERING_PHYSICIAN { get; set; }
        public string TRANSACTION_DATETIME { get; set; }
        public string NOTES { get; set; }
    }
}
