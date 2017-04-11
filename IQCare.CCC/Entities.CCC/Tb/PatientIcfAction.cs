using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Tb
{
    [Serializable]
    [Table("PatientIcfAction")]
    internal class PatientIcfAction : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PatientId { get; set; }
        public bool SputumSmear { get; set; }
        public bool ChestXray { get; set; }
        public bool StartAntiTb { get; set; }
        public bool InvitationOfContacts { get; set; }
        public bool EvaluatedForIpt { get; set; }

    }
}