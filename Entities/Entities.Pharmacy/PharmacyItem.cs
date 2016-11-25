
using System;
namespace Entities.Pharmacy
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class PharmacyItem
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
       public int Id { get; set; }
       /// <summary>
       /// Gets or sets the name.
       /// </summary>
       /// <value>
       /// The name.
       /// </value>
       public string Name { get; set; }
       /// <summary>
       /// Gets or sets the type identifier.
       /// </summary>
       /// <value>
       /// The type identifier.
       /// </value>
       public int TypeId { get; set; }
       /// <summary>
       /// Gets or sets the name of the type.
       /// </summary>
       /// <value>
       /// The name of the type.
       /// </value>
       public int TypeName { get; set; }

    }
}
