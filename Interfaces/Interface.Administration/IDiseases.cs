using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace Interface.Administration
{
    public interface IDiseases
    {
        DataSet GetDiseases();
        DataSet GetDiseasesByID(int diseaseid);
        DataSet DeleteDisease(int diseaseid);
        int SaveNewDisease(string DiseaseName, int UserID,  int Sequence);
        int UpdateDisease(int Disease_Pk, string DiseaseName, int UserID, int DeleteFlag, int Sequence);
    }
 }
