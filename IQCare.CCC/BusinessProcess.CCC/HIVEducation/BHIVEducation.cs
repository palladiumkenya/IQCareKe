using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.CCC.HIVEducation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC.HIVEducation;
using DataAccess.CCC.Interface.HIVEducation;
namespace BusinessProcess.CCC.HIVEducation

{
    public class BHIVEducation : ProcessBase, IHIVEducation
    {
       // private int result;

        public int AddPatientHIVEducation(HIVEducationFollowup HEF)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientHIVEducationFollowupRepository.Add(HEF);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return HEF.Id;
            }
        }

        public DataTable getCounsellingTopics(string counsellingtopics)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject(); // Entity
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@regimenLine", SqlDbType.Int, counsellingtopics);

                return (DataTable)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getPharmacyRegimens", ClsUtility.ObjectEnum.DataTable);

            }
        }

        public int UpdatePatientHIVEducation(HIVEducationFollowup HEF)
        {
            throw new NotImplementedException();
        }
    }
}
