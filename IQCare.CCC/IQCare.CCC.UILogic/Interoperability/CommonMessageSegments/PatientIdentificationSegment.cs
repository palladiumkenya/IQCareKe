using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC.Enrollment;
using Entities.CCC.Interoperability;
using IQCare.DTO.CommonEntities;

namespace IQCare.CCC.UILogic.Interoperability.CommonMessageSegments
{
    public class PatientIdentificationSegment
    {
        public static INTERNALPATIENTID getInternalPatientIdCCC(PatientMessage patientMessage)
        {
            try
            {
                INTERNALPATIENTID internalPatientId = new INTERNALPATIENTID();
                internalPatientId.ID = patientMessage.IdentifierValue;
                internalPatientId.IDENTIFIER_TYPE = "CCC_NUMBER";
                internalPatientId.ASSIGNING_AUTHORITY = "CCC";

                return internalPatientId;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static INTERNALPATIENTID getInternalPatientIdNationalId(PatientMessage patientMessage)
        {
            try
            {
                INTERNALPATIENTID internalNationalId = new INTERNALPATIENTID();
                if (!String.IsNullOrWhiteSpace(patientMessage.NATIONAL_ID) && patientMessage.NATIONAL_ID != "99999999")
                {
                    internalNationalId.ID = patientMessage.NATIONAL_ID;
                    internalNationalId.IDENTIFIER_TYPE = "NATIONAL_ID";
                    internalNationalId.ASSIGNING_AUTHORITY = "GOK";
                }

                return internalNationalId;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static EXTERNALPATIENTID GetExternalPatientId(List<PersonIdentifier> personIdentifiers)
        {
            try
            {
                EXTERNALPATIENTID externalPatientId = new EXTERNALPATIENTID();
                externalPatientId.ID = personIdentifiers.Count > 0 ? personIdentifiers[0].IdentifierValue : String.Empty; ;
                externalPatientId.ASSIGNING_AUTHORITY = "MPI";
                externalPatientId.IDENTIFIER_TYPE = "GODS_NUMBER";

                return externalPatientId;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static PATIENTNAME GetPatientName(PatientMessage patientMessage)
        {
            PATIENTNAME patientname = new PATIENTNAME();
            try
            {
                patientname.FIRST_NAME = !string.IsNullOrWhiteSpace(patientMessage.FIRST_NAME) ? patientMessage.FIRST_NAME : "";
                patientname.MIDDLE_NAME = !string.IsNullOrWhiteSpace(patientMessage.MIDDLE_NAME) ? patientMessage.MIDDLE_NAME : "";
                patientname.LAST_NAME = !string.IsNullOrWhiteSpace(patientMessage.LAST_NAME) ? patientMessage.LAST_NAME : "";

                return patientname;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
