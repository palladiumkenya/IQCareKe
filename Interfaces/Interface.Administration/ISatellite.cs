using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Administration
{
    public interface ISatellite
    {
        DataTable GetSatellite(int ID);
        DataTable GetSatelliteByID_Name(string SatID, string SatName);
        DataSet GetSatelliteByID_Edit(string ID);
        DataTable GetAllSatellite();
        int SaveUpdateSatellite(String ID, String SatelliteID, String SatelliteName, string status, int priority, int Flag, int UserID, String Createdate);
    }
}
