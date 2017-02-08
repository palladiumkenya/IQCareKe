using Entities.Common;
using System;

namespace Entities.PatientCore
{
    [Serializable]
    public class PatientPopulation: BaseEntity
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PopulationType { get; set; }
        public int PopulationCategory { get; set; }
        public bool Active { get; set; }
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
