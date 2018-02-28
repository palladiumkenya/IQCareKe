using System;
using IQCare.SharedKernel.Model;

namespace IQCare.HTS.Core.Model
{
    public class Form:Entity<Guid>
    {
        public string Name { get; set; }
        public Guid ModuleId { get; set; }
    }
}