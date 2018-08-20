using Application.Presentation;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.CCC.UILogic.Baseline
{
    public class PatientHivTestingManager
    {
        private readonly IPatientHivTestingManager _mgr = (IPatientHivTestingManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Baseline.BPatientHivTestingManager, BusinessProcess.CCC");


        public PatientHivTesting GetPatientHivTesting(int PersonId)
        {
            var hivTesting = _mgr.GetAll().OrderByDescending(y => y.Id);
            PatientHivTesting pht = hivTesting.Where(n => n.PersonId == PersonId).FirstOrDefault();
            return pht;
        }
    }
}
