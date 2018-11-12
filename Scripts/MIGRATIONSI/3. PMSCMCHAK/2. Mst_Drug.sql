
/*
 Created By Joseph Njung'e
 Return DrugList from mst_itemmaster
 ItemTypeID 300	 = Drugs
*/


 IF NOT EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'QtyUnitDisp'
          AND Object_ID = Object_ID(N'mst_ItemMaster'))
BEGIN
ALTER  table mst_ItemMaster ADD  QtyUnitDisp int null
END
 IF NOT EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'MorDose'
          AND Object_ID = Object_ID(N'mst_ItemMaster'))
BEGIN
ALTER  table mst_ItemMaster ADD  MorDose int null
END

IF NOT EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'MidDose'
          AND Object_ID = Object_ID(N'mst_ItemMaster'))
BEGIN
ALTER  table mst_ItemMaster ADD  MidDose int null
END

 IF NOT EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'EvenDose '
          AND Object_ID = Object_ID(N'mst_ItemMaster'))
BEGIN
ALTER  table mst_ItemMaster ADD  EvenDose int null
END

 IF NOT EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'NightDose'
          AND Object_ID = Object_ID(N'mst_ItemMaster'))
BEGIN
ALTER  table mst_ItemMaster ADD  NightDose int null
END


 IF NOT EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'Syrup'
          AND Object_ID = Object_ID(N'mst_ItemMaster'))
BEGIN
ALTER TABLE mst_ItemMaster ADD Syrup int null
END




IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[Mst_Drug]'))
BEGIN

EXEC('ALTER VIEW [dbo].[Mst_Drug]
AS
Select	D.Item_PK Drug_pk,
		D.ItemCode DrugID,
		D.ItemTypeID,
		D.ItemName DrugName,
		D.DeleteFlag,
		D.CreatedBy UserID,
		D.CreateDate CreateDate,
		D.UpdateDate,
		D.DispensingMargin,
		D.DispensingUnitPrice,
		D.FDACode,
		D.Manufacturer,
		D.MaxStock,
		D.MinStock,
		D.PurchaseUnitPrice,
		D.QtyPerPurchaseUnit,		
		Isnull(CC.ItemSellingPrice,0)SellingUnitPrice,
		D.DispensingUnit,
		D.PurchaseUnit,
		CC.EffectiveDate,
		1 As [Sequence] ,
		D.ItemInstructions,
		D.Abbreviation,
		D.Syrup,
		D.QtyUnitDisp,
		D.MorDose, 
		D.MidDose ,
		D.EvenDose ,
		D.NightDose 
From dbo.Mst_ItemMaster D
Inner Join
	Mst_ItemType I On I.ItemTypeID= D.ItemTypeID
Left Outer Join
	(Select Distinct ItemId, ItemType,PriceStatus, ItemSellingPrice,EffectiveDate From dbo.lnk_ItemCostConfiguration) CC On CC.ItemId = D.Item_PK And CC.ItemType=D.ItemTypeID And CC.PriceStatus = 1
Where I.ItemName=''Pharmaceuticals'';')





END


GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[Mst_Drug]'))
BEGIN

EXEC('CREATE VIEW [dbo].[Mst_Drug]
AS
Select	D.Item_PK Drug_pk,
		D.ItemCode DrugID,
		D.ItemTypeID,
		D.ItemName DrugName,
		D.DeleteFlag,
		D.CreatedBy UserID,
		D.CreateDate CreateDate,
		D.UpdateDate,
		D.DispensingMargin,
		D.DispensingUnitPrice,
		D.FDACode,
		D.Manufacturer,
		D.MaxStock,
		D.MinStock,
		D.PurchaseUnitPrice,
		D.QtyPerPurchaseUnit,		
		Isnull(CC.ItemSellingPrice,0)SellingUnitPrice,
		D.DispensingUnit,
		D.PurchaseUnit,
		CC.EffectiveDate,
		1 As [Sequence] ,
		D.ItemInstructions,
		D.Abbreviation,
		D.Syrup,
		D.QtyUnitDisp,
		D.MorDose, 
		D.MidDose ,
		D.EvenDose ,
		D.NightDose 
From dbo.Mst_ItemMaster D
Inner Join
	Mst_ItemType I On I.ItemTypeID= D.ItemTypeID
Left Outer Join
	(Select Distinct ItemId, ItemType,PriceStatus, ItemSellingPrice,EffectiveDate From dbo.lnk_ItemCostConfiguration) CC On CC.ItemId = D.Item_PK And CC.ItemType=D.ItemTypeID And CC.PriceStatus = 1
Where I.ItemName=''Pharmaceuticals'';')
END


