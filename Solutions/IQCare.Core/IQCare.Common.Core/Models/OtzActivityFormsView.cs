using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class OtzActivityFormsView
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime VisitDate { get; set; }
        public string AttendedSupportGroup { get; set; }
        public string Remarks { get; set; }
        public string Provider { get; set; }
        public int ModulesDone { get; set; }
    }
}
