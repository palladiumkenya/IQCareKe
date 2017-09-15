using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.CCC.Lookup
{
    [Serializable]
    [Table("PatientTreatmentSupporterView")]
    public class PatientTreatmentSupporterLookup
    {
        [Key]
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int SupporterId { get; set; }
        public string MobileContact { get; set; }
        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
