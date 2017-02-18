using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Entities.Common;
using Entities.CCC.Enrollment;

namespace Entities.CCC.Visit
{
    [Serializable]
    [Table("PatientLabTracker")]
    public class PatientLabTracker : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public string LabName { get; set; }
        public int masterVisitId { get; set; }
       //[ForeignKey("patientMasterVisitId")]
        //public virtual PatientMasterVisit PatientMasterVisit { get; set; }
        public string SampleDate { get; set; }
        public string Reasons { get; set; }       
        public string Results  { get; set; }
        //public string CreatedBy { get; set; }
        //public int DeleteFlag { get; set; }
       // public string AuditData { get; set; }



    }
}

      