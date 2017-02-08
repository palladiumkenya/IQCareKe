using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

namespace IQCare.CCC.UILogic
{
    public class PatientLookupManager
    {
        readonly IPatientLookupmanager _patientLookupmanager = (IPatientLookupmanager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientLookupManager, BusinessProcess.CCC");

       public List<PatientLookup> GetPatientDetailSummary(int id)
        {
            var patientDetails = _patientLookupmanager.GetPatientDetailsLookup(id);
            if (patientDetails != null && patientDetails.Count > 0)
            {
                return patientDetails;
            }
            else
            {
                
            }
            return patientDetails;
        }


       public List<PatientLookup> GetPatientSearchList()
        {
            var patientDetails = _patientLookupmanager.SearchPatient();
            return patientDetails;
        }
    }
}
