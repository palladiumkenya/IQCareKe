using Application.Presentation;
using Entities.CCC.Visit;
using Entities.CCC.Encounter;
using Interface.CCC.Visit;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;


namespace IQCare.CCC.UILogic
{
    public class ListLabOrder
    {
        
        public string labType { get; set; }
        public string orderReason { get; set; }
        public string results { get; set; }
        public DateTime labOrderDate { get; set; }
        public int labOrderId { get; set; }
        //public int labTestId { get; set; }
        public string labNotes { get; set; }

    }
    public class PatientLabOrderManager
    {
        private string Msg { get; set; }
        IPatientLabOrderManager _mgr = (IPatientLabOrderManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.visit.BPatientLabOrdermanager, BusinessProcess.CCC");
        ILookupManager _lookupTest = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");


        public void savePatientLabOrder(int patient_ID, int patient_Pk, int facilityID, int patientMasterVisitId, string patientLabOrder)
        {
           
            try
            {
                var jss = new JavaScriptSerializer();
                IList<ListLabOrder> data = jss.Deserialize<IList<ListLabOrder>>(patientLabOrder);

                if (patient_ID > 0)
                {
                    int returnValue;
                    int returnLabOrderSuccess;
                    var pending = "Pending";
                    foreach (ListLabOrder t in data)
                    {
                                // Get LabID
                                string labType = t.labType;
                                if (labType != null)
                                {
                                LookupLabs testId = _lookupTest.GetLabTestId(labType);
                                int labTestId = testId.LabTestId;

                            PatientLabTracker labTracker = new PatientLabTracker()
                            {
                                PatientId = patient_ID,
                                PatientMasterVisitId = patientMasterVisitId,
                                LabName = t.labType,
                                Reasons = t.orderReason,
                                Results = pending,
                                SampleDate = t.labOrderDate
                                //LabNotes =data[i].labNotes --take to clinical notes 

                            };
                            _mgr.AddPatientLabTracker(labTracker);

                            LabOrderEntity labOrder = new LabOrderEntity()
                            {
                                Ptn_pk = patient_Pk,
                                LocationId = facilityID,
                                ModuleId = 1,
                                OrderedBy = 1,
                                LabTestId = labTestId,
                                PatientMasterVisitId = patientMasterVisitId,
                                ClinicalOrderNotes = t.labNotes,
                                OrderStatus = pending,
                                OrderDate = t.labOrderDate,
                                PreClinicLabDate = t.labOrderDate
                                //UserId = data[i].labType,
                                //ClinicalOrderNotes = data[i].results,       
                                //LocationId = data[i].orderReason,
                            };
                            _mgr.AddPatientLabOrder(labOrder);

                            //returnLabOrderSuccess;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Msg = ex.Message + ' ' + ex.InnerException;
            }

           // return int.Parse(Msg);
        }
    }
   }
