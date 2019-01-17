using Application.Presentation;
using BusinessProcess.CCC.Lookup;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.CCC.UILogic
{
   public  class PatientLookupLabManager
    {
         IPatientLookupLabManager _patientLookuplabmanager = (IPatientLookupLabManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Lookup.BPatientLookupLabManager, BusinessProcess.CCC");
        public List<PatientLab> GetPatientLabs(int patientId)
        {
            return _patientLookuplabmanager.GetPatientLabs(patientId);
        }

    }
}
