using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Administration
{
    public interface IDrugMst
    {
        DataSet GetDrug();
        DataSet GetDrugMst();
        DataSet DeleteDrug(int Drug_ID);
        DataTable GetExistDrug(string DrugName);
        DataSet GetGenericDrug(int DrugId, int GenericId, int DrugTypeId);
        
        DataTable GetGeneric(int DrugTypeID);
        DataSet GetGenericByID(int GenericId);
        DataTable GetExistGeneric(string DrugType);
        DataSet GetDrugTypes();
       
        DataSet GetAllDropDowns();
       
        DataSet GetDrugStrength();
      
        DataSet GetExistDrugDetail(int Drug_pk, string DrugType, string Generic );
        DataSet GetStrengthLists(int DrugId);

        int SaveUpdateDrugDetails(int Drug_Pk, string DrugName, string DrugAbbreviation, int Status, DataTable ExistGeneric, DataTable Generics, decimal MaxDose, decimal MinDose, int DrugTypeID, int UserID, DataTable Strength, DataTable Frequency, DataTable Schedule,int Update);
        DataTable CreateStrength(string StrengthName, int UserId);
        DataTable CreateFrequency(string FrequencyName, int UserId);
        DataTable CreateSchedule(string ScheduleName, int UserId);
        DataSet CreateGeneric(string GenericName,string GenericAbbrivation,int DrugTypeId, int UserId);
        DataTable GetStrengthByGenericID(int GenericId);
        DataTable GetFrequencyByGenericID(int GenericId);
        DataTable GetScheduleByDrugID(int DrugId);
        DataTable GetDrugsForGenericID(int GenericId);
        int InActivateGeneric(int GenericId);
        int ActivateGeneric(int GenericId);
        int SaveUpdateRegimenGeneric(string RegimenName,string RegimenCode,int RegimenID, string Stage, int Status, string GenericID, int UserID,int SRNo, int flag);
        int SaveUpdateTBRegimenGeneric(string RegimenName, int RegimenID, int TreatmentTime, int Status, string GenericID, int UserID, int SRNo, int flag);
        DataSet GetRegimenGeneric(int RegimenID);
        DataSet GetTBRegimenGeneric(int RegimenID);
        DataSet GetAllRegimenGeneric();
        DataSet GetAllTBRegimenGeneric();
        DataSet GetRegimenName(string RegimenName);
        DataSet GetTBRegimenName(string RegimenName);
    }
}
