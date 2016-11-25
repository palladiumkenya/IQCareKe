using System;

namespace Entities.Common
{
     [Serializable]
    public class Filter:IFilter
    {
         bool _deleted = false;
         public int LocationId { get; set; }
         public int? PatientId { get; set; }
         public virtual Boolean DeleteFlag { get
         {
             return _deleted;
         } set{ _deleted=value;
         } }
         public virtual string Name { get; set; }
         public virtual string ReferenceNumber { get; set; }
         public virtual DateTime? DateFrom { get; set; }
         public virtual DateTime? DateTo { get; set; }
    }
     public interface IFilter
     {
         int LocationId { get; set; }
         int? PatientId { get; set; }
         Boolean DeleteFlag { get; set; }
         string Name { get; set; }
         string ReferenceNumber { get; set; }
         DateTime? DateFrom { get; set; }
         DateTime? DateTo { get; set; }
     }
     [Serializable]
     public  class BusinessRule : IBusinessRule
     {
         public  bool DeleteFlag
         {
             get;
             set;
         }        
         public int RuleId
         {
             get;
             set;
         }
         public string ReferenceId
         {
             get;
             set;
         }

         public string RuleName
         {
             get;
             set;
         }
         



     }
     public interface IBusinessRule
     {
         int RuleId { get; set; }
         string RuleName { get; set; }
         string ReferenceId { get; set; }
         Boolean DeleteFlag { get; set; }
     }
}
