using System.Collections.Generic;

namespace IQCare.DTO
{
    public class DtoPatientIdentification
    {
        public DtoPatientIdentification()
        {
            EXTERNAL_PATIENT_ID=new DTOIdentifier();
            INTERNAL_PATIENT_ID=new List<DTOIdentifier>();
            PATIENT_NAME=new PatientNameDto();
        }
        
        public DTOIdentifier EXTERNAL_PATIENT_ID { get; set; }
        public List<DTOIdentifier> INTERNAL_PATIENT_ID { get; set; }
        public PatientNameDto PATIENT_NAME { get; set; }
    }
}
