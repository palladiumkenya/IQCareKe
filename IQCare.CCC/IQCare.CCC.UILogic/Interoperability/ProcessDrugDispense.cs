using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Interoperability;
using IQCare.CCC.UILogic.Enrollment;
using IQCare.CCC.UILogic.Interoperability.DTOValidator;
using IQCare.CCC.UILogic.Visit;
using IQCare.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.CCC.UILogic.Interoperability
{
    public class ProcessDrugDispense: IInteropDTO<DtoDrugDispensed>
    {
        private readonly IDrugPrescriptionManager _drugPrescriptionManager = (IDrugPrescriptionManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Interoperability.BDrugPrescriptionManager, BusinessProcess.CCC");

        public DtoDrugDispensed Get(int entityId)
        {
            throw new NotImplementedException();
        }

        public string Save(DtoDrugDispensed t)
        {
            try
            {
                ValidateRDS013 _validator = new ValidateRDS013();
                string results = _validator.ValidateDTO(t);
                if (!String.IsNullOrWhiteSpace(results))
                {
                    throw new Exception(results);
                }
                //Get FacilityI mfl code
                int dispensing_facility_mflcode = Convert.ToInt32(t.MESSAGE_HEADER.SENDING_FACILITY);
                int prescription_facility_mflcode = Convert.ToInt32(t.MESSAGE_HEADER.RECEIVING_FACILITY);

                string _god_number = t.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.IdentifierValue;

                string nationalId = String.Empty;
                var temp = t.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Where(x=> x.IdentifierType=="NATIONAL_ID").FirstOrDefault();
                if(temp != null) { nationalId = temp.IdentifierValue; }
                string cccNumber = String.Empty;
                t.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Where(x => x.IdentifierType == "CCC_NUMBER").FirstOrDefault();
                if (temp != null) { nationalId = temp.IdentifierValue; }

                DateTime dispenseDate = t.COMMON_ORDER_DETAILS.TRANSACTION_DATETIME;
                string dispensingNotes= t.COMMON_ORDER_DETAILS.NOTES;
                List<PHARMACY_ENCODED_ORDER> prescipedItems = t.PHARMACY_ENCODED_ORDER;
                List<PHARMACY_DISPENSE> dispensedItems = t.PHARMACY_DISPENSE;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return "success";
        }

        public string Update(DtoDrugDispensed t)
        {
            throw new NotImplementedException();
        }
    }
}
