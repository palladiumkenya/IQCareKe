using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.CCC.Lookup
{
    [Serializable]
    [Table("VW_PatientLabsView")]
    public class PatientLab
    {   
        [Key]
        public int Id { get; set; }
        public int Ptn_pk { get; set; }

        public int? PatientId { get; set; }

        public int?  VisitID { get; set; }

        public string TestName { get; set; }

        public string TestResult { get; set; }

        public DateTime OrderedbyDate { get; set; }

        public DateTime? ReportedByDate { get; set; }
    }
}
