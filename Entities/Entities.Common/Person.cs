using System;

namespace Entities.Common
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public  class Person
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public  int Id { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public  string LastName { get; set; }
        /// <summary>
        /// Gets or sets the name of the mid.
        /// </summary>
        /// <value>
        /// The name of the mid.
        /// </value>
        public  string MidName { get; set; }
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public string FullName
        {
            get { return FirstName + ", "+ MidName +" " + LastName; }
        }
        public int IdentificationType { get; set; }
        public string IdentificationNo { get; set; }
    }
}
