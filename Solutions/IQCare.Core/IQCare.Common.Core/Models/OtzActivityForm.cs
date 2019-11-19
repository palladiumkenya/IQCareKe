using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class OtzActivityForm
    {
        public int Id { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int AttendedSupportGroup { get; set; }
        public int UserId { get; set; }
        public bool DeleteFlag { get; set; }
        public string Remarks { get; set; }
    }
}
