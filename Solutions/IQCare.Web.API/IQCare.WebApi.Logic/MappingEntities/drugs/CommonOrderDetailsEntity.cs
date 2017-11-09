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
            PlacerOrderNumberEntity=new PlacerOrderNumberEntity();
            OrderingPhysicianEntity=new OrderingPhysicianEntity();
        }
        public string ORDER_CONTROL { get; set; }
        public PlacerOrderNumberEntity PlacerOrderNumberEntity { get; set; }
        public string ORDER_STATUS { get; set; }
        public OrderingPhysicianEntity OrderingPhysicianEntity { get; set; }
        public string TRANSACTION_DATETIME { get; set; }
        public string NOTES { get; set; }
    }
}
