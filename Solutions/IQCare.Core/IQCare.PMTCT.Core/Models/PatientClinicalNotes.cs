using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.Core.Models
{
    public class PatientClinicalNotes
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int? FacilityId {get;set;}
        public int PatientMasterVisitId { get; set; }
        public int? ServiceAreaId { get; set; }
        public string ClinicalNotes { get; set; }
        public int DeleteFlag { get; set; }

    }
}
