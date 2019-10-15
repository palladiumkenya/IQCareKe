using System;
using System.ComponentModel.DataAnnotations;

namespace IQCare.PMTCT.Core.Models
{
    public class BaselineAntenatalCare
    {
        [Key]
        public int Id { get; set; }
        public int  PatientId { get; set; }
        public int  PatientMasterVisitId { get; set; }
        public int PregnancyId { get; set; }
        public int HivStatusBeforeAnc { get; set; }
        public int? TreatedForSyphilis { get; set; }
        public int BreastExamDone { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Boolean DeleteFlag { get; set; }

        public int? TestedForSyphilis { get; set; }
        public int? SyphilisTestUsed { get; set; }
        public int? SyphilisResults { get; set; }
    }
}