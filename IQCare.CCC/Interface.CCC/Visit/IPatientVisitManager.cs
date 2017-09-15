using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.CCC.Visit;

namespace Interface.CCC.Visit
{
   public interface IPatientVisitManager
    {
     
        int AddPatientVisit(PatientVisit patientVisit);
    }
}
