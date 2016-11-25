using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Interface.FormBuilder
{
   public interface IRptFieldValidations
    {
       DataSet GetRptFieldDetails();
       DataTable ReturnDatatableQueryResult(string theQuery);
       DataTable ParseXml(string stringxml, int FacilityId, int ModuleId);
    }
}
