using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DataAccess.Base;
using Entities.PatientCore;
using Interface.CCC;
using DataAccess.CCC.Repository;
using DataAccess.Context;

namespace BusinessProcess.CCC
{

    public class BPatientTreatmentSupporterManager:ProcessBase,IPatientTreatmeantSupporterManager
    {
       // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext());
        private int _result;

        private PatientTreatmentSupporter _patienPersonTreatmentSupporter;

        public int AddPatientTreatmentSupporter(PatientTreatmentSupporter patientTreatmentSupporter)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                SqlParameter personIdParameter = new SqlParameter("personIdParameter", SqlDbType.Int);
                personIdParameter.Value = patientTreatmentSupporter.PersonId;

                SqlParameter supporterIdParameter = new SqlParameter("supporterIdParameter", SqlDbType.Int);
                supporterIdParameter.Value = patientTreatmentSupporter.SupporterId;

                SqlParameter mobileNumberParameter = new SqlParameter("mobileNumberParameter", SqlDbType.VarBinary);
                mobileNumberParameter.Value = Encoding.ASCII.GetBytes(patientTreatmentSupporter.MobileContact);

                SqlParameter userId = new SqlParameter("UserId", SqlDbType.Int);
                userId.Value = patientTreatmentSupporter.CreatedBy;

                _unitOfWork.PersonContactRepository.ExecuteProcedure(
                    "exec PatientTreatmentSupporter_Insert @personIdParameter, @supporterIdParameter, @mobileNumberParameter,@UserId",
                    personIdParameter, supporterIdParameter, mobileNumberParameter, userId);
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;

                //_unitOfWork.PatientTreatmentSupporterRepository.Add(patientTreatmentSupporter);
                //_result = _unitOfWork.Complete();
                //_unitOfWork.Dispose();
                //return _result;
            }
        }

        public int UpdatePatientTreatmentSupporter(PatientTreatmentSupporter patientTreatmentSupporter)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {

                _unitOfWork.PatientTreatmentSupporterRepository.Add(patientTreatmentSupporter);
                 _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }

        public int DeletePatientTreatmentSupporter(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                _patienPersonTreatmentSupporter = _unitOfWork.PatientTreatmentSupporterRepository.GetById(id);
                _unitOfWork.PatientTreatmentSupporterRepository.Remove(_patienPersonTreatmentSupporter);
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }

        public List<PatientTreatmentSupporter> GetCurrentTreatmentSupporter(int personId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                List<PatientTreatmentSupporter> patientTreatmentSupporters =
                   _unitOfWork.PatientTreatmentSupporterRepository.FindBy(
                           x => x.PersonId == personId & x.DeleteFlag == false)
                       .OrderByDescending(x => x.Id)
                       .Take(1)
                       .ToList();
                _unitOfWork.Dispose();
                return patientTreatmentSupporters;
            }
        }

        public List<PatientTreatmentSupporter> GetAllTreatmentSupporter(int personId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                List<PatientTreatmentSupporter> patientTreatmentSupporters =
                _unitOfWork.PatientTreatmentSupporterRepository.FindBy(
                        x => x.PersonId == personId & x.DeleteFlag == false)
                    .OrderByDescending(x => x.Id)
                    .ToList();
                _unitOfWork.Dispose();
                return patientTreatmentSupporters;
            }
        }
    }
}
