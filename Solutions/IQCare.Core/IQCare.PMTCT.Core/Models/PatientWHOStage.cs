using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.Core.Models
{
    public class PatientWhoStage
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId {get;set;}
        public int WHOStage { get; set; }

    }
}
