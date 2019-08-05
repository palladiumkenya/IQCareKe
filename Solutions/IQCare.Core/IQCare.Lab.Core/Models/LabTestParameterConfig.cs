namespace IQCare.Lab.Core.Models
{
    public class LabTestParameterConfig
    {
        public int Id { get; set; }
        public int ParameterId { get; set; }
        public decimal? MinBoundary { get; set; }
        public decimal? MaxBoundary { get; set; }
        public decimal? MinNormalRange { get; set; }
        public decimal? MaxNormalRange { get; set; }
        public int UnitId { get; set; }
        public bool DefaultUnit { get; set; }
        public decimal DetectionLimit { get; set; }
        public bool DeleteFlag { get; set; }
        public virtual LabTestParameter Parameter { get; set; }
        public virtual   LabTestParameterUnit Unit { get; set; }

    }
}