using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.SharedKernel.Model
{
    public abstract class Entity<TId> : BaseEntity
    {
        public TId Id { get; set; }
    }
}
