using System;
using Microsoft.Build.Framework;

namespace Common.Core.Model
{
    public abstract class BaseEntity 
    {
        [Required]
        public virtual int Id { get; set; }
        [Required]
        public virtual bool DeleteFlag { get; set; }
        [Required]
        public virtual int CreateBy { get; set; }
        [Required]
        public virtual DateTime CreateDate { get; set; }

        protected BaseEntity()
        {
            CreateBy = 1;
            CreateDate = DateTime.Now;

        }

    }
}
