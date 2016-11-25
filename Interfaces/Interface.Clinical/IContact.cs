using System;
using System.Data;
using System.Data.SqlClient;

namespace Interface.Clinical
{
    public interface IContact
    {
        DataSet GetTestNorthwindEmployees();
    }
}
