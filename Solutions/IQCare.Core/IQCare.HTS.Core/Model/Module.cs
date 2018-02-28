using System;
using System.Collections.Generic;
using IQCare.SharedKernel.Model;

namespace IQCare.HTS.Core.Model
{
    public class Module : Entity<Guid>
    {
        public string Name { get; set; }

        public ICollection<Form> Forms { get; set; }    
    }
}