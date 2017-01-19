using System;
using System.Text;
using Entities.Common;
using Interface.CCC;
using Application.Presentation;
using Application.Common;

namespace IQCare.CCC.UILogic
{
    public class PersonManager
    {
        public int AddPersonUiLogic(string fname, string mname, string lname,int gender,int natId)
        {
            int retval;

            Utility x = new Utility();

            Person p = new Person()
            {
                FirstName = x.Encrypt(fname),
                MidName = x.Encrypt(mname),
                LastName = x.Encrypt(lname),
                Sex = gender,
                NationalId = natId
            };

            IPersonManager mgr =
                (IPersonManager) ObjectFactory.CreateInstance("BusinessProcess.CCC.BPersonManager, BusinessProcess.CCC");
            retval = mgr.AddPerson(p);

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
            return p;
        }
    }
}
