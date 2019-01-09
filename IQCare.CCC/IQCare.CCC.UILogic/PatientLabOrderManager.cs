using Application.Presentation;
using Entities.CCC.Visit;
using Entities.CCC.Encounter;
using Interface.CCC.Visit;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using IQCare.CCC.UILogic.Visit;


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
        public DateTime? ResultDate { get; set; }
        public int LabOrderId { get; set; }
        //public int labTestId { get; set; }
        public string LabNotes { get; set; }

    }
    public class PatientLabOrderManager
    {
        private string Msg { get; set; }
        private int Result { get; set; }
        private int Id { get; set; }
        IPatientLabOrderManager _mgr = (IPatientLabOrderManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.visit.BPatientLabOrdermanager, BusinessProcess.CCC");
        ILookupManager _lookupTest = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
        private readonly IPatientVisitManager _visitManager = (IPatientVisitManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.visit.BPatientVisit, BusinessProcess.CCC");
        PatientEncounterManager _patientEncounterManager=new PatientEncounterManager();
        /// <summary>
        /// Saves the patient lab order.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="patient_Pk">The patient pk.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="facilityID">The facility identifier.</param>
        /// <param name="moduleId">The module identifier.</param>
        /// <param name="patientMasterVisitId">The patient master visit identifier.</param>
        /// <param name="labOrderDate">The lab order date.</param>
        /// <param name="orderNotes">The order notes.</param>
        /// <param name="patientLabOrder">The patient lab order.</param>
        /// <param name="orderStatus">The order status.</param>
        /// <returns>order Id</returns>
        public int savePatientLabOrder(LabOrderEntity labOrder)
        {
            return savePatientLabOrder(labOrder, null);
        }

        public int savePatientLabOrder(LabOrderEntity labOrder, string patientLabOrders)
        {
            return savePatientLabOrder(labOrder.Id, labOrder.PatientId, labOrder.Ptn_pk, labOrder.UserId, labOrder.LocationId, labOrder.ModuleId, labOrder.PatientMasterVisitId, labOrder.OrderDate.ToString(), labOrder.ClinicalOrderNotes, patientLabOrders, labOrder.OrderStatus);
        }

        public int savePatientLabOrder(int patientID, int patient_Pk, int userId, int facilityID, int moduleId, int patientMasterVisitId, string labOrderDate, string orderNotes, string patientLabOrder, string orderStatus)
        {
            return savePatientLabOrder(0, patientID, patient_Pk, userId, facilityID, moduleId, patientMasterVisitId, labOrderDate, orderNotes, patientLabOrder, orderStatus);
        }

        public int savePatientLabOrder(int orderId, int patientID, int patient_Pk, int userId, int facilityID, int moduleId, int patientMasterVisitId, string labOrderDate, string orderNotes, string patientLabOrder, string orderStatus)
        {
            int visitId = 0;
            int testId = 0;
            //int _paramId = 0;
            DateTime orderDate = Convert.ToDateTime(labOrderDate);
            // DateTime orderDate = DateTime.Now;
            //DateTime orderDate = DateTime.Parse(labOrderDate);
            var jss = new JavaScriptSerializer();
            IList<ListLabOrder> data = new List<ListLabOrder>();

            if (patientLabOrder != null)
            {
                data = jss.Deserialize<IList<ListLabOrder>>(patientLabOrder);
            }

            if (patientID > 0)
            {
                PatientVisit visit = new PatientVisit()
                {
                    Ptn_Pk = patient_Pk,
                    LocationID = facilityID,
                    UserID = userId,
                    TypeofVisit = 70, //Self (70) 71 (Treatmentsupporter)
                    VisitDate = orderDate,
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
                    OrderStatus = orderStatus,
                    OrderDate = orderDate,
                    UserId = userId
                };
                if (orderId > 0)
                {
                    labOrder.Id = orderId;
                    _mgr.EditPatientLabOrder(labOrder);
                } else {
                    orderId = _mgr.AddPatientLabOrder(labOrder);
                }
                // DateTime? nullDate = null;
                foreach (ListLabOrder t in data)
                {
                    LabDetailsEntity labDetails = new LabDetailsEntity()
                    {
                        LabOrderId = orderId,
                        LabTestId = t.LabNameId,
                        TestNotes = t.LabNotes,
                        UserId = userId,
                        CreatedBy = userId,
                        StatusDate = DateTime.Now,
                        ResultDate = t.ResultDate

                    };
                    testId = _mgr.AddLabOrderDetails(labDetails);

                    PatientLabTracker labTracker = new PatientLabTracker()
                    {
                        PatientId = patientID,
                        PatientMasterVisitId = patientMasterVisitId,
                        LabName = t.LabName,
                        SampleDate = orderDate,
                        Reasons = t.OrderReason,

                        CreatedBy = userId,
                        Results = orderStatus,
                        LabOrderId = orderId,
                        LabTestId = t.LabNameId,  //parameter
                        LabOrderTestId = testId,  //uniquely identifies a particular test
                        FacilityId = facilityID,
                        ResultDate = t.ResultDate
                    };

                    Result = _mgr.AddPatientLabTracker(labTracker);
                    if (Result > 0)
                    {
                        Id = _patientEncounterManager.AddpatientEncounter(patientID, patientMasterVisitId, _patientEncounterManager.GetPatientEncounterId("EncounterType", "lab-encounter".ToLower()), 205, userId);
                    }
                    //add to dtlresults

                    List<LookupTestParameter> _parameters = _lookupTest.GetTestParameter(t.LabNameId);
                    if (_parameters != null)
                    {

                        foreach (LookupTestParameter p in _parameters)
                        {

                            LabResultsEntity labresults = new LabResultsEntity()
                            {
                                LabOrderId = orderId,
                                LabTestId = t.LabNameId,
                                LabOrderTestId = testId,
                                ParameterId = p.Id,
                                UserId = userId,
                                CreatedBy = userId,
                                StatusDate = DateTime.Now

                            };
                            _mgr.AddPatientLabResults(labresults);
                        }
                    }

                }
            }
            return orderId;
        }

        public void UpdateLabOrderCompleteStatus(int patientId, int patientMasterVisitId, int orderId, string orderStatus)
        {
            var labOrder = _mgr.GetLabOrderEntityById(orderId);
            var patientLabTrackers = _mgr.GetPatientLabTracker(patientId, patientMasterVisitId, orderId);
            if (labOrder != null)
            {
                labOrder.OrderStatus = orderStatus;

                _mgr.EditPatientLabOrder(labOrder);

                if (patientLabTrackers.Count > 0)
                {
                    patientLabTrackers[0].Results = orderStatus;

                    _mgr.UpdatePatientLabOrder(patientLabTrackers[0]);
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

        public List<LookupFacilityViralLoad> GetFacilityVLSuppressed(int facilityId)
        {
            var facilityVLsuppressed = _lookupTest.GetFacilityVLSuppressed(facilityId);
            return facilityVLsuppressed;
        }

        public List<LookupFacilityViralLoad> GetFacilityVLUnSuppressed(int facilityId)
        {
            var facilityVLunsuppressed = _lookupTest.GetFacilityVLUnSuppressed(facilityId);
            return facilityVLunsuppressed;
        }

        public List<LabOrderEntity> GetPatientLabOrdersByDate(int patientId, DateTime visitDate)
        {
            var labOrders = _mgr.GetPatientLabOrdersByDate(patientId, visitDate);
            return labOrders;
        }

        public LabOrderEntity GetLabOrdersById(int labOrderId)
        {
            var labOrders = _mgr.GetLabOrderById(labOrderId);
            return labOrders;
        }
        public List<LabDetailsEntity> GetPatientLabDetailsByDate(int labOrderId, DateTime visitDate)
        {
            var labDetails = _mgr.GetPatientLabDetailsByDate(labOrderId, visitDate);
            return labDetails;
        }
        public List<LabDetailsEntity> GetLabTestsOrderedById(int labOrderId)
        {
            var labDetails = _mgr.GetPatientLabDetailsByLabOrderId(labOrderId);
            return labDetails;
        }
        public int AddPatientLabResults(LabResultsEntity labResultsEntity)
        {
            var labResults = _mgr.AddPatientLabResults(labResultsEntity);
            return labResults;
        }
    }
   }
