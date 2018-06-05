using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public int Ptn_pk { get; set; }
        public int PersonId { get; set; }
        public string PatientIndex { get; set; }
        public int PatientType { get; set; }
        public int FacilityId { get; set; }
        public bool Active { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool DobPrecision { get; set; }
        public string NationalId { get; set; }
        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuditData { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
