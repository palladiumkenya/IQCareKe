using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.HTS
{
    [Serializable]
    public class Provider
    {
        [Key]
        public int Id { get; set; }
        public string SysUuid { get; set; }
        public string FirstName { get; set; }
        public string LoginName { get; set; }
        public string LoginPass { get; set; }
        public string ProviderIdentifier { get; set; }
        public string Cadre { get; set; }
        public string MflCode { get; set; }
    }
}
