using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Entities.Common;

namespace Entities.CCC.Lookup
{
    [Serializable]
    [Table("VW_PatientLaboratory")]
    public class LookupPreviousLabs 
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public DateTime OrderedByDate { get; set; }
        public string OrderedByName { get; set; }
        public string TestResults { get; set; }
        //public int LabId { get; set; }
       
    
    }
}
