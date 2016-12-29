using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core.Model;

namespace PersonManagement.Core.Model
{
    [Table("Person")]
   public class Person :BaseEntity
    {
        public string Firstname { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public int sex { get; set; }
        public int NationalId { get; set; }
        public int Active { get; set; }
    }
}
