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
        int SaveGoodReceivedNote(DataTable DtMasterGRN, DataTable dtGRNItems, int IsPOorIST);

        int SavePOWithGRN(DataTable dtMasterPO, DataTable dtPOItems, bool isUpdate);
        DataSet GetPurchaseOrderItem(int isPO, int userId, int storeId);
        DataSet GetPurchaseOrderDetailsByPoid(int purchaseOrderId);
        DataTable GetPurchaseOrderDetails(int userId, int DestinStoreID, int locationID);
        DataTable GetPurchaseOrderDetailsForGRN(int userId, int storeId, int locationID, bool IsSourceStore=false);
        DataSet GetPurchaseOrderDetailsByPoidGRN(int purchaseOrderId);
        DataSet GetOpenStock();
        int SaveUpdateOpeningStock(DataTable theDTOPStock, int userId, DateTime TransactionDate);
        int SaveUpdateStockAdjustment(DataTable theDTAdjustStock, int LocationId, int storeId, string AdjustmentDate, int AdjustmentPreparedBy, int AdjustmentAuthorisedBy, int Updatestock, int userId);
      
        DataSet GetDisposeStock(int storeId, DateTime AsofDate);
        int SaveDisposeItems(int storeId, int locationId, DateTime AsofDate, int userId, DataTable theDT);
        DataSet GetStockforAdjustment(int storeId, string AdjustmentDate);
        DataSet GetPurchaseOrderItems(int purchaseOrderId);


     
    }
}
