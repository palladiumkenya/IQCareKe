using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Tb
{
    [Serializable]
    [Table("PatientIcfAction")]
    public class PatientIcfAction : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PatientId { get; set; }
        public int SputumSmear { get; set; }
        public int GeneXpert { get; set; }
        public int ChestXray { get; set; }
        public bool StartAntiTb { get; set; }
        public bool InvitationOfContacts { get; set; }
        public bool EvaluatedForIpt { get; set; }

    }
}