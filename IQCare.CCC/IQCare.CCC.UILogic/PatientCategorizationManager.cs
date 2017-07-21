using Entities.CCC.Encounter;
using Interface.CCC;
using System;
using Application.Presentation;

namespace IQCare.CCC.UILogic
{
    public class PatientCategorizationManager
    {
        private IPatientCategorizationManager _categorization = (IPatientCategorizationManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientCategorization, BusinessProcess.CCC");
        public int AddPatientCategorization(PatientCategorization p)
        {
            var categorization = new PatientCategorization()
            {
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
    }
}