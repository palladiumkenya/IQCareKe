using System;
using System.Collections.Generic;
using Application.Presentation;
using Entities.PatientCore;
using Interface.CCC;

namespace IQCare.CCC.UILogic
{
    public class PatientTreatmentSupporterManager
    {
        private IPatientTreatmeantSupporterManager _mgr = (IPatientTreatmeantSupporterManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientTreatmentSupporterManager, BusinessProcess.CCC");
        private int _result;
        private string _msg;

        public int AddPatientTreatmentSupporter(int personId,int supporterId,string mobileConatct,int userId)
        {

                PatientTreatmentSupporter supporter = new PatientTreatmentSupporter()
                {
                   PersonId = personId,
                   SupporterId = supporterId,
                   MobileContact = mobileConatct,
                   CreatedBy = userId
                };

                _result=_mgr.AddPatientTreatmentSupporter(supporter);
            
            return _result;
        }

        public int UpdatePatientTreatmentSupporter(PatientTreatmentSupporter supporter)
        {
            _result = _mgr.UpdatePatientTreatmentSupporter(supporter);
            return _result;
        }

        public string DeletePersonTreatmentSupporter(int id)
        {
            try
            {
                _result = _mgr.DeletePatientTreatmentSupporter(id);
                if (_result > 0)
                {
                    _msg = "Person Treatment recorded deleted successfully";
                }
            }
            catch (Exception e)
            {
                _msg = e.Message + ' ' + e.InnerException;
            }
            return _msg;
        }

        public List<PatientTreatmentSupporter> GetPatientTreatmentSupporter(int personId)
        {
            List<PatientTreatmentSupporter> myList;

            try
            {
                myList = _mgr.GetCurrentTreatmentSupporter(personId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message +  ' ' +e.InnerException);
                throw;
            }
            return myList;
        }

        public List<PatientTreatmentSupporter> GetAllPatientTreatmentSupporter(int personId)
        {
            List<PatientTreatmentSupporter> myList;

            try
            {
                myList = _mgr.GetAllTreatmentSupporter(personId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ' ' + e.InnerException);
                throw;
            }
            return myList;
        }

    }
}
