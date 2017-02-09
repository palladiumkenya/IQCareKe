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
    public class PatientHivEnrollmentBaselineManager
    {
        private readonly IPatientHivEnrolmetBaselineManager _patientHivEnrolmet = (IPatientHivEnrolmetBaselineManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Baseline.BPatientArtInitiationBaselineManager, BusinessProcess.CCC");
        private int _result;


        public int AddHivEnrollmentBaseline(int patientId,int patientMasterVisitId,DateTime hivDiagnosisDate,DateTime enrollmentDate,int enrollmentWhoStage,DateTime artInitiationDate,bool artHistoryUse,bool hivRetest,int hivRetestTypeId,string reasonForNotRetest)
        {
            PatientHivEnrollmentBaseline patientHivEnrollment=new PatientHivEnrollmentBaseline()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                HivDiagnosisDate = hivDiagnosisDate,
                EnrollmentDate = enrollmentDate,
                EnrollmentWhoStage = enrollmentWhoStage,
                ArtInitiationDate = artInitiationDate,
                ArtHistoryUse = artHistoryUse,
                HivRetest = hivRetest,
                HivRetestTypeId = hivRetestTypeId,
                ReasonForNoRetest = reasonForNotRetest
            };

            return _result=_patientHivEnrolmet.AddPatientHivEnrollment(patientHivEnrollment);
        }
    }
}
