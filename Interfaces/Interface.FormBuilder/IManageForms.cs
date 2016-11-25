using System;
using System.Collections;
using System.Data;

namespace Interface.FormBuilder
{
    public interface IManageForms
    {

        DataSet GetFormDetail(string strFormStatus, string strTechArea, Int32 CountryId);
        DataSet GetFormDetail(string strFormStatus, Int32 CountryId);
        DataSet CheckFormDetail(string strFormName, Int32 iFormID);
        void DeleteFormTableDetail(string strFormName, Int32 iFormId);
        int ResetFormStatus(Int32 iFormId, string strValue, Int32 iUserID);
        DataSet GetPublishedModuleList();
       

    }
}
