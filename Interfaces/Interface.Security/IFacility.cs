using System;
using System.Data;
using System.Data.SqlClient;

namespace Interface.Security
{
    public interface IFacility
    {
        DataSet GetFacilityStats(int LocationId);
        DataSet GetHIVCareFacilityStats(int LocationId);
        DataSet GetFacilityStatsPMTCT(int LocationId);
        DataSet GetFacilityStatsExposedInfants(int LocationId);
        DataSet GetFacilityData(DateTime RangeFrom, DateTime RangeTo, int LocationId);
        DataSet GetExportData(string theStr);
    }
}
