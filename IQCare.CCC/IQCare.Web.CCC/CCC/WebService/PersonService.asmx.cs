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
    [System.Web.Script.Services.ScriptService]
    public class PersonSeervice : System.Web.Services.WebService
    {
        private int _personId;
        private string _msg;

        [WebMethod]
        public string AddPerson(string firstname, string middlename, string lastname, int gender, string nationalId, int userId)
        {
           var  personLogic=new PersonManager();

            _personId = personLogic.AddPersonUiLogic(firstname, middlename, lastname, gender, nationalId,userId);
            if (_personId > 0)
            {
                _msg = "New Person Added Successfully!";
            }

            return _msg;
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
