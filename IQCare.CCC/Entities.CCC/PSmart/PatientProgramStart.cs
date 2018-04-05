using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.PSmart
{
    [Serializable][Table("Lnk_PatientProgramStart")]
    public class PatientProgramStart
    {
        [Key]
        public int Ptn_pk { get; set; }
        public int? ModuleId { get; set; }
        public DateTime StartDate { get; set; }
        public int ?UserID { get; set; }
        public DateTime CreateDate { get; set; }
    }
}