using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IQCare.Queue.Core.Models
{
    public class Rooms
    {
        [Key]
        public int Id { get; set; }
        public string RoomName { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }

        public Boolean DeleteFlag { get; set; }

        public int CreatedBy { get; set; }

        public Boolean Active { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdatedBy { get; set; }
    }
}
