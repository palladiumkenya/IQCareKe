IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetDuplicatePatientRecords]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_GetDuplicatePatientRecords]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetDuplicatePatientRecords]    Script Date: 9/10/2018 10:36:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		KOchieng
-- Create date: 25-07-2018
-- Description:	Get all possible duplicates based on selected criteria
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetDuplicatePatientRecords] 
	@matchFirstName bit = 1, 
	@matchMiddleName bit = 0,
	@matchLastname as Bit = 1,
	@matchSex as Bit = 1,
	@matchEnrollmentNumber as Bit = 1,
	@matchDOB as Bit = 1,
	@matchEnrollmentDate as Bit = 1,
	@matchARTStartDate as Bit = 0,
	@matchHIVDiagnosisDate as Bit = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	exec pr_OpenDecryptedSession;

	DECLARE @strSQL NVARCHAR(MAX)
	DECLARE @rankSQL NVARCHAR(MAX)

	BEGIN TRY 
		DROP table #rawDataCTE
	END TRY
	BEGIN CATCH
	END CATCH

	SELECT 
		ptn_pk,PatientId,PersonId,EnrollmentNumber,FirstName,MiddleName,LastName,Sex, EnrollmentDate,DateOfBirth,MobileNumber, PatientEnrollmentId,
		SOUNDEX(FirstName) as sFirstName,SOUNDEX(MiddleName) as sMiddleName,SOUNDEX(LastName) as sLastName, SOUNDEX(CONCAT(FirstName,LastName)) as sFirstLastName,SOUNDEX(CONCAT(FirstName,MiddleName,LastName)) as sFirstMiddleLastName ,
		CASE WHEN LEN(EnrollmentNumber) = 11 THEN --To match values like 13939-26222
			(SUBSTRING(EnrollmentNumber,CHARINDEX('-', EnrollmentNumber)+1, LEN(EnrollmentNumber)))  
		ELSE 
			CASE WHEN LEN(EnrollmentNumber) = 10 AND ISNUMERIC(EnrollmentNumber) = 1 THEN --To match values like 1393926222
				RIGHT(EnrollmentNumber, 5)
			ELSE			
				CASE WHEN CHARINDEX('-', EnrollmentNumber) = 3 THEN --To match values like 14-26222
					(SUBSTRING(EnrollmentNumber,(CHARINDEX('-', EnrollmentNumber) + 1), (LEN(EnrollmentNumber)))) 
				ELSE			
					(SUBSTRING(EnrollmentNumber,1, (LEN(EnrollmentNumber) - CHARINDEX('-', REVERSE(EnrollmentNumber))))) 
				END
			END
		END 
			as MatchingEnrollmentNo
	INTO #rawDataCTE
	FROM (
		SELECT mp.ptn_pk,
			p.Id as PatientId,
			p.PersonId,
			REPLACE(pid.PatientEnrollmentId, '/', '-') as EnrollmentNumber,
			pid.PatientEnrollmentId,
			CAST(DecryptByKey(FirstName) AS varchar(100)) as FirstName,
			CAST(DECRYPTBYKEY(MIddleName) AS varchar(50)) as MiddleName,
			CAST(DECRYPTBYKEY(LastName) AS varchar(50)) as LastName,
			Sex,
			CAST(mp.RegistrationDate AS DATE) as EnrollmentDate,
			CAST(DOB AS DATE) as DateOfBirth,
			CAST(DECRYPTBYKEY(Phone) AS varchar(50)) as MobileNumber
		FROM mst_patient mp
		INNER JOIN patient p ON
			mp.ptn_pk = p.ptn_pk 
		INNER JOIN (
			SELECT * FROM (
				SELECT PatientId,pid.IdentifierValue as PatientEnrollmentId,ROW_NUMBER() OVER (PARTITION BY pid.PatientId ORDER BY CreateDate DESC) as RowNum FROM PatientIdentifier pid 
				WHERE pid.IdentifierTypeId = 1
				) pid WHERE pid.RowNum = 1 		
			) pid ON pid.PatientId = p.id
		WHERE 
			P.DeleteFlag = 0 AND MP.DeleteFlag = 0 AND 
			Len(pid.PatientEnrollmentID) > 0
	) p

	set @strSQL =	CONCAT('SELECT ', 
			'a.MatchingEnrollmentNo,', 
			'a.FirstName, a.MiddleName, a.LastName, a.DateOfBirth, a.PatientEnrollmentId,a.EnrollmentDate,a.MobileNumber,',
			'a.Ptn_Pk,a.PatientId,a.PersonId ',
			',DENSE_RANK() OVER (ORDER BY ')

		if @matchEnrollmentNumber = 1
			set @rankSQL = CONCAT(@rankSQL, 'a.MatchingEnrollmentNo, ')
		if @matchEnrollmentDate = 1
			set @rankSQL = CONCAT(@rankSQL, 'a.EnrollmentDate, ')
		if @matchFirstname = 1
			set @rankSQL = CONCAT(@rankSQL,'SOUNDEX(a.FirstName), ')
		if @matchMiddlename = 1
			set @rankSQL = CONCAT(@rankSQL,'SOUNDEX(a.MiddleName), ')
		if @matchLastname = 1
			set @rankSQL = CONCAT(@rankSQL, 'SOUNDEX(a.LastName), ')
		if @matchSex = 1
			set @rankSQL = CONCAT(@rankSQL, 'a.Sex, ')
		if @matchDOB = 1
			set @rankSQL = CONCAT(@rankSQL, 'a.DateOfBirth, ')
		set @rankSQL = LEFT(@rankSQL, LEN(@rankSQL) - 1) -- Remove trailing ,(comma character)
		set @rankSQL = CONCAT(@rankSQL,	' ) AS GroupingFilter ')

		set @strSQL = CONCAT(@strSQL, @rankSQL)

	set @strSQL = CONCAT(@strSQL, 'FROM #rawDataCTE a ', 
		'INNER JOIN #rawDataCTE b ON ',
			'a.Ptn_Pk <> b.Ptn_Pk ')

	IF @matchEnrollmentDate= 1 
		set @strSQL = CONCAT(@strSQL, 'and (a.EnrollmentDate = b.EnrollmentDate) ')
	IF @matchFirstname = 1 
		set @strSQL = CONCAT(@strSQL,	'and (a.sFirstName = b.sFirstName) ')
	IF @matchMiddlename = 1 
		set @strSQL = CONCAT(@strSQL,	'and (a.sMiddleName = b.sMiddleName) ')
	IF @matchLastname = 1 
		set @strSQL = CONCAT(@strSQL,	'and (a.sLastName = b.sLastName) ')
	IF @matchSex = 1 
		set @strSQL = CONCAT(@strSQL,	'and (a.sex = b.sex)  ')
	IF @matchEnrollmentNumber = 1 
		set @strSQL = CONCAT(@strSQL,	'and (a.MatchingEnrollmentNo = b.MatchingEnrollmentNo) ') 
	IF @matchDOB = 1 
		set @strSQL = CONCAT(@strSQL,	'and (a.DateOfBirth = b.DateOfBirth) ') 
	set @strSQL = CONCAT(@strSQL,	'ORDER BY a.sFirstLastName, a.FirstName, a.MatchingEnrollmentNo, LEN(a.EnrollmentNumber) DESC, MobileNumber DESC')

	EXECUTE sp_executesql @strSQL

	
END
