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
        
        public string LabName{ get; set; }
        public int LabNameId { get; set; }
        public int VisitId { get; set; }
        public int FacilityId { get; set; }
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
        private readonly IPatientVisitManager _visitManager = (IPatientVisitManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.visit.BPatientVisit, BusinessProcess.CCC");

        public void savePatientLabOrder(int patientID, int patient_Pk, int userId, int facilityID, int moduleId, int patientMasterVisitId, string labOrderDate, string orderNotes, string patientLabOrder)
        {
            int visitId = 0;
            int orderId = 0;
            int testId = 0;
            int _paramId = 0;
            DateTime orderDate = Convert.ToDateTime(labOrderDate);
           // DateTime orderDate = DateTime.Now;
            //DateTime orderDate = DateTime.Parse(labOrderDate);
            var pending = "Pending";
            var jss = new JavaScriptSerializer();
                IList<ListLabOrder> data = jss.Deserialize<IList<ListLabOrder>>(patientLabOrder);

                if (patientID > 0)
                {
                         PatientVisit visit = new PatientVisit()
                            {
                             Ptn_Pk = patient_Pk,
                             LocationID = facilityID,
                             UserID = userId,
                             TypeofVisit = 6,
                             VisitDate = DateTime.Now,
                             ModuleId = moduleId,
                             VisitType = 6
                            };
                      visitId = _visitManager.AddPatientVisit(visit);

                LabOrderEntity labOrder = new LabOrderEntity()
                {
                    Ptn_pk = patient_Pk,
                    PatientId = patientID,
                    LocationId = facilityID,
                    VisitId = visitId,
                    ModuleId = moduleId,
                    OrderedBy = userId,
                    ClinicalOrderNotes = orderNotes,
                    PatientMasterVisitId = patientMasterVisitId,                    
                    OrderStatus = pending,
                    OrderDate = orderDate,                   
                    UserId = userId                 
                };
                orderId=_mgr.AddPatientLabOrder(labOrder);
              
            foreach (ListLabOrder t in data)
                    {
                                LabDetailsEntity labDetails = new LabDetailsEntity()
                                        {
                                            LabOrderId = orderId,
                                            LabTestId = t.LabNameId,
                                            TestNotes = t.LabNotes,
                                            UserId = userId,
                                            CreatedBy= userId,                                                                                    
                                            StatusDate = DateTime.Now

                                };
                    testId=_mgr.AddLabOrderDetails(labDetails);
                  
                            PatientLabTracker labTracker = new PatientLabTracker()
                                        {
                                            PatientId = patientID,
                                            PatientMasterVisitId = patientMasterVisitId,
                                            LabName = t.LabName,                                          
                                            SampleDate = orderDate,
                                            Reasons = t.OrderReason,
                                            CreatedBy = userId,
                                            Results = pending,
                                            LabOrderId = orderId,
                                            LabTestId = t.LabNameId,
                                            FacilityId = facilityID
                            };

                             _mgr.AddPatientLabTracker(labTracker);
                    //add to dtlresults
                  
                    LookupTestParameter _testId = _lookupTest.GetTestParameter(t.LabNameId);
                            if (_testId != null)
                            {
                                _paramId = _testId.Id;
                            }
                    LabResultsEntity labresults = new LabResultsEntity()
                                            {
                                                LabOrderId = orderId,
                                                LabTestId = t.LabNameId,
                                                LabOrderTestId = testId,
                                                ParameterId = _paramId,
                                                UserId = userId,                                                
                                                CreatedBy = userId,                                                
                                                StatusDate = DateTime.Now

                    };
                    _mgr.AddPatientLabResults(labresults);
                }
             }
               
            }
         
        public List<PatientLabTracker> GetVlPendingCount(int facilityId)
        {
            var pendingLabs = _lookupTest.GetVlPendingCount(facilityId);
            return pendingLabs;
        }
        public List<PatientLabTracker> GetVlCompleteCount(int facilityId)
        {
            var completeLabs = _lookupTest.GetVlCompleteCount(facilityId);
            return completeLabs;
        }
    }
   }
