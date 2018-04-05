using System;
using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.psmart;
using Interface.WebApi;

namespace IQCare.WebApi.Logic.PSmart
{
    public class HivTestTrackerManager
    {
        private readonly IHivTestTrackerManager hivTestTrackerManager = (IHivTestTrackerManager)ObjectFactory.CreateInstance("BusinessProcess.WebApi.BHivTestTrackerManager, BusinessProcess.WebApi");
        private int result = 0;

        public int AddHivTestTracker(int personId, string facilityMflCode, string diagnosisMode, string providerName, string providerId, int ptnpk, DateTime resultDate, string testResult, string strategy, string testCategory)
        {
            if (testResult != null)
            {
                result = hivTestTrackerManager.AddHivTestTracker(personId,facilityMflCode,diagnosisMode,providerName,providerId,ptnpk,resultDate,testResult,strategy,testCategory);
            }
            return result;
        }


        public int EditHivTestTracker(HivTestTracker hivTestTracker)
        {
            return hivTestTrackerManager.EditHivTestTracker(hivTestTracker);
        }
    }
}