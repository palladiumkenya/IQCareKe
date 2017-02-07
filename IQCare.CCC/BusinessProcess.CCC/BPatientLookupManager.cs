using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.CCC.Repository;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using Application.Common;
using DataAccess.CCC.Context;

namespace BusinessProcess.CCC
{
    public class BPatientLookupManager : IPatientLookupmanager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext());
        private readonly Utility _utility = new Utility();

        public List<PatientLookup> GetPatientDetailsLookup(int id)
        {
            var patientDetails = _unitOfWork.PatientLookupRepository
                .FindBy(x => x.Id == id || x.PtnPk == id & !x.DeleteFlag)
                .Take(1).ToList();

            return patientDetails;
        }

        public List<PatientLookup> SearchPatient()
        {
            throw new NotImplementedException();
        }
    }
}
