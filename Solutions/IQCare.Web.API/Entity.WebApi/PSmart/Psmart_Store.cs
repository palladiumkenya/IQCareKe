using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.WebApi.PSmart
{
    [Serializable]
    [Table("Psmart_Store")]
    public class Psmart_Store
    {
        public int Id { get; set; }
        public string SHR { get; set; }
      // public DateTime Date_created { get; set; }
       public string Status { get; set; }
       //public DateTime? Status_date { get; set; }
       public string uuid { get; set; }
    }
}