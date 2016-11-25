using Entities.Administration;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.PatientCore
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [Table("Mst_HomePageSection")]
    public class HomePageSection
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the service area identifier.
        /// </summary>
        /// <value>
        /// The service area identifier.
        /// </value>
        [ForeignKey("ServiceArea")]
        [Column("ModuleId")]
        public int ServiceAreaId { get; set; }
        /// <summary>
        /// Gets or sets the service area.
        /// </summary>
        /// <value>
        /// The service area.
        /// </value>
        public ServiceArea ServiceArea { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [delete flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [delete flag]; otherwise, <c>false</c>.
        /// </value>
        public bool DeleteFlag { get; set; }
        /// <summary>
        /// Gets or sets the icon font.
        /// </summary>
        /// <value>
        /// The icon font.
        /// </value>
        public string IconFont { get; set;}
        /// <summary>
        /// Gets or sets the ord rank.
        /// </summary>
        /// <value>
        /// The ord rank.
        /// </value>
        public double OrdRank { get; set; }
    }
}
