using Entities.PatientCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface.CCC
{
    public interface IPatientOvcStatusmanager
    {
        void AddPatientOvcStatus(PatientOVCStatus ovc);
        void UpdatePatientOvcStatus(PatientOVCStatus ovc);
        List<PatientOVCStatus> GetPatientOvcStatus(int id);
        void DeletePatientOvcStatus(int id);
    }
}
