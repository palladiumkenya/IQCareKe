using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;
using Application.Presentation;
using Interface.Lookup;

namespace IQCare.Web.WebService
{
    /// <summary>
    /// Summary description for IQLookupWS
    /// </summary>
    /// <seealso cref="System.Web.Services.WebService" />
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [System.Web.Script.Services.ScriptService]
    public class IQLookupWS : System.Web.Services.WebService
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="IQLookupWS"/> class.
        /// </summary>
        public IQLookupWS()
        {

        }
        /// <summary>
        /// </summary>
        private ILookupService lkMgr = (ILookupService)ObjectFactory.CreateInstance("BusinessProcess.Lookup.BLookup, BusinessProcess.Lookup");

        /// <summary>
        /// Gets the lookup value.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        /// <param name="lookupname">The lookupname.</param>
        /// <param name="lookupCategory">The lookup category.</param>
        /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetLookupValue(string prefix, string lookupname, string lookupCategory) 
    {
        List<string> lookups = new List<string>();
           var  items =      lkMgr.GetLookupItems(prefix,lookupname,lookupCategory);
           {
                 if(items != null){
                     items.ForEach(it=>
                     {  lookups.Add(string.Format("{0}:{1}", it.Name, it.Id));
                       // lookups.Add(string.Format("id:{0}, name:{1},lookupid:{2}, lookupname:{3},category{4}",it.Id,it.Name,it.LookupId,it.LookupName,it.Category));
                     })  ;
                 }
           }
      
            return lookups.ToArray();
        }
    }
}

