using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Lab
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [Table("mst_LabDepartment", Schema = "dbo")]
    public class TestDepartment
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Column("LabDepartmentID")]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        [Column("LabDepartmentName")]
        public string Name { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }
        /// <summary>
        /// Gets or sets a value indicating whether [delete flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [delete flag]; otherwise, <c>false</c>.
        /// </value>
        public bool DeleteFlag { get; set; }
    }
   
}
