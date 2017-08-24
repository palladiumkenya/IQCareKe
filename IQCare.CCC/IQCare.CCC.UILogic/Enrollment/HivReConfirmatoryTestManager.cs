using System;
using Application.Presentation;
using Entities.CCC.Enrollment;
using Interface.CCC.Enrollment;

namespace IQCare.CCC.UILogic.Enrollment
{
    public class HivReConfirmatoryTestManager
    {
        private IHivReConfirmatoryTestManager mgr =
            (IHivReConfirmatoryTestManager) ObjectFactory.CreateInstance(
                "BusinessProcess.CCC.Enrollment.BHivReConfirmatoryTest, BusinessProcess.CCC");

        public int AddHivReConfirmatoryTest(int personId, int typeOfTest, int testResult, string testResultDate, int userId)
        {
            int result = 0;
            try
            {
                HivReConfirmatoryTest hivReConfirmatoryTest = new HivReConfirmatoryTest()
                {
                    PersonId = personId,
                    TypeOfTest = typeOfTest,
                    TestResult = testResult,
                    TestResultDate = DateTime.Parse(testResultDate),
                    CreatedBy = userId
                };

                result = mgr.AddHivReConfirmatoryTest(hivReConfirmatoryTest);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return result;
        }

        public HivReConfirmatoryTest GetPersonLastestReConfirmatoryTest(int personId, int positiveResult)
        {
            try
            {
                return mgr.GetPersonLastestReConfirmatoryTest(personId, positiveResult);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
