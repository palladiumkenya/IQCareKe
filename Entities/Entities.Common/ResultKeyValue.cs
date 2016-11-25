using System;

namespace Entities.Common
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
  public  class ResultKeyValue
    {
        /// <summary>
        /// Gets or sets the name of the result.
        /// </summary>
        /// <value>
        /// The name of the result.
        /// </value>
        public string ResultName { get; set; }
        /// <summary>
        /// Gets or sets the result value.
        /// </summary>
        /// <value>
        /// The result value.
        /// </value>
        public object ResultValue { get; set; }
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}: {1}", ResultName, ResultValue.ToString());
        }
    }
}
