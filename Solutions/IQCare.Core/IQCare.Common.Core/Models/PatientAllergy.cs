using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class PatientAllergy
    {
      public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int AllergyType { get; set; }
        public string Allagen { get; set; }
        public string Description { get; set; }
        public DateTime? OnsetDate{ get; set; }
        public int Void { get; set; }
        public int? VoidBy { get; set; }
        public DateTime? VoidDate { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
