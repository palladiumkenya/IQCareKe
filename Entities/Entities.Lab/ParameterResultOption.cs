using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Lab
{
    /// <summary>
    ///  Parameter Result Options
    /// </summary>
    [Serializable]
    [Table("dtl_LabTestParameterResultOption")]
    public class ParameterResultOption
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the parameter identifier.
        /// </summary>
        /// <value>
        /// The parameter identifier.
        /// </value>
        [Column("ParameterId")]
        [ForeignKey("Parameter")]
        public int ParameterId { get; set; }
        public virtual TestParameter Parameter { get; set; }
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
       [Column("Value")]
        public string Text { get; set; }
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
            return Text;
        }
    }
   
}
