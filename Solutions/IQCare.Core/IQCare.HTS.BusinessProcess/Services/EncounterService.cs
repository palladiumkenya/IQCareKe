using System;
using IQCare.HTS.BusinessProcess.Interfaces;
using IQCare.HTS.Core.Interfaces.Repositories;
using IQCare.HTS.Core.Model;
using IQCare.HTS.Core.ViewModel;

namespace IQCare.HTS.BusinessProcess.Services
{
    public class EncounterService : IHTSEncounterService
    {
        private readonly IHtsEncounterRepository _htsEncounterRepository;

        public EncounterService(IHtsEncounterRepository htsEncounterRepository)
        {
            _htsEncounterRepository = htsEncounterRepository;
        }

        public void addHtsEncounter(EncounterViewModel encounterViewModel)
        {
            try
            {
                HtsEncounter htsEncounter = new HtsEncounter()
                {
                    PatientEncounterID = 1,
                    EverTested = encounterViewModel.EverTested,
                    NoOfMonthsSinceReTest = encounterViewModel.NoOfMonthsReTest,
                    SelfTestPastTwelveMonths = encounterViewModel.SelfTestLastTwelveMonths,
                    TestedAs = encounterViewModel.ClientTested,
                    TestingStrategy = encounterViewModel.Strategy,
                    EncounterRemarks = encounterViewModel.Remarks,
                    FinalResultGiven = 1,
                    CoupleDiscordant = 1,
                    AcceptedPartnerListing = 1,
                    CreatedBy = 1,
                    CreateDate = DateTime.Now,
                    DeleteFlag = false
                };

                _htsEncounterRepository.Add(htsEncounter);
                _htsEncounterRepository.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            

        }
    }
}