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
    public class BPersonLocationmanager : ProcessBase,IPersonLocationManager
    {
        private UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext());
        private int _result;

        public int AddPersonLocation(PersonLocation location)
        {
            try
            {
                _unitOfWork.PersonLocationRepository.Add(location);
                _result = _unitOfWork.Complete();
                return _result;
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

            // return location.Id;
        }

        public int DeletePersonLocation(int id)
        {
            try
            {
                PersonLocation location = _unitOfWork.PersonLocationRepository.GetById(id);
                _unitOfWork.PersonLocationRepository.Remove(location);
                _result = _unitOfWork.Complete();
                return _result;
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

        public List<PersonLocation> GetCurrentPersonLocation(int persoId)
        {
            try
            {
                return
                    _unitOfWork.PersonLocationRepository.FindBy(x => x.PersonId == persoId && x.DeleteFlag == false)
                        .ToList();

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
                /*personLocation.FindBy(x => x.PersonId == personId & x.DeleteFlag == false)
                .OrderBy(x => x.Id)
                .FirstOrDefault();
            var myList = _unitOfWork.PersonLocationRepository.GetPersonCurrentLocation(persoId);
            return myList;*/
        }

        public List<PersonLocation> GetPersonLocationAll(int personId)
        {
            try
            {
                var mylist = _unitOfWork.PersonLocationRepository.FindBy(x => x.PersonId == personId);
                return mylist.ToList();
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

        public  int UpdatePersonLocation(PersonLocation location)
        {
            try
            {
                _unitOfWork.PersonLocationRepository.Update(location);
                _result = _unitOfWork.Complete();
                return _result;
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
