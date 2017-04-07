using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Application.Common;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using DataAccess.Common;
using DataAccess.Entity;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

namespace BusinessProcess.CCC
{
    public class BPersonLookUpManager : ProcessBase, IPersonLookUpManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext());
        Utility _utility = new Utility();

        public List<PersonLookUp> GetPatientSearchresults(string firstName, string middleName, string lastName, string dob)
        {
            var results = _unitOfWork.PersonLookUpRepository.GetAll();
            //List<PersonLookUp> filteredList = new List<PersonLookUp>();
            //DateTime dobDateTime = DateTime.Parse(dob);

            results = results.Where(x => _utility.Decrypt(x.FirstName).ToLower().Contains(firstName.ToLower())).ToList();

            results = results.Where(x => _utility.Decrypt(x.MiddleName).ToLower().Contains(middleName.ToLower())).ToList();

            results = results.Where(x => _utility.Decrypt(x.LastName).ToLower().Contains(lastName.ToLower())).ToList();

            //foreach (var item in results)
            //{
            //    var dobResults = _unitOfWork.PatientLookupRepository.FindBy(
            //        y => y.PersonId == item.Id && y.DateOfBirth == dobDateTime).ToList();

            //    if (dobResults.Count > 0)
            //    {
            //        filteredList.Add(item);
            //    }
            //}

            return results.ToList();
        }

        public PersonLookUp GetPersonById(int id)
        {
            return _unitOfWork.PersonLookUpRepository.GetById(id);
        }
    }
}
