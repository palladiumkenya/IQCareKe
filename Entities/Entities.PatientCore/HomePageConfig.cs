using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.PatientCore
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [Table("dtl_HomePageConfig")]
    public class HomePageConfig
    {
        /// <summary>
        /// Gets or sets the section identifier.
        /// </summary>
        /// <value>
        /// The section identifier.
        /// </value>
        [Key, Column(Order = 0)]
        [ForeignKey("Section")]
        public int SectionId { get; set; }
        /// <summary>
        /// Gets or sets the query identifier.
        /// </summary>
        /// <value>
        /// The query identifier.
        /// </value>
        [Key, Column(Order = 1)]
        [ForeignKey("Query")]
        public int QueryId { get; set; }
        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        /// <value>
        /// The query.
        /// </value>
        public virtual QueryDefinition Query { get; set; }    
        /// <summary>
        /// Gets or sets the section.
        /// </summary>
        /// <value>
        /// The section.
        /// </value>
        public virtual HomePageSection Section { get; set; }
        /// <summary>
        /// Gets or sets the ord rank.
        /// </summary>
        /// <value>
        /// The ord rank.
        /// </value>
        public double OrdRank { get; set; }
    }
}
