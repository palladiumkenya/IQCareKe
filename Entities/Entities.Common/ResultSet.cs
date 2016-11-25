using System;
using System.Collections.Generic;

namespace Entities.Common
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class ResultSet<T>
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public List<T> Items { get; set; }
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The total numbe of pages based on the pager setting.
        /// </value>
        public int Count { get; set; }
    }
    
}
