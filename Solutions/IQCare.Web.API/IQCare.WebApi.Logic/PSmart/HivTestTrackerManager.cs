using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Application.Presentation;
using Entities.CCC.Lookup;
using Entities.CCC.PSmart;
using Interface.WebApi;

namespace IQCare.WebApi.Logic.PSmart
{
    public class HivTestTrackerManager
    {
        private readonly IHivTestTrackerManager hivTestTrackerManager = (IHivTestTrackerManager)ObjectFactory.CreateInstance("BusinessProcess.WebApi.BHivTestTrackerManager, BusinessProcess.WebApi");
        //private int result = 0;

        //public void TrackHIVTest(PatientLookup patient, List<DTO.PSmart.HIVTEST> hivTests)
        //{
        //    List<HivTestTracker> hivTracker = this.GetPersonsHivTests(patient.PersonId);

        //    foreach (var hivtest in hivTests.Where(tx => !string.IsNullOrWhiteSpace(tx.RESULT)))
        //    {
        //        if (!hivTracker.Exists(xx => xx.PersonId == patient.PersonId
        //         && xx.MFLCode == hivtest.FACILITY
        //         && xx.Strategy == hivtest.STRATEGY
        //         && xx.Result == hivtest.RESULT
        //         && xx.ResultDate == DateTime.ParseExact(hivtest.DATE, "yyyyMMdd", CultureInfo.InvariantCulture)
        //          && xx.ProviderName.Trim().ToLower() == xx.ProviderName.Trim().ToLower()))
        //        {
        //            DateTime resultDate = DateTime.ParseExact(hivtest.DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
        //            hivTestTrackerManager.AddHivTestTracker(patient.PersonId, hivtest.FACILITY,
        //                                hivtest.STRATEGY, hivtest.PROVIDER_DETAILS.NAME, hivtest.PROVIDER_DETAILS.ID,
        //                                Convert.ToInt32(patient.ptn_pk), resultDate, hivtest.RESULT, hivtest.STRATEGY,
        //                                hivtest.TYPE);
        //        }
        //    }
        //}
        //public int AddHivTestTracker(int personId, string facilityMflCode, string diagnosisMode, string providerName, string providerId, int ptnpk, DateTime resultDate, string testResult, string strategy, string testCategory)
        //{
        //    if (testResult != null)
        //    {
        //        result = hivTestTrackerManager.AddHivTestTracker(personId,facilityMflCode,diagnosisMode,providerName,providerId,ptnpk,resultDate,testResult,strategy,testCategory);
        //    }
        //    return result;
        //}

        //public List<HivTestTracker> GetPersonsHivTests(int personId)
        //{

        //    return hivTestTrackerManager.GetPersonHIVTest(personId);

        //}
        public int EditHivTestTracker(HivTestTracker hivTestTracker)
        {
            return hivTestTrackerManager.EditHivTestTracker(hivTestTracker);
        }
    }
}