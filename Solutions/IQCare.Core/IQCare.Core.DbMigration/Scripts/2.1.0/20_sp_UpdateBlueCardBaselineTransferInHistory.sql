-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE sp_UpdateBlueCardBaselineTransferInHistory 
	-- Add the parameters for the stored procedure here
	@ptn_pk int,
	@ConfirmHIVPosDate datetime,
	@DateEnrolledInCare datetime,
	@WHOStage int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS(SELECT * FROM dtl_PatientHivPrevCareEnrollment PHC 
	INNER JOIN ord_Visit ON PHC.ptn_pk = ord_Visit.Ptn_Pk AND PHC.Visit_pk = dbo.ord_Visit.Visit_Id 
	INNER JOIN dbo.mst_VisitType ON dbo.ord_Visit.VisitType = dbo.mst_VisitType.VisitTypeID 
	WHERE (dbo.mst_VisitType.VisitName = 'ART History') AND PHC.ptn_pk = @ptn_pk AND PHC.ARTStartDate <> '1900-01-01')
	BEGIN
		UPDATE PHC 
		SET PHC.ConfirmHIVPosDate = @ConfirmHIVPosDate, PHC.DateEnrolledInCare = @DateEnrolledInCare
		FROM dtl_PatientHivPrevCareEnrollment PHC
		INNER JOIN ord_Visit ON PHC.ptn_pk = ord_Visit.Ptn_Pk AND PHC.Visit_pk = dbo.ord_Visit.Visit_Id 
		INNER JOIN dbo.mst_VisitType ON dbo.ord_Visit.VisitType = dbo.mst_VisitType.VisitTypeID 
		WHERE (dbo.mst_VisitType.VisitName = 'ART History') AND PHC.ptn_pk = @ptn_pk;
	END

	DECLARE @WHO varchar(20), @BlueWHO int;
		SET @WHO = (SELECT TOP 1 ItemName from LookupItemView where MasterName = 'WHOStage' AND ItemId = @WHOStage);
		SET @BlueWHO = CASE @WHO  
					WHEN 'Stage1' THEN 1
					WHEN 'Stage2' THEN 2   
					WHEN 'Stage3' THEN 3   
					WHEN 'Stage4' THEN 4
					ELSE 1
				END;

		With cte as (
		select  top(1) WHOStage  
		From dtl_PatientStage 
		where   Ptn_pk = @ptn_pk
		order by  WABStageID asc)
		Update cte set WHOStage = @BlueWHO;
END
