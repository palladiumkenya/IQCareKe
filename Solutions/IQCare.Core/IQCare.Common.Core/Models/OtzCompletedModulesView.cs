using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class OtzCompletedModulesView
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Topic { get; set; }
        public DateTime DateCompleted { get; set; }
    }
}
