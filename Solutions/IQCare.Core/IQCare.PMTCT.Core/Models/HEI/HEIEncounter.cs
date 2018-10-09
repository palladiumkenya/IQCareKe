using System;

namespace IQCare.PMTCT.Core.Models.HEI
{
    public class HEIEncounter
    {
        public int Id { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PatientId { get; set; }
        public int PlaceOfDeliveryId { get; set; }
        public string PlaceOfDeliveryOther { get; set; }
        public int ModeOfDeliveryId { get; set; }
        public float BirthWeight { get; set; }
        public int ArvProphylaxisId { get; set; }
        public string ArvProphylaxisOther { get; set; }
        public bool MotherRegisteredId { get; set; }
        public int? MotherPersonId { get; set; }
        public int MotherStatusId { get; set; }
        public int PrimaryCareGiverID { get; set; }
        public string MotherName { get; set; }
        public string MotherCCCNumber { get; set; }
        public int MotherPMTCTDrugsId { get; set; }
        public int? MotherPMTCTRegimenId { get; set; }

        public string MotherPMTCTRegimenOther { get; set; }

        public int MotherArtInfantEnrolId { get; set; }

        public int MotherArtInfantEnrolRegimenId { get; set; }

        public int MotherCurrentRegimenId { get; set; }

        public int MotherLatestVL { get; set; }

        public int Outcome24MonthId { get; set; }

        public bool DeleteFlag { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreatedBy { get; set; }
    }
}