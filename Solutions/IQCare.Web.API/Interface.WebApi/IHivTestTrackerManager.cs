using Entities.CCC.PSmart;
using System;
using System.Collections.Generic;

namespace Interface.WebApi
{
    public interface IHivTestTrackerManager
    {
        int AddHivTestTracker(int personId, string facilityMflCode, string diagnosisMode, string providerName, string providerId, int ptnpk, DateTime resultDate, string testResult, string strategy,string testCategory);
        int EditHivTestTracker(HivTestTracker hivTestTracker);
        List<HivTestTracker> GetPersonHIVTest(int personId);
    }
}