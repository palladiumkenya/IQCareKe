using Entities.PatientCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface.CCC
{
    public interface IPatientOvcStatusmanager
    {
        int AddPatientOvcStatus(PatientOVCStatus ovc);
        int UpdatePatientOvcStatus(PatientOVCStatus ovc);
        List<PatientOVCStatus> GetPatientOvcStatus(int id);
        int DeletePatientOvcStatus(int id);
    }
}
