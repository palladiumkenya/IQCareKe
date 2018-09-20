using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.HTS.Core.Model
{
    public class ClientDisability
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int PatientEncounterId { get; set; }
        public int DisabilityId { get; set; }
        public bool DeleteFlag { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
    }
}
