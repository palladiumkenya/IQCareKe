using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.CCC.Lookup
{
    [Serializable]
    [Table("PatientServiceEnrollmentView")]
    public class PatientServiceEnrollmentLookup
    {
        [Key]
        public Int64 Id { get; set; }
        public string EnrollmentNumber { get; set; }
        public string ServiceArea { get; set; }
        public DateTime ? EnrollmentDate { get; set; }
        public string PatientStatus { get; set; }
        public int? PatientId { get; set; }
        public int PersonId { get; set; }
    }
}
