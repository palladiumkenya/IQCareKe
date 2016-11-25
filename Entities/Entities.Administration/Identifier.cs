using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.Common;

namespace Entities.Administration
{
    [Serializable]
    public class Identifier : BaseObject
    {

        /// <summary>
        /// Gets or sets the type of the base.
        /// </summary>
        /// <value>
        /// The type of the base.
        /// </value>
        public FieldControlType BaseType { get; set; }
        /// <summary>
        /// Gets the type of the data.
        /// </summary>
        /// <value>
        /// The type of the data.
        /// </value>
        public string DataType { get { return BaseType.DataType; } }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Identifier"/> is serial.
        /// </summary>
        /// <value>
        ///   <c>true</c> if serial; otherwise, <c>false</c>.
        /// </value>
        public bool Serial { get; set; }
        /// <summary>
        /// Gets or sets the seed.
        /// </summary>
        /// <value>
        /// The seed.
        /// </value>
        public int? Seed { get; set; }
    }
}
