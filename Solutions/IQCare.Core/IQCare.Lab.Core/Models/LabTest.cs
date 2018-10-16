using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IQCare.Lab.Core.Models
{
    public class LabTest
    {
        public int Id { get; set; }
        public string ReferenceId { get; set; }
        public string Name { get; set; }
        public bool IsGroup { get; set; }
        public int DepartmentId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int ParameterCount { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
        public bool DeleteFlag { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string LoincCode { get; set; }
    }
}