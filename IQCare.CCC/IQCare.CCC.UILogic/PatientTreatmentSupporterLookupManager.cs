using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

namespace IQCare.CCC.UILogic
{
    public class PatientTreatmentSupporterLookupManager
    {
        readonly IPatientTreatmentSupporterLookupManager _patientServiceEnrollmentLookup = (IPatientTreatmentSupporterLookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Lookup.BPatientTreatmentSupporterLookupManager, BusinessProcess.CCC");

        public List<PatientTreatmentSupporterLookup> GetAllPatientTreatmentSupporter(int personId)
        {
            try
            {
                return _patientServiceEnrollmentLookup.GetAllPatientTreatmentSupporter(personId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
