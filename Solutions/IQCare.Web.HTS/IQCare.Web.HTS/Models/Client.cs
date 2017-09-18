using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IQCare.Web.HTS.Models
{
    [Serializable]
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int KeyPopId { get; set; }
        public string KeyPopOther { get; set; }
        public string HtsNumber { get; set; }
    }
}
