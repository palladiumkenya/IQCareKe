

/****** Object:  View [dbo].[Mst_Drug]    Script Date: 9/12/2018 1:50:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
 Created By Joseph Njung'e
 Return DrugList from mst_itemmaster
 ItemTypeID 300	 = Drugs
*/
ALTER VIEW [dbo].[Mst_Drug]
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
Where I.ItemName='Pharmaceuticals';


GO


