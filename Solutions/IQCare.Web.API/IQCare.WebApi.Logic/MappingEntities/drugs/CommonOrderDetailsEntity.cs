using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.WebApi.Logic.MappingEntities.drugs
{
   public class CommonOrderDetailsEntity
    {
        public string OrderControl { get; set; }
        public PlacerOrderNumberEntity PlacerOrderNumberEntity { get; set; }
        public string OrderStatus { get; set; }
        public OrderingPhysicianEntity OrderingPhysicianEntity { get; set; }
        public string TransactionDatetime { get; set; }
        public string Notes { get; set; }
    }
}
