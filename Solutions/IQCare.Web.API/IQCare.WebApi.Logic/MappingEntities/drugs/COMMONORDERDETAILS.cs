using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.WebApi.Logic.MappingEntities.drugs
{
   public class COMMONORDERDETAILS
    {
        public string ORDER_CONTROL { get; set; }
        public PLACERORDERNUMBER PLACER_ORDER_NUMBER { get; set; }
        public FILLERORDERNUMBER FILLER_ORDER_NUMBER { get; set; }
        public string ORDER_STATUS { get; set; }
        public ORDERINGPHYSICIAN ORDERING_PHYSICIAN { get; set; }
        public string TRANSACTION_DATETIME { get; set; }
        public string NOTES { get; set; }
    }
}
