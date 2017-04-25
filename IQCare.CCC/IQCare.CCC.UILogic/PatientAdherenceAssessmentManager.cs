using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Presentation;
using Entities.CCC.Encounter;
using Interface.CCC.Encounter;

namespace IQCare.CCC.UILogic
{
    public class PatientAdherenceAssessmentManager
    {
        private IPatientAdherenceAssessessment mgr =
            (IPatientAdherenceAssessessment)
            ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientAdherenceAssessment, BusinessProcess.CCC");

        public int AddPatientAdherenceAssessment(int patientId, int patientMasterVisitId, int createdBy,bool feelBetter, bool carelessAboutMedicine, bool feelWorse, bool forgetMedicine)
        {
            try
            {
                PatientAdherenceAssessment adherenceAssessment = new PatientAdherenceAssessment();

                adherenceAssessment.PatientId = patientId;
                adherenceAssessment.PatientMasterVisitId = patientMasterVisitId;

                adherenceAssessment.FeelBetter = feelBetter;
                adherenceAssessment.CarelessAboutMedicine = carelessAboutMedicine;
                adherenceAssessment.FeelWorse = feelWorse;
                adherenceAssessment.ForgetMedicine = forgetMedicine;

                adherenceAssessment.DeleteFlag = false;
                adherenceAssessment.CreatedBy = createdBy;
                adherenceAssessment.CreateDate = DateTime.Now;

                return mgr.AddPatientAdherenceAssessment(adherenceAssessment);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
