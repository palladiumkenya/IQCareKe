using System.Collections.Generic;
using Application.Common;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

namespace IQCare.CCC.UILogic
{
    public class PatientLookupManager
    {
        readonly IPatientLookupmanager _patientLookupmanager = (IPatientLookupmanager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientLookupManager, BusinessProcess.CCC");
        Utility _utility=new Utility();
        
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


       public List<PatientLookup> GetPatientSearchListPayload()
       {
           var patientDetails = _patientLookupmanager.GetPatientSearchPayload();         
            return patientDetails;
        }

        public int GetTotalpatientCount()
        {
            return _patientLookupmanager.GetTotalpatientCount();
        }
    }
}
