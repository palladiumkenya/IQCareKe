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
            HtsEncounter htsEncounter = new HtsEncounter()
            {
                //EverTested = encounterViewModel.EverTested
            };

            _htsEncounterRepository.Add(htsEncounter);
        }
    }
}