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
            EXTERNAL_PATIENT_ID= new IdentificationEntity();
            INTERNAL_PATIENT_ID= new List<IdentificationEntity>();
            PATIENT_NAME= new PATIENTNAME();
        }

        public IdentificationEntity EXTERNAL_PATIENT_ID { get; set; }
        public List<IdentificationEntity> INTERNAL_PATIENT_ID { get; set; }
        public PATIENTNAME PATIENT_NAME { get; set; }
    }
}
