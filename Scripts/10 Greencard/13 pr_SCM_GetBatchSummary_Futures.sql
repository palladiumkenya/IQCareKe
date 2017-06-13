/****** Object:  StoredProcedure [dbo].[pr_SCM_GetBatchSummary_Futures]    Script Date: 6/5/2017 6:39:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[pr_SCM_GetBatchSummary_Futures]                            
@StoreId int=NULL,                        
@ItemId int=NULL,                        
@FromDate datetime=NULL,                        
@ToDate datetime=NULL                        
                            
AS                                      
BEGIN                                   
                
--0 For use bind item data by storeID                   
           
 set @Todate = dateadd(dd,1,@Todate)          
           
 Select ls.StoreId[StoreId], md.Drug_pk, md.DrugName[DrugName]                                  
 from lnk_storeitem ls inner join mst_drug md on ls.ItemId=md.Drug_pk                           
 inner join dtl_StockTransaction dt on md.Drug_pk=dt.ItemId                                     
 where ls.StoreId=@StoreId group by ls.StoreId, md.Drug_pk, md.DrugName order by md.DrugName asc            
    
-- 1 use to get report details behalf of store when item is not selected           
Select  VWBS.ItemId, VWBS.ItemName[Item Description], VWBS.DispensingUnit[Unit],VWBS.BatchId, mb.Name[Batch Name],     
replace(convert(varchar,isnull(VWBS.ExpiryDate,''),106),' ','-')[Expiry Date],    
Case when VWBS.OpeningStock <0 then 0 else VWBS.OpeningStock end[Opening Stock],VWBS.QtyRecieved [Recieved Quantity],
VWBS.QtyDispensed[Dispensed Quantity], VWBS.ClosingQty[Closing Quantity]     
from(           
select Drg.Drug_Pk[ItemId],Drg.DrugName[ItemName],Unit.Name[DispensingUnit],    
r.BatchId, r.ExpiryDate, Sum(r.OpeningQuantity)[OpeningStock],            
sum(r.RecQty)[QtyRecieved], abs(Sum(r.IssQty))[QtyDispensed],            
(isnull(sum(r.OpeningQuantity),0)+isnull(Sum(r.RecQty),0))-isnull(abs(Sum(r.IssQty)),0)[ClosingQty],            
(Select Id from Mst_Store where Id = @StoreId)[StoreId],            
(Select Name from Mst_Store where Id = @StoreId)[StoreName]            
from Mst_Drug Drg Inner Join Mst_DispensingUnit Unit on Drg.DispensingUnit = Unit.Id             
Left Outer Join             
(select tmp.drug_pk,tmp.drugname,tmp.dispensingunit, tmp.BatchId, tmp.ExpiryDate,     
 sum(tmp.openingquantity)[OpeningQuantity],''[RecQty], ''[IssQty] from           
 (select a.Drug_Pk,a.DrugName,c.Name [DispensingUnit],b.BatchId, b.ExpiryDate, sum(b.Quantity)[OpeningQuantity]            
 from mst_drug a Left Outer join dtl_stocktransaction b on a.Drug_Pk = b.ItemId            
 Inner Join Mst_DispensingUnit c on a.DispensingUnit = c.Id           
 where b.storeid = @StoreId and b.ExpiryDate>=@FromDate and           
 b.transactiondate>=@FromDate and b.transactiondate<@ToDate  and openingstock = 1            
 group by a.drug_pk,a.drugname,c.name,b.BatchId, b.ExpiryDate          
 union          
 select a.Drug_Pk,a.DrugName,c.Name [DispensingUnit],d.BatchId, d.ExpiryDate,           
 nullif(dbo.fn_GetItemOpeningStock(a.Drug_pk,@StoreId,@FromDate),0)[OpeningQuanitity]          
 from mst_drug a Inner Join Mst_DispensingUnit c on a.DispensingUnit = c.Id   
 inner join dtl_stocktransaction d on a.Drug_Pk = d.ItemId and d.StoreId=@StoreId and d.ExpiryDate >= GetDate()  
 inner join mst_batch e on d.BatchId=e.Id   
 group by a.drug_pk,a.drugname,c.name,d.BatchId, d.ExpiryDate         
 union          
 select a.Drug_Pk,a.DrugName,c.Name [DispensingUnit], b.BatchId, b.ExpiryDate,sum(b.Quantity)[OpeningQuantity]              
 from mst_drug a Left Outer join dtl_stocktransaction b on a.Drug_Pk = b.ItemId              
 Inner Join Mst_DispensingUnit c on a.DispensingUnit = c.Id              
 where b.storeid = @StoreId and b.ExpiryDate>=@FromDate and             
 b.transactiondate>=@FromDate and b.transactiondate<@ToDate  and POID IS NULL and          
 GRNId IS NULL and DisposeId IS NULL and Ptn_Pharmacy_Pk IS NULL and PtnPk IS NULL           
 and OpeningStock IS NULL               
 group by a.drug_pk,a.drugname,c.name,b.BatchId, b.ExpiryDate          
)tmp group by tmp.drug_pk,tmp.drugname,tmp.dispensingunit,tmp.BatchId, tmp.ExpiryDate    
UNION    
select a.Drug_Pk,a.DrugName,c.Name [DispensingUnit], b.BatchId, b.ExpiryDate, ''[OpeningQuantity], Sum(b.Quantity)[RecQty], ''[IssQty]      
from mst_drug a Inner join dtl_stocktransaction b on a.Drug_Pk = b.ItemId            
Inner Join Mst_DispensingUnit c on a.DispensingUnit = c.Id          
Inner Join Mst_Batch mb on b.BatchId=mb.Id           
where b.storeid = @StoreId and (b.GrnId is not null or b.GrnId > 0)           
and b.ExpiryDate>=@FromDate and b.transactiondate>=@FromDate and b.transactiondate<@ToDate            
group by a.drug_pk,a.drugname,c.name, b.BatchId, b.ExpiryDate     
UNION    
select a.Drug_Pk,a.DrugName,c.Name [DispensingUnit],b.BatchId, b.ExpiryDate,''[OpeningQuantity], ''[RecQty], Sum(b.Quantity)[IssQty]              
from mst_drug a inner join dtl_stocktransaction b on a.Drug_Pk = b.ItemId            
Inner Join Mst_DispensingUnit c on a.DispensingUnit = c.Id            
Inner Join Mst_Batch mb on b.BatchId=mb.Id           
where b.storeid = @StoreId and b.Ptn_Pharmacy_pk > 0             
and b.ExpiryDate>=@FromDate and b.transactiondate>=@FromDate and b.transactiondate<@ToDate            
group by b.StoreId, a.drug_pk,a.drugname,c.name, b.BatchId, b.ExpiryDate)r on Drg.Drug_Pk = r.Drug_Pk     
where (r.OpeningQuantity is not null or r.RecQty is not null or r.IssQty is not null)   
group by Drg.Drug_Pk,Drg.DrugName,Unit.Name,r.BatchId, r.ExpiryDate   
)VWBS inner join           
dtl_stocktransaction ds on VWBS.StoreId=ds.StoreId and VWBS.ItemId=ds.ItemId and VWBS.BatchId=ds.BatchId    
and VWBS.ExpiryDate=ds.ExpiryDate      
inner join mst_batch mb on ds.BatchId=mb.Id   
group by VWBS.ItemId, VWBS.ItemName, VWBS.DispensingUnit,  
VWBS.OpeningStock,     
VWBS.QtyRecieved,VWBS.QtyDispensed, VWBS.ClosingQty,  
VWBS.BatchId, mb.Name, VWBS.ExpiryDate     
having VWBS.OpeningStock> 0 and VWBS.QtyRecieved>0 or VWBS.QtyDispensed>0 or VWBS.ClosingQty>0     
  
-- 2 for behalf of store and ITem      
Select  VWBS.ItemId, VWBS.ItemName[Item Description], VWBS.DispensingUnit[Unit],VWBS.BatchId, mb.Name[Batch Name],     
replace(convert(varchar,isnull(VWBS.ExpiryDate,''),106),' ','-')[Expiry Date],    
VWBS.OpeningStock [Opening Stock],
--Case when VWBS.OpeningStock < 0 then 0 else VWBS.OpeningStock end[Opening Stock],
VWBS.QtyRecieved [Recieved Quantity],
VWBS.QtyDispensed[Dispensed Quantity], VWBS.ClosingQty[Closing Quantity]     
from(           
select Drg.Drug_Pk[ItemId],Drg.DrugName[ItemName],Unit.Name[DispensingUnit],    
r.BatchId, r.ExpiryDate, Sum(r.OpeningQuantity)[OpeningStock],            
sum(r.RecQty)[QtyRecieved], abs(Sum(r.IssQty))[QtyDispensed],            
(isnull(sum(r.OpeningQuantity),0)+isnull(Sum(r.RecQty),0))-isnull(abs(Sum(r.IssQty)),0)[ClosingQty],            
(Select Id from Mst_Store where Id = @StoreId)[StoreId],            
(Select Name from Mst_Store where Id = @StoreId)[StoreName]            
from Mst_Drug Drg Inner Join Mst_DispensingUnit Unit on Drg.DispensingUnit = Unit.Id             
Left Outer Join             
(select tmp.drug_pk,tmp.drugname,tmp.dispensingunit, tmp.BatchId, tmp.ExpiryDate,     
 sum(tmp.openingquantity)[OpeningQuantity],''[RecQty], ''[IssQty] from           
 (select a.Drug_Pk,a.DrugName,c.Name [DispensingUnit],b.BatchId, b.ExpiryDate, sum(b.Quantity)[OpeningQuantity]            
 from mst_drug a Left Outer join dtl_stocktransaction b on a.Drug_Pk = b.ItemId            
 Inner Join Mst_DispensingUnit c on a.DispensingUnit = c.Id           
 where b.storeid = @StoreId and b.ExpiryDate>=@FromDate and           
 b.transactiondate>=@FromDate and transactiondate<@ToDate  and openingstock = 1            
 group by a.drug_pk,a.drugname,c.name,b.BatchId, b.ExpiryDate          
 union          
 select a.Drug_Pk,a.DrugName,c.Name [DispensingUnit],''BatchId, ''ExpiryDate,           
 nullif(dbo.fn_GetItemOpeningStock(a.Drug_pk,@StoreId,@FromDate),0)[OpeningQuanitity]          
 from mst_drug a Inner Join Mst_DispensingUnit c on a.DispensingUnit = c.Id        
 union          
 select a.Drug_Pk,a.DrugName,c.Name [DispensingUnit], b.BatchId, b.ExpiryDate,sum(b.Quantity)[OpeningQuantity]              
 from mst_drug a Left Outer join dtl_stocktransaction b on a.Drug_Pk = b.ItemId              
 Inner Join Mst_DispensingUnit c on a.DispensingUnit = c.Id              
 where b.storeid = @StoreId and b.ExpiryDate>=@FromDate and             
 transactiondate>=@FromDate and transactiondate<@ToDate  and POID IS NULL and          
 GRNId IS NULL and DisposeId IS NULL and Ptn_Pharmacy_Pk IS NULL and PtnPk IS NULL           
 and OpeningStock IS NULL               
 group by a.drug_pk,a.drugname,c.name,b.BatchId, b.ExpiryDate          
)tmp group by tmp.drug_pk,tmp.drugname,tmp.dispensingunit,tmp.BatchId, tmp.ExpiryDate    
UNION    
select a.Drug_Pk,a.DrugName,c.Name [DispensingUnit], b.BatchId, b.ExpiryDate, ''[OpeningQuantity], Sum(b.Quantity)[RecQty], ''[IssQty]      
from mst_drug a Inner join dtl_stocktransaction b on a.Drug_Pk = b.ItemId            
Inner Join Mst_DispensingUnit c on a.DispensingUnit = c.Id          
Inner Join Mst_Batch mb on b.BatchId=mb.Id           
where b.storeid = @StoreId and (b.GrnId is not null or b.GrnId > 0)             
and b.ExpiryDate>=@FromDate and b.transactiondate>=@FromDate and b.transactiondate<@ToDate            
group by a.drug_pk,a.drugname,c.name, b.BatchId, b.ExpiryDate    
UNION    
select a.Drug_Pk,a.DrugName,c.Name [DispensingUnit],b.BatchId, b.ExpiryDate,''[OpeningQuantity], ''[RecQty], Sum(b.Quantity)[IssQty]              
from mst_drug a inner join dtl_stocktransaction b on a.Drug_Pk = b.ItemId            
Inner Join Mst_DispensingUnit c on a.DispensingUnit = c.Id            
Inner Join Mst_Batch mb on b.BatchId=mb.Id           
where b.storeid = @StoreId and b.Ptn_Pharmacy_pk > 0             
and b.ExpiryDate>=@FromDate and b.transactiondate>=@FromDate and b.transactiondate<@ToDate            
group by b.StoreId, a.drug_pk,a.drugname,c.name, b.BatchId, b.ExpiryDate)r on Drg.Drug_Pk = r.Drug_Pk     
where (r.OpeningQuantity is not null or r.RecQty is not null or r.IssQty is not null)    
group by Drg.Drug_Pk,Drg.DrugName,Unit.Name,r.BatchId, r.ExpiryDate     
)VWBS inner join           
dtl_stocktransaction ds on VWBS.StoreId=ds.StoreId and VWBS.ItemId=ds.ItemId and VWBS.BatchId=ds.BatchId    
and VWBS.ExpiryDate=ds.ExpiryDate    
inner join mst_batch mb on ds.BatchId=mb.Id  where VWBS.ItemId=@ItemId    
group by VWBS.ItemId, VWBS.ItemName, VWBS.DispensingUnit,VWBS.OpeningStock,     
VWBS.QtyRecieved,VWBS.QtyDispensed, VWBS.ClosingQty,VWBS.BatchId, mb.Name, VWBS.ExpiryDate     
    
END
