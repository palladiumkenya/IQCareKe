namespace IQCare.Lab.Core.Models
{
    public class ParameterResultOption
    {
        public int Id { get; set; }
        public int ParameterId { get; set; }
        public string Value { get; set; }
        public bool DeleteFlag { get; set; }

        public virtual LabTestParameter Parameter { get; set; }
    }
}