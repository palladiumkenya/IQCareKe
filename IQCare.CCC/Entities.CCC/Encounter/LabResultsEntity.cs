using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;


namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("dtl_LabOrderTestResult")]
    public class LabResultsEntity : BaseEntity
    {


        public int Id { get; set; }
        public int LabOrderId { get; set; }
        public int LabTestId { get; set; }
        public int LabOrderTestId { get; set; }
        public int ParameterId { get; set; }
        public decimal? ResultValue { get; set; }
        public String ResultText { get; set; }
        public int? ResultOptionId { get; set; }
        public String ResultOption { get; set; }
        public String ResultUnit { get; set; }
        public int? ResultUnitId { get; set; }
        public int? ResultConfigId { get; set; }
        public bool? Undetectable { get; set; }
        public decimal? DetectionLimit { get; set; }
        public int UserId { get; set; }
        //public bool DeleteFlag { get; set; }
        //public DateTime CreateDate { get; set; }
        //public int CreatedBy { get; set; }
        public DateTime StatusDate { get; set; }
        [NotMapped]public bool HasResult { get; set; }


    }
}
