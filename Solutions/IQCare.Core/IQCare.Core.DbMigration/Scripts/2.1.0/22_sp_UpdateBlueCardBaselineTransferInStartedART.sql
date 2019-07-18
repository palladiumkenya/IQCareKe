-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE sp_UpdateBlueCardBaselineTransferInStartedART
	-- Add the parameters for the stored procedure here
	@ptn_pk int,
	@FirstLineRegStDate datetime,
	@FirstLineReg varchar(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS(select * from dtl_PatientARTCare WHERE ptn_pk = @ptn_pk)
	BEGIN
		UPDATE PAC
		SET PAC.FirstLineRegStDate = @FirstLineRegStDate,
		PAC.FirstLineReg = (SELECT TOP 1 Name FROM LookupItem where Id = @FirstLineReg)

		FROM dtl_PatientARTCare PAC
		WHERE PAC.ptn_pk = @ptn_pk;
	END
END
