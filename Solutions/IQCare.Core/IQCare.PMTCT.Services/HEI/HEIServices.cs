
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.Infrastructure;
using IQCare.PMTCT.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IQCare.PMTCT.Services
{
    public  class HeiServices :IHeiService
    {
       private readonly IPmtctUnitOfWork _unitOfWork;

        public HeiServices(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<HeiFeeding> AddHeiFeeding(HeiFeeding heiFeeding)
        {
            try
            {
                await _unitOfWork.Repository<HeiFeeding>().AddAsync(heiFeeding);
                await _unitOfWork.SaveAsync();
                return heiFeeding;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw;
            }
        }

        public async Task<HeiFeeding> DeleteHeiFeeding(int id)
        {
            try
            {
                HeiFeeding heiFeeding = _unitOfWork.Repository<HeiFeeding>().FindById(id);
                if (null != heiFeeding)
                {
                    heiFeeding.DeleteFlag = false;
                }
                _unitOfWork.Repository<HeiFeeding>().Update(heiFeeding);
                await _unitOfWork.SaveAsync();

                return heiFeeding;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw;
            }
        }

        public async Task<HeiFeeding> EditHeiFeeding(HeiFeeding heiFeeding)
        {
            try
            {
                HeiFeeding heiFeedingData =
                    _unitOfWork.Repository<HeiFeeding>().FindById(heiFeeding.Id);
                if (null != heiFeedingData)
                {
                    heiFeedingData.PatientMasterVisitId = heiFeedingData.PatientMasterVisitId;
                    heiFeedingData.FeedingModeId = heiFeedingData.FeedingModeId;

                }
                _unitOfWork.Repository<HeiFeeding>().Update(heiFeedingData);
                await _unitOfWork.SaveAsync();
                return heiFeedingData;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw;
            }
        }

        public async Task<List<HeiFeeding>> GetAllHeiFeeding(int patientId)
        {
            try
            {
                List<HeiFeeding> heiFeedingList = await _unitOfWork.Repository<HeiFeeding>()
                    .Get(x => x.PatientId == patientId && !x.DeleteFlag).ToListAsync();
                return heiFeedingList;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }

        public async Task<HeiFeeding> GetHeiFeeding(int id)
        {
            try
            {
                HeiFeeding heiFeeding = await _unitOfWork.Repository<HeiFeeding>().FindByIdAsync(id);
                return heiFeeding;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }
    }
}
