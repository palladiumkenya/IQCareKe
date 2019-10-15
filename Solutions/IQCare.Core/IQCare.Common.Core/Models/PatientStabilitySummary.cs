using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class PatientStabilitySummary
    {
        public Int64 Id { get; set; }
        public int? Value { get; set; }
        public string Category { get; set; }
    }
}
