using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Interface.SCM
{
    public interface IPurchase
    {
        int SavePurchaseOrder(DataTable DtMasterPO, DataTable dtPOItems, bool isUpdate);
        DataSet GetPurcaseOrderItem(int isPO, int UserID, int StoreID);
        DataSet GetPurchaseOrderDetailsByPoid(Int32 POId);
        DataTable GetPurchaseOrderDetails(Int32 UserID, Int32 DestinStoreID, Int32 locationID);
        DataTable GetPurchaseOrderDetailsForGRN(Int32 UserID, Int32 StoreID, Int32 locationID, bool IsSourceStore=false);
        DataSet GetPurchaseOrderDetailsByPoidGRN(Int32 POId);
        DataSet GetOpenStock();
        int SaveUpdateOpeningStock(DataTable theDTOPStock, Int32 UserID, DateTime TransactionDate);
        int SaveUpdateStockAdjustment(DataTable theDTAdjustStock, int LocationId, int StoreId, string AdjustmentDate, int AdjustmentPreparedBy, int AdjustmentAuthorisedBy, int Updatestock, int UserID);
        int SaveGoodreceivedNotes(DataTable DtMasterGRN, DataTable dtGRNItems, int IsPOorIST);
        DataSet GetDisposeStock(int StoreId, DateTime AsofDate);
        int SaveDisposeItems(int StoreId, int LocationId, DateTime AsofDate, int UserId, DataTable theDT);
        DataSet GetStockforAdjustment(int StoreId, string AdjustmentDate);


     
    }
}
