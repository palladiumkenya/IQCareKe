using System;
using DataAccess.Base;
using Entities.CCC.Tanners;
using Interface.CCC;
using System.Collections.Generic;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using DataAccess.Entity;
using DataAccess.Common;
using System.Data;

namespace BusinessProcess.CCC
{
    public class BTannersStaging: ProcessBase, ITannersStaging
    {
        int _result;
        public int AddTannersStaging(PatientTannersStaging T)
        {
            using (UnitOfWork unitofwork = new UnitOfWork(new GreencardContext()))
            {
                unitofwork.TannersStagingRepository.Add(T);
                _result = unitofwork.Complete();
                unitofwork.Dispose();
                return T.Id;
            }
        }
        public List<PatientTannersStaging> getTannersStaging(int patientId)
        {
            using (UnitOfWork unitofwork = new UnitOfWork(new GreencardContext()))
            {
                var tannersStaging = unitofwork.TannersStagingRepository.getTannersStaging(patientId).OrderByDescending(x => x.Id);
                unitofwork.Dispose();
                return tannersStaging.ToList();
            }
        }
        public void DeleteTanners(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientTannersStaging tanners = _unitOfWork.TannersStagingRepository.GetById(id);
                _unitOfWork.TannersStagingRepository.Remove(tanners);
                _unitOfWork.Complete();
                _unitOfWork.Dispose();
            }

        }
        public List<PatientTannersStaging> getPubicHair(int patientId, int pubicHair)
        {
            using (UnitOfWork unitofwork = new UnitOfWork(new GreencardContext()))
            {
                var pubicHairList = unitofwork.TannersStagingRepository.getPubicHair(patientId,pubicHair);
                unitofwork.Dispose();
                return pubicHairList.ToList();
            }
        }
        public List<PatientTannersStaging> getBreastsGenitals(int patientId, int breastsGenitals)
        {
            using (UnitOfWork unitofwork = new UnitOfWork(new GreencardContext()))
            {
                var breastsGenitalsList = unitofwork.TannersStagingRepository.getBreastsGenitals(patientId, breastsGenitals);
                unitofwork.Dispose();
                return breastsGenitalsList.ToList();
            }
        }
        public List<PatientTannersStaging> getTannersHighestDate(int patientId, DateTime tannersStagingDate)
        {
            using (UnitOfWork unitofwork = new UnitOfWork(new GreencardContext()))
            {
                var breastsGenitalsList = unitofwork.TannersStagingRepository.getTannersHighestDate(patientId, tannersStagingDate);
                unitofwork.Dispose();
                return breastsGenitalsList.ToList();
            }
        }

        public int recordTannersStaging(TannersStaging R)
        {
            using (UnitOfWork unitofwork = new UnitOfWork(new GreencardContext()))
            {
                unitofwork.RecordTannersStagingRepository.Add(R);
                _result = unitofwork.Complete();
                unitofwork.Dispose();
                return R.Id;
            }
        }
        public List<TannersStaging> getRecordTannersStaging(int patientId)
        {
            using (UnitOfWork unitofwork = new UnitOfWork(new GreencardContext()))
            {
                var tannersStaging = unitofwork.RecordTannersStagingRepository.getRecordTannersStaging(patientId).OrderByDescending(x => x.Id);
                unitofwork.Dispose();
                return tannersStaging.ToList();
            }
        }
        public int updateRecordTannersStaging(TannersStaging TS)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.RecordTannersStagingRepository.Update(TS);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }
    }
}
