using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core.Interfaces;
using VisitManagement.Core.Model;

namespace VisitManagement.Core.Interfaces
{
    public interface IPatientEncounterRepository :IRepository<PatientEncounter>
    {
        PatientEncounter GetPatientEncounterByService(int serviceId);
        PatientEncounter GetPatientEncounterByMasterVisit(int patientmastervisitId);
        PatientEncounter GetPatientEncounterByDateRangEncounter(DateTime dateFrom, DateTime dateTo);
    }
}
