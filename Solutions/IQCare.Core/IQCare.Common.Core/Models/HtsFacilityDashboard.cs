using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class HtsFacilityDashboard
    {
        public Int64 Id { get; set; }
        public int TotalTested { get; set; }
        public int TotalPositive { get; set; }
        public int TotalPositiveWithLinkageForm { get; set; }
        public int TotalPositiveAndEnrolledInCCC { get; set; }
    }
}
