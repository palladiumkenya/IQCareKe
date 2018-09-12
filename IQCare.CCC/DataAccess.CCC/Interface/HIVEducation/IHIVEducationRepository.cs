using DataAccess.Context;
using Entities.CCC.HIVEducation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CCC.Interface.HIVEducation
{
    public interface IHIVEducationRepository : IRepository<HIVEducationFollowup>
    {
        List<HIVEducationFollowup> getPatientHIVEducation(int patientId);
        List<HIVEducationFollowup> getPatientHIVEducationByTopic(int patientId, int topic);
        int updatePatientClinicalNotes(HIVEducationFollowup HEF);
    }
}
