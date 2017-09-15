using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.CCC.Baseline;

namespace Interface.CCC.Baseline
{
    public interface IPatientLinkageManager
    {
        int AddPatientLinkage(PatientLinkage patientLinkage);
        List<PatientLinkage> GetPatientLinkage(int personId);
        bool CccNumberExists(string cccNumber);
    }
}
