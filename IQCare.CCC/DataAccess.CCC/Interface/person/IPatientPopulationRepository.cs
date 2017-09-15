using System.Collections.Generic;
using DataAccess.Context;
using Entities.PatientCore;

namespace DataAccess.CCC.Interface.person
{
    public  interface IPatientPopulationRepository:IRepository<PatientPopulation>
    {
        List<PatientPopulation> GetPatientPopulations(int patientId);
    }
}
