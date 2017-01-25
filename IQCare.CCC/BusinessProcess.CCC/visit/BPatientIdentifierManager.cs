using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Enrollment;
using Interface.CCC.Visit;

namespace BusinessProcess.CCC.visit
{
   public class BPatientIdentifierManager:IPatientIdentifierManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatientIdentifier(PatientIdentifier patientIdentifier)
        {
            throw new NotImplementedException();
        }

        public int UpdatePatientIdentifier(PatientIdentifier patientIdentifier)
        {
            throw new NotImplementedException();
        }

        public int DeletePatientIdentifier(int id)
        {
            throw new NotImplementedException();
        }

        public List<PatientIdentifier> GetPatientIdentifiers(int patientId)
        {
            throw new NotImplementedException();
        }

        public List<PatientIdentifier> GetPatientServicePatientIdentifiers(int serviceId, int personId)
        {
            throw new NotImplementedException();
        }
    }
}
