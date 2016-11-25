using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Lab
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [Table("Mst_LabTestParameter", Schema = "dbo")]
    public class TestParameter
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the lab test identifier.
        /// </summary>
        /// <value>
        /// The lab test identifier.
        /// </value>
        [ForeignKey("LabTest")]
        public int LabTestId { get; set; }
        public virtual LabTest LabTest { get; set; }
        /// <summary>
        /// Gets or sets the reference identifier.
        /// </summary>
        /// <value>
        /// The reference identifier.
        /// </value>
        public string ReferenceId { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Column("ParameterName")]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TestParameter" /> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool DeleteFlag { get; set; }
        /// <summary>
        /// Gets or sets the type of the data.
        /// </summary>
        /// <value>
        /// The type of the data.
        /// </value>
        public string DataType { get; set; }
        /// <summary>
        /// Gets or sets the loinc code.
        /// </summary>
        /// <value>
        /// The loinc code.
        /// </value>
        public string LoincCode { get; set; }
        [Column("OrdRank")]
        public Double Rank { get; set; }
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
        /// Gets or sets the result configuration.
        /// </summary>
        /// <value>
        /// The result configuration.
        /// </value>
        public virtual List<ParameterResultConfig> ResultConfig { get; set; }

        /// <summary>
        /// Gets or sets the result option.
        /// </summary>
        /// <value>
        /// The result option.
        /// </value>
        public virtual List<ParameterResultOption> ResultOption { get; set; }
    }
    
}
