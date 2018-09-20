using System;
using System.Threading.Tasks;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.Infrastructure;
using Serilog;

namespace IQCare.PMTCT.Services
{
    public class HEIEncounterService
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public HEIEncounterService(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<HEIEncounter> AddHeiEncounter(HEIEncounter heiEncounter)
        {
            try
            {
                await _unitOfWork.Repository<HEIEncounter>().AddAsync(heiEncounter);
                await _unitOfWork.SaveAsync();

                return heiEncounter;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }
    }
}