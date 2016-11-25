using System;
using System.Data;
namespace Interface.Clinical
{
    public interface IFollowupEducation
    {
        int SaveFollowupEducation(int id, int patientId, int councellingTypeId, int councellingTopicId, int visitPk, int locationId, DateTime visitDate, string comments, string otherDetail, int userId, int deleteFlag,int? moduleId = null);  
        int DeleteFollowupEducation(int id, int patientId);
        DataSet GetSearchFollowupEducation(int patientId);
        DataSet GetCouncellingTopic(int councellingTypeId);
        DataSet GetCouncellingType();
        DataSet GetAllFollowupEducationData(int patientId);
    }
}
