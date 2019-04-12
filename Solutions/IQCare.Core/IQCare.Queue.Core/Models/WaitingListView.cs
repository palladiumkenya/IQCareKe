using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Queue.Core.Models
{
    public class WaitingListView
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        

        public string RoomName { get; set; }

        public int PatientId { get; set; }

        public int PersonId { get; set; }

        public int ServiceRoomId { get; set; }


        public int ServiceAreaId { get; set; }
        public string ServiceAreaName { get; set; }

        public int ServicePointId { get; set; }

        public string ServicePointName { get; set; }

        public string Priority { get; set; }

        public Boolean DeleteFlag { get; set; }

        public Boolean Status { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
