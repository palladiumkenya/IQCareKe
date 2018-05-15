using Application.Common;
using Application.Presentation;
using Entities.Common;
using Interface.Records;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.Records.UILogic
{
    public class PersonManager
    {
        Utility util = new Utility();
        readonly TextInfo _textInfo = new CultureInfo("en-US", false).TextInfo;
        IPersonManager _mgr = (IPersonManager)ObjectFactory.CreateInstance("BusinessProcess.Records.BPersonManager, BusinessProcess.Records");

        public int AddPersonUiLogic(string firstName, string midName, string lastName, int gender, int userId)
        {
            int retval;

            try
            {
                //disable double encryption
                Person p = new Person()
                {
                    FirstName = (_textInfo.ToTitleCase(firstName)),
                    MidName = (_textInfo.ToTitleCase(midName)),
                    LastName = (_textInfo.ToTitleCase(lastName)),
                    Sex = gender,
                    //DateOfBirth = dateOfBirth,
                    //NationalId = util.Encrypt(nationalId),
                    CreatedBy = userId
                };
                retval = _mgr.AddPerson(p);
                //HttpContext.Current.Session["PersonId"] = p.Id;
            }
            catch (Exception exception)
            {

                throw new Exception(exception.Message);
            }

            return retval;
        }

        public int AddPersonEmergencySupporterUiLogic(string firstName, string midName, string lastName, int gender, int userId)
        {
            int retval;

            try
            {
                Person p = new Person()
                {
                    FirstName = (_textInfo.ToTitleCase(firstName)),
                    MidName = (_textInfo.ToTitleCase(midName)),
                    LastName = (_textInfo.ToTitleCase(lastName)),
                    Sex = gender,
                    //NationalId = util.Encrypt(nationalId),
                    CreatedBy = userId,
                    //DateOfBirth = DateTime.Now
                };
                retval = _mgr.AddPerson(p);
                //HttpContext.Current.Session["PersonId"] = p.Id;
            }
            catch (Exception exception)
            {

                throw new Exception(exception.Message);
            }

            return retval;
        }
        public int AddPersonUiLogic(string firstName, string midName, string lastName, int gender, int userId, DateTime dateOfBirth, bool dobPrecision)
        {
            int retval;

            try
            {
                //disable double encryption
                Person p = new Person()
                {
                    FirstName = (_textInfo.ToTitleCase(firstName)),
                    MidName = (_textInfo.ToTitleCase(midName)),
                    LastName = (_textInfo.ToTitleCase(lastName)),
                    Sex = gender,
                    DateOfBirth = dateOfBirth,
                    DobPrecision = dobPrecision,
                    //NationalId = util.Encrypt(nationalId),
                    CreatedBy = userId
                };
                retval = _mgr.AddPerson(p);
                //HttpContext.Current.Session["PersonId"] = p.Id;
            }
            catch (Exception exception)
            {

                throw new Exception(exception.Message);
            }

            return retval;
        }
        public void UpdatePerson(string firstname, string middlename, string lastname, int gender, int userId, int id, DateTime dateOfBirth, bool dobPrecision)
        {
            Person person = new Person()
            {
                FirstName = (_textInfo.ToTitleCase(firstname)),
                MidName = (_textInfo.ToTitleCase(middlename)),
                LastName = (_textInfo.ToTitleCase(lastname)),
                Sex = gender,
                DateOfBirth = dateOfBirth,
                DobPrecision = dobPrecision,
                //NationalId = util.Encrypt(nationalId),
                CreatedBy = userId
            };

            _mgr.UpdatePerson(person, id);
        }

        public void UpdatePerson(string firstname, string middlename, string lastname, int gender, int userId, int id)
        {
            Person person = new Person()
            {
                FirstName = (_textInfo.ToTitleCase(firstname)),
                MidName = (_textInfo.ToTitleCase(middlename)),
                LastName = (_textInfo.ToTitleCase(lastname)),
                Sex = gender,
                //DateOfBirth = DateTime.Parse(dateOfBirth),
                //NationalId = util.Encrypt(nationalId),
                CreatedBy = userId
            };

            _mgr.UpdatePerson(person, id);
        }
        public void DeletePerson(int id)
        {
            IPersonManager mgr = (IPersonManager)ObjectFactory.CreateInstance("BusinessProcess.Records.BPersonManager, BusinessProcess.Records");
            mgr.DeletePerson(id);
        }

        public Person GetPerson(int id)
        {
            IPersonManager mgr = (IPersonManager)ObjectFactory.CreateInstance("BusinessProcess.Records.BPersonManager, BusinessProcess.Records");
            Person p = mgr.GetPerson(id);
            p.Id = p.Id;
            p.FirstName = (p.FirstName);
            p.MidName = (p.MidName);
            p.LastName = (p.LastName);
            //p.NationalId = util.Decrypt(p.NationalId);
            return p;
        }
    }

}

