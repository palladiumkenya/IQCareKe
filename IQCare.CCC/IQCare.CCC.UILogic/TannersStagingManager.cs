using System;
using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.Tanners;
using Interface.CCC;
using IQCare.Events;

namespace IQCare.CCC.UILogic
{
    public class TannersStagingManager
    {
        ITannersStaging _tannersStaging = (ITannersStaging)ObjectFactory.CreateInstance("BusinessProcess.CCC.BTannersStaging, BusinessProcess.CCC");
        public int AddTannersStaging(PatientTannersStaging T)
        {
            PatientTannersStaging tannersStaging = new PatientTannersStaging()
            {
                PatientId = T.PatientId,
                PatientMasterVisitId = T.PatientMasterVisitId,
                CreatedBy = T.CreatedBy,
                TannersStagingDate = T.TannersStagingDate,
                BreastsGenitalsId = T.BreastsGenitalsId,
                PubicHairId = T.PubicHairId
            };
            //Check existing breatsGenitals greater than supplied breatsGenitals
            List<PatientTannersStaging> tannersList = new List<PatientTannersStaging>();
            int existingBG = 0, existingPH = 0, tannerid = 0, existingId = 0;
            tannersList = _tannersStaging.getBreastsGenitals(T.PatientId,T.BreastsGenitalsId);
            foreach(var items in tannersList)
            {
                existingBG = items.BreastsGenitalsId;
            }
            //check existing pubicHair greater than the current pubichair
            tannersList = _tannersStaging.getPubicHair(T.PatientId, T.PubicHairId);
            foreach(var items in tannersList)
            {
                existingPH = items.PubicHairId;
            }

            //get the highest date
            tannersList = _tannersStaging.getTannersHighestDate(T.PatientId, T.TannersStagingDate);
            foreach(var items in tannersList)
            {
                existingId = items.Id;
            }

            if (existingBG > T.BreastsGenitalsId || existingPH > T.PubicHairId)
            {
                tannerid = 0;
            }
            else
            {
                if (existingId > 0){
                    tannerid = 0;
                }
                else
                {
                    tannerid = _tannersStaging.AddTannersStaging(tannersStaging);
                }
                
            }
            return tannerid;
        }
        public List<PatientTannersStaging> getTannersStaging(int patientId)
        {
            List<PatientTannersStaging> tannersList = new List<PatientTannersStaging>();
            try
            {
                tannersList = _tannersStaging.getTannersStaging(patientId);
            }
            catch
            {
                throw;
            }
            return tannersList;
        }
        public void DeleteTanners(int Id)
        {
            _tannersStaging.DeleteTanners(Id);
        }
        public int recordTannersStaging(int patientId, int patientMasterVisitId, int createdBy, int recordTannersStaging)
        {
            TannersStaging tannersStaging = new TannersStaging()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                CreatedBy = createdBy,
                RecordTannersStaging = recordTannersStaging
            };
            int result = 0;
            result = _tannersStaging.recordTannersStaging(tannersStaging);
            return result;
        }
        public List<TannersStaging> getRecordTannersStaging(int patientId)
        {
            List<TannersStaging> tannersList = new List<TannersStaging>();
            try
            {
                tannersList = _tannersStaging.getRecordTannersStaging(patientId);
            }
            catch
            {
                throw;
            }
            return tannersList;
        }
        public int updateRecordTannersStaging(int PatientId, int PatientMasterVisitId, int recordTannersStaging, int tannersId)
        {
            TannersStaging tnStg = new TannersStaging()
            {
                PatientId = PatientId,
                PatientMasterVisitId = PatientMasterVisitId,
                RecordTannersStaging = recordTannersStaging,
                Id=tannersId
            };
            int tannersResult = 0;
            tannersResult = _tannersStaging.updateRecordTannersStaging(tnStg);
            return tannersResult;
        }
    }
}
