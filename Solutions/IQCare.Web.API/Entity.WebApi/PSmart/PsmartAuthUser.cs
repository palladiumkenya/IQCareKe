using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.WebApi.PSmart
{
    [Serializable]
    [Table("psmartAuthUser")]
    public class PsmartAuthUser
    {
        [Key]
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string FACILITY { get; set; }
    }
}
