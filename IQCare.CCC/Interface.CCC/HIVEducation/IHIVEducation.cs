using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC;

namespace Interface.CCC.HIVEducation
{
    public interface IHIVEducation
    {
        int AddPatientHIVEducation(int Id, int Ptn_Pk, int visitPk, int locationID, int? councellingTypeId = null, int? councellingTopicId = null, DateTime? visitDate = null, string comments = null, string otherDetail = null, int? userId = null, bool? deleteFlag = null, int? moduleId = null);
        int UpdatePatientHIVEducation(HIVEducationFollowup HEF);
        DataTable getCounsellingTopics(string counsellingtopics);
        DataTable GetPatientFollowupEducationData(int ptnPk);
    }
}
