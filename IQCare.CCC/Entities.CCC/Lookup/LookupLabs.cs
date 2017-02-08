using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

   
namespace Entities.CCC.Lookup
{
    [Serializable]
    [Table("[Mst_LabTestParameter]")]
    public class LookupLabs
    {
        public int Id { get; set; }       
        public string ParameterName { get; set; }  
        public int LabTestId { get; set; }   


    }
}
