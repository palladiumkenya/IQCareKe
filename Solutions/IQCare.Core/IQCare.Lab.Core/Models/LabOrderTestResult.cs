using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IQCare.Lab.Core.Models
{
    public class LabOrderTestResult
    {
        public int Id { get; set; }
        public int LabTestId { get; set; }
        public int LabOrderTestId { get; set; }
        public int ParameterId { get; set; }
        public decimal ResultValue { get; set; }
        public string ResultText { get; set; }
        public int ResultOptionId { get; set; }
        public string ResultOption { get; set; }
        public string ResultUnit { get; set; }
        public int ResultUnitId { get; set; }
        public int ResultConfigId { get; set; }
        public bool Undetectable { get; set; }
        public decimal DetectionLimit { get; set; }
        public int UserId { get; set; }
        public bool DeleteFlag { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime StatusDate { get; set; }
        public string AuditData { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public bool HasResult { get; set; }
    }
}
 