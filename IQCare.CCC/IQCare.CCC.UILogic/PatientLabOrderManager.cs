using Application.Presentation;
using Entities.CCC.Visit;
using Interface.CCC.Visit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.CCC.UILogic
{
    public class PatientLabOrderManager
    {
        IPatientLabOrderManager _mgr = (IPatientLabOrderManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.visit.BPatientLabOrdermanager, BusinessProcess.CCC");

       
        public int savePatientLabOrder(int patientID, string labType, string orderReason, string labNotes, string SampleDate)
        {


            int returnValue;
            try
            {
                PatientLabTracker patientLabOrder = new PatientLabTracker()
                {
                    PatientId = patientID,
                    LabName = labType,
                    Reasons = orderReason,
                    SampleDate = SampleDate,

                };

                returnValue = _mgr.AddPatientLabOrder(patientLabOrder);
                return returnValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
