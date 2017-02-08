﻿using System;
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
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext());
        private readonly Utility _utility = new Utility();

        public List<PatientLookup> GetPatientDetailsLookup(int id)
        {
            var patientDetails = _unitOfWork.PatientLookupRepository
                .FindBy(x => x.Id == id || x.ptn_pk.Value == id & !x.Active)
                .Take(1).ToList();

            return patientDetails;
        }

        public List<PatientLookup> SearchPatient()
        {
            var patientDetails =
                _unitOfWork.PatientLookupRepository.FindBy(x => !x.Active)
                    .OrderByDescending(x => x.Id)
                    .Distinct()
                    .Select(
                        x =>
                            new PatientLookup
                            {
                               EnrollmentNumber = x.EnrollmentNumber,
                              PatientIndex  = x.PatientIndex,
                              FirstName  = x.FirstName,
                              MiddleName  = x.MiddleName,
                              DateOfBirth  = x.DateOfBirth,
                              Sex  = x.Sex,
                              RegistrationDate  = x.RegistrationDate,
                              PatientStatus  = x.PatientStatus
                            })
                    .OrderByDescending(x => x.Id)
                    .ToList();

            return patientDetails;
        }
    }
}
