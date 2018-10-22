using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IQCare.PMTCT.Core.Models.HEI
{
    public class Vaccination
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int Vaccine { get; set; }
        public int VaccineStage { get; set; }
        public int DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? VaccineDate { get; set; }
        public int Active { get; set; }
        public int? AppointmentId { get; set; }
        public int? Period { get; set; }
        [NotMapped]
        public DateTime? NextSchedule { get; set; }
    }
}
