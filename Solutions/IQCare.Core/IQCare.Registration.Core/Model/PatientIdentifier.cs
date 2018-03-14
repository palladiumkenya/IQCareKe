using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace IQCare.Registration.Core.Model
{
    public class PatientIdentifier
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientEnrollmentId { get; set; }
        public int IdentifierTypeId { get; set; }
        public string IdentifierValue { get; set; }
        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Active { get; set; }
        public string AuditData { get; set; }
    }
}
