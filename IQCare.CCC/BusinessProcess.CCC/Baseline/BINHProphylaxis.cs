using System;
using DataAccess.Base;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;
using DataAccess.CCC.Repository;
using DataAccess.CCC.Context;
using System.Collections.Generic;
using System.Linq;

namespace BusinessProcess.CCC.Baseline
{
    public class BINHProphylaxis : ProcessBase, IINHProphylaxisManager
    {
      //  private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddINHProphylaxis(INHProphylaxis iNHProphylaxis)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.INHProphylaxisRepository.Add(iNHProphylaxis);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public int DeleteINHProphylaxis(int id)
        {
            throw new NotImplementedException();
        }

        public int UpdateINHProphylaxis(INHProphylaxis iNHProphylaxis)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.INHProphylaxisRepository.Update(iNHProphylaxis);
                Result= _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public List<INHProphylaxis> GetPatientProphylaxes(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
               var inhList= _unitOfWork.INHProphylaxisRepository.FindBy(x => x.PatientId == patientId && x.DeleteFlag == false)
                       .ToList();
                _unitOfWork.Dispose();
                return inhList;
            }
        }
    }
}
