using System;
using DataAccess.Base;
using DataAccess.WebApi;
using DataAccess.WebApi.Repository;
using Entities.CCC.PSmart;
using Interface.WebApi;

namespace BusinessProcess.WebApi
{
    public class BFamilyInfoManager : ProcessBase, IFamilyInfoManager
    {
        private int _results;

        public int AddMotherDetails(FamilyInfo familyInfo)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    unitOfWork.FamilyInfoRepository.Add(familyInfo);
                    _results = unitOfWork.Complete();
                    unitOfWork.Dispose();
                    return _results;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public int EditMotherDetails(FamilyInfo familyInfo)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
            {
                unitOfWork.FamilyInfoRepository.Update(familyInfo);
                _results = unitOfWork.Complete();
                unitOfWork.Dispose();
                return _results;
            }
        }
    }
}