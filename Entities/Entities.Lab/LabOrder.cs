using Entities.Common;
using Entities.PatientCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Lab
{
    [Serializable]
    public class LabOrderFilter : Filter
    {
         public string OrderStatus { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    /// 

    [Serializable]
    [Table("Ord_LabOrder")]
    public class LabOrder
    {
        public LabOrder()
        {
          //  Client = new Patient();
           // OrderedTest = new List<LabOrderTest>();
        }
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        /// 
        [Column("Ptn_Pk")]
        [ForeignKey("Client")]
        public int PatientPk { get; set; }
        /// <summary>
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public int LocationId { get; set; }
        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>
        /// The module identifier.
        /// </value>
        public int ModuleId { get; set; }
        /// <summary>
        /// Gets or sets the order number.
        /// </summary>
        /// <value>
        /// The order number.
        /// </value>
        public string OrderNumber { get; set; }
        /// <summary>
        /// Gets or sets the visit identifier.
        /// </summary>
        /// <value>
        /// The visit identifier.
        /// </value>
        public int VisitId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [delete flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [delete flag]; otherwise, <c>false</c>.
        /// </value>
        public bool DeleteFlag { get; set; }
        /// <summary>
        /// Gets or sets the delete reason.
        /// </summary>
        /// <value>
        /// The delete reason.
        /// </value>
        [NotMapped]
        public string DeleteReason { get; set; }
        /// <summary>
        /// Gets or sets the order date.
        /// </summary>
        /// <value>
        /// The order date.
        /// </value>
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>
        /// The create date.
        /// </value>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// Gets or sets the pre clinic date.
        /// </summary>
        /// <value>
        /// The pre clinic date.
        /// </value>
        [Column("PreClinicLabDate")]
        public DateTime? PreClinicDate { get; set; }
        /// <summary>
        /// Gets or sets the ordered by.
        /// </summary>
        /// <value>
        /// The ordered by.
        /// </value>
        public int OrderedBy { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets the deleted by.
        /// </summary>
        /// <value>
        /// The deleted by.
        /// </value>
        [NotMapped]
        public int? DeletedBy { get; set; }
        public virtual Patient Client { get; set; }
        /// <summary>
        /// Gets or sets the clinical notes.
        /// </summary>
        /// <value>
        /// The clinical notes.
        /// </value>
        [Column("ClinicalOrderNotes")]
        public string ClinicalNotes { get; set; }

        /// <summary>
        /// Gets or sets the ordered test.
        /// </summary>
        /// <value>
        /// The ordered test.
        /// </value>
        public virtual List<LabOrderTest> OrderedTest
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the order status.
        /// </summary>
        /// <value>
        /// The order status.
        /// </value>

        public string OrderStatus
        {
            get;
            set;
            //get
            //{
            //    string strStatus = "Pending";
            //    if (null != OrderedTest)
            //    {
            //        strStatus = OrderedTest.Count(ot => ot.TestOrderStatus == "Complete") == OrderedTest.Count() ? "Complete" : "Pending";
            //    }
            //    return strStatus;
            //}
        }
    }

}
