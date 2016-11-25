using System;
using System.Collections.Generic;

namespace Entities.Lab
{
    [Serializable]
    public class LabTestGroup
    {
        /// <summary>
        /// Gets or sets the group test.
        /// </summary>
        /// <value>
        /// The group test.
        /// </value>
        public LabTest GroupTest { get; set; }
        /// <summary>
        /// Gets or sets the component test.
        /// </summary>
        /// <value>
        /// The component test.
        /// </value>
        public List<LabTest> ComponentTest { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [delete flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [delete flag]; otherwise, <c>false</c>.
        /// </value>
        public Boolean DeleteFlag { get; set; }
    }

}
