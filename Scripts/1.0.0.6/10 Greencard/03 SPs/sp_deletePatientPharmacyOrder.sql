
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE sp_deletePatientPharmacyOrder
	@ptn_pharmacy_pk int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Update ord_PatientPharmacyOrder set DeleteFlag = 1 where ptn_pharmacy_pk = @ptn_pharmacy_pk

	update dtl_regimenmap set deleteflag = 1 where orderid = @ptn_pharmacy_pk

	delete from dtl_stocktransaction where ptn_pharmacy_pk = @ptn_pharmacy_pk
END
GO
