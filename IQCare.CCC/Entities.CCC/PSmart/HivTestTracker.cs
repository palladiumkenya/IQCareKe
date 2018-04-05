using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.psmart
{
    [Serializable][Table("HIvTestTracker")]
    public class HivTestTracker
    {
        [Key]
        public int Id { get; set; }
       public int  Ptn_pk { get; set; }
        public int PersonId { get; set; }
        public string DiagnosisMode { get; set; }
        public string TestEpisode { get; set; }
        public string Result { get; set; }
        public DateTime ResultDate { get; set; }
        public string ResultCategory { get; set; }
        public string  MFLCode { get; set; }
       public string Strategy { get; set; }
       public string ProviderName { get; set; }
       public string  ProviderId { get; set; }
     
           
    }
}