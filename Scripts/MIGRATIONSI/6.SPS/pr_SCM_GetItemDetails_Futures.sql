



IF   EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_SCM_GetItemDetails_Futures]') AND type in (N'P', N'PC'))
BEGIN

EXEC('
ALTER Procedure [dbo].[pr_SCM_GetItemDetails_Futures]        
@ItemId int        
        
as        
        
begin        
 select c.Drug_Pk,DrugId,c.DrugName,dbo.fn_GetDrugGenericCommaSeprated(c.Drug_Pk) [GenericName],        
 dbo.fn_Drug_Abbrev_Constella (c.Drug_Pk) [GenAbbr],c.FDACode,c.DispensingUnit,c.MinStock,c.MaxStock,        
 c.PurchaseUnit,c.QtyPerPurchaseUnit,c.PurchaseUnitPrice,        
 c.Manufacturer,c.DispensingUnitPrice,c.DispensingMargin,c.SellingUnitPrice,c.EffectiveDate,c.DeleteFlag            
 from Mst_Drug c where c.drug_pk = @Itemid           
        
 select DrugTypeId,DrugTypeName from Mst_DrugType        
 Select ID,Name from mst_Decode where Codeid = 202         
 select Id,Name from Mst_DispensingUnit        
 select Id,Name,DeleteFlag from Mst_Manufacturer        
 select Drug_pk, DrugId, DrugName from mst_drug    
end
')

END
IF  NOT  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_SCM_GetItemDetails_Futures]') AND type in (N'P', N'PC'))
BEGIN

EXEC('
CREATE Procedure [dbo].[pr_SCM_GetItemDetails_Futures]        
@ItemId int        
        
as        
        
begin        
 select c.Drug_Pk,DrugId,c.DrugName,dbo.fn_GetDrugGenericCommaSeprated(c.Drug_Pk) [GenericName],        
 dbo.fn_Drug_Abbrev_Constella (c.Drug_Pk) [GenAbbr],c.FDACode,c.DispensingUnit,c.MinStock,c.MaxStock,        
 c.PurchaseUnit,c.QtyPerPurchaseUnit,c.PurchaseUnitPrice,        
 c.Manufacturer,c.DispensingUnitPrice,c.DispensingMargin,c.SellingUnitPrice,c.EffectiveDate,c.DeleteFlag            
 from Mst_Drug c where c.drug_pk = @Itemid           
        
 select DrugTypeId,DrugTypeName from Mst_DrugType        
 Select ID,Name from mst_Decode where Codeid = 202         
 select Id,Name from Mst_DispensingUnit        
 select Id,Name,DeleteFlag from Mst_Manufacturer        
 select Drug_pk, DrugId, DrugName from mst_drug    
end
')

END

