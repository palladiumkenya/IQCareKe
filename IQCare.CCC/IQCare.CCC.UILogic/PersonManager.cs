using System;
using System.Globalization;
using Entities.Common;
using Application.Presentation;
using Application.Common;
using Interface.CCC;

namespace IQCare.CCC.UILogic
{
    public class PersonManager
    {
        Utility util = new Utility();
        readonly TextInfo _textInfo = new CultureInfo("en-US", false).TextInfo;
        IPersonManager _mgr =  (IPersonManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPersonManager, BusinessProcess.CCC");

       /* public int AddPersonUiLogic(string firstName, string midName, string lastName, int gender, int userId)
        {
            int retval;

            try
            {
                //disable double encryption
                Person p = new Person()
                {
                    FirstName = (_textInfo.ToTitleCase(firstName)),
                    MidName =(_textInfo.ToTitleCase(midName)),
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

               throw new Exception(exception .Message);
            }

            return retval;
        }
        */
        public int AddPersonUiLogic(string firstName, string midName, string lastName, int gender, int userId, DateTime? dateOfBirth = null, bool? dobPrecision=null)
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

        public int AddPersonTreatmentSupporterUiLogic(string firstName, string midName, string lastName, int gender, int userId)
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

            _mgr.UpdatePerson(person,id);
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


        public void DeletePerson(int id)
        {
            IPersonManager mgr = (IPersonManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPersonManager, BusinessProcess.CCC");
            mgr.DeletePerson(id);  
        }

        public Person GetPerson(int id)
        {
            IPersonManager mgr = (IPersonManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPersonManager, BusinessProcess.CCC");
            Person p = mgr.GetPerson(id);
            p.Id = p.Id;
            p.FirstName = (p.FirstName);
            p.MidName = (p.MidName);
            p.LastName =(p.LastName);
            //p.NationalId = util.Decrypt(p.NationalId);
            return p;
        }
    }
}
