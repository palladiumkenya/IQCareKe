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
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE  sp_updatePatientProgramStart
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
	END
END
GO
