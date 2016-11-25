using System;
using System.Data;
using System.Collections;

namespace Interface.FormBuilder
{
    public interface IViewAssociation
    {
        DataSet GetViewAssociationFields(string FieldName, int ModuleId);
        DataSet GetMoudleName();
    }
}
