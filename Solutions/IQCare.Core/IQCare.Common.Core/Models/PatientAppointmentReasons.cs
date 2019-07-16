namespace IQCare.Common.Core.Models
{
    public class PatientAppointmentReasons
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int ReasonAppointmentNotGiven { get; set; }
    }
}