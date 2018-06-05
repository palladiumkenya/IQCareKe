/****** Object:  StoredProcedure [dbo].[sp_getPharmacyDrugList]    Script Date: 3/8/2018 7:22:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPharmacyDrugList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getPharmacyDrugList]
GO

/****** Object:  StoredProcedure [dbo].[sp_getPharmacyDrugList]    Script Date: 3/8/2018 7:22:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		John Macharia
-- Create date: 14th Feb 2017
-- Description:	get pharmacy drug list
-- =============================================
CREATE PROCEDURE [dbo].[sp_getPharmacyDrugList]
	-- Add the parameters for the stored procedure here
	@pmscm int = null,@tp varchar(10)=null
	

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
	DECLARE @drugTypeId int =0;

	--select Drug_pk, DrugName,CONCAT(Drug_pk, '~',abbreviation, '~', DrugName)val 
	--from mst_drug
	-- ////////////////////////////////////////////////////////////////////////////////////////
	IF(@tp IN('ART','PMTCT','PEP','PREP','HBV','Hepatitis B'))
		BEGIN
		  SET @drugTypeId=37
		END


	-- ///////////////////////////////////////////////////////////////////////////////////////

	
	IF(@pmscm = 1)
	BEGIN

		IF(@drugTypeId=37)
		  BEGIN
				SELECT
					D.Drug_pk
					,CASE WHEN D.abbreviation IS NULL THEN D.DrugName
					ELSE 
						CONCAT(D.DrugName,' (', D.abbreviation, ')')
					END drugName					
				  -- , (Convert(varchar(8),D.Drug_pk)+ '~' + isnull(D.abbreviation,D.DrugName) + '~' + D.DrugName) val
				  ,CONCAT(D.Drug_pk, '~',D.abbreviation, '~', D.DrugName)val 
				FROM Dtl_StockTransaction AS ST
				INNER JOIN Mst_Store AS S
					ON S.Id = ST.StoreId
						AND S.DispensingStore = 1
				RIGHT OUTER JOIN Mst_Drug AS D
					ON D.Drug_pk = ST.ItemId
				INNER JOIN lnk_DrugGeneric l
					ON D.Drug_pk = l.Drug_pk
				--INNER JOIN lnk_DrugTypeGeneric g
				--	ON l.GenericID = g.GenericId
				WHERE D.DeleteFlag = 0
				-- AND g.DrugTypeId =37
				GROUP BY D.Drug_pk
						,D.DrugName
						,D.abbreviation
				HAVING SUM(ST.Quantity) > 0

				--select D.*, G.DrugTypeId From Mst_Drug D Inner join lnk_DrugGeneric DG on DG.Drug_pk=D.drug_pk
				-- inner join lnk_DrugTypeGeneric G on G.GenericId= DG.GenericID and g.DrugTypeId=37

			END
		ELSE
		 BEGIN 
			SELECT
					D.Drug_pk
					,CASE WHEN D.abbreviation IS NULL THEN D.DrugName
					ELSE 
						CONCAT(D.DrugName,' (', D.abbreviation, ')')
					END drugName					
				   -- ,(Convert(varchar(8),D.Drug_pk) + '~' + isnull(D.abbreviation,D.DrugName) +  '~' + D.DrugName) val
				   ,CONCAT(D.Drug_pk, '~',D.abbreviation, '~', D.DrugName)val 
				FROM Dtl_StockTransaction AS ST
				INNER JOIN Mst_Store AS S
					ON S.Id = ST.StoreId
						AND S.DispensingStore = 1
				RIGHT OUTER JOIN Mst_Drug AS D
					ON D.Drug_pk = ST.ItemId
				INNER JOIN lnk_DrugGeneric l
					ON D.Drug_pk = l.Drug_pk

				--INNER JOIN lnk_DrugTypeGeneric g
				--	ON l.GenericID = g.GenericId
				WHERE D.DeleteFlag = 0 AND D.Drug_pk IN(SELECT x.Drug_pk FROM lnk_DrugGeneric x WHERE x.GenericID IN(SELECT k.GenericId FROM lnk_DrugTypeGeneric k WHERE k.DrugTypeId<>37))
				-- AND g.DrugTypeId <>37
				GROUP BY D.Drug_pk
						,D.DrugName
						,D.abbreviation
				HAVING SUM(ST.Quantity) > 0   
		 END

	END
	ELSE
		BEGIN

			IF(@drugTypeId=37)
			BEGIN
				Select	D.Drug_pk, 
					CASE WHEN D.abbreviation IS NULL THEN D.DrugName
					ELSE 
						CONCAT(D.DrugName,' (', D.abbreviation, ')')
					END drugName,					
					CONCAT(D.Drug_pk, '~',D.abbreviation, '~', D.DrugName)val 
				-- (Convert(varchar(8),D.Drug_pk) +  '~' + isnull(D.abbreviation,D.DrugName) +  '~' + D.DrugName) val 
				From Dtl_StockTransaction As ST	Inner Join Mst_Store As S On S.Id = ST.StoreId And S.DispensingStore = 1
				Right Outer Join Mst_Drug As D On D.Drug_pk = ST.ItemId 
								INNER JOIN lnk_DrugGeneric l
						ON D.Drug_pk = l.Drug_pk
					--INNER JOIN lnk_DrugTypeGeneric g
					--	ON l.GenericID = g.GenericId
					WHERE D.DeleteFlag = 0
					--AND g.DrugTypeId = @drugTypeId
				Group By D.Drug_pk,	D.DrugName, D.abbreviation
			END
			ELSE
			BEGIN
						Select	D.Drug_pk, D.DrugName,CONCAT(D.Drug_pk, '~',D.abbreviation, '~', D.DrugName)val 
				-- (Convert(varchar(8),D.Drug_pk) + '~' + isnull(D.abbreviation,D.DrugName) + '~' + D.DrugName) val 
				From Dtl_StockTransaction As ST	Inner Join Mst_Store As S On S.Id = ST.StoreId And S.DispensingStore = 1
				Right Outer Join Mst_Drug As D On D.Drug_pk = ST.ItemId 
								INNER JOIN lnk_DrugGeneric l
						ON D.Drug_pk = l.Drug_pk
					INNER JOIN lnk_DrugTypeGeneric g
						ON l.GenericID = g.GenericId
					WHERE D.DeleteFlag = 0 --AND D.Drug_pk IN(SELECT x.Drug_pk FROM lnk_DrugGeneric x WHERE x.GenericID IN(SELECT k.GenericId FROM lnk_DrugTypeGeneric k WHERE k.DrugTypeId<>37))
					 AND g.DrugTypeId <> 37 
				Group By D.Drug_pk,	D.DrugName, D.abbreviation
			END
		END

End
GO