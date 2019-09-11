using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Queue.Core.Models
{
    public class QueueWaitingList
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public int ServiceRoomId { get; set; }

        public int  Priority { get; set; }

        public Boolean DeleteFlag { get; set; }

        public Boolean Status { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdateDate { get; set; }

    }
}
