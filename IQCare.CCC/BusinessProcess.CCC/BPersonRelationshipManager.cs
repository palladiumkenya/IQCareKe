using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using DataAccess.Context;
using Entities.Common;
using Interface.CCC;

namespace BusinessProcess.CCC
{
    public class BPersonRelationshipManager :ProcessBase,IPersonRelationshipManager
    {
        //private readonly UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext());
        private int _result;

        public int AddPersonRelationship(PersonRelationship personRelationship)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PersonContext()))
            {
                try
                {
                    unitOfWork.PersonRelationshipRepository.Add(personRelationship);
                    return _result = unitOfWork.Complete();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    unitOfWork.Dispose();
                }
            }
        }

        public int DeletePersonRelationship(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PersonContext()))
            {
                try
                {
                    var personRelation = unitOfWork.PersonRelationshipRepository.GetById(id);
                    unitOfWork.PersonRelationshipRepository.Remove(personRelation);
                    return _result = unitOfWork.Complete();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    unitOfWork.Dispose();
                }
            }
        }

        public List<PersonRelationship> GetAllPersonRelationship(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PersonContext()))
            {
                try
                {
                    var myList =
                        unitOfWork.PersonRelationshipRepository.FindBy(
                            x => x.PatientId == patientId & x.DeleteFlag == false);
                    return myList.ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    unitOfWork.Dispose();
                }
            }
        }

        public PersonRelationship GetSpecificRelationship(int personId,int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PersonContext()))
            {
                try
                {
                    PersonRelationship pm = unitOfWork.PersonRelationshipRepository
                        .FindBy(x => x.PersonId == personId && x.PatientId == patientId).FirstOrDefault();
                    return pm;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    unitOfWork.Dispose();
                }
            }

        }
        public bool PersonLinkedToPatient(int personId, int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PersonContext()))
            {
                return unitOfWork.PersonRelationshipRepository
                    .FindBy(x => x.PersonId == personId && x.PatientId == patientId).Any();
            }
        }

        public int UpdatePersonRelationship(PersonRelationship personRelationship)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PersonContext()))
            {
                try
                {
                    unitOfWork.PersonRelationshipRepository.Update(personRelationship);
                    return _result = unitOfWork.Complete();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    unitOfWork.Dispose();
                }

            }
        }
    }
}
