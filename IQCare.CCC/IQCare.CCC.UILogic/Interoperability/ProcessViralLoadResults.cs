using IQCare.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Entities.CCC.Encounter;
using IQCare.CCC.UILogic.Visit;
using Entities.CCC.Lookup;

namespace IQCare.CCC.UILogic.Interoperability
{
    public class ProcessViralLoadResults
    {
        private string Msg { get; set; }
        //int _userId = Convert.ToInt32(HttpContext.Current.Session["AppUserId"]);
        //int _facilityId = Convert.ToInt32(HttpContext.Current.Session["AppLocationId"]);
        public string Save(ViralLoadResultsDto viralLoadResults)
        {
            LabOrderEntity labOrder = null;
            List<LabDetailsEntity> labDetails = null;
            var results = viralLoadResults.ViralLoadResult;
            if (results != null)
            {
                try
                {
                    var patientLookup = new PatientLookupManager();
                    var labOrderManager = new PatientLabOrderManager();
                    var patientCcc = viralLoadResults.PatientIdentification.INTERNAL_PATIENT_ID.FirstOrDefault(n => n.IdentifierType == "CCC_NUMBER").IdentifierValue;
                    var patient = patientLookup.GetPatientByCccNumber(patientCcc);
                    string receivingFacilityMFLCode = viralLoadResults.MesssageHeader.ReceivingFacility;
                    LookupLogic flm = new LookupLogic();
                    LookupFacility thisFacility = flm.GetFacility(receivingFacilityMFLCode);
                    if (thisFacility == null)
                    {
                        Msg = $"The facility {receivingFacilityMFLCode} does not exist";

                        return Msg;
                    }
                    if (patient == null)
                    {
                        Msg = $"Patient {patientCcc} does not exist ";

                        return Msg;
                    }
                    if (patient != null && thisFacility != null)
                    {
                        
                        //todo brian check
                        labOrder = labOrderManager.GetPatientLabOrdersByDate((int) patient.ptn_pk,results.FirstOrDefault().DateSampleCollected).DefaultIfEmpty(null).FirstOrDefault();
                        DateTime sampleCollectionDate = results.FirstOrDefault().DateSampleCollected;
                        if (labOrder == null)
                        {
                            var patientMasterVisitManager = new PatientMasterVisitManager();
                            
                            //var visitType = flm.GetItemIdByGroupAndItemName("VisitType", "Enrollment")[0]
                            //    .ItemId;
                            int patientMasterVisitId =
                                patientMasterVisitManager.AddPatientMasterVisit(patient.Id, 1, 316);
                            var listOfTestsOrdered = new List<ListLabOrder>();
                            var order = new ListLabOrder()
                            {
                                FacilityId = Convert.ToInt32(viralLoadResults.MesssageHeader.ReceivingFacility),
                                LabName = "Viral Load",// results.FirstOrDefault().LabTestedIn,
                                LabNameId = 3,
                                LabNotes = results.FirstOrDefault().Regimen + " " + results.FirstOrDefault().SampleType,
                                LabOrderDate = sampleCollectionDate,
                                LabOrderId = 0,
                                OrderReason = "",
                                Results = results.FirstOrDefault().VlResult,
                                VisitId = patientMasterVisitId,
                                ResultDate = viralLoadResults.MesssageHeader.MessageDatetime
                                
                            };
                            listOfTestsOrdered.Add(order);
                            var jss = new JavaScriptSerializer();
                            string patientLabOrder = jss.Serialize(listOfTestsOrdered);
                            //include userid and facility ID
                           int orderId= labOrderManager.savePatientLabOrder(patient.Id, (int)patient.ptn_pk, 1, thisFacility.FacilityID, 203, patientMasterVisitId, sampleCollectionDate.ToString(), "IL lab order", patientLabOrder,"completed");
                            
                            labOrder = labOrderManager.GetLabOrdersById( orderId);
                            labDetails = labOrderManager.GetPatientLabDetailsByDate(labOrder.Id, sampleCollectionDate);
                        }
                        else
                        {
                        labDetails = labOrderManager.GetPatientLabDetailsByDate(labOrder.Id, results.FirstOrDefault().DateSampleCollected);
                        }

                        if (labOrder != null)
                        {
                            bool isUndetectable = false;
                            string resultText = null;
                            decimal resultvalue = Decimal.Zero;

                            foreach (var result in results)
                            {
                                if (result.VlResult.Contains("LDL"))
                                {
                                    isUndetectable = true;
                                    resultText = result.VlResult;
                                }
                                else
                                {
                                    var resultString = result.VlResult.Replace("copies/ml", "");
                                    bool isSuccess = decimal.TryParse(resultString, out resultvalue);
                                }
                                //var labOrd = labOrder.FirstOrDefault();
                                if (labOrder != null)
                                {

                                    var labResults = new LabResultsEntity()
                                    {
                                        //todo remove hard coding
                                        LabOrderId = labOrder.Id,
                                        LabOrderTestId = labDetails.FirstOrDefault().Id,
                                        ParameterId = 3,
                                        LabTestId = 3,
                                        ResultText = resultText,
                                        ResultValue = resultvalue,
                                        ResultUnit = "copies/ml",
                                        ResultUnitId = 129,
                                        Undetectable = isUndetectable,
                                        StatusDate = result.DateSampleTested,
                                        HasResult = true
                                    };
                                    labOrderManager.AddPatientLabResults(labResults);
                                }
                            }
                        Msg = "Sucess";
                        }
                        
                    }
                    else
                    {
                        Msg = "Patient does not exist";
                        return Msg;
                    }
                }
                catch (Exception e)
                {
                    Msg = "error " + e.Message;
                }
            }
            else
            {
                Msg = "Message does not contain results";
            }
            
            return Msg;
        }

        public string Update(ViralLoadResultsDto viralLoadResults)
        {
            try
            {
                Msg = "Sucess";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Msg = "error " + e.Message;
            }
            return Msg;
        }
    }
}