using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class AppointmentSummary
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int Total { get; set; }
        public int Met { get; set; }
        public int Missed { get; set; }
        public int Pending { get; set; }
        public int PreviouslyMissed { get; set; }
    }
}
