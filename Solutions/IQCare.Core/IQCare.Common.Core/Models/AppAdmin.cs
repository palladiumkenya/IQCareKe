using System;

namespace IQCare.Common.Core.Models
{
    public class AppAdmin
    {
        public int Id { get; set; }
        public string AppVer { get; set; }
        public string DBVer { get; set; }
        public DateTime RelDate { get; set; }
        public string VersionName { get; set; }
    }
}