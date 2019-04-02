using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Queue.Core.Models
{
    public class RoomStatus
    {
        public int  Id { get; set; }

        public  int ServiceRoomId { get; set; }

        public  Boolean DeleteFlag { get; set; }

        public int CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public Boolean Active { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
