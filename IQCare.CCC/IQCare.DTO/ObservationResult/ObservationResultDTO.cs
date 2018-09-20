using System.Collections.Generic;
using IQCare.DTO.CommonEntities;

namespace IQCare.DTO.ObservationResult
{
    public class ObservationResultDTO
    {
        public ObservationResultDTO()
        {
            MESSAGE_HEADER = new MESSAGEHEADER();
            PATIENT_IDENTIFICATION = new OBSERVATIONPATIENTIDENTIFICATION();
            OBSERVATION_RESULT = new List<OBSERVATION_RESULT>();
        }

        public MESSAGEHEADER MESSAGE_HEADER { get; set; }
        public OBSERVATIONPATIENTIDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
        public List<OBSERVATION_RESULT> OBSERVATION_RESULT { get; set; }
    }
}
