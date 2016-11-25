using System;
using System.Collections.Generic;
using Entities.PatientCore;
using Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Pharmacy
{
    [Serializable]
    public class PrescriptionFilter : Filter
    {
        public string OrderStatus { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [Table("ord_PatientPharmacyOrder")]
    public class Prescription
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Prescription"/> class.
        /// </summary>
        public Prescription()
        {
          
        }
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Column("ptn_pharmacy_pk")]             
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        [Column("Ptn_Pk")]
        [ForeignKey("Client")]
        public int PatientId { get; set; }
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
       [NotMapped] 
        public int ModuleId { get; set; }
        /// <summary>
        /// Gets or sets the prescription number.
        /// </summary>
        /// <value>
        /// The prescription number.
        /// </value>
        [Column("ReportingID")]
        public string PrescriptionNumber { get; set; }
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
        public int DeleteFlag { get; set; }
        /// <summary>
        /// Gets or sets the prescription date.
        /// </summary>
        /// <value>
        /// The prescription date.
        /// </value>
        [Column("OrderedByDate")]
        public DateTime PrescriptionDate { get; set; }
          [Column("DispensedByDate")]
        public DateTime DispensedDate { get; set; }
        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>
        /// The create date.
        /// </value>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// Gets or sets the prescribed by.
        /// </summary>
        /// <value>
        /// The prescribed by.
        /// </value>
       [Column("OrderedBy")]
        public int PrescribedBy { get; set; }
        /// <summary>
        /// Gets or sets the dispensed by.
        /// </summary>
        /// <value>
        /// The dispensed by.
        /// </value>
       [Column("DispensedBy")]
       public int DispensedBy { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        public virtual Patient Client { get; set; }
        /// <summary>
        /// Gets or sets the prescription notes.
        /// </summary>
        /// <value>
        /// The prescription notes.
        /// </value>
        [Column("PharmacyNotes")]
        public string PrescriptionNotes { get; set; }
        /// <summary>
        /// Gets or sets the items prescribed.
        /// </summary>
        /// <value>
        /// The items prescribed.
        /// </value>
        public virtual List<PrescriptionItem> ItemsPrescribed
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the order status.
        /// </summary>
        /// <value>
        /// The order status.
        /// </value>
        /// 
 
        public int OrderStatus
        {
            get;
            set;               
        }
    }
}
