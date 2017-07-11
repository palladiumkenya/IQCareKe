using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Lab
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [Table("dtl_LabOrderTestResult")]
    public class LabTestParameterResult
    {
        int? nullInt = null;
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>     
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the lab order identifier.
        /// </summary>
        /// <value>
        /// The lab order identifier.
        /// </value>
        public int LabOrderId { get; set; }
        public int LabTestId { get; set; }
        /// <summary>
        /// Gets or sets the lab test identifier.
        /// </summary>
        /// <value>
        /// The lab test identifier.
        /// </value>
        public virtual TestParameter Parameter { get; set; }

       
        [ForeignKey("Parameter")]
        public int ParameterId { get; set; }

        public string ParameterName { get { return Parameter.Name; } }
        public string ResultDataType { get { return Parameter.DataType; } }
        [NotMapped]
        public string SampleType { get; set; }
        /// <summary>
        /// Gets or sets the lab order test identifier.
        /// </summary>
        /// <value>
        /// The lab order test identifier.
        /// </value>
        public int LabOrderTestId { get; set; }

        /// <summary>
        /// Gets or sets the result date.
        /// </summary>
        /// <value>
        /// The result date.
        /// </value>
        // public DateTime? ResultDate { get; set; }
        /// <summary>
        /// Gets or sets the result value.
        /// </summary>
        /// <value>
        /// The result value.
        /// </value>
        public decimal? ResultValue { get; set; }
        /// <summary>
        /// Gets or sets the result by.
        /// </summary>
        /// <value>
        /// The result by.
        /// </value>
        //   public int? ResultBy { get; set; }
        /// <summary>
        /// Gets or sets the undetectable.
        /// </summary>
        /// <value>
        /// The undetectable.
        /// </value>
        public bool? Undetectable { get; set; }
        /// <summary>
        /// Gets or sets the detection limit.
        /// </summary>
        /// <value>
        /// The detection limit.
        /// </value>
        public decimal? DetectionLimit { get; set; }
        /// <summary>
        /// Gets or sets the result text.
        /// </summary>
        /// <value>
        /// The result text.
        /// </value>
        public string ResultText { get; set; }
        /// <summary>
        /// Gets or sets the result option.
        /// </summary>
        /// <value>
        /// The result option.
        /// </value>
        public string ResultOption { get; set; }
        public int? ResultOptionId { get; set; }
        /// <summary>
        /// Gets or sets the result unit.
        /// </summary>
        /// <value>
        /// The result unit.
        /// </value>
     
        public virtual ResultUnit ResultUnit { get; set; }


        [ForeignKey("ResultUnit")]
        public int? ResultUnitId { get; set; }

        [NotMapped]
        public string ResultUnitName { get { return (null == ResultUnit) ? "" : ResultUnit.Text; } }

        [ForeignKey("Config")]
        [Column("ResultConfigId")]
        public int? ConfigId
        {
            get;set;
        }
      
        public virtual ParameterResultConfig Config { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [delete flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [delete flag]; otherwise, <c>false</c>.
        /// </value>
        public bool DeleteFlag { get; set; }
        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>
        /// The create date.
        /// </value>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has result.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has result; otherwise, <c>false</c>.
        /// </value>
        [NotMapped]
        public bool HasResult
        {
            get
            {
                if (ResultValue.HasValue || !(ResultText != null && string.IsNullOrEmpty(ResultText)) || !(ResultOption != null && string.IsNullOrEmpty(ResultOption)) || Undetectable == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

}
