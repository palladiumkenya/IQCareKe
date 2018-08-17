using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Presentation;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;

namespace IQCare.CCC.UILogic.Baseline
{
    public class PatientLinkageManager
    {
        private IPatientLinkageManager _mgr = (IPatientLinkageManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Baseline.BPatientLinkageManager, BusinessProcess.CCC");

        public bool CccNumberExists(string cccNumber)
        {
            return _mgr.CccNumberExists(cccNumber);
        }


        public PatientLinkage GetPatientLinkage(int personId)
        {

            return _mgr.GetPatientLinkage(personId).FirstOrDefault();
        }
    }
}
