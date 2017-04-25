using System;
using System.Collections.Generic;
using Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Administration
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Entities.Common.BaseObject" />
    [Serializable]
    [Table("Mst_Module")]
    public class ServiceArea : BaseObject
    {
        [Column("ModuleId")]
        public override int Id
        {
            get;

            set;
        }
        [Column("ModuleName")]
        public override string Name
        {
            get;
            set;
        }
        [NotMapped]
        public override bool Active
        {
            get;

            set;
        }
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }
       [NotMapped]
        public override string Description
        {
            get
            {
                return DisplayName;
            }

            set
            {
                DisplayName = value;
            }
        }
        public string Code { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [publish flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [publish flag]; otherwise, <c>false</c>.
        /// </value>
        [NotMapped]
        public bool PublishFlag { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [enrol flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enrol flag]; otherwise, <c>false</c>.
        /// </value>
        [Column("CanEnroll")]
        public bool EnrolFlag { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ServiceArea"/> is clinical.
        /// </summary>
        /// <value>
        ///   <c>true</c> if clinical; otherwise, <c>false</c>.
        /// </value>
        [NotMapped]
        public bool Clinical { get; set; }
        [NotMapped]
        public bool ModuleFlag { get; set; }

        /// <summary>
        /// Gets or sets the business rules.
        /// </summary>
        /// <value>
        /// The business rules.
        /// </value>
        public virtual List<ServiceRule> BusinessRules
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the identifiers.
        /// </summary>
        /// <value>
        /// The identifiers.
        /// </value>
        public virtual List<Identifier> Identifiers
        {
            get;
            set;
        }
        public override string ToString()
        {
            return $"{Code} - {Name}";
        }

    }
}
