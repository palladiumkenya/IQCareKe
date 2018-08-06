using DataAccess.Base;
using DataAccess.Records;
using DataAccess.Records.Context;
using Entities.Records.Consent;
using Interface.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProcess.Records
{
    public class BPatientConsent :ProcessBase,IPatientConsent
    {
        private int _result;
        public int AddPatientConsents(PatientConsent p)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
            {
                unitOfWork.PatientConsentRepository.Add(p);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }

        public PatientConsent GetPatientConsent(int id)
        {
            using(UnitOfWork unitOfWork=new UnitOfWork(new RecordContext()))

            {
                var consent = unitOfWork.PatientConsentRepository.FindBy(x => x.PatientId == id).OrderBy(x => x.Id).FirstOrDefault();
                unitOfWork.Dispose();
                return consent;

            }
        }

        public int UpdatePatientConsent(PatientConsent p)
        {
            using (UnitOfWork unitofWork = new UnitOfWork(new RecordContext()))
            {
                PatientConsent consent = new PatientConsent()
                {
                    ConsentType = p.ConsentType,
                    ConsentDate = p.ConsentDate,
                    DeclineReason = p.DeclineReason,
                    ConsentReason = p.ConsentReason,
                    ConsentValue = p.ConsentValue
                };
                unitofWork.PatientConsentRepository.Update(consent);
                _result = unitofWork.Complete();
                unitofWork.Dispose();
                return _result;
            }
        }
        public void DeletePatientConsent(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
            {
                PatientConsent consent = unitOfWork.PatientConsentRepository.GetById(id);
                unitOfWork.PatientConsentRepository.Remove(consent);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
            }
        }
        public List<PatientConsent> GetByPatientId(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
            {
                List<PatientConsent> consent = unitOfWork.PatientConsentRepository.GetByPatientId(patientId);
                unitOfWork.Dispose();
                return consent;
            }
        }

    }
}
