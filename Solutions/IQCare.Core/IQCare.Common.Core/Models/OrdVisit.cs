using System;
using System.ComponentModel.DataAnnotations;

namespace IQCare.Common.Core.Models
{
    public class OrdVisit
    {
        [Key]
        public int Visit_Id { get; set; }

        public int Ptn_Pk { get; set; }
        public int LocationID { get; set; }
        public DateTime VisitDate { get; set; }
        public int VisitType { get; set; }
        public int DataQuality { get; set; }
        public bool DeleteFlag { get; set; }
        public int UserID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}