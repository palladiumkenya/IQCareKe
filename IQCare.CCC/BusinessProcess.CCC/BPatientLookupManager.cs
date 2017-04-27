using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.CCC.Repository;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using Application.Common;
using DataAccess.Base;
using DataAccess.CCC.Context;

namespace BusinessProcess.CCC
{
    public class BPatientLookupManager :ProcessBase, IPatientLookupmanager
    {
       // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext());
        private readonly Utility _utility = new Utility();

        public PatientLookup GetPatientDetailsLookup(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var patientDetails = _unitOfWork.PatientLookupRepository
                .FindBy(x => x.Id == id && !x.Active).DefaultIfEmpty(null).FirstOrDefault();

                return patientDetails;
            }

        }

        public List<PatientLookup> GetPatientByPersonId(int personId)
        {
            using (UnitOfWork u = new UnitOfWork(new LookupContext()))
            {
                return u.PatientLookupRepository.FindBy(x => x.PersonId == personId && x.Active).ToList();
            }
        }
      
        public List<PatientLookup> GetPatientSearchPayload()
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var patientSearchDetails = _unitOfWork.PatientLookupRepository
                    .GetAll()
                //    .Select(x=> new PatientLookup 
                //{
                //    EnrollmentNumber = x.EnrollmentNumber,
                //    PatientIndex = x.PatientIndex,
                //    FirstName = x.FirstName,
                //    MiddleName = x.MiddleName,
                //    DateOfBirth = x.DateOfBirth,
                //    Sex = x.Sex,
                //    RegistrationDate = x.RegistrationDate,
                //    PatientStatus = x.PatientStatus
                //})
                    .ToList();

                return patientSearchDetails;
            }

        }

        public List<PatientLookup> GetPatientSearchPayloadWithParameter(string patientId, string fname, string mname, string lname, DateTime doB, int sex, int facility,  int start, int length)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var result = _unitOfWork.PatientLookupRepository.GetAll();

                //if (!string.IsNullOrWhiteSpace(patientId))
                //{
                //    result = result.Where(x => x.EnrollmentNumber == patientId.ToString());
                //}
                //if (sex > 0)
                //{
                //    result = result.Where(x => x.Sex == sex);
                //}

                //if (!string.IsNullOrWhiteSpace(fname))
                //{
                //    result = result.Where(x =>utility.Decrypt(x.FirstName).ToLower().Contains(fname.ToLower()));
                //}
                //if (!string.IsNullOrWhiteSpace(lname))
                //{
                //    result = result.Where(x => utility.Decrypt(x.LastName).ToLower().Contains(lname.ToLower()));
                //}
                //if (!string.IsNullOrWhiteSpace(mname))
                //{
                //    result = result.Where(x => utility.Decrypt(x.MiddleName).ToLower().Contains(mname.ToLower()));
                //}
                //if (!string.IsNullOrWhiteSpace(doB.ToShortDateString()))
                //{
                //    result = result.Where(x => x.DateOfBirth.ToShortDateString() == doB.ToShortDateString());
                //}
                //if (facility > 0)
                //{
                //    result = result.Where(x => x.FacilityId == facility);
                //}
                _unitOfWork.Dispose();
                return result.ToList();
            }
        }

        public int GetTotalpatientCount()
        {
            int totalCount = 0;
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                totalCount = _unitOfWork.PatientLookupRepository.GetAll().Count();

                _unitOfWork.Dispose();

            }
            return totalCount;

        }
        public PatientLookup GetGenderID(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                PatientLookup ptnLookup =_unitOfWork.PatientLookupRepository.GetGenderId(patientId);

                _unitOfWork.Dispose();

                return ptnLookup;
            }
        }

        public int GetPatientTypeId(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                int PatientTypeId= _unitOfWork.PatientLookupRepository.FindBy(x => x.Id == patientId)
                            .Select(x => x.PatientType)
                            .FirstOrDefault();
                _unitOfWork.Dispose();

                return PatientTypeId;
            }
        }
    }
}
