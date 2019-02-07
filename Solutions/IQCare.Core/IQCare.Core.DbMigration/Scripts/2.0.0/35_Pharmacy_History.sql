-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gikandi/Felix
-- Create date: 05-Feb-2019
-- Description:	Pharmacy History
-- =============================================
CREATE PROCEDURE Pharmacy_History
	-- Add the parameters for the stored procedure here
	@Ptn_Pk int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	;WITH cte AS
	(
	SELECT a.regimentype, b.DispensedByDate,
	ROW_NUMBER() OVER (PARTITION BY regimentype ORDER BY DispensedByDate asc) AS rn
	From dtl_regimenmap a
	Inner Join
	ord_PatientPharmacyOrder b On a.OrderID = b.ptn_pharmacy_pk
	Where b.Ptn_pk = @Ptn_Pk
	--And b.DispensedByDate = @artstart 
	And b.ProgID In (222,223)
	)
	SELECT regimentype, DispensedByDate
	FROM cte
	WHERE rn = 1
	order by DispensedByDate
END
GO
