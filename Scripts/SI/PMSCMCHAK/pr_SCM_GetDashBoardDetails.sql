/****** Object:  StoredProcedure [dbo].[pr_SCM_GetDashBoardDetails]    Script Date: 8/13/2018 1:26:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_SCM_GetDashBoardDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_SCM_GetDashBoardDetails]
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<John Macharia>
-- Create date: <20th Feb 2015>
-- Description:	<Pharmacy dashboard>
-- =============================================
Create PROCEDURE [dbo].[pr_SCM_GetDashBoardDetails] 
@storeid int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @DateFrom datetime = DATEADD(wk, DATEDIFF(wk,0,GETDATE()), 0)
	declare @DateTo datetime = DATEADD(wk, DATEDIFF(wk,0,GETDATE()), 4)

	IF(@storeid = 0)
	BEGIN
		-- Drugs Expiring
		select TOP 10 
		mstD.Drug_pk, mstD.DrugName, SUM(dtlST.Quantity)[Quantity]
		from Mst_Batch mstB inner JOIN Dtl_StockTransaction dtlST ON mstB.ID = dtlST.BatchId
		inner JOIN mst_Drug mstD ON mstB.ItemID = mstD.Drug_pk
		WHERE mstB.ExpiryDate between GETDATE() AND DATEADD(MONTH,1,GETDATE())
		GROUP BY mstD.Drug_pk, mstD.DrugName
		having SUM(dtlST.Quantity) > 0
		ORDER BY SUM(dtlST.Quantity) DESC


		--patient appointments
		SET @DateFrom = DATEADD(wk, DATEDIFF(wk,0,GETDATE()), 0)
		SET @DateTo = DATEADD(wk, DATEDIFF(wk,0,GETDATE()), 4)
	
		select a.Dates [Date],datename(dw,a.Dates)[Day], (SELECT COUNT(*) FROM dtl_PatientAppointment where AppDate=a.Dates)NoOfAppointments,
		(SELECT COUNT(*) FROM ord_Visit where VisitDate=a.Dates and VisitType=4)NoOfVisits
		--SUM(CASE WHEN b.AppDate IS NULL THEN 0 ELSE 1 END) as NoOfAppointments ,b.AppDate
		--SUM(CASE WHEN Ord.VisitDate IS NULL THEN 0 ELSE 1 END) as NoOfVisits
		from 
		(SELECT DATEADD(wk, DATEDIFF(wk,0,GETDATE()), 0) Dates
		UNION
		select DATEADD(wk, DATEDIFF(wk,0,GETDATE()), 1) 
		union
		select DATEADD(wk, DATEDIFF(wk,0,GETDATE()), 2) 
 		UNION
		select DATEADD(wk, DATEDIFF(wk,0,GETDATE()), 3) 
		UNION
		select DATEADD(wk, DATEDIFF(wk,0,GETDATE()), 4)) a
		ORDER BY a.Dates ASC


		---drugs about to run out
		select TOP 15 mstD.Drug_pk, mstD.DrugName,mstDU.Name[Unit], SUM(dtlST.Quantity)AvailQty, mstD.MinStock 
		from Dtl_StockTransaction dtlST inner JOIN Mst_Store mstS ON dtlST.StoreId = mstS.Id
		inner JOIN mst_Drug mstD ON dtlST.ItemId = mstD.Drug_pk
		inner JOIN Mst_DispensingUnit mstDU ON mstD.DispensingUnit = mstDU.Id
		where dtlST.StoreId=4 and dtlST.ExpiryDate > GETDATE()
		group BY mstD.Drug_pk, mstD.DrugName,mstDU.Name,mstD.MinStock 
		having SUM(dtlST.Quantity) > 0
		order BY SUM(dtlST.Quantity) asc
	
	END
	ELSE
	BEGIN
		-- Drugs Expiring
		select TOP 10 
		mstD.Drug_pk, mstD.DrugName, SUM(dtlST.Quantity)[Quantity]
		from Mst_Batch mstB inner JOIN Dtl_StockTransaction dtlST ON mstB.ID = dtlST.BatchId
		inner JOIN mst_Drug mstD ON mstB.ItemID = mstD.Drug_pk
		WHERE dtlST.StoreId=@storeid and mstB.ExpiryDate between GETDATE() AND DATEADD(MONTH,1,GETDATE())
		GROUP BY mstD.Drug_pk, mstD.DrugName
		having SUM(dtlST.Quantity) > 0
		ORDER BY SUM(dtlST.Quantity) DESC


		--patient appointments
		SET @DateFrom = DATEADD(wk, DATEDIFF(wk,0,GETDATE()), 0)
		SET @DateTo = DATEADD(wk, DATEDIFF(wk,0,GETDATE()), 4)
	
		select a.Dates [Date],datename(dw,a.Dates)[Day], (SELECT COUNT(*) FROM dtl_PatientAppointment where AppDate=a.Dates)NoOfAppointments,
		(SELECT COUNT(*) FROM ord_Visit where VisitDate=a.Dates and VisitType=4)NoOfVisits
		--SUM(CASE WHEN b.AppDate IS NULL THEN 0 ELSE 1 END) as NoOfAppointments ,b.AppDate
		--SUM(CASE WHEN Ord.VisitDate IS NULL THEN 0 ELSE 1 END) as NoOfVisits
		from 
		(SELECT DATEADD(wk, DATEDIFF(wk,0,GETDATE()), 0) Dates
		UNION
		select DATEADD(wk, DATEDIFF(wk,0,GETDATE()), 1) 
		union
		select DATEADD(wk, DATEDIFF(wk,0,GETDATE()), 2) 
 		UNION
		select DATEADD(wk, DATEDIFF(wk,0,GETDATE()), 3) 
		UNION
		select DATEADD(wk, DATEDIFF(wk,0,GETDATE()), 4)) a
		ORDER BY a.Dates ASC

		--LEFT join dtl_PatientAppointment b ON a.Dates = b.AppDate and AppDate between @DateFrom and @DateTo

		--LEFT JOIN ord_Visit ord ON a.Dates = ord.VisitDate and ord.VisitType=4 and ord.VisitDate between @DateFrom and @DateTo
		-- --where AppReason in (SELECT ID FROM mst_Decode where Name='Pharmacy Refill' and CodeID IN (SELECT CodeID from mst_Code where Name='Appointment Reason'))
		--group BY a.Dates,b.AppDate--,Ord.VisitDate,b.AppDate


		---drugs about to run out
		select TOP 15 mstD.Drug_pk, mstD.DrugName,mstDU.Name[Unit], SUM(dtlST.Quantity)AvailQty, mstD.MinStock 
		from Dtl_StockTransaction dtlST inner JOIN Mst_Store mstS ON dtlST.StoreId = mstS.Id
		inner JOIN mst_Drug mstD ON dtlST.ItemId = mstD.Drug_pk
		inner JOIN Mst_DispensingUnit mstDU ON mstD.DispensingUnit = mstDU.Id
		where dtlST.StoreId=@storeid and dtlST.ExpiryDate > GETDATE()
		group BY mstD.Drug_pk, mstD.DrugName,mstDU.Name,mstD.MinStock 
		having SUM(dtlST.Quantity) > 0
		order BY SUM(dtlST.Quantity) asc
	END
	
END

