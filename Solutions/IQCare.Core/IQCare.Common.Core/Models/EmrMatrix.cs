using System;

namespace IQCare.Common.Core.Models
{
    public class EmrMatrix
    {
        public DateTime? LastLoginDate { get; set; }
        public string EmrVersion { get; set; }
        public string EmrName { get; set; }
        public DateTime? LastMoH731RunDate { get; set; }
    }
}