using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace DataAccess.Report
{
    public class QueryCollection:NameValueCollection
    {
        public void Add(Query q)
        {
            base.BaseAdd(q.QueryID.ToString(), q);
        }
        /// <summary>
        /// Gets the entry at the specified index of the <see cref="T:System.Collections.Specialized.NameValueCollection" />.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public Query this[int index]
        {
            get
            {
                return (Query)base.BaseGet(index);
            }
            set
            {
                base.BaseSet(index, value);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Query"/> with the specified key.
        /// </summary>
        /// <value>
        /// The <see cref="Query"/>.
        /// </value>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public Query this[string key]
        {
            get
            {
                return (Query)base.BaseGet(key);
            }
            set
            {
                base.BaseSet(key, value);
            }
        }
    }
}
