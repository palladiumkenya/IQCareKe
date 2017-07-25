using DataAccess.Base;
using Entities.CCC.Enrollment;
using Interface.CCC.Enrollment;
using System;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;

namespace BusinessProcess.CCC.Enrollment
{
    public class BHivReConfirmatoryTest : ProcessBase, IHivReConfirmatoryTestManager
    {
        public int AddHivReConfirmatoryTest(HivReConfirmatoryTest hivReConfirmatoryTest)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.HivReConfirmatoryTestRepository.Add(hivReConfirmatoryTest);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return hivReConfirmatoryTest.Id;
            }
        }
    }
}
