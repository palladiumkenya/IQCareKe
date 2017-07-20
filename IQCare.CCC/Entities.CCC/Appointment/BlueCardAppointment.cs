using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.CCC.Appointment
{
    [Serializable]
    [Table("BlueCardAppointmentView")]
    public class BlueCardAppointment
    {
        [Key]
        public Int64 RowID { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        public string FacilityName { get; set; }
        public int VisitId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; }
        public string AppointmentStatus { get; set; }
        public string Provider { get; set; }
        public string Description { get; set; }
        public string ServiceArea { get; set; }
        public DateTime? StatusDate { get; set; }
    }
}
