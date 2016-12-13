using System.Collections.Generic;
using System;
using Entities.Common;

namespace Entities.FormBuilder
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class FormObject: BaseObject
    {
        
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }
        /// <summary>
        /// Gets or sets the name of the reference.
        /// </summary>
        /// <value>
        /// The name of the reference.
        /// </value>
        public string ReferenceName { get; set; }
        public string FormTypeReferenceName { get; set; }
        public int FormTypeId { get; set; }
        public int FeatureId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FormObject"/> is published.
        /// </summary>
        /// <value>
        ///   <c>true</c> if published; otherwise, <c>false</c>.
        /// </value>
        public bool Published { get; set; }
        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>
        /// The module identifier.
        /// </value>
        public int ModuleId { get; set; }
        public bool Custom { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [multi visit].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [multi visit]; otherwise, <c>false</c>.
        /// </value>
        public bool MultiVisit { get; set; }
        /// <summary>
        /// Gets or sets the tabs.
        /// </summary>
        /// <value>
        /// The tabs.
        /// </value>
        public virtual List<FormTab> Tabs { get; set; }
    }
}
