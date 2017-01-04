using System;

namespace Entities.Lab
{
    /// <summary>
    /// Parameter result unit configuration
    /// </summary>
    [Serializable]
    public class ParameterResultConfig
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
        public int ParameterId { get; set; }
        /// <summary>
        /// Gets or sets the minimum boundary.
        /// </summary>
        /// <value>
        /// The minimum boundary.
        /// </value>
        public decimal? MinBoundary { get; set; }
        /// <summary>
        /// Gets or sets the maximum boundary.
        /// </summary>
        /// <value>
        /// The maximum boundary.
        /// </value>
        public decimal? MaxBoundary { get; set; }
        /// <summary>
        /// Gets or sets the maximum normal range.
        /// </summary>
        /// <value>
        /// The maximum normal range.
        /// </value>
        public decimal? MaxNormalRange { get; set; }
        /// <summary>
        /// Gets or sets the minimum normal range.
        /// </summary>
        /// <value>
        /// The minimum normal range.
        /// </value>
        public decimal? MinNormalRange { get; set; }
        /// <summary>
        /// Gets or sets the detection limit.
        /// </summary>
        /// <value>
        /// The detection limit.
        /// </value>
        public decimal? DetectionLimit { get; set; }
        /// <summary>
        /// Gets the unit identifier.
        /// </summary>
        /// <value>
        /// The unit identifier.
        /// </value>
        public int UnitId
        {
            get
            {
                return (null == ResultUnit) ? -1 : ResultUnit.Id;
            }
        }
        /// <summary>
        /// Gets the name of the unit.
        /// </summary>
        /// <value>
        /// The name of the unit.
        /// </value>
        public string UnitName
        {
            get
            {
                return (null == ResultUnit) ? "" : ResultUnit.Text;
            }
        }
        public string ReferenceRange { get; set; }
        /// <summary>
        /// Gets or sets the result unit.
        /// </summary>
        /// <value>
        /// The result unit.
        /// </value>
        public virtual ResultUnit ResultUnit { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is default; otherwise, <c>false</c>.
        /// </value>
        public bool IsDefault { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [delete flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [delete flag]; otherwise, <c>false</c>.
        /// </value>
        public bool DeleteFlag { get; set; }
    }
}
