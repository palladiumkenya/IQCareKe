using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Presentation;
using Entities.CCC.Encounter;
using Interface.CCC.Encounter;

namespace IQCare.CCC.UILogic
{
   public  class PatientSexualHistoryManager
    {
        IPatientSexualHistoryManager _mgr = (IPatientSexualHistoryManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientSexualHistoryManager, BusinessProcess.CCC");


        public PatientSexualHistory  AddPatientSexualHistory(PatientSexualHistory psh)
        {
            return _mgr.AddPatientSexualHistory(psh);
        }

        public PatientSexualHistory UpdatePatientSexualHistory(PatientSexualHistory patientsexual)
        {
            return _mgr.UpdatePatientSexualHistory(patientsexual);
        }

        public PatientSexualHistory GetPatientSexualHistory(int patientid,int patientmastervisitid,int id)
        {
            return _mgr.GetPatientSexualHistory(patientid,patientmastervisitid,id);
        }
        public List<PatientSexualHistory> GetPatientSexualHistoryList(int patientid,int patientmastervisitid)
        {
            return _mgr.GetPatientSexualHistoryList(patientid, patientmastervisitid);
        }
    }
}