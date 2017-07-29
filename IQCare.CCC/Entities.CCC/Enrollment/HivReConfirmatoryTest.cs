using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;

namespace Entities.CCC.Enrollment
{
    [Serializable]
    [Table("HIVReConfirmatoryTest")]
    public class HivReConfirmatoryTest : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public int TypeOfTest { get; set; }
        public int TestResult { get; set; }
        public DateTime TestResultDate { get; set; }

        public virtual Person Person { get; set; }
    }
}
