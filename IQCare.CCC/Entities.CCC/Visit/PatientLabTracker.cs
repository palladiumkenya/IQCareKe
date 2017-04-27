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
        public int PatientMasterVisitId { get; set; }
       //[ForeignKey("patientMasterVisitId")]
        //public virtual PatientMasterVisit PatientMasterVisit { get; set; }
        public DateTime? SampleDate { get; set; }
        public string Reasons { get; set; }       
        public string Results  { get; set; }
        public int LabOrderId { get; set; }
        public int LabTestId { get; set; }
        public int FacilityId { get; set; }
        public decimal ResultValues { get; set; }
        public string ResultTexts { get; set; }
        //public int DeleteFlag { get; set; }
        // public string AuditData { get; set; }



    }
}

      