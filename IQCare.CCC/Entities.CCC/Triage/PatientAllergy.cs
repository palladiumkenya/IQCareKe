using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Entities.CCC.Triage
{
    [Serializable]
    [Table("PatientAllergy")]
    public class PatientAllergy
    {
        [Column]
        public virtual int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public string Allergen { get; set; }
        public string AllergyResponse { get; set; }
     }
}
