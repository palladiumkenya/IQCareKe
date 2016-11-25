using System;

namespace Entities.Common
{
    public interface IBaseObject 
    {
        int Id { get; set; }
        string Name { get; set; }
        bool DeleteFlag { get; set; }
        bool Active { get; set; }
        string Description { get; set; }
        DateTime CreateDate { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class BaseObject:IBaseObject
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public virtual int Id { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public virtual  string Name { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [delete flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [delete flag]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool DeleteFlag { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BaseObject"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public virtual bool Active { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public virtual  string  Description { get; set; }
        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>
        /// The create date.
        /// </value>
        public  DateTime CreateDate { get; set; }


    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Entities.Common.BaseObject" />
   
}
