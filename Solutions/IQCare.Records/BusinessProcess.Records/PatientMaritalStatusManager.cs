using DataAccess.Base;

using DataAccess.Context;
using Entities.PatientCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface.Records;
using DataAccess.Records;

namespace BusinessProcess.Records
{
    public class PatientMaritalStatusManager : ProcessBase,IPatientMaritalStatusManager
    {
        //private readonly UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext());
        private int result;

        public int AddPatientMaritalStatus(PatientMaritalStatus patientMarital)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PersonContext()))
            {
                unitOfWork.PatientMaritalStatusRepository.Add(patientMarital);
                result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return result;
            }
        }

        public int DeletePatientMaritalStatus(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PersonContext()))
            {
                var patientmaritalstatus = unitOfWork.PatientMaritalStatusRepository.GetById(id);
                unitOfWork.PatientMaritalStatusRepository.Remove(patientmaritalstatus);
                result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return result;
            }
        }

        public List<PatientMaritalStatus> GetAllMaritalStatuses(int personId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PersonContext()))
            {
                List<PatientMaritalStatus> myList;
                myList = unitOfWork.PatientMaritalStatusRepository.FindBy(x => x.PersonId == personId & !x.DeleteFlag)
                    .OrderBy(x => x.Id)
                    .ToList();
                return myList;
            }

        }

        public int UpdatePatientMaritalStatus(PatientMaritalStatus patientMarital)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PersonContext()))
            {
                unitOfWork.PatientMaritalStatusRepository.Update(patientMarital);
                result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return result;

            }

        }

        public PatientMaritalStatus GetFirstPatientMaritalStatus(int personId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PersonContext()))
            {
                var patientms = unitOfWork.PatientMaritalStatusRepository.FindBy(x => x.PersonId == personId)
             .OrderByDescending(o => o.CreateDate)
             .FirstOrDefault();
                unitOfWork.Dispose();
                return patientms;
            }

        }

        public PatientMaritalStatus GetCurrentPatientMaritalStatus(int personId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PersonContext()))
            {
                var patientms = unitOfWork.PatientMaritalStatusRepository.FindBy(x => x.PersonId == personId && !x.DeleteFlag)
                    .OrderBy(x => x.Id).FirstOrDefault();
                unitOfWork.Dispose();
                return patientms;

            }

        }
    }
}
