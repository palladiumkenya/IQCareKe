using System;
using IQCare.Common.Core.Models;

namespace IQCare.PMTCT.Core.Models
{
    public class PatientPartnerTesting
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PartnerTested { get; set; }
        public int PartnerHivResult { get; set; }
        public int DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
    }
}
