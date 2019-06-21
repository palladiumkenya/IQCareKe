/****** Object:  StoredProcedure [dbo].[sp_getltfu]    Script Date: 2019-06-14 2:36:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Darius Kirui
-- Create date: 30th May 2019
-- Description:	get tx curr patients
-- =============================================
ALTER PROCEDURE [dbo].[sp_getltfu]
	-- Add the parameters for the stored procedure here
	@fromdate datetime = null,
	@ltodate datetime = null
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
	Declare @SymKey varchar(400)                                    
        Set @SymKey = 'Open symmetric key Key_CTC decryption by password='+ '''ttwbvXWpqb5WOLfLrBgisw=='''  + ''                                        
    Exec(@SymKey)
	SELECT * FROM (SELECT tv.*, row_number() over (partition by Ptn_Pk ORDER BY ExitDate DESC) as ExitNbr FROM 
	(
		SELECT nocareending.*,pce.ExitDate,li.Name AS ExitReason FROM(
			SELECT * FROM (SELECT tv.*, row_number() over (partition by Ptn_Pk ORDER BY CreateDate ASC) as RowIdNbr FROM 
			(
				SELECT id.IdentifierValue,primaryids.*,id.CreateDate
				 FROM(
					SELECT --pid.IdentifierValue AS PatientID,
					 p.personid,p.id AS PID, 
					 Convert(varchar(50), Decryptbykey(pr.FirstName)) As FirstName,
					 Convert(varchar(50), Decryptbykey(pr.MidName)) As MidName,
					 Convert(varchar(50), Decryptbykey(pr.LastName)) As LastName,
					 Convert(varchar(50), Decryptbykey(pc.MobileNumber)) As MobileNumber,
					 expectedreturnresults.*,DATEDIFF(DAY,expectedreturnresults.ExpectedReturn,@ltodate) AS Due,
					 DATEADD(DAY,30,expectedreturnresults.ExpectedReturn) AS DefaultingDay,
					 CASE WHEN pt.TracingStatus = 1 THEN 'Yes' ELSE 'No' END AS TracingStatus
					  FROM(
						SELECT endperiodresults.*, DATEADD(DAY,endperiodresults.Duration,endperiodresults.DispensedByDate) AS ExpectedReturn FROM (
						SELECT * FROM (SELECT tv.*, row_number() over (partition by Ptn_Pk ORDER BY Ptn_PK ASC, DispensedByDate DESC, Duration DESC) as RowNbr FROM 
						(
							SELECT ord.ptn_pharmacy_pk,ord.Ptn_pk,ord.OrderedBy,ord.OrderedByDate,ord.DispensedByDate,md.Abbreviation,dtl.GenericID,dtl.Duration FROM
							ord_PatientPharmacyOrder ord LEFT JOIN
							dtl_PatientPharmacyOrder dtl ON ord.ptn_pharmacy_pk = dtl.ptn_pharmacy_pk
							INNER JOIN Mst_Drug md ON md.Drug_pk = dtl.Drug_Pk
							WHERE dtl.ptn_pharmacy_pk IS NOT NULL AND dtl.Prophylaxis = 0 AND ord.DispensedByDate <= @ltodate
						)
						tv )source 
						WHERE RowNbr = 1
						)endperiodresults
					)expectedreturnresults LEFT JOIN Patient p ON expectedreturnresults.Ptn_pk = p.ptn_pk
					INNER JOIN Person pr ON pr.Id = p.Id
					LEFT JOIN PersonContact pc ON pc.PersonId = pr.Id
					LEFT JOIN PatientTracing pt ON p.Id = pt.PatientId
				)primaryids 
				LEFT JOIN PatientIdentifier id ON primaryids.PID = id.PatientId
				WHERE Due >= 91 and (DefaultingDay >= @fromdate and DefaultingDay <= @ltodate)
			)
			tv )source 
			WHERE RowIdNbr = 1
		)nocareending LEFT JOIN PatientCareEnding pce ON nocareending.PID = pce.PatientId
		LEFT JOIN LookupItem li ON li.Id = pce.ExitReason
		WHERE pce.ExitDate < nocareending.DispensedByDate OR pce.ExitDate IS NULL OR pce.ExitDate >= @ltodate
	)
	tv )source 
	WHERE ExitNbr = 1 
	Close symmetric key Key_CTC 
End








