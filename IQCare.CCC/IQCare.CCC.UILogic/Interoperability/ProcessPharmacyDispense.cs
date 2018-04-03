using System;
using System.Linq;
using System.Text.RegularExpressions;
using Application.Presentation;
using Entities.CCC.Lookup;
using Entities.CCC.pharmacy;
using Interface.CCC.Pharmacy;
using IQCare.DTO;

namespace IQCare.CCC.UILogic.Interoperability
{
    public class ProcessPharmacyDispense
    {
        private string Msg { get; set; }
        private readonly IPharmacyOrderManager _pharmacyOrderManager = (IPharmacyOrderManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Pharmacy.BPharmacyOrder, BusinessProcess.CCC");
        private readonly IPatientPharmacyDispenseManager _pharmacyDispenseManager = (IPatientPharmacyDispenseManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Pharmacy.BPatientPharmacyDispense, BusinessProcess.CCC");

        public string Process(DtoDrugDispensed drugDispensed)
        {
            try
            {
                LookupLogic facilityLookup = new LookupLogic();
                string receivingFacilityMflCode = drugDispensed.MESSAGE_HEADER.SENDING_FACILITY;
                //check if facility exists
                LookupFacility facility = facilityLookup.GetFacility(receivingFacilityMflCode);
                if (facility == null)
                {
                    return Msg = $"The facility {receivingFacilityMflCode} does not exist";
                }
                //check if it is the right facility
                LookupFacility correctFacility = facilityLookup.GetFacility(receivingFacilityMflCode);
                if (correctFacility.FacilityID != facility.FacilityID)
                {
                    return Msg = "The sending facility code is invalid!";
                }
                var patientLookup = new PatientLookupManager();
                //check patient
                var identifier = drugDispensed.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.FirstOrDefault(n => n.IdentifierType == "CCC_NUMBER");
                if (identifier == null)
                {
                    return Msg = "Message does not contain a CCC number!";
                }
                var patient = patientLookup.GetPatientByCccNumber(identifier.IdentifierValue);
                if (patient == null)
                {
                    return Msg = "Patient could not be found!";
                }
                //check pharmacy order exists
                int orderId = drugDispensed.COMMON_ORDER_DETAILS.PLACER_ORDER_NUMBER.NUMBER;
                var pharmacyOrder = _pharmacyOrderManager.GetPharmacyOrder(orderId);
                if (pharmacyOrder == null)
                {
                    return Msg = "Pharmacy Order could not be found!";
                }
                var dispensedDrugs = _pharmacyDispenseManager.GetByPharmacyOrderId(pharmacyOrder.ptn_pharmacy_pk);
                if (dispensedDrugs != null)
                {
                    var newDispensedDrugs = dispensedDrugs;
                    foreach (var drug in newDispensedDrugs)
                    {
                        //todo get drugnames for drugId
                        string drugname = "";
                        var messageDispensed = drugDispensed.PHARMACY_DISPENSE.FirstOrDefault(x=>x.DRUG_NAME ==drugname);
                        if (messageDispensed != null)
                        {
                            drug.DispensedQuantity = messageDispensed.QUANTITY_DISPENSED;
                            //todo get frequencyId 
                            //drug.FrequencyID = messageDispensed.FREQUENCY;
                            drug.PatientInstructions = messageDispensed.DISPENSING_NOTES;
                            //todo get strength ids
                            //drug.StrengthID = messageDispensed.STRENGTH;
                            drug.Duration = messageDispensed.DURATION;
                            drug.UpdateDate = DateTime.Now;
                            drug.UserID = 1;
                            drug.SingleDose = Convert.ToDecimal(Regex.Replace(messageDispensed.DOSAGE, @"^[A-Za-z]+", ""));

                        }
                        try
                        {
                            _pharmacyDispenseManager.UpdatePatientPharmacyDispense(drug);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            return Msg = "error " + e.Message;
                        }
                    }
                }
                else
                {
                    PatientPharmacyDispense newlyDispensedDrugs = new PatientPharmacyDispense();
                    foreach (var drug in drugDispensed.PHARMACY_DISPENSE)
                    {
                        //todo get drug ids from drug names
                        //newlyDispensedDrugs.Drug_Pk = ;
                        newlyDispensedDrugs.DispensedQuantity = drug.QUANTITY_DISPENSED;
                        newlyDispensedDrugs.Duration = drug.DURATION;
                        //todo get frequencyId 
                        //newlyDispensedDrugs.FrequencyID = drug.FREQUENCY;
                        newlyDispensedDrugs.SingleDose = Convert.ToDecimal(Regex.Replace(drug.DOSAGE, @"^[A-Za-z]+", ""));
                        //todo get strength ids
                        //newlyDispensedDrugs.StrengthID = drug.STRENGTH;
                        newlyDispensedDrugs.PatientInstructions = drug.DISPENSING_NOTES;
                        newlyDispensedDrugs.Prophylaxis = 0;
                    }
                    foreach (var order in drugDispensed.PHARMACY_ENCODED_ORDER)
                    {
                        newlyDispensedDrugs.OrderedQuantity = Convert.ToInt32(order.QUANTITY_PRESCRIBED);
                        newlyDispensedDrugs.PatientInstructions = order.PRESCRIPTION_NOTES;
                    }
                    try
                    {
                        _pharmacyDispenseManager.AddPatientPharmacyDispense(newlyDispensedDrugs);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return Msg = "error " + e.Message;
                    }
                }
                var updatedPharmacyOrder = pharmacyOrder;
                try
                {
                    _pharmacyOrderManager.UpdatePharmacyOrder(updatedPharmacyOrder);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return Msg = "error " + e.Message;
                }
                Msg = "Success";
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