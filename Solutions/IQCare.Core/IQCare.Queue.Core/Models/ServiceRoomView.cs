using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Queue.Core.Models
{
   public class ServiceRoomView
    {
        public int Id { get; set; }
        public int ServicePointId { get; set; }

        public string ServicePointName { get; set; }
        public int RoomId { get; set; }

        public string RoomName { get; set; }


        public string RoomDisplayName { get; set; }

        public int ServiceAreaId { get; set; }

        public string ServiceAreaName { get; set; }

        public Boolean DeleteFlag { get; set; }

        public int CreatedBy { get; set; }

        public Boolean Active { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdatedBy { get; set; }


    }
}
