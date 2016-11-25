using System;
using Entities.Common;
using System.Collections.Generic;

namespace Entities.Administration
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Entities.Common.BaseObject" />
    [Serializable]
    public class Facility : BaseObject
    {
        /// <summary>
        /// Gets or sets the grace period.
        /// </summary>
        /// <value>
        /// The grace period.
        /// </value>
        public int GracePeriod { get; set; }
        /// <summary>
        /// Gets or sets the satellite identifier.
        /// </summary>
        /// <value>
        /// The satellite identifier.
        /// </value>
        public int SatelliteId { get; set; }
        /// <summary>
        /// Gets or sets the country identifier.
        /// </summary>
        /// <value>
        /// The country identifier.
        /// </value>
        public int CountryId { get; set; }
        /// <summary>
        /// Gets or sets the backup drive.
        /// </summary>
        /// <value>
        /// The backup drive.
        /// </value>
        public string BackupDrive { get; set; }
        /// <summary>
        /// Gets or sets the system identifier.
        /// </summary>
        /// <value>
        /// The system identifier.
        /// </value>
        public int SystemId { get; set; }
        /// <summary>
        /// Gets or sets the login image.
        /// </summary>
        /// <value>
        /// The login image.
        /// </value>
        public string LoginImage { get; set; }
        /// <summary>
        /// Gets or sets the date format.
        /// </summary>
        /// <value>
        /// The date format.
        /// </value>
        public string DateFormat { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Facility"/> is preffered.
        /// </summary>
        /// <value>
        ///   <c>true</c> if preffered; otherwise, <c>false</c>.
        /// </value>
        public Boolean Preffered { get; set; }

        /// <summary>
        /// Gets or sets the index of the master.
        /// </summary>
        /// <value>
        /// The index of the master.
        /// </value>
        public string MasterIndex { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public override string Description
        {
            get
            {
                return string.Format("{0}-{1}-{2}-{3}", this.CountryId, MasterIndex, SatelliteId, Name);
            }
            set
            {
                base.Description = value;
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [paper less].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [paper less]; otherwise, <c>false</c>.
        /// </value>
        public Boolean PaperLess { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Facility"/> is integrated.
        /// </summary>
        /// <value>
        ///   <c>true</c> if integrated; otherwise, <c>false</c>.
        /// </value>
        public Boolean Integrated { get; set; }
        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; }
        /// <summary>
        /// Gets or sets the modules.
        /// </summary>
        /// <value>
        /// The modules.
        /// </value>
        public virtual List<ServiceArea> Modules { get; set; }
    }
}
