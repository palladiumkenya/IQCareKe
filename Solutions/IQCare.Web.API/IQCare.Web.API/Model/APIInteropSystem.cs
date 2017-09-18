using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IQCare.Web.API.Model
{
    [Serializable]
    //[Table("API_InteropSystems")]
    public class ApiInteropSystem
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public  string EndPoint { get; set; }
        public string ApiKey { get; set; }
        public int Active { get; set; }
        public int DeleteFlag { get; set; }
    }
}

