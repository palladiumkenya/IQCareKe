using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.CCC.Enrollment;





namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("ord_LabOrder")]
    public class LabOrderEntity : BaseEntity
    {
        [Column]

        [Key]
        public int Id { get; set; }
        public int Ptn_pk { get; set; }   //should use a patientID        
        public DateTime OrderDate { get; set; }
        public string ClinicalOrderNotes { get; set; }
        public string OrderStatus { get; set; }
        public int UserId { get; set; }
        public int LocationId { get; set; }
        public int visitid { get; set; }   
        public int ModuleId { get; set; }
        public string OrderNumber { get; set; }
       





    }
}
