using Entities.CCC.HIVEducation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.CCC.HIVEducation
{
    public interface IHIVEducation
    {
        int AddPatientHIVEducation(HIVEducationFollowup HEF);
        int UpdatePatientHIVEducation(HIVEducationFollowup HEF);
        DataTable getCounsellingTopics(string counsellingtopics);
        //int AddPatientHIVEducation(int patientId, DateTime visitdate, int councellingTypeId, string councellingType, int councellingTopicId, string councellingTopic, string comments, string other);
    }
}
