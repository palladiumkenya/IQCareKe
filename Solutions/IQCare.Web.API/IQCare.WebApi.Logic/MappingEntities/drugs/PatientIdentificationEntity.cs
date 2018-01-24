using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.WebApi.Logic.MappingEntities.drugs
{

   public class PATIENT_IDENTIFICATION
    {
        public PATIENT_IDENTIFICATION()
        {
            EXTERNAL_PATIENT_ID= new INTERNAL_PATIENT_ID();
            INTERNAL_PATIENT_ID= new List<INTERNAL_PATIENT_ID>();
            PATIENT_NAME= new PATIENTNAME();
        }

        public INTERNAL_PATIENT_ID EXTERNAL_PATIENT_ID { get; set; }
        public List<INTERNAL_PATIENT_ID> INTERNAL_PATIENT_ID { get; set; }
        public PATIENTNAME PATIENT_NAME { get; set; }
    }
}
