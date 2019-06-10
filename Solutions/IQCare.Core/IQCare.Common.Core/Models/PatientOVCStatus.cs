using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class PatientOVCStatus
    {
        public int Id { get; set; }
        public int PersonId { get; set; }

        public int? GuardianId { get; set; }
        public bool Orphan { get; set; }
        public bool InSchool { get; set; }
        public bool Active { get; set; }
        public bool Deleteflag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }

        public string AuditData { get; set; }

    }
}
