using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Pharm.Core.Models
{
    public class VisitType
    {
        public int VisitTypeID { get; set; }

        public string VisitName { get; set; }

        public int? DeleteFlag { get; set; }
    }
}
