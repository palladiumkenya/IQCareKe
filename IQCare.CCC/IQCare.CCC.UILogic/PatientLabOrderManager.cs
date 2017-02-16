using Application.Presentation;
using Entities.CCC.Visit;
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
        public string labOrderDate { get; set; }

    }
    public class PatientLabOrderManager
    {
        private string Msg { get; set; }
        IPatientLabOrderManager _mgr = (IPatientLabOrderManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.visit.BPatientLabOrdermanager, BusinessProcess.CCC");

       
        public int savePatientLabOrder(int patientId, int visitId, string patientLabOrder)
        {
           
            try
            {
                var jss = new JavaScriptSerializer();
            IList<ListLabOrder> data = jss.Deserialize<IList<ListLabOrder>>(patientLabOrder);

                 if (patientId > 0)
                {

                    int returnValue;

                    for (int i = 0; i < data.Count; i++)
                    {
                        PatientLabTracker LabOrder = new PatientLabTracker()
                        {
                            PatientId = patientId,
                            patientMasterVisitId = visitId,
                            Results= data[i].results,
                            LabName = data[i].labType,
                            Reasons = data[i].orderReason,
                            SampleDate = data[0].labOrderDate

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
