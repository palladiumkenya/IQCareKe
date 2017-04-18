using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

   
namespace Entities.CCC.Lookup
{
    [Serializable]
    [Table("[mst_LabTestMaster]")]
    public class LookupLabs
    {
        public int Id { get; set; }
        public string ReferenceId { get; set; }
        public string Name { get; set; }

    }
}
