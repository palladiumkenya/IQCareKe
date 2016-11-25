using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Interface.SCM
{
    public interface ISCMReport
    {
        DataTable GetExperyReport(int StoreId, DateTime TransDate, DateTime ExpiryDate);
        DataSet GetStockSummary(int StoreId, int ItemId, DateTime FromDate, DateTime ToDate);
        DataSet GetBatchSummary(int StoreId, int ItemId, DateTime FromDate, DateTime ToDate);
        DataSet GetStockLedgerData(int StoreId, DateTime FromDate, DateTime ToDate);
        DataSet GetBINCard(int StoreId, int ItemId, DateTime FromDate, DateTime ToDate, int LocationId);
    }
}
