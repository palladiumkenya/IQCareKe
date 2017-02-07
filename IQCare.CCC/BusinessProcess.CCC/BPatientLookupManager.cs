using System;
using System.Collections.Generic;
using DataAccess.CCC.Repository;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using Application.Common;
using DataAccess.CCC.Context;

namespace BusinessProcess.CCC
{
    public class BPatientLookupManager:IPatientLookupmanager
    {
        private readonly UnitOfWork _unitOfWork=new UnitOfWork(new LookupContext());
        private Utility _utility=new Utility();

        public List<PatientLookup> GetPatientDetailsLookup(int id)
        {
            
            //IEnumerable<PatientLookup> patientDetails = _unitOfWork.PatientLookupRepository
            //    .FindBy(x => x.Id == id || x.PtnPk == id || !x.DeleteFlag)
            //    .Select(p=>  
            //    {
            //        //p.Id,(p.FirstName
            //    })

            //return (List<PatientLookup>) patientDetails;

            throw new NotImplementedException();

        }

        public List<PatientLookup> SearchPatient()
        {
            throw new NotImplementedException();
        }
    }
}
