using Entities.Common;
using Interface.CCC;
using Application.Presentation;
using Application.Common;

namespace IQCare.CCC.UILogic
{
    public class PersonManager
    {
        Utility util = new Utility();

        public int AddPersonUiLogic(string firstName, string midName, string lastName,int gender,string nationalId, int userId)
        {
            int retval;

           Person p = new Person()
            {
                FirstName = util.Encrypt(firstName),
                MidName = util.Encrypt(midName),
                LastName = util.Encrypt(lastName),
                Sex = gender,
                NationalId = util.Encrypt(nationalId),
                CreatedBy = userId

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
            p.FirstName = util.Decrypt(p.FirstName);
            return p;
        }
    }
}
