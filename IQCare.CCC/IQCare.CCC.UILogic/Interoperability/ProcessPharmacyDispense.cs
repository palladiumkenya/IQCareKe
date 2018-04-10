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
        private readonly IDrugManager _drugManager = (IDrugManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Pharmacy.BDrug, BusinessProcess.CCC");

        public string Process(DtoDrugDispensed drugDispensed)
        {
            try
            {
                LookupLogic facilityLookup = new LookupLogic();
                string receivingFacilityMflCode = drugDispensed.MESSAGE_HEADER.RECEIVING_FACILITY;
                string sendingFacilityMflCode = drugDispensed.MESSAGE_HEADER.SENDING_FACILITY;
                //check if facility exists
                LookupFacility recieverfacility = facilityLookup.GetFacility(receivingFacilityMflCode);
                LookupFacility senderfacility = facilityLookup.GetFacility(sendingFacilityMflCode);
                if (recieverfacility == null)
                {
                    return Msg = $"The facility {receivingFacilityMflCode} does not exist";
                }
                if (senderfacility == null)
                {
                    return Msg = $"The facility {sendingFacilityMflCode} does not exist";
                }
                
                if (recieverfacility.FacilityID != senderfacility.FacilityID)
                {
                    return Msg = "The sending facility is not the same as the receiving facility!";
                }

                //check if it is the right facility
                LookupFacility thisFacility = facilityLookup.GetFacility();
                if (recieverfacility.FacilityID != thisFacility.FacilityID)
                {
                    return Msg = $"This message belongs to {receivingFacilityMflCode}, not this facility {thisFacility.MFLCode}!";
                }
                var patientLookup = new PatientLookupManager();
                //check patient
                var identifier = drugDispensed.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.FirstOrDefault(n => n.IDENTIFIER_TYPE == "CCC_NUMBER");
                if (identifier == null)
                {
                    return Msg = "Message does not contain a CCC number!";
                }
                var patient = patientLookup.GetPatientByCccNumber(identifier.ID);
                if (patient == null)
                {
                    return Msg = "Patient could not be found!";
                }
                //check pharmacy order exists
                int orderId = Convert.ToInt32(drugDispensed.COMMON_ORDER_DETAILS.PLACER_ORDER_NUMBER.NUMBER);
                var pharmacyOrder = _pharmacyOrderManager.GetPharmacyOrder(orderId);
                if (pharmacyOrder == null)
                {
                    return Msg = "Pharmacy Order could not be found!";
                }
                var orderedDrugs = _pharmacyDispenseManager.GetByPharmacyOrderId(pharmacyOrder.ptn_pharmacy_pk);
                if (orderedDrugs != null)
                {
                    var newDispensedDrugs = orderedDrugs;
                    foreach (var drug in newDispensedDrugs)
                    {
                        //todo refactor to use drug codes and possibly drug ids to be included in the message
                        var drugFind = _drugManager.GetDrug(drug.Drug_Pk);
                        string drugname = drugFind.DrugName;
                        PHARMACY_DISPENSE messageDispensed = null;
                        messageDispensed = drugDispensed.PHARMACY_DISPENSE.FirstOrDefault(x=> drugname.Contains(x.ACTUAL_DRUGS));
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
                            //drug.SingleDose = Convert.ToDecimal(Regex.Replace(messageDispensed.DOSAGE, @"^[A-Za-z]+", ""));

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
                        string drugNameQuery = "%" + drug.ACTUAL_DRUGS + "%";
                        var drugFind = _drugManager.GetDrugsByName(drugNameQuery).FirstOrDefault();
                        newlyDispensedDrugs.Drug_Pk = drugFind.Drug_pk;
                        newlyDispensedDrugs.DispensedQuantity = drug.QUANTITY_DISPENSED;
                        newlyDispensedDrugs.Duration = drug.DURATION;
                        //todo get frequencyId 
                        //newlyDispensedDrugs.FrequencyID = drug.FREQUENCY;
                        //newlyDispensedDrugs.SingleDose = Convert.ToDecimal(Regex.Replace(drug.DOSAGE, @"^[A-Za-z]+", ""));
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
                var orderingPhysician = drugDispensed.COMMON_ORDER_DETAILS.ORDERING_PHYSICIAN;
                updatedPharmacyOrder.OrderedByName = orderingPhysician.PREFIX + " " + orderingPhysician.FIRST_NAME +
                                                     " " + orderingPhysician.LAST_NAME;
                //todo harmonise users
                updatedPharmacyOrder.DispensedBy = 1;
                updatedPharmacyOrder.DispensedByDate = drugDispensed.MESSAGE_HEADER.MESSAGE_DATETIME;
                updatedPharmacyOrder.OrderStatus = 2;
                string str = updatedPharmacyOrder.PharmacyNotes;
                if (str != null)
                {
                    str += " Dispensed from IL";
                }
                updatedPharmacyOrder.PharmacyNotes = str;
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