using Entities.CCC.Encounter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.CCC.Encounter
{
    public interface IPatientPartners
    {
        PatientPartner addPatientPartner(PatientPartner patientpartner);
        PatientPartner GetPatientPartner(int patientId, int patientMasterVisitId);


        List<PatientPartner> GetPatientPartners(int patientId, int patientMasterVisitId);

        PatientPartner GetPatientPartnerbyId(int entityId);

        PatientPartner UpdatePatientPartner(PatientPartner patientpartner);
    }
}
