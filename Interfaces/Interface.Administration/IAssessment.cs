using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace Interface.Administration
{
    public interface IAssessment
    {
        DataSet GetAssessmentList();
        DataSet GetAssessmentByIDList(int Assessment);
        DataSet DeleteAssessment(int AssessmentID);
        int SaveNewAssessment(int AssessmentCategoryID, string AssessmentName, int UserId);
        int UpdateAssessment(int AssessmentId, int AssessmentCategoryID, string AssessmentName, int UserId);
        DataSet GetAssessmentTypeList();
        DataSet DeleteAssessmentType(int AssessmentCategoryID);
        DataSet GetAssessmentTypeByIDList(int AssessmentTypeID);
        int SaveNewAssessmentType( string AssessmentCategoryName, int UserId);
        int UpdateAssessmentType( int AssessmentCategoryID, string AssessmentCategoryName, int UserId);
    }
}
