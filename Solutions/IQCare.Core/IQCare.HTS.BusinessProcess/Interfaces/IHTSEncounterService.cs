using System.Threading.Tasks;

namespace IQCare.HTS.BusinessProcess.Interfaces
{
    public interface IHTSEncounterService
    {
        Task AddHtsEncounter(HTS.Core.Model.HtsEncounter htsEncounter);
    }
}