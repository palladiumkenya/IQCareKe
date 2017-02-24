using Application.Presentation;
using Entities.CCC.Visit;
using Entities.CCC.Encounter;
using Interface.CCC.Visit;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;


namespace IQCare.CCC.UILogic
{
    public class ListLabOrder
    {
        
        public string labType { get; set; }
        public string orderReason { get; set; }
        public string results { get; set; }
        public DateTime labOrderDate { get; set; }
        public string labNotes { get; set; }

    }
    public class PatientLabOrderManager
    {
        private string Msg { get; set; }
        IPatientLabOrderManager _mgr = (IPatientLabOrderManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.visit.BPatientLabOrdermanager, BusinessProcess.CCC");

       
        public int savePatientLabOrder(int patient_ID, int patientMasterVisitId, string patientLabOrder)
        {
           
            try
            {
                var jss = new JavaScriptSerializer();
            IList<ListLabOrder> data = jss.Deserialize<IList<ListLabOrder>>(patientLabOrder);

                 if (patient_ID > 0)
                {

                    int returnValue;
                   

                    for (int i = 0; i < data.Count; i++)
                    {
                        PatientLabTracker LabTracker = new PatientLabTracker()
                        {
                            PatientId = patient_ID,
                            PatientMasterVisitId = patientMasterVisitId,
                            LabName = data[i].labType,
                            Reasons = data[i].orderReason,                           
                            Results = data[i].labNotes,
                            SampleDate = data[i].labOrderDate

                        };
                        returnValue = _mgr.AddPatientLabTracker(LabTracker);

                        LabOrderEntity LabOrder = new LabOrderEntity()
                        {
                            Ptn_Pk = patient_ID,
                            visitid = patientMasterVisitId,
                            ClinicalOrderNotes = data[i].labNotes,
                            OrderStatus = data[i].results,
                            OrderDate = data[i].labOrderDate
                            //UserId = data[i].labType,
                            //ClinicalOrderNotes = data[i].results,       
                            //LocationId = data[i].orderReason,




                        };
                        returnValue = _mgr.AddPatientLabOrder(LabOrder);
                        return returnValue;

                    }
                }
             }
            catch (Exception ex)
            {
                Msg = ex.Message + ' ' + ex.InnerException;
            }

            return int.Parse(Msg);
        }
    }
   }
