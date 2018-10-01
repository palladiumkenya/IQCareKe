using IQCare.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Entities.CCC.Encounter;
using IQCare.CCC.UILogic.Visit;
using Entities.CCC.Lookup;
using System.Text.RegularExpressions;

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
                    int interopUserId = InteropUser.UserId;
                    if (thisFacility == null)
                    {
                        Msg = $"The facility {receivingFacilityMFLCode} does not exist";
                        throw new Exception(Msg);
                    }
                    if (patient == null)
                    {
                        Msg = $"Patient {patientCcc} does not exist ";
                        throw new Exception(Msg);
                    }
                    if (results.Count(r=> string.IsNullOrWhiteSpace(r.VlResult.Trim()))> 0)
                    {
                        Msg = $"Viral load message has no results indicated ";
                        throw new Exception(Msg);
                    }
                    int invalidResult = 0;
                    foreach (var result in results)
                    {
                        if (result.VlResult.Contains("LDL"))
                        {

                        }
                        else if(Regex.Split(result.VlResult, @"[^0-9\.]+").Length > 0)
                        {

                        }
                        else
                        {
                            invalidResult++;

                        }
                    }
                        
                    if (invalidResult > 0)
                    {
                        Msg = $"Viral load message has invalid results indicated ";
                        throw new Exception(Msg);
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
                                patientMasterVisitManager.AddPatientMasterVisit(patient.Id, interopUserId, 316);
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
                           int orderId= labOrderManager.savePatientLabOrder(patient.Id, (int)patient.ptn_pk, interopUserId, thisFacility.FacilityID, 203, patientMasterVisitId, sampleCollectionDate.ToString(), "IL lab order", patientLabOrder,"Complete");
                            
                            labOrder = labOrderManager.GetLabOrdersById( orderId);
                            labDetails = labOrderManager.GetLabTestsOrderedById(labOrder.Id);
                        }
                        else
                        {
                            labDetails = labOrderManager.GetLabTestsOrderedById(labOrder.Id);
                        }

                        if (labOrder != null)
                        {
                            bool isUndetectable = false;
                            string resultText = "";
                            decimal? resultValue = null;
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
                                    string[] numbers =  Regex.Split(resultString, @"[^0-9\.]+");
                                    //bool isSuccess = decimal.TryParse(resultString, out decimalValue);
                                    //if (isSuccess) resultValue = decimalValue;
                                    for (int i = 0; i < numbers.Length; i++)
                                    {
                                        if(Regex.IsMatch(numbers[i], @"^\d+$"))
                                        {
                                            resultValue = Convert.ToDecimal(numbers[i]);
                                            break;
                                        }
                                    }

                                }
                                
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
                                        ResultValue = resultValue,
                                        ResultUnit = "copies/ml",
                                        ResultUnitId = 129,
                                        Undetectable = isUndetectable,
                                        StatusDate = result.DateSampleTested,
                                        HasResult = true
                                    };
                                    labOrderManager.AddPatientLabResults(labResults);
                                    labOrder.OrderStatus = "Complete";
                                    labOrderManager.savePatientLabOrder(labOrder);

                                }
                            }
                        Msg = "Success";
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
                    throw e;
                }
            }
            else
            {
                Msg = "Message does not contain results";
                throw new Exception(Msg);
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