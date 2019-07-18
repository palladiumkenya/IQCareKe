-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE  sp_updatePatientProgramStart
	-- Add the parameters for the stored procedure here
	@Ptn_Pk int,
	@DateOfEnrollment datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Insert statements for procedure here
	UPDATE Lnk_PatientProgramStart SET StartDate = @DateOfEnrollment WHERE Ptn_pk = @Ptn_Pk AND ModuleId IN (203, 2);

	IF EXISTS(SELECT * FROM Patient WHERE ptn_pk = @Ptn_Pk AND DeleteFlag = 0 AND PatientType = (SELECT TOP 1 Id FROM LookupItem WHERE Name = 'New'))
	BEGIN
		UPDATE PatientHivDiagnosis SET EnrollmentDate = @DateOfEnrollment WHERE PatientId = (SELECT TOP 1 Id FROM Patient WHERE ptn_pk = @Ptn_Pk) AND DeleteFlag = 0;

		IF EXISTS(SELECT * FROM dtl_PatientHivPrevCareEnrollment PHC 
	INNER JOIN ord_Visit ON PHC.ptn_pk = ord_Visit.Ptn_Pk AND PHC.Visit_pk = dbo.ord_Visit.Visit_Id 
	INNER JOIN dbo.mst_VisitType ON dbo.ord_Visit.VisitType = dbo.mst_VisitType.VisitTypeID 
	WHERE (dbo.mst_VisitType.VisitName = 'ART History') AND PHC.ptn_pk = @Ptn_Pk)
	BEGIN
		UPDATE PHC 
		SET PHC.DateEnrolledInCare = @DateOfEnrollment
		FROM dtl_PatientHivPrevCareEnrollment PHC
		INNER JOIN ord_Visit ON PHC.ptn_pk = ord_Visit.Ptn_Pk AND PHC.Visit_pk = dbo.ord_Visit.Visit_Id 
		INNER JOIN dbo.mst_VisitType ON dbo.ord_Visit.VisitType = dbo.mst_VisitType.VisitTypeID 
		WHERE (dbo.mst_VisitType.VisitName = 'ART History') AND PHC.ptn_pk = @Ptn_Pk
	END
	END
END
