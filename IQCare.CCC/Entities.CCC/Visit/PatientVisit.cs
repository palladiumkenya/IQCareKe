using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;

namespace Entities.CCC.Visit
{
    [Serializable]
    [Table("ord_Visit")]
   public class PatientVisit : BaseEntity
    {
        [Key]
        public int Visit_Id { get; set; }
        public int Ptn_Pk { get; set; }
        public int LocationID { get; set; }
        public DateTime VisitDate { get; set; }        
        public int VisitType { get; set; }
        public int? DataQuality { get; set; }
        public int UserID { get; set; }
        public int TypeofVisit { get; set; }
       
        public int OrderedBy { get; set; }
       
        public int ReportedBy { get; set; }
        public int Signature { get; set; }
        public int ModuleId { get; set; }       
        public int old_signature_employee_id { get; set; }


    }
}
