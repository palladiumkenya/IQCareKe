using System;
using System.Collections.Generic;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;

namespace BusinessProcess.CCC.Baseline
{
    public class BPatientFamilyTestingManager:ProcessBase,IPatientHivTestingManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatientHivTesting(PatientHivTesting patientHivTesting)
        {
            throw new NotImplementedException();
        }

        public int UpdatePatientHivTesting(PatientHivTesting patientHivTesting)
        {
            throw new NotImplementedException();
        }

        public int DeletePatientHivTesting(int id)
        {
            throw new NotImplementedException();
        }

        public List<PatientHivTesting> GetPatientHivTestings(int patientId)
        {
            throw new NotImplementedException();
        }
    }
}
