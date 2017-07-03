using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Entities.Lab
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [Table("dtl_LabOrderTest")]
    public class LabOrderTest
    {
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
        [ForeignKey("Order")]
        public int LabOrderId { get; set; }
        
        public virtual LabOrder Order { get; set; }
        /// <summary>
        /// Gets or sets the lab test identifier.
        /// </summary>
        /// <value>
        /// The lab test identifier.
        /// </value>
        public virtual LabTest Test { get; set; }

        //[Column("LabTestId")]
        //[ForeignKey("Test")]
        public int TestId
        {
            get
            {
                return Test.Id;
            }
            set { TestId = value; }
        }
               
        [NotMapped]
        public string TestName
        {
            get
            {
                return Test.Name;
            }
        }
        /// <summary>
        /// Gets or sets the test notes.
        /// </summary>
        /// <value>
        /// The test notes.
        /// </value>
        public string TestNotes { get; set; }
        /// <summary>
        /// Gets or sets the result notes.
        /// </summary>
        /// <value>
        /// The result notes.
        /// </value>
        public string ResultNotes { get; set; }

        /// <summary>
        /// Gets or sets the result by.
        /// </summary>
        /// <value>
        /// The result by.
        /// </value>
        public int? ResultBy { get; set; }

        /// <summary>
        /// Gets or sets the result date.
        /// </summary>
        /// <value>
        /// The result date.
        /// </value>
        public DateTime? ResultDate { get; set; }
        [NotMapped]
        public DateTime OrderDate { get; set; }
        [NotMapped]
        public string OrderNumber { get; set; }
        [NotMapped]
        public int OrderedBy { get; set; }
        [NotMapped]
        public int ServiceAreaId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [delete flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [delete flag]; otherwise, <c>false</c>.
        /// </value>
        public bool DeleteFlag { get; set; }
        [NotMapped]
        public bool IsGroup
        {
            get
            {
                return Test.IsGroup;
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is parent.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is parent; otherwise, <c>false</c>.
        /// </value>
        [Column("ParentTestId")]
        public int? ParentLabTestId { get; set; }

        /// <summary>
        /// Gets or sets the parameter results.
        /// </summary>
        /// <value>
        /// The parameter results.
        /// </value>
        public virtual List<LabTestParameterResult> ParameterResults
        {
            get;
            set;
        }
        [NotMapped]
        public int? SpecimenId { get; set; }
       [NotMapped]
        public virtual TestSpecimen Specimen { get; set; }
        /// <summary>
        /// Gets the test order status.
        /// </summary>
        /// <value>
        /// The test order status.
        /// </value>
        [NotMapped]
        public string TestOrderStatus
        {
            get
            {
                string strStatus = "Pending";
                if (null != ParameterResults)
                {
                    strStatus = ParameterResults.Count(ot => ot.HasResult == true) == ParameterResults.Count() ? "Complete" : "Pending";
                }
                return strStatus;
            }
        }
    }
   
}
