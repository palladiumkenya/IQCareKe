using System;

namespace Entities.Pharmacy
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class PrescriptionItem
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the prescription identifier.
        /// </summary>
        /// <value>
        /// The prescription identifier.
        /// </value>
        public int PrescriptionId { get; set; }
        /// <summary>
        /// Gets or sets the item prescribed.
        /// </summary>
        /// <value>
        /// The item prescribed.
        /// </value>
        public PharmacyItem ItemPrescribed { get; set; }
        /// <summary>
        /// Gets the name of the item.
        /// </summary>
        /// <value>
        /// The name of the item.
        /// </value>
        public string ItemName { get { return ItemPrescribed.Name; } }
        /// <summary>
        /// Gets the item identifier.
        /// </summary>
        /// <value>
        /// The item identifier.
        /// </value>
        public int ItemId { get { return ItemPrescribed.Id; } }
        /// <summary>
        /// Gets the item type identifier.
        /// </summary>
        /// <value>
        /// The item type identifier.
        /// </value>
        public int ItemTypeId { get { return ItemPrescribed.TypeId; } }
        /// <summary>
        /// Gets the name of the item type.
        /// </summary>
        /// <value>
        /// The name of the item type.
        /// </value>
        public int ItemTypeName { get { return ItemPrescribed.TypeName; } }
        /// <summary>
        /// Gets or sets the dose.
        /// </summary>
        /// <value>
        /// The dose.
        /// </value>
        public double Dose { get; set; }
        /// <summary>
        /// Gets or sets the frequency.
        /// </summary>
        /// <value>
        /// The frequency.
        /// </value>
        public string Frequency { get; set; }
        /// <summary>
        /// Gets or sets the duration in days.
        /// </summary>
        /// <value>
        /// The duration in days.
        /// </value>
        public int DurationInDays { get; set; }
        /// <summary>
        /// Gets or sets the quantity prescribed.
        /// </summary>
        /// <value>
        /// The quantity prescribed.
        /// </value>
        public double QuantityPrescribed { get; set; }
        /// <summary>
        /// Gets or sets the quantity dispensed.
        /// </summary>
        /// <value>
        /// The quantity dispensed.
        /// </value>
        public double QuantityDispensed { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PrescriptionItem"/> is prophylaxis.
        /// </summary>
        /// <value>
        ///   <c>true</c> if prophylaxis; otherwise, <c>false</c>.
        /// </value>
        public bool Prophylaxis { get; set; }
        /// <summary>
        /// Gets or sets the instruction.
        /// </summary>
        /// <value>
        /// The instruction.
        /// </value>
        public string Instruction { get; set; }

    }
}
