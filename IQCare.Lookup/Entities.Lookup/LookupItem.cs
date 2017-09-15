
using System.ComponentModel.DataAnnotations.Schema;
using System;
using Entities.Common;
namespace Entities.Lookup
{
   
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [Table("LookupView")]
    public class LookupItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Item" /> class.
        /// </summary>
        public LookupItem()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        /// The _val
        /// </summary>
        string _val;

        /// <summary>
        /// The _id
        /// </summary>
        int _id;

        /// <summary>
        /// The _item code
        /// </summary>
        string _itemCode;
        /// <summary>
        /// The _deleted
        /// </summary>
        bool _deleted = false;

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get { return _id; } set { _id = value; } }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Name { get { return _val; } set { _val = value; } }
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string LookupName { get { return _itemCode; } set { _itemCode = value; } }
        public string Category { get; set; }
        public int LookupId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Item"/> is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if deleted; otherwise, <c>false</c>.
        /// </value>
        public bool Deleted { get { return _deleted; } set { _deleted = value; } }   
            [NotMapped]
        public decimal OrdRank { get; set; }
    }
}
