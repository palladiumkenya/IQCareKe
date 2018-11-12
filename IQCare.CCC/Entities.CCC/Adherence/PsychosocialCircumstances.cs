using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC.Enrollment;
using Entities.CCC.Visit;
using Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Adherence
{
    [Serializable]
    [Table("AdherencePsychosocialCircumstances")]
    public class PsychosocialCircumstances : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string LivingWith { get; set; }
        public string Aware { get; set; }
        public int SupportSystem { get; set; }
        public string SupportSystemNotes { get; set; }
        public int RelationshipChanges { get; set; }
        public string RelationshipChangesNotes { get; set; }
        public int Bothered { get; set; }
        public string BotheredNotes { get; set; }
        public int TreatedDifferently { get; set; }
        public string TreatedDifferentlyNotes { get; set; }
        public int InterferenceStigma { get; set; }
        public string InterferenceStigmaNotes { get; set; }
        public int StoppedMedication { get; set; }
        public string StoppedMedicationNotes { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
    }
}
