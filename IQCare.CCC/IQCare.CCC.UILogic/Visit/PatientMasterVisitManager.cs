using Application.Presentation;
using Entities.CCC.Visit;
using Interface.CCC.Visit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.CCC.UILogic.Visit
{
    public class PatientMasterVisitManager
    {
        IPatientMasterVisitManager _mgr = (IPatientMasterVisitManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.visit.BPatientmasterVisit, BusinessProcess.CCC");

        public int addMasterVisit(PatientMasterVisit patientMasterVisit)
        {
            int returnValue;
            try
            {
                returnValue = _mgr.AddPatientmasterVisit(patientMasterVisit);
                return returnValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
