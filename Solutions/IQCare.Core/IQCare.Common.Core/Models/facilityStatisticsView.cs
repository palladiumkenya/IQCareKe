using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class FacilityStatisticsView
    {
        public int Id { get; set; }
        public int TotalPatientsTransferedOut { get; set; }
        public int TotalPatientsDead { get; set; }
        public int LostToFollowUp { get; set; }
    }
}
