using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.WebApi;
using DataAccess.WebApi.Repository;
using Entities.CCC.PSmart;
using Entity.WebApi.PSmart;
using Interface.WebApi;

namespace BusinessProcess.WebApi
{
    public class BHivTestTrackerManager:ProcessBase,IHivTestTrackerManager
    {
        private int _result=0;

        public int AddHivTestTracker(int personId, string facilityMflCode, string diagnosisMode, string providerName, string providerId, int ptnpk, DateTime resultDate, string testResult, string strategy, string testCategory)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var hivTestResult = unitOfWork.HivTestTrackerRepository.FindBy(x =>
                            x.PersonId == personId && x.ResultDate == resultDate &&
                            x.Result == testResult && x.Strategy == testResult && x.MFLCode==facilityMflCode)
                        .FirstOrDefault();
                    if (hivTestResult!=null)
                    {

                    }else
                    {
                        var newHivTest = new HivTestTracker()
                        {
                            MFLCode = facilityMflCode,
                           
                            PersonId = personId, //hivtest.PersonId,
                            ProviderName = providerName,
                            ProviderId = providerId,
                            Ptn_pk = ptnpk,
                            ResultDate = resultDate,
                            ResultCategory = testCategory,
                            Result = testResult,
                            Strategy = strategy
                        };
                        if (!string.IsNullOrEmpty(diagnosisMode))
                        {
                            newHivTest.DiagnosisMode = diagnosisMode; }

                        unitOfWork.HivTestTrackerRepository.Add(newHivTest);
                        _result = unitOfWork.Complete();
                        unitOfWork.Dispose();
                    }
                }

                return _result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }

        public int EditHivTestTracker(HivTestTracker hivTestTracker)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    unitOfWork.HivTestTrackerRepository.Update(hivTestTracker);
                    _result = unitOfWork.Complete();
                    unitOfWork.Dispose();
                    return _result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<HivTestTracker> GetPersonHIVTest(int personId)
        {
            HivTestTrackerRepository repo = new HivTestTrackerRepository(new PsmartContext());
            return repo.FindBy(xx => xx.PersonId == personId).ToList();
        }

        //public int AddHivTestTracker(int personId, List<IQCare.DTO.PSmart.HIVTEST> hivtests)
        //{
        //    try
        //    {
        //        using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
        //        {
        //            foreach (var hivtest in hivtests)
        //            {
        //                var hivTestResult = unitOfWork.HivTestTrackerRepository.FindBy(x =>
        //                        x.PersonId == personId && x.ResultDate == Convert.ToDateTime(hivtest.DATE) &&
        //                        x.TestResult == hivtest.RESULT && x.TestStrategy==hivtest.STRATEGY)
        //                    .FirstOrDefault();
        //                if (hivTestResult != null)
        //                {
        //                    ////todo save the new lab to the tracker.
        //                    //var newHivTest=new HivTestTracker()
        //                    //{
        //                    //    FacilityMflCode =Convert.ToInt32(hivtest.FACILITY) ,
        //                    //    DiagnosisMode = "",
        //                    //    FacilityName = "",
        //                    //    PersonId = personId, //hivtest.PersonId,
        //                    //    ProvideName = hivtest.PROVIDER_DETAILS.NAME,
        //                    //    ProviderId = Convert.ToInt32(hivtest.PROVIDER_DETAILS.ID),
        //                    //    PtnPk =personId,// hivtest.PatientId,
        //                    //    ResultDate = Convert.ToDateTime(hivtest.DATE),
        //                    //    ResultCategory = hivtest.STRATEGY,
        //                    //    TestResult = hivtest.RESULT
        //                    //};

        //                  //  unitOfWork.HivTestTrackerRepository.Add(newHivTest);
        //                    _result = unitOfWork.Complete();

        //                    return _result;
        //                }
        //            }
        //            unitOfWork.Dispose();
        //        }
        //            return 0;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }
        //}
    }
}