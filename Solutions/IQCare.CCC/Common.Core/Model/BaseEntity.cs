using System;

namespace Common.Core.Model
{
    public abstract class BaseEntity 
    {
        public virtual int Id { get; set; }
        public virtual bool Void { get; set; }
        public virtual int VoidBy { get; set; }
        public virtual DateTime VoidDate { get; set; }
        public virtual int CreateBy { get; set; }
        public virtual DateTime CreateDate { get; set; }
    }
}
