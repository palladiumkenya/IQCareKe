using System;

namespace Entities.Administration
{
    [Serializable]
    public class PatientWard
    {
        /// <summary>
        /// The ward identifier
        /// </summary>
        /// <value>
        /// The ward identifier.
        /// </value>
        public int? WardID {get;set;}
        /// <summary>
        /// The location identifier
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public int LocationID { get; set; }
        /// <summary>
        /// The ward name
        /// </summary>
        /// <value>
        /// The name of the ward.
        /// </value>
        public string WardName { get; set; }
        /// <summary>
        /// The patient category
        /// </summary>
        /// <value>
        /// The patient category.
        /// </value>
        public string PatientCategory { get; set; }
        /// <summary>
        /// The capacity
        /// </summary>
        /// <value>
        /// The capacity.
        /// </value>
        public int Capacity { get; set; }
        /// <summary>
        /// The occupancy
        /// </summary>
        /// <value>
        /// The occupancy.
        /// </value>
        public int Occupancy { get; set; }
        /// <summary>
        /// The active
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Active { get; set; }

    }
  
}
