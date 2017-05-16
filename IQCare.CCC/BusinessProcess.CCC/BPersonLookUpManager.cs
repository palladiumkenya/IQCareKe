using System.Collections.Generic;
using System.Data;
using System.Linq;
using Application.Common;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

namespace BusinessProcess.CCC
{
    public class BPersonLookUpManager : ProcessBase, IPersonLookUpManager
    {
       // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext());
        Utility _utility = new Utility();

        public List<PersonLookUp> GetPatientSearchresults(string firstName, string middleName, string lastName, string dob)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var results = _unitOfWork.PersonLookUpRepository.GetAll();
                //List<PersonLookUp> filteredList = new List<PersonLookUp>();
                //DateTime dobDateTime = DateTime.Parse(dob);

                results =
                    results.Where(x => x.FirstName.ToLower().Contains(firstName.ToLower()));//.ToList();

                if(!System.String.IsNullOrEmpty(middleName))
                results =
                    results.Where(x => x.MiddleName.ToLower().Contains(middleName.ToLower()));//.ToList();

                results =
                    results.Where(x => x.LastName.ToLower().Contains(lastName.ToLower()));//.ToList();

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

        }

        public PersonLookUp GetPersonById(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var personLookupList = _unitOfWork.PersonLookUpRepository.GetById(id);
                _unitOfWork.Dispose();
                    return personLookupList;
            }
        }
    }
}
