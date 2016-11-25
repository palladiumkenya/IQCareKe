using System;
using Entities.Common;

namespace Entities.Administration
{
    [Serializable]
    public class User:Person
    {
       
        public string LoginName { get; set; }       
        public bool DeleteFlag { get; set; }
        public bool Active { get; set; }
        public override string ToString()
        {
            return String.Format("{0} {1} ({2})", FirstName, LastName, LoginName);
        }
        public virtual Employee Employee { get; set; }
    }
     [Serializable]
    public class Employee:Person
    {

        public int DesignationId { get; set; }
        public string Designation { get; set; }
        public bool Active { get; set; }
    }
}
