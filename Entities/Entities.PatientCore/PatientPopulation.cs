using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.PatientCore
{
    [Serializable]
    public class PatientPopulation: BaseEntity
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string PopulationType { get; set; }
        public int PopulationCategory { get; set; }
        public bool Active { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
    }

    //[Serializable]
    //public enum PopulationCategory
    //{
    //    General,
    //    Key
    //}
    //[Serializable]
    //public class PopulationType:BaseObject
    //{ 
    //    public PopulationCategory Category { set; get; }
    //    public override string ToString()
    //    {
    //        return string.Format("{0} - {1} {2}", Category.ToString(), Name, Description);
    //    }
    //}

}
