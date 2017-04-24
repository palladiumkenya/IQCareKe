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
        
        public string LabType { get; set; }
        public string OrderReason { get; set; }
        public string Results { get; set; }
        public DateTime LabOrderDate { get; set; }
        public int LabOrderId { get; set; }
        //public int labTestId { get; set; }
        public string LabNotes { get; set; }

    }
    public class PatientLabOrderManager
    {
        private string Msg { get; set; }
        IPatientLabOrderManager _mgr = (IPatientLabOrderManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.visit.BPatientLabOrdermanager, BusinessProcess.CCC");
        ILookupManager _lookupTest = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");


        public int savePatientLabOrder(int patient_ID, int patient_Pk, int userId, int facilityID, int patientMasterVisitId, string patientLabOrder)
        {
            int returnLabOrderSuccess = 0;
            try
            {
                var jss = new JavaScriptSerializer();
                IList<ListLabOrder> data = jss.Deserialize<IList<ListLabOrder>>(patientLabOrder);

                if (patient_ID > 0)
                {
                   
                  
                    var pending = "Pending";
                    foreach (ListLabOrder t in data)
                    {
                                // Get LabTestID
                                string labType = t.LabType;
                                if (labType != null)
                                {
                                LookupLabs testId = _lookupTest.GetLabTestId(labType);
                                int labTestId = testId.Id;

                            LabOrderEntity labOrder = new LabOrderEntity()
                            {
                                Ptn_pk = patient_Pk,
                                LocationId = facilityID,
                                ModuleId = 211,
                                OrderedBy = userId,
                                LabTestId = labTestId,
                                PatientMasterVisitId = patientMasterVisitId,
                                ClinicalOrderNotes = t.LabNotes,
                                OrderStatus = pending,
                                OrderDate = t.LabOrderDate,
                                CreatedBy = userId,
                                UserId = userId,
                                PreClinicLabDate = t.LabOrderDate,
                                LabName = t.LabType,
                                patientId = patient_ID,
                                Reason = t.OrderReason
                            };
                          returnLabOrderSuccess = _mgr.AddPatientLabOrder(labOrder);
                            return returnLabOrderSuccess;



                        }
                    }
                }
            }
            catch (Exception ex)
             {
                Msg = ex.Message + ' ' + ex.InnerException;
             }

            return int.Parse(Msg);
        }
        public List<LabOrderEntity> GetVlPendingCount(int facilityId)
        {
            var pendingLabs = _mgr.GetVlPendingCount(facilityId);
            return pendingLabs;
        }
        public List<LabOrderEntity> GetVlCompleteCount(int facilityId)
        {
            var completeLabs = _mgr.GetVlCompleteCount(facilityId);
            return completeLabs;
        }
    }
   }
