/****** Object:  View [dbo].[VW_Drug]    Script Date: 8/15/2018 12:39:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[VW_Drug]
AS
SELECT        dbo.Mst_Drug.Drug_pk, dbo.Mst_Drug.DrugName, dbo.fn_GetDrugTypeId_futures(dbo.Mst_Drug.Drug_pk) AS DrugTypeId, 
                         dbo.fn_GetDrugTypeName_futures(dbo.Mst_Drug.Drug_pk) AS DrugTypeName, dbo.fn_GetFixedDoseDrugAbbrevation(dbo.Mst_Drug.Drug_pk) 
                         AS [Generic Abbrevation], dbo.Mst_Drug.DrugID AS [Item Code], dbo.Mst_Drug.FDACode AS [FDA Code], Mst_DispensingUnit_1.Name AS [Dispensing Unit], 
                         Mst_DispensingUnit_1.Id AS [Dispensing Unit Id], dbo.Mst_Drug.MaxStock, dbo.Mst_Drug.MinStock, dbo.Mst_DispensingUnit.Id AS PurchaseUnitId, 
                         dbo.Mst_DispensingUnit.Name AS [Purchase Unit], dbo.Mst_Drug.QtyPerPurchaseUnit AS [Purchase Unit Qty], 
                         dbo.Mst_Drug.PurchaseUnitPrice AS [Purchase Unit Price], dbo.Mst_Manufacturer.Id AS ManufacturerId, dbo.Mst_Manufacturer.Name AS [Manufacturer Name], 
                         dbo.Mst_Drug.DispensingUnitPrice AS [Dispensing Unit Cost], dbo.Mst_Drug.DispensingMargin AS [Dispensing Margin], 
                         dbo.Mst_Drug.SellingUnitPrice AS [Selling Price], dbo.mst_Generic.GenericID, dbo.mst_Generic.GenericName, dbo.Mst_Drug.QtyUnitDisp, dbo.Mst_Drug.syrup
FROM            dbo.lnk_DrugGeneric LEFT OUTER JOIN
                         dbo.mst_Generic ON dbo.lnk_DrugGeneric.GenericID = dbo.mst_Generic.GenericID RIGHT OUTER JOIN
                         dbo.Mst_Drug ON dbo.lnk_DrugGeneric.Drug_pk = dbo.Mst_Drug.Drug_pk LEFT OUTER JOIN
                         dbo.Mst_Manufacturer ON dbo.Mst_Drug.Manufacturer = dbo.Mst_Manufacturer.Id LEFT OUTER JOIN
                         dbo.Mst_DispensingUnit ON dbo.Mst_Drug.PurchaseUnit = dbo.Mst_DispensingUnit.Id LEFT OUTER JOIN
                         dbo.Mst_DispensingUnit AS Mst_DispensingUnit_1 ON dbo.Mst_Drug.DispensingUnit = Mst_DispensingUnit_1.Id
WHERE        (dbo.Mst_Drug.DeleteFlag = 0) OR
                         (dbo.Mst_Drug.DeleteFlag IS NULL) AND (dbo.mst_Generic.DeleteFlag IS NULL) OR
                         (dbo.mst_Generic.DeleteFlag = 0)

GO


