using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Repository;
using DataAccess.Context;
using Entities.Common;
using Interface.CCC;

namespace BusinessProcess.CCC
{
    public class BPersonRelationshipManager :ProcessBase,IPersonRelationshipManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext());
        private int _result;

        public int AddPersonRelationship(PersonRelationship personRelationship)
        {
            try
            {
                _unitOfWork.PersonRelationshipRepository.Add(personRelationship);
                return _result = _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
          
        }

        public int DeletePersonRelationship(int id)
        {
            try
            {
                var personRelation = _unitOfWork.PersonRelationshipRepository.GetById(id);
                _unitOfWork.PersonRelationshipRepository.Remove(personRelation);
                return _result = _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
 
        }

        public List<PersonRelationship> GetAllPersonRelationship(int patientId)
        {
            try
            {
                var myList =
                    _unitOfWork.PersonRelationshipRepository.FindBy(
                        x => x.RelatedTo == patientId & x.DeleteFlag == false);
                return myList.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }

        }

        public int UpdatePersonRelationship(PersonRelationship personRelationship)
        {
            try
            {
                _unitOfWork.PersonRelationshipRepository.Update(personRelationship);
                return _result = _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
      
        }
    }
}
