using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.WebApi.Logic.MappingEntities.drugs
{
   public class CommonOrderDetailsEntity
    {
        public CommonOrderDetailsEntity()
        {
            PLACER_ORDER_NUMBER=new PlacerOrderNumberEntity();
            ORDERING_PHYSICIAN=new OrderingPhysicianEntity();
        }
        public string ORDER_CONTROL { get; set; }
        public PlacerOrderNumberEntity PLACER_ORDER_NUMBER { get; set; }
        public string ORDER_STATUS { get; set; }
        public OrderingPhysicianEntity ORDERING_PHYSICIAN { get; set; }
        public string TRANSACTION_DATETIME { get; set; }
        public string NOTES { get; set; }
    }
}
