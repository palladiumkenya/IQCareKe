using DataAccess.Base;
using Entities.CCC.Enrollment;
using Interface.CCC.Enrollment;
using System;
using System.Linq;
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

        public HivReConfirmatoryTest GetPersonLastestReConfirmatoryTest(int personId, int positiveResult)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                HivReConfirmatoryTest hivReConfirmatoryTest = unitOfWork.HivReConfirmatoryTestRepository.FindBy(x => x.PersonId == personId && x.TestResult == positiveResult)
                    .OrderByDescending(x => x.TestResultDate).FirstOrDefault();
                unitOfWork.Dispose();
                return hivReConfirmatoryTest;
            }
        }

        public HivReConfirmatoryTest GetPersonLastReConfirmatoryTest(int personId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                HivReConfirmatoryTest reConfirmatoryTest = unitOfWork.HivReConfirmatoryTestRepository
                    .FindBy(x => x.PersonId == personId && !x.DeleteFlag).FirstOrDefault();
                unitOfWork.Dispose();
                return reConfirmatoryTest;
            }
        }

        public int UpdateHivReConfirmatoryTest(HivReConfirmatoryTest hivReConfirmatoryTest)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.HivReConfirmatoryTestRepository.Update(hivReConfirmatoryTest);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return hivReConfirmatoryTest.Id;
            }
        }
    }
}
