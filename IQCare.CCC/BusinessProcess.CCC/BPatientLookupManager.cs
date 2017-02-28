using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.CCC.Repository;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using Application.Common;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository.Lookup;

namespace BusinessProcess.CCC
{
    public class BPatientLookupManager :ProcessBase, IPatientLookupmanager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext());
        private readonly Utility _utility = new Utility();

        public List<PatientLookup> GetPatientDetailsLookup(int id)
        {
            var patientDetails = _unitOfWork.PatientLookupRepository
                .FindBy(x => x.Id == id || (x.ptn_pk.Value == id & !x.Active))
                .Take(1).ToList();

            return patientDetails;
        }

        public List<PatientLookup> GetPatientSearchPayload()
        {
            var patientSearchDetails =_unitOfWork.PatientLookupRepository
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

        public List<PatientLookup> GetPatientSearchPayloadWithParameter(string patientId, string fname, string mname, string lname, DateTime doB, int sex, int facility,  int start, int length)
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

                return result.ToList();
        }

        public int GetTotalpatientCount()
        {
            var totalCount = _unitOfWork.PatientLookupRepository.GetAll().Count();

            return totalCount;
        }
        public PatientLookup GetGenderID(int patientId)

        {
            PatientLookupRepository lookupGender = new PatientLookupRepository();
            return lookupGender.GetGenderID(patientId);
        }
    }
}
