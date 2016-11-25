using System;

namespace Entities.Clinical
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Service
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the service area.
        /// </summary>
        /// <value>
        /// The service area.
        /// </value>
        public string ServiceArea { get; set; }
        /// <summary>
        /// Gets or sets the service area identifier.
        /// </summary>
        /// <value>
        /// The service area identifier.
        /// </value>
        public int ServiceAreaId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [delete flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [delete flag]; otherwise, <c>false</c>.
        /// </value>
        public bool DeleteFlag { get; set; }
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Name + "  " + Description;
        }
    }
  }
