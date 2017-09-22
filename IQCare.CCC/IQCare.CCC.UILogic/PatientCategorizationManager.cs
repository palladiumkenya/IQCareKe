using Application.Presentation;
using Entities.CCC.Triage;
using Interface.CCC;
using System.Collections.Generic;

namespace IQCare.CCC.UILogic
{
    public class PatientCategorizationManager
    {
        private IPatientCategorizationManager _categorization = (IPatientCategorizationManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientCategorization, BusinessProcess.CCC");

        public int AddPatientCategorization(PatientCategorization p)
        {
            var categorization = new PatientCategorization()
            {
                PatientId = p.PatientId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                Categorization = p.Categorization,
                DateAssessed = p.DateAssessed
            };
            return _categorization.AddPatientCategorization(categorization);
        }

        public PatientCategorization GetPatientCategorization(int id)
        {
            var categorization = _categorization.GetPatientCategorization(id);
            return categorization;
        }

        public void DeletePatientCategorization(int id)
        {
            _categorization.DeletePatientCategorization(id);
        }

        public int UpdatePatientCategorization(PatientCategorization p)
        {
            var categorization = new PatientCategorization()
            {
                Categorization = p.Categorization,
                DateAssessed = p.DateAssessed
            };
            return _categorization.UpdatePatientCategorization(categorization);
        }

        public List<PatientCategorization> GetByPatientId(int patientId)
        {
            var categorization = _categorization.GetByPatientId(patientId);
            return categorization;
        }
    }
}