using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Interface.FormBuilder
{
    public interface IImportExportForms
    {
        //DataSet GetAllFormDetail(string strFormStatus, string strTechArea, Int32 CountryId);
        DataSet GetAllFormDetail(string strFormStatus, string strTechArea, Int32 CountryId, string frmFormType);
        DataSet GetImportExportFormDetail(String strFeatureName);
        int ImportForms(DataSet dsImportForms, int iUserId, int iCountryId);
        DataSet GetImportExportHomeFormDetail(String strFeatureName);
        int ImportHomeForms(DataSet dsImportForms, int iUserId, int iCountryId);
        
        //////////DataSet ReturnQueryResult(string theQuery);
        //////////void InsertBulk(DataTable theDT);
        //////////int ImportDatabase();
    }
}
