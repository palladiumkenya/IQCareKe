using System;
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
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        private int _result;

        public int AddPatientConsents(PatientConsent p)
        {
            try
            {
                _unitOfWork.PatientConsentRepository.Add(p);
                _result = _unitOfWork.Complete();
                return _result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }

        }

        public PatientConsent GetPatientConsent(int id)
        {
            try
            {
                var consent =
                    _unitOfWork.PatientConsentRepository.FindBy(x => x.PatientId == id)
                        .OrderBy(x => x.Id)
                        .FirstOrDefault();
                return consent;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        
        }

        public void DeletePatientConsent(int id)
        {
            try
            {
                PatientConsent consent = _unitOfWork.PatientConsentRepository.GetById(id);
                _unitOfWork.PatientConsentRepository.Remove(consent);
                _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }

        }

        public int UpdatePatientConsent(PatientConsent p)
        {
            try
            {
                PatientConsent consent = new PatientConsent()
                {
                    ConsentType = p.ConsentType,
                    ConsentDate = p.ConsentDate,
                    DeclineReason = p.DeclineReason
                };
                _unitOfWork.PatientConsentRepository.Update(consent);
                _result = _unitOfWork.Complete();
                return _result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
  
        }

        public List<PatientConsent> GetByPatientId(int patientId)
        {
            try
            {
                List<PatientConsent> consent = _unitOfWork.PatientConsentRepository.GetByPatientId(patientId);
                return consent;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
      
        }
    }
}