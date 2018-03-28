using System;
using System.Linq;
using Entities.CCC.Lookup;
using IQCare.DTO;

namespace IQCare.CCC.UILogic.Interoperability
{
    public class ProcessPharmacyDispense
    {
        private string Msg { get; set; }

        public string Process(DtoDrugDispensed drugDispensed)
        {
            try
            {
                LookupLogic facilityLookup = new LookupLogic();
                string receivingFacilityMflCode = drugDispensed.MESSAGE_HEADER.SENDING_FACILITY;
                //todo use commented code
                //LookupFacility facility = facilityLookup.GetFacility(receivingFacilityMflCode);
                LookupFacility facility = facilityLookup.GetFacility();
                if (facility == null)
                {
                    return Msg = "The sending facilty code is invalid!";
                }
                //todo check if its right facility
                var patientLookup = new PatientLookupManager();
                var identifier = drugDispensed.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.FirstOrDefault(n => n.IdentifierType == "CCC_NUMBER");
                if (identifier == null)
                {
                    return Msg = "Message does not contain a CCC number!";
                }
                var patient = patientLookup.GetPatientByCccNumber(identifier.IdentifierValue);
                if (patient == null)
                {
                    return Msg = "Patient cold not be found!";
                }
                //var pharmacyOrder = new 

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