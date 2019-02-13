using Entities.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Records
{
   public interface IPatientVisitManager
    {
        int AddPatientVisit(PatientVisit patientVisit);
    }
}
