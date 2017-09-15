using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.CCC.Enrollment;

namespace Interface.CCC.Enrollment
{
    public interface IPatientArtDistributionManager
    {
        int AddPatientArtDistribution(PatientArtDistribution p);

        PatientArtDistribution GetPatientArtDistribution(int id);

        void DeletePatientArtDistribution(int id);

        int UpdatePatientArtDistribution(PatientArtDistribution p);

        List<PatientArtDistribution> GetByPatientId(int patientId);
    }
}
