using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC.Enrollment;
using Entities.CCC.Visit;
using Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Reports
{
    public class sp_gettxcurr: BaseEntity
    {
        [Column]
        [Key]
        public int PID { get; set; }
        public string IdentifierValue { get; set; }
        public DateTime DispensedByDate { get; set; }
        public DateTime ExpectedReturn { get; set; }
        public DateTime ExitDate { get; set; }
        public string ExitReason { get; set; }
    }
}
