using System;
using DataAccess.Base;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;
using DataAccess.CCC.Repository;
using DataAccess.CCC.Context;

namespace BusinessProcess.CCC.Baseline
{
    public class BINHProphylaxis : ProcessBase, IINHProphylaxisManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddINHProphylaxis(INHProphylaxis iNHProphylaxis)
        {
            _unitOfWork.INHProphylaxisRepository.Add(iNHProphylaxis);
            Result = _unitOfWork.Complete();
            return Result;
        }

        public int DeleteINHProphylaxis(int id)
        {
            throw new NotImplementedException();
        }

        public int UpdateINHProphylaxis(INHProphylaxis iNHProphylaxis)
        {
            throw new NotImplementedException();
        }
    }
}
