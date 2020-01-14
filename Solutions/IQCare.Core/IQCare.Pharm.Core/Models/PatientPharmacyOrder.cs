using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Pharm.Core.Models
{
    public class PatientPharmacyOrder
    {
        public int PatientMasterVisitId { get; set; }

        public int PatientId { get; set; }
        public int ptn_pharmacy_pk { get; set; }

        public int Ptn_pk { get; set; }

        public int VisitId { get; set; }


        public int LocationID { get; set; }


        public int? OrderedBy { get; set; }

        public DateTime? OrderedbyDate { get; set; }


        public int DispensedBy { get; set; }

        public DateTime? DispensedByDate { get; set; }


        public int ProgId { get; set; }


        public int UserId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int ProviderId { get; set; }

        public int RegimenLine { get; set; }

        public string PharmacyNotes { get; set; }

        public int? PharmacyPeriodTaken { get; set; }

        public string ReportingID { get; set; }

        public int? DeleteFlag { get; set; }
    }
}
