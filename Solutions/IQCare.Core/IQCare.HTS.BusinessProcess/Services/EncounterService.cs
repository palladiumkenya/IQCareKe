using System;
using System.Threading.Tasks;
using IQCare.HTS.BusinessProcess.Interfaces;
using IQCare.HTS.Core.Model;
using IQCare.HTS.Infrastructure;

namespace IQCare.HTS.BusinessProcess.Services
{
    public class EncounterService : IHTSEncounterService
    {
        private readonly IHTSUnitOfWork _htsUnitOfWork;

        public EncounterService(IHTSUnitOfWork htsUnitOfWork)
        {
            _htsUnitOfWork = htsUnitOfWork ?? throw new ArgumentNullException(nameof(htsUnitOfWork));
        }

        public async Task AddHtsEncounter(HtsEncounter htsEncounter)
        {
            try
            {
                var repository = _htsUnitOfWork.Repository<HtsEncounter>();
                await repository.AddAsync(htsEncounter);
                await _htsUnitOfWork.SaveAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}