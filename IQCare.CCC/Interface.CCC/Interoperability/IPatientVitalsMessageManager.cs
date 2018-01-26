using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.CCC.Interoperability;

namespace Interface.CCC.Interoperability
{
    public interface IPatientVitalsMessageManager
    {
        PatientVitalsMessage GetPatientVitalsMessageByPatientIdPatientMasterVisitId(int patientId, int patientMasterVisitId);
    }
}
