using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Queue
{
    [Serializable]
    [Table("QueueManager")]
 public   class QueueManager :BaseEntity
    {
        protected QueueManager()
        {
        }

        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public DateTime QueueStartTime { get; set; } 
        public DateTime ? QueueEndTime { get; set; }
        public int? PatientEncounterId { get; set; }
        public int? NursingStationId { get; set; }
        public int? ConsultationRoomId { get; set; }
        public int PatientServiceAreaId { get; set; }
        public int Status { get; set; }

    }
}
