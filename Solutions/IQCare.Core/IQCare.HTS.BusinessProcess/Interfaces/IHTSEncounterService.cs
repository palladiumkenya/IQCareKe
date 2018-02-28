using IQCare.HTS.Core.ViewModel;

namespace IQCare.HTS.BusinessProcess.Interfaces
{
    public interface IHTSEncounterService
    {
        void addHtsEncounter(EncounterViewModel encounterViewModel);
    }
}