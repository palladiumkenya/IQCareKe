using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Application.Common;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface;
using DataAccess.CCC.Repository;
using DataAccess.Common;
using DataAccess.Entity;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

namespace BusinessProcess.CCC
{
    public class BPersonLookUpManager : ProcessBase, IPersonLookUpManager
    {
        //private readonly UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext());
        Utility _utility = new Utility();

        public List<PersonLookUp> GetPatientSearchresults(string firstName, string middleName, string lastName, string dob)
        {
            try
            {
                using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
                {
                    var results = _unitOfWork.PersonLookUpRepository.GetAll();
                    //List<PersonLookUp> filteredList = new List<PersonLookUp>();
                    //DateTime dobDateTime = DateTime.Parse(dob);

                    results =
                        results.Where(x => _utility.Decrypt(x.FirstName).ToLower().Contains(firstName.ToLower()))
                            .ToList();

                    results =
                        results.Where(x => _utility.Decrypt(x.MiddleName).ToLower().Contains(middleName.ToLower()))
                            .ToList();

                    results =
                        results.Where(x => _utility.Decrypt(x.LastName).ToLower().Contains(lastName.ToLower())).ToList();

                    return results.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public PersonLookUp GetPersonById(int id)
        {
            try
            {
                using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
                {
                    return _unitOfWork.PersonLookUpRepository.GetById(id);
                }                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }       
        }
    }
}
