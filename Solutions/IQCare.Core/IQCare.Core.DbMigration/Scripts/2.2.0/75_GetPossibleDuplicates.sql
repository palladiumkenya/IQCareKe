-- =============================================
-- Author:		KOchieng
-- Create date: 25-07-2018
-- Description:	Get all possible duplicates based on selected criteria 
-- Updated by FelixKithinji
-- =============================================
CREATE PROCEDURE sp_GetPossibleDuplicates 
	@matchFirstName bit = 1,
	@matchMiddleName bit = 0,
	@matchLastname as Bit = 1,
	@matchSex as Bit = 1,
	@matchEnrollmentNumber as Bit = 0,
	@matchDOB as Bit = 1,
	@matchEnrollmentDate as Bit = 0,
	@matchARTStartDate as Bit = 0,
	@matchHIVDiagnosisDate as Bit = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	exec pr_OpenDecryptedSession;
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @rankSQL NVARCHAR(MAX);

	IF OBJECT_ID('tempdb..#temp') IS NOT NULL DROP TABLE #temp;
	IF OBJECT_ID('tempdb..#potentialduplicate') IS NOT NULL DROP TABLE #potentialduplicate;

	SELECT * INTO #temp FROM (SELECT 
		ptn_pk,PatientId,PersonId,EnrollmentNumber,FirstName,MiddleName,LastName,Sex, EnrollmentDate,DateOfBirth,MobileNumber, IdentifierTypeId, PatientEnrollmentId,
		SOUNDEX(FirstName) as sFirstName,SOUNDEX(MiddleName) as sMiddleName,SOUNDEX(LastName) as sLastName, 
		SOUNDEX(CONCAT(FirstName,LastName)) as sFirstLastName,SOUNDEX(CONCAT(FirstName,MiddleName,LastName)) as sFirstMiddleLastName,
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
	--INTO #rawDataCTE
	FROM (
		SELECT mp.ptn_pk,
			p.Id as PatientId,
			p.PersonId,
			REPLACE(pid.PatientEnrollmentId, '/', '-') as EnrollmentNumber,
			pid.PatientEnrollmentId,
			CAST(DecryptByKey(pr.FirstName) AS varchar(100)) as FirstName,
			CAST(DECRYPTBYKEY(pr.MidName) AS varchar(50)) as MiddleName,
			CAST(DECRYPTBYKEY(pr.LastName) AS varchar(50)) as LastName,
			pr.Sex,
			CAST(PE.EnrollmentDate AS DATE) as EnrollmentDate,
			ISNULL(pr.DateOfBirth, p.DateOfBirth) as DateOfBirth,
			CAST(DECRYPTBYKEY(Phone) AS varchar(50)) as MobileNumber,
			IdentifierTypeId
		FROM mst_patient mp
		INNER JOIN patient p ON mp.ptn_pk = p.ptn_pk 
		INNER JOIN Person pr ON pr.Id = p.PersonId
		INNER JOIN PatientEnrollment PE ON PE.PatientId = P.Id
		INNER JOIN (
			SELECT * FROM (
				SELECT PatientId, IdentifierTypeId, [PatientEnrollmentId] as EnrollmentId, pid.IdentifierValue as PatientEnrollmentId,ROW_NUMBER() OVER (PARTITION BY pid.PatientId ORDER BY CreateDate DESC) as RowNum FROM PatientIdentifier pid 
				) pid WHERE pid.RowNum = 1 		
			) pid ON pid.PatientId = PE.PatientId AND pid.EnrollmentId = PE.Id
		WHERE 
			P.DeleteFlag = 0 AND MP.DeleteFlag = 0 AND 
			Len(pid.PatientEnrollmentID) > 0
	) p
	) AS X

	set @strSQL =	CONCAT('SELECT ', 
			'a.MatchingEnrollmentNo,', 
			'a.FirstName, a.MiddleName, a.LastName, a.DateOfBirth, a.PatientEnrollmentId,a.EnrollmentDate,a.MobileNumber,',
			'a.Ptn_Pk, a.PatientId,a.PersonId, a.Sex, a.IdentifierTypeId ',
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

	set @strSQL = CONCAT(@strSQL, 'FROM #temp a ', 
		'INNER JOIN #temp b ON ',
			'a.Ptn_Pk <> b.Ptn_Pk AND a.PatientId <> b.PatientId ')

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

	CREATE TABLE #potentialduplicate
	(
	   [MatchingEnrollmentNo] VARCHAR(50) NULL,
		[FirstName] VARCHAR(50) NULL,
		[MiddleName] VARCHAR(50) NULL,
		[LastName] VARCHAR(50) NULL,
		[DateOfBirth] DATETIME NULL,
		[PatientEnrollmentId] VARCHAR(50) NULL,
		[EnrollmentDate] DATETIME NULL,
		[MobileNumber] VARCHAR(50) NULL,
		[Ptn_Pk] INT NULL,
		[PatientId] INT NULL,
		[PersonId] INT NULL,
		[Sex] INT NULL,
		[IdentifierTypeId] INT NULL,
		[GroupingFilter] INT NULL
	)
	INSERT INTO #potentialduplicate
	EXECUTE sp_executesql @strSQL

	SELECT DISTINCT [MatchingEnrollmentNo],
		[FirstName],
		[MiddleName],
		[LastName],
		[DateOfBirth],
		PatientEnrollmentId = CONCAT((SELECT Code FROM Identifiers WHERE Id = IdentifierTypeId), ': ' , [PatientEnrollmentId]),
		[EnrollmentDate],
		[MobileNumber],
		[Ptn_Pk],
		[PatientId],
		[PersonId],
		[Sex] = (select ItemName from LookupItemView where MasterName = 'Gender' and ItemId = Sex),
		#potentialduplicate.[GroupingFilter] FROM #potentialduplicate
		inner join (
	SELECT GroupingFilter, COUNT(GroupingFilter) AS Expr1 
	FROM #potentialduplicate
	GROUP BY GroupingFilter
	HAVING        (COUNT(GroupingFilter) > 1)) b on b.GroupingFilter = #potentialduplicate.GroupingFilter
	order by #potentialduplicate.GroupingFilter;

END
