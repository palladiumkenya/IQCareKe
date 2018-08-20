using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Records
{
    [Serializable]
    [Table("PatientRegistrationView")]
    public class PatientRegistrationLookup
    {
        public int Id { get; set; }
        public int? ptn_pk { get; set; }
        public int PersonId { get; set; }
        public string PatientIndex { get; set; }
        public int PatientType { get; set; }
        public int FacilityId { get; set; }
        public bool Active { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NationalId { get; set; }
        public bool DobPrecision { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}