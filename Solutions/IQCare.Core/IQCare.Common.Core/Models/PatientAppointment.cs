using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class PatientAppointment
    {
       public int Id { get; set; }
       public int PatientMasterVisitId { get; set; }
        public int ServiceAreaId { get; set; }
     public int PatientId { get; set; }
         public DateTime AppointmentDate { get; set; }
        public int  ReasonId { get; set; }
         public string Description { get; set; }
       public int   StatusId { get; set; }
        public DateTime  StatusDate { get; set; }
        public int  DifferentiatedCareId { get; set; }
        public int  DeleteFlag{ get; set; } 
    }
}
