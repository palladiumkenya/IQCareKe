using System;

namespace Entities.Lab
{
    [Serializable]
    public class TestSpecimen    
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the specimen number.
        /// </summary>
        /// <value>
        /// The specimen number.
        /// </value>
        public string SpecimenNumber { get; set; }
        /// <summary>
        /// Gets or sets the specimen date.
        /// </summary>
        /// <value>
        /// The specimen date.
        /// </value>
        public DateTime SpecimenDate { get; set; }
        /// <summary>
        /// Gets or sets the type of the specimen.
        /// </summary>
        /// <value>
        /// The type of the specimen.
        /// </value>
        public string SpecimenType { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
        
    }
}
