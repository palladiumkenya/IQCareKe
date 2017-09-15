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
    public class PatientServiceEnrollmentLookupManager
    {
        readonly IPatientServiceEnrollmentLookupManager _patientServiceEnrollmentLookup = (IPatientServiceEnrollmentLookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Lookup.BPatientServiceEnrollmentLookupManager, BusinessProcess.CCC");

        public List<PatientServiceEnrollmentLookup> GetPatientServiceEnrollments(int personId)
        {
            return _patientServiceEnrollmentLookup.GetPatientServiceEnrollments(personId);
        }
    }
}
