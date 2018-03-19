using IQCare.Common.Core.Models;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class AddEncounterCommand : IRequest<Result<AddEncounterResponse>>
    {
        // Encounter
        public Encounter Encounter { get; set; }

        // Testing
        public List<Testing> Testing { get; set; }

        // HtsEncounter Result
        public FinalTestingResult FinalTestingResult { get; set; }
    }

    public class Encounter
    {
        public int PersonId { get; set; }
        public int ProviderId { get; set; }
        public int PatientEncounterID { get; set; }
        public int PatientId { get; set; }
        public int EverTested { get; set; }
        public int? MonthsSinceLastTest { get; set; }
        public int? MonthSinceSelfTest { get; set; }
        public int TestedAs { get; set; }
        public int TestingStrategy { get; set; }
        public string EncounterRemarks { get; set; }
        public int TestEntryPoint { get; set; }
        public int Consent { get; set; }
        public int EverSelfTested { get; set; }
        public string GeoLocation { get; set; }
        public int HasDisability { get; set; }
        public List<int> Disabilities { get; set; }
        public int TbScreening { get; set; }
        public int ServiceAreaId { get; set; }
        public int EncounterTypeId { get; set; }
    }


    public class Testing
    {
        public int KitId { get; set; }
        public string KitLotNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Outcome { get; set; }
        public int TestRound { get; set; }
    }

    public class FinalTestingResult
    {
        public int FinalResultHiv1 { get; set; }
        public int FinalResultHiv2 { get; set; }
        public int FinalResult { get; set; }
        public int FinalResultGiven { get; set; }
        public int CoupleDiscordant { get; set; }
        public string FinalResultsRemarks { get; set; }
        public int AcceptedPartnerListing { get; set; }


    }

    public class AddEncounterResponse
    {
        public int HtsEncounterId { get; set; }
    }
}

