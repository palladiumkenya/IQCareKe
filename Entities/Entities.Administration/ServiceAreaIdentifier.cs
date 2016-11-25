using System;

namespace Entities.Administration
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ServiceAreaIdentifier
    {
        /// <summary>
        /// Gets the service area identifier.
        /// </summary>
        /// <value>
        /// The service area identifier.
        /// </value>
        public int ServiceAreaId
        {
            get
            {
                return ServiceArea.Id;
            }
        }
        /// <summary>
        /// Gets or sets the service area.
        /// </summary>
        /// <value>
        /// The service area.
        /// </value>
        public virtual ServiceArea ServiceArea { get; set; }
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public virtual Identifier Identifier { get; set; }
    }
}
