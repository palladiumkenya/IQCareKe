using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.CCC.Enrollment;

namespace Interface.CCC.Enrollment
{
    public interface IHivReConfirmatoryTestManager
    {
        int AddHivReConfirmatoryTest(HivReConfirmatoryTest hivReConfirmatoryTest);
        HivReConfirmatoryTest GetPersonLastestReConfirmatoryTest(int personId, int positiveResult);
    }
}
