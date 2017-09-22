using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.CCC.Enrollment;





namespace Entities.CCC.Triage
{
    [Serializable]
    [Table("ord_LabOrder")]
    public class LabOrderEntity : BaseEntity
    {
        [Column]

        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }          
        public int Ptn_pk { get; set; }         
        public DateTime? OrderDate { get; set; }
        public DateTime? PreClinicLabDate { get; set; }
        public string ClinicalOrderNotes { get; set; }
        public string OrderStatus { get; set; }
        public int UserId { get; set; }
        public int LocationId { get; set; }
        public int VisitId { get; set; }       
        public int PatientMasterVisitId { get; set; }
        public int ModuleId { get; set; }
        public string OrderNumber { get; set; }
        public int OrderedBy { get; set; }
     





    }
}
