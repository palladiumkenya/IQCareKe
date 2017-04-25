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
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientConsentRepository.Add(p);
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }

        public PatientConsent GetPatientConsent(int id)
        {

            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var consent = _unitOfWork.PatientConsentRepository.FindBy(x => x.PatientId == id)
                       .OrderBy(x => x.Id)
                       .FirstOrDefault();
                _unitOfWork.Dispose();
                return consent;
            }
        }

        public void DeletePatientConsent(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientConsent consent = _unitOfWork.PatientConsentRepository.GetById(id);
                _unitOfWork.PatientConsentRepository.Remove(consent);
               _result= _unitOfWork.Complete();
                _unitOfWork.Dispose();
            }
        }

        public int UpdatePatientConsent(PatientConsent p)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientConsent consent = new PatientConsent()
                {
                    ConsentType = p.ConsentType,
                    ConsentDate = p.ConsentDate,
                    DeclineReason = p.DeclineReason
                };
                _unitOfWork.PatientConsentRepository.Update(consent);
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }

        public List<PatientConsent> GetByPatientId(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientConsent> consent = _unitOfWork.PatientConsentRepository.GetByPatientId(patientId);
                _unitOfWork.Dispose();
                return consent;
            }
        }
    }
}