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
        public IcfTestOptions SputumSmear { get; set; }
        public IcfTestOptions? GeneXpert { get; set; }
        public IcfRadiologyOptions ChestXray { get; set; }
        public bool StartAntiTb { get; set; }
        public bool InvitationOfContacts { get; set; }
        public bool EvaluatedForIpt { get; set; }

    }

    public enum IcfTestOptions
    {
        Ordered = 2,
        Positive = 1,
        Negative = 0,
        NotDone = 3,
    }

    public enum IcfRadiologyOptions
    {
        Ordered = 2,
        Positive = 1,
        Negative = 0,
        NotDone = 3,
    }
}