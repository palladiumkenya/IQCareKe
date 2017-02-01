using Entities.CCC.Baseline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface.CCC.Baseline
{
    public interface IPatientDisclosureManager
    {
        int AddPatientDisclosure(PatientDisclosure patientDisclosure);

        int UpdatePatientDisclosure(PatientDisclosure patientDisclosure);

        int DeletePatientDisclosure(int id);

        //List<PatientDisclosure> GetPatientDisclosures(int patientId, DateTime visitDate);
        //List<PatientDisclosure> GetPatientDisclosureAll(int patientId);
    }
}
