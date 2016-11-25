using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Interface.Administration;


namespace Interface.Administration
{
    public interface IEducation
    {
        DataSet  GetEducation();

        DataSet GetEducationByID(int EducationId);

        int SaveNewEducation( int UserID ,string EducationName , int Sequence);

        int UpdateEducation(int EducationID, int UserID, string EducationName, int DeleteFlag, int Sequence);

        DataSet DeleteEducation(int EducationId);

    }


}
