namespace IQCare.DTO.PSmart
{
    public class DtoSmartcardPatientList
    {
        public int PATIENTID { get; set; }
        public string FIRSTNAME { get; set; }
        public string MIDDLENAME { get; set; }
        public string LASTNAME { get; set; }
        public string GENDER { get; set; }
        public int? AGE { get; set; }
    }
}