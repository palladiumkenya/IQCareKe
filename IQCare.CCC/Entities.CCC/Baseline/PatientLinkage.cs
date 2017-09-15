using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Baseline
{
    [Serializable]
    [Table("PatientLinkage")]
    public class PatientLinkage : BaseEntity
    {
        public int Id { get; set; }
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public DateTime LinkageDate { get; set; }
        public string CCCNumber { get; set; }
        public string Facility { get; set; }
        public bool Enrolled { get; set; }
        public int PatientId { get; set; }
        public virtual Person Person { get; set; }
    }
}
