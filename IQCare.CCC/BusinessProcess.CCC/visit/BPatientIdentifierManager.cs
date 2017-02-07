using System;
using System.Collections.Generic;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Enrollment;
using Interface.CCC.Visit;

namespace BusinessProcess.CCC.visit
{
    public class BPatientIdentifierManager:IPatientIdentifierManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
    

        public int AddPatientIdentifier(PatientEntityIdentifier patientIdentifier)
        {
            throw new NotImplementedException();
        }

        public int UpdatePatientIdentifier(PatientEntityIdentifier patientIdentifier)
        {
            throw new NotImplementedException();
        }

        public int DeletePatientIdentifier(int id)
        {
            throw new NotImplementedException();
        }

        public List<PatientEntityIdentifier> GetPatientIdentifiers(int patientId)
        {
            throw new NotImplementedException();
        }

        public List<PatientEntityIdentifier> GetPatientServicePatientIdentifiers(int serviceId, int personId)
        {
            throw new NotImplementedException();
        }
    }
}
