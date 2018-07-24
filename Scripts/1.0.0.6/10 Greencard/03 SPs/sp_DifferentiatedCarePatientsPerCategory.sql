IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_DifferentiatedCarePatientsPerCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_DifferentiatedCarePatientsPerCategory]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE sp_DifferentiatedCarePatientsPerCategory 
	@category varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	if(@category = 'Advanced')
	begin
		select * from (
			select Pin.IdentifierValue PatientId,
			case when (LUI.Name = 'Stage1' OR LUI.Name = 'Stage2') AND (PB.CD4Count > 200) Then 'Well'
			when (LUI.Name = 'Stage3' OR LUI.Name = 'Stage4') OR (PB.CD4Count <= 200) Then 'Advanced'
			ELSE 'Unknown (Missing Baseline WHO Stage or CD4)' END AS Category 
			from PatientBaselineAssessment PB 
			inner join Patient PT on PB.PatientId = PT.ID
			inner join person P on P.Id = PT.PersonId
			inner join LookUpItem LUI on PB.WHOStage = LUI.Id
			inner join PatientEnrollment PE on PB.PatientId = PE.PatientID
			inner join PatientIdentifier PIn on PIn.PatientId = PT.Id
			where DATEDIFF(MONTH, PE.EnrollmentDate, GETDATE()) <= 12 AND PE.CareEnded = 0) data
		where data.Category = 'Advanced'
	end
	else if(@category = 'Well')
	begin
		select * from (
			select Pin.IdentifierValue PatientId,
			case when (LUI.Name = 'Stage1' OR LUI.Name = 'Stage2') AND (PB.CD4Count > 200) Then 'Well'
			when (LUI.Name = 'Stage3' OR LUI.Name = 'Stage4') OR (PB.CD4Count <= 200) Then 'Advanced'
			ELSE 'Unknown (Missing Baseline WHO Stage or CD4)' END AS Category 
			from PatientBaselineAssessment PB 
			inner join Patient PT on PB.PatientId = PT.ID
			inner join person P on P.Id = PT.PersonId
			inner join LookUpItem LUI on PB.WHOStage = LUI.Id
			inner join PatientEnrollment PE on PB.PatientId = PE.PatientID
			inner join PatientIdentifier PIn on PIn.PatientId = PT.Id
			where DATEDIFF(MONTH, PE.EnrollmentDate, GETDATE()) <= 12 AND PE.CareEnded = 0) data
		where data.Category = 'Well'
	end
	else if(@category = 'Unknown (Missing Baseline WHO Stage or CD4)')
	begin
		select * from (
			select Pin.IdentifierValue PatientId,
			case when (LUI.Name = 'Stage1' OR LUI.Name = 'Stage2') AND (PB.CD4Count > 200) Then 'Well'
			when (LUI.Name = 'Stage3' OR LUI.Name = 'Stage4') OR (PB.CD4Count <= 200) Then 'Advanced'
			ELSE 'Unknown (Missing Baseline WHO Stage or CD4)' END AS Category 
			from PatientBaselineAssessment PB 
			inner join Patient PT on PB.PatientId = PT.ID
			inner join person P on P.Id = PT.PersonId
			inner join LookUpItem LUI on PB.WHOStage = LUI.Id
			inner join PatientEnrollment PE on PB.PatientId = PE.PatientID
			inner join PatientIdentifier PIn on PIn.PatientId = PT.Id
			where DATEDIFF(MONTH, PE.EnrollmentDate, GETDATE()) <= 12 AND PE.CareEnded = 0) data
		where data.Category = 'Unknown (Missing Baseline WHO Stage or CD4)'
	end
	else if(@category = 'Stable')
	begin
		select * from (
			SELECT PI.IdentifierValue PatientId, CASE WHEN C.Id IS NULL OR C.Categorization = 2 THEN 'Unstable' ELSE 'Stable' END AS Category
				FROM PatientEnrollment PE INNER JOIN dbo.Patient PT ON PT.Id = pe.PatientId 
				INNER JOIN dbo.PatientIdentifier PI ON PE.Id = PI.PatientEnrollmentId 
				INNER JOIN dbo.Identifiers IE ON PI.IdentifierTypeId = IE.Id 
				LEFT OUTER JOIN (SELECT PatientId, Id, Categorization, row_number() OVER (Partition BY PatientId
				ORDER BY DateAssessed DESC) RowNum FROM PatientCategorization) C ON C.PatientId = Pe.PatientId AND C.RowNum = 1
			WHERE ServiceAreaId = 1 AND IE.Name = 'CCC Registration Number' AND PT.DeleteFlag = 0 AND DATEDIFF(MONTH, PE.EnrollmentDate, GETDATE()) > 12 AND PE.CareEnded = 0) data
		where data.Category = 'Stable'
	end
	else if(@category = 'Unstable')
	begin
		select * from (
			SELECT PI.IdentifierValue PatientId, CASE WHEN C.Id IS NULL OR C.Categorization = 2 THEN 'Unstable' ELSE 'Stable' END AS Category
				FROM PatientEnrollment PE INNER JOIN dbo.Patient PT ON PT.Id = pe.PatientId 
				INNER JOIN dbo.PatientIdentifier PI ON PE.Id = PI.PatientEnrollmentId 
				INNER JOIN dbo.Identifiers IE ON PI.IdentifierTypeId = IE.Id 
				LEFT OUTER JOIN (SELECT PatientId, Id, Categorization, row_number() OVER (Partition BY PatientId
				ORDER BY DateAssessed DESC) RowNum FROM PatientCategorization) C ON C.PatientId = Pe.PatientId AND C.RowNum = 1
			WHERE ServiceAreaId = 1 AND IE.Name = 'CCC Registration Number' AND PT.DeleteFlag = 0 AND DATEDIFF(MONTH, PE.EnrollmentDate, GETDATE()) > 12 AND PE.CareEnded = 0) data
		where data.Category = 'Unstable'
	end
END
GO
