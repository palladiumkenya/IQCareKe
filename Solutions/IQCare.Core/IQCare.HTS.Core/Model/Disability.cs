using System;
using IQCare.SharedKernel.Model;

namespace IQCare.HTS.Core.Model
{
    public class Disability : Entity<Int32>
    {
        public int PersonID { get; set; }
        public int DisabilityID { get; set; }
    }
}