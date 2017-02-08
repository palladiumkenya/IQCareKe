using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;


namespace Entities.CCC.Labs
{
    [Serializable]
    [Table("ord_LabOrder")]
    class LabOrderEntity : BaseEntity
    {
        [Column]

        [Key]
        public int Id { get; set; }
        public int Ptn_Pk { get; set; }   //should use a patientID        
        public int OrderDate { get; set; }
        public string ClinicalOrderNotes { get; set; }
        public string OrderStatus { get; set; }
        public int UserId { get; set; }






    }
}
