using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Lab
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [Table("vw_LabTestParameterUnits", Schema = "dbo")]
    public class ResultUnit
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Column("UnitId")]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        [Column("UnitName")]
        public string Text { get; set; }

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
