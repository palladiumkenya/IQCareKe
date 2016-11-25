using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    public interface IFamilyInfo
    {
        int SaveFamilyInfo(int Id, int Ptn_Pk, string RFirstName, string RLastName, int Sex, int AgeYear, int AgeMonth, int RelationshipType, int HivStatus, int HivCareStatus, int UserId, int DeleteFlag, int ReferenceId, string RegistrationNo, DateTime RelationshipDate);
        DataSet GetAllFamilyData(int PatientId);
        DataSet GetSearchFamilyInfo(int PatientId);
        int DeleteFamilyInfo(int Id, int @UserId);
        DataSet GetDropDowns();
    }
}
