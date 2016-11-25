using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Administration
{
    public interface IPassword
    {
        DataSet GetUserData(int userID);
        int UpdatePassword(int userID, string Password);
    }
}
