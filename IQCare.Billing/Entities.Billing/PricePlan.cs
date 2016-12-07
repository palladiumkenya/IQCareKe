using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities.Billing
{
    [Serializable]
    public class PricePlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrdRank { get; set; }
        public int CodeId { get; set; }
        public string Code { get; set; }
        public int DeleteFlag { get; set; }
        public bool Default
        {
            get
            {
                return null != this && Code == "STD";
            }
        }
    }
}
