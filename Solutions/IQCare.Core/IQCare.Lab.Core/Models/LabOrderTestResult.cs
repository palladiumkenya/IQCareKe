using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IQCare.Lab.Core.Models
{
    public class LabOrderTestResult
    {
        public LabOrderTestResult()
        {
            
        }

        public LabOrderTestResult(int labOrderId, int labOrderTestId, int labTestId,int parameterId, decimal ? resultValue, int ? resultOptionId,string resultOption,string resultUnit,int? resultUnitId,int userId,bool undetectedTable,decimal ? detectionLimit)
        {
            LabOrderId = labOrderId;
            LabOrderTestId = labOrderTestId;
            LabTestId = labTestId;
            ParameterId = parameterId;
            ResultValue = resultValue;
            ResultOption = resultOption; 
            ResultUnit = resultUnit;
            ResultUnitId = resultUnitId;
            UserId = userId;
            CreatedBy = userId;
            CreateDate = DateTime.Now;
            StatusDate = DateTime.Now;
            Undetectable = undetectedTable;
            DetectionLimit = detectionLimit;
        }
        public int Id { get; private set; }
        public int LabTestId { get; private set; }
        public int LabOrderId { get; set; }
        public int LabOrderTestId { get; private set; }
        public int ParameterId { get; private set; }
        public decimal ? ResultValue { get; private set; }
        public string ResultText { get; private set; }
        public int ? ResultOptionId { get; private set; }
        public string ResultOption { get; set; }
        public string ResultUnit { get; set; }
        public int? ResultUnitId { get; set; }
        public int ? ResultConfigId { get; set; }
        public bool Undetectable { get; set; }
        public decimal ? DetectionLimit { get; set; }
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
 