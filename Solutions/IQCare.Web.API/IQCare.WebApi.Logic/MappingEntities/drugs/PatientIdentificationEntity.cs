using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.WebApi.Logic.MappingEntities.drugs
{

   public class PatientIdentificationEntity
    {
        public PatientIdentificationEntity()
        {
            EXTERNAL_PATIENT_ID= new EXTERNAL_PATIENT_ID();
            INTERNAL_PATIENT_ID= new List<EXTERNAL_PATIENT_ID>();
            PATIENT_NAME= new PATIENTNAME();
        }

        public EXTERNAL_PATIENT_ID EXTERNAL_PATIENT_ID { get; set; }
        public List<EXTERNAL_PATIENT_ID> INTERNAL_PATIENT_ID { get; set; }
        public PATIENTNAME PATIENT_NAME { get; set; }
    }
}
