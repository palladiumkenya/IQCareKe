using System;
using System.Linq;
using Application.Presentation;
using Entities.CCC.Lookup;
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

                }
                else
                {
                    
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