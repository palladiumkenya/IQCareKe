using DataAccess.Context;
using Entities.Records.Consent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Records.Interface { 
   public  interface IPatientConsentRepository:IRepository<PatientConsent>
    {
        List<PatientConsent> GetByPatientId(int patientId);
    }
}
