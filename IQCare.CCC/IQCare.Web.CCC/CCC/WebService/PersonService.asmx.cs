using System.Web.Services;
using DataAccess.CCC.Repository.person;
using Entities.Common;
using IQCare.CCC.UILogic;

namespace IQCare.Web.CCC.WebService
{
    /// <summary>
    /// Summary description for PersonSeervice
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PersonSeervice : System.Web.Services.WebService
    {
        private int _personId;
        private string msg;

        [WebMethod]
        public void AddPerson(string fname, string mname, string lname, int gender, string natId)
        {
           var  personLogic=new PersonManager();

            _personId = personLogic.AddPersonUiLogic(fname, mname, lname, gender, natId);
            if (_personId > 0)
            {
                msg = "New Person Added Successfully!";
            }
        }

        [WebMethod]
        public void AddPersonContact(PersonContact c)
        {
            
        }

        [WebMethod]
        public void AddPersonRelationship(PersonRelationshipRepository r)
        {
            
        }

        [WebMethod]
        public void AddPersonLocation(PersonLocation l)
        {
            
        }
    }
}
