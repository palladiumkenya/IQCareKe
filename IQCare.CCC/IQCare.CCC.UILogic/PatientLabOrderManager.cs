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
        public int labOrderId { get; set; }
        public string labNotes { get; set; }

    }
    public class PatientLabOrderManager
    {
        private string Msg { get; set; }
        IPatientLabOrderManager _mgr = (IPatientLabOrderManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.visit.BPatientLabOrdermanager, BusinessProcess.CCC");

       
        public int savePatientLabOrder(int patient_ID, int facilityID, int patientMasterVisitId, string patientLabOrder)
        {
           
            try
            {
                var jss = new JavaScriptSerializer();
            IList<ListLabOrder> data = jss.Deserialize<IList<ListLabOrder>>(patientLabOrder);

                 if (patient_ID > 0)
                {

                    int returnValue;
                    int returnLabOrderSuccess;
                    int returnLabDetailsSuccess;


                    for (int i = 0; i < data.Count; i++)
                    {
                        PatientLabTracker labTracker = new PatientLabTracker()
                        {
                            PatientId = patient_ID,
                            PatientMasterVisitId = patientMasterVisitId,
                            LabName = data[i].labType,
                            Reasons = data[i].orderReason,                           
                            Results = data[i].labNotes,
                            SampleDate = data[i].labOrderDate

                        };
                        returnValue = _mgr.AddPatientLabTracker(labTracker);

                        LabOrderEntity labOrder = new LabOrderEntity()
                        {
                            Ptn_pk = patient_ID,
                            LocationId = facilityID,
                            visitid = patientMasterVisitId,
                            ClinicalOrderNotes = data[i].labNotes,
                            OrderStatus = data[i].results,
                            OrderDate = data[i].labOrderDate
                            //UserId = data[i].labType,
                            //ClinicalOrderNotes = data[i].results,       
                            //LocationId = data[i].orderReason,
                        };
                        returnLabOrderSuccess = _mgr.AddPatientLabOrder(labOrder);

                      
                        //Populate lab details
                        //if (returnLabOrderSuccess > 0)
                        //{
                        
                        //    LabDetailsEntity LabDetails = new LabDetailsEntity()
                        //    {
                        //        LabOrderId = labOrderId,
                        //        LabTestId = facilityID,
                        //        TestNotes = data[i].labNotes,
                        //        //IsParent = data[i].labNotes,
                        //        //ParentTestId = data[i].results,
                        //        //ResultNotes = data[i].labOrderDate
                        //        //ResultStatus = data[i].labType,
                        //        //ResultDate = data[i].results,
                        //       // UserId = data[i].orderReason,
                        //        //StatusDate = data[i].orderReason,
                        //    };

                        //    returnLabDetailsSuccess = _mgr.AddPatientLabDetails(LabDetails);
                        // }
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
