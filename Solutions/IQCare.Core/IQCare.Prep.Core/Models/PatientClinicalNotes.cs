using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Prep.Core.Models
{
    public class PatientClinicalNotes
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int? FacilityId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int? ServiceAreaId { get; set; }
        public string ClinicalNotes { get; set; }
        public bool DeleteFlag { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public Boolean Active { get; set; }

        public int? NotesCategoryId { get; set; }

    }
}
