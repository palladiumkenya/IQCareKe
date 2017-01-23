using System;
using System.Globalization;
using System.Web;
using System.Web.ModelBinding;
using Entities.Common;
using Interface.CCC;
using Application.Presentation;
using Application.Common;

namespace IQCare.CCC.UILogic
{
    public class PersonManager
    {
        Utility util = new Utility();
        readonly TextInfo _textInfo = new CultureInfo("en-US", false).TextInfo;

        public int AddPersonUiLogic(string firstName, string midName, string lastName, int gender, string nationalId,
            int userId)
        {
            int retval;

            try
            {
                Person p = new Person()
                {
                    FirstName = util.Encrypt(_textInfo.ToTitleCase(firstName)),
                    MidName = util.Encrypt(_textInfo.ToTitleCase(midName)),
                    LastName = util.Encrypt(_textInfo.ToTitleCase(lastName)),
                    Sex = gender,
                    NationalId = util.Encrypt(nationalId),
                    CreatedBy = userId

                };

                IPersonManager mgr =
                    (IPersonManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPersonManager, BusinessProcess.CCC");
                retval = mgr.AddPerson(p);
                HttpContext.Current.Session["PersonId"] = p.Id;
            }
            catch (Exception exception)
            {
                    
               throw new Exception(exception.Message);
            }

            return retval;
        }

        public void UpdatePerson(Person p)
        {
            IPersonManager mgr = (IPersonManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPersonManager, BusinessProcess.CCC");
            mgr.UpdatePerson(p);
        }

        public void DeletePerson(int id)
        {
            IPersonManager mgr = (IPersonManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPersonManager, BusinessProcess.CCC");
            mgr.DeletePerson(id);  
        }

        public Person GetPerson(int id)
        {
            IPersonManager mgr = (IPersonManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPersonManager, BusinessProcess.CCC");
            Person p = mgr.GetPerson(id);
            p.FirstName = util.Decrypt(p.FirstName);
            return p;
        }
    }
}
