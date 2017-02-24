using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Presentation;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;

namespace IQCare.CCC.UILogic.Baseline
{
    public class PatientFamilyTestingManager
    {
        private readonly IPatientHivTestingManager _patientHvTesting = (IPatientHivTestingManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Baseline.BPatientArtInitiationBaselineManager, BusinessProcess.CCC");
        private int _result;

        public int AddFamilyHivTesting(int personId, int patientMasterVisitId, int baselineResult, DateTime baselineDate,
            DateTime testingDate, int testingResult, bool referredTocare, string cccNumber, int enrolmentid)
        {
            PatientHivTesting patientHivTesting=new PatientHivTesting()
            {
                PersonId = personId,
                PatientMasterVisitId = patientMasterVisitId,
                BaselineResult = baselineResult,
                BaselineDate = baselineDate,
                TestingDate = testingDate,
                TestintResult = testingResult,
                ReferredToCare = referredTocare,
                CccNumber = cccNumber
            };
            return _result = _patientHvTesting.AddPatientHivTesting(patientHivTesting);
        }

        public int UpdateFamilyHivTesting(int personId, int patientMasterVisitId, int baselineResult,
            DateTime baselineDate,
            DateTime testingDate, int testingResult, bool referredTocare, string cccNumber, int enrolmentid)
        {
            PatientHivTesting patientHivTesting = new PatientHivTesting()
            {
                PersonId = personId,
                PatientMasterVisitId = patientMasterVisitId,
                BaselineResult = baselineResult,
                BaselineDate = baselineDate,
                TestingDate = testingDate,
                TestintResult = testingResult,
                ReferredToCare = referredTocare,
                CccNumber = cccNumber
            };
            return _result = _patientHvTesting.AddPatientHivTesting(patientHivTesting);
        }

        public int DeleteFamilyTesting(int id)
        {
           return _result= _patientHvTesting.DeletePatientHivTesting(id);
        }

        public List<PatientHivTesting> GetFamilyTestingDeatils(int patientId )
        {
            return _patientHvTesting.GetPatientHivTestings(patientId);
        }
    }
}
