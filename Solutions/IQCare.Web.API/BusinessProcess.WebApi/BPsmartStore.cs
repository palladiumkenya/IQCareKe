using System;
using DataAccess.Base;
using DataAccess.WebApi;
using DataAccess.WebApi.Repository;
using Entity.WebApi.PSmart;
using Interface.WebApi;

namespace BusinessProcess.WebApi
{
    public class BPsmartStore : ProcessBase, IPsmartStoreManager
    {
        private int _result;

        public BPsmartStore()
        {
            _result = 0;
        }

        public int SaveShr(Psmart_Store psmartStore)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    unitOfWork.PSmartStoreRepository.Add(psmartStore);
                    _result = unitOfWork.Complete();
                    unitOfWork.Dispose();
                    return _result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }
    }
}