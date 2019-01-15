using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Consent;
using Interface.CCC;
using System.Collections.Generic;
using System.Linq;

namespace BusinessProcess.CCC
{
    public class BPatientConsent : ProcessBase, IPatientConsent
    {
       // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        private int _result;

        public int AddPatientConsents(PatientConsent p)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientConsentRepository.Add(p);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }

        public PatientConsent GetPatientConsent(int id)
        {

            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var consent = unitOfWork.PatientConsentRepository.FindBy(x => x.PatientId == id)
                       .OrderBy(x => x.Id)
                       .FirstOrDefault();
                unitOfWork.Dispose();
                return consent;
            }
        }

        public void DeletePatientConsent(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientConsent consent = unitOfWork.PatientConsentRepository.GetById(id);
                unitOfWork.PatientConsentRepository.Remove(consent);
               _result= unitOfWork.Complete();
                unitOfWork.Dispose();
            }
        }

        public int UpdatePatientConsent(PatientConsent p)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientConsent consent = new PatientConsent()
                {
                    ConsentType = p.ConsentType,
                    ConsentDate = p.ConsentDate,
                    DeclineReason = p.DeclineReason
                };
                unitOfWork.PatientConsentRepository.Update(consent);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }

        public List<PatientConsent> GetByPatientId(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientConsent> consent = unitOfWork.PatientConsentRepository.GetByPatientId(patientId);
                unitOfWork.Dispose();
                return consent;
            }
        }

        public List<PatientConsent> GetPatientConsentByType(int patientId, int consentType)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientConsent> consent = unitOfWork.PatientConsentRepository.FindBy(x => x.PatientId == patientId && x.ConsentType == consentType && x.DeleteFlag == false).ToList();
                unitOfWork.Dispose();
                return consent;
            }
        }
    }
}