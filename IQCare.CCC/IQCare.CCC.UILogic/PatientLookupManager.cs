using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<PatientLookup> GetPatientSearchPayloadWithParameter(string patientId, string fname, string mname,
            string lname, DateTime doB, int sex, int facility, int start, int length)
        {
            IPatientLookupmanager patientLookupmanager = (IPatientLookupmanager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientLookupManager, BusinessProcess.CCC");

            var patientList = patientLookupmanager.GetPatientSearchPayloadWithParameter(patientId, fname, mname, lname,
                doB, sex, facility,start,length);
            return patientList;
        }


        public int GetTotalpatientCount()
        {
            return _patientLookupmanager.GetTotalpatientCount();
        }

        public static bool IsEncrypted(string text)
        {
            if (string.IsNullOrEmpty(text)) return false;
            return text.StartsWith("0x002", StringComparison.InvariantCulture);
        }
    }
}
