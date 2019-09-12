-- =============================================
-- Author: Felix Kithinji
-- Create date: 23-Aug-2019
-- Description:	Lost to follow clients
-- =============================================
CREATE PROCEDURE GetPatientPharmacyStatus
	-- Add the parameters for the stored procedure here
	@ptn_pk int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
	TOP 1 Ptn_pk, 
	DispensedByDate,
	ExpectedReturnDate = DATEADD(DAY, Duration, DispensedByDate),
	LostToFollowDate = DATEADD(DAY, 90, DATEADD(DAY, Duration, DispensedByDate))

	FROM [dbo].[VW_PatientPharmacy]
	WHERE Ptn_pk = @ptn_pk AND ISNULL(DispensedByDate,'') <> '' AND Prophylaxis=0
	ORDER BY  DispensedByDate DESC;
END