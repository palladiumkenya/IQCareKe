
/****** Object:  StoredProcedure [dbo].[sp_getdefaulters]    Script Date: 2019-07-12 12:52:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Darius Kirui
-- Create date: 30th May 2019
-- Description:	get tx curr patients
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[sp_getdefaulters]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [sp_getdefaulters]
GO
CREATE PROCEDURE [dbo].[sp_getdefaulters]
	-- Add the parameters for the stored procedure here
	@ftodate datetime = null,
	@minfdefault int = null,
	@maxfdefault int = null
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
	Declare @SymKey varchar(400)                                    
        Set @SymKey = 'Open symmetric key Key_CTC decryption by password='+ '''ttwbvXWpqb5WOLfLrBgisw=='''  + ''                                        
    Exec(@SymKey) 
	SELECT IdentifierValue AS CCCNumber,Ptn_pk AS PatientPK,FirstName,MidName AS MiddleName, LastName,MobileNumber,DispensedByDate As DispenseDate,
	Abbreviation AS Drug,ExpectedReturn,Due AS DaysOverDue, TracingStatus AS Traced,PID,PersonId
	 FROM (SELECT tv.*, row_number() over (partition by Ptn_Pk ORDER BY ExitDate DESC) as ExitNbr FROM 
	(
		SELECT nocareending.*,pce.ExitDate,li.Name AS ExitReason FROM(
			SELECT * FROM (SELECT tv.*, row_number() over (partition by Ptn_Pk ORDER BY CreateDate ASC) as RowIdNbr FROM 
			(
				SELECT id.IdentifierValue,primaryids.*,id.CreateDate
				 FROM(
					SELECT --pid.IdentifierValue AS PatientID,
					 p.personid, p.id AS PID,
					 Convert(varchar(50), Decryptbykey(pr.FirstName)) As FirstName,
					 Convert(varchar(50), Decryptbykey(pr.MidName)) As MidName,
					 Convert(varchar(50), Decryptbykey(pr.LastName)) As LastName,
					 Convert(varchar(50), Decryptbykey(pc.MobileNumber)) As MobileNumber,
					 expectedreturnresults.*,DATEDIFF(DAY,expectedreturnresults.ExpectedReturn,@ftodate) AS Due,
					 CASE WHEN pt.PatientMasterVisitId > 1 THEN 'Yes' ELSE 'No' END AS TracingStatus
					 FROM(
						SELECT endperiodresults.*, DATEADD(DAY,endperiodresults.Duration,endperiodresults.DispensedByDate) AS ExpectedReturn FROM (
						SELECT * FROM (SELECT tv.*, row_number() over (partition by Ptn_Pk ORDER BY Ptn_PK ASC, DispensedByDate DESC, Duration DESC) as RowNbr FROM 
						(
							--SELECT ord.ptn_pharmacy_pk,ord.Ptn_pk,ord.OrderedBy,ord.OrderedByDate,ord.DispensedByDate,md.Abbreviation,dtl.GenericID,dtl.Duration FROM
							--ord_PatientPharmacyOrder ord LEFT JOIN
							--dtl_PatientPharmacyOrder dtl ON ord.ptn_pharmacy_pk = dtl.ptn_pharmacy_pk
							--INNER JOIN Mst_Drug md ON md.Drug_pk = dtl.Drug_Pk
							--WHERE dtl.ptn_pharmacy_pk IS NOT NULL AND dtl.Prophylaxis = 0 AND ord.DispensedByDate <= @ftodate

							SELECT --mdc.Name,mdcd.Name,md.DrugName,dtl.Prophylaxis,
							ord.ptn_pharmacy_pk,ord.Ptn_pk,ord.OrderedBy,ord.OrderedByDate,ord.DispensedByDate,md.Abbreviation,dtl.GenericID,dtl.Duration FROM
							ord_PatientPharmacyOrder ord LEFT JOIN
							dtl_PatientPharmacyOrder dtl ON ord.ptn_pharmacy_pk = dtl.ptn_pharmacy_pk
							INNER JOIN Mst_Drug md ON md.Drug_pk = dtl.Drug_Pk
							LEFT JOIN Mst_Decode mdc  ON ord.ProgID = mdc.Id
							LEFT JOIN Mst_Decode mdcd ON ord.OrderType = mdcd.Id
							WHERE dtl.ptn_pharmacy_pk IS NOT NULL --AND dtl.Prophylaxis = 0 
							AND ord.DispensedByDate <= @ftodate
							--and ((mdc.Name = 'ART' and mdcd.Name != 'Paediatric') OR (mdc.Name = 'PMTCT' and mdcd.Name != 'Paediatric')) 
							--and (md.DrugName != 'Sulfa/TMX-Cotrimoxazole 200mg/40mg/5ml' OR md.DrugName != 'Nevirapine-NVP10mg/ml 10mg/ml' OR md.Abbreviation != 'NVP')
							and (md.Abbreviation != 'NVP' AND md.Abbreviation IS NOT NULL)
						)
						tv )source 
						WHERE RowNbr = 1
						)endperiodresults
					)expectedreturnresults LEFT JOIN Patient p ON expectedreturnresults.Ptn_pk = p.ptn_pk
					INNER JOIN Person pr ON pr.Id = p.PersonId
					LEFT JOIN PersonContact pc ON pc.PersonId = pr.Id
					LEFT JOIN Tracing pt ON p.personid = pt.PersonID
				)primaryids 
				LEFT JOIN PatientIdentifier id ON primaryids.PID = id.PatientId
				WHERE Due >= @minfdefault and Due < = @maxfdefault
			)
			tv )source 
			WHERE RowIdNbr = 1
		)nocareending LEFT JOIN 
		(
			SELECT * FROM (SELECT tv.*, row_number() over (partition by PatientID ORDER BY ExitDate DESC) as RowNbr FROM 
			(
				SELECT * FROM PatientCareEnding 
			)
			tv )source 
			WHERE RowNbr = 1 
		)
		pce ON nocareending.PID = pce.PatientId
		LEFT JOIN LookupItem li ON li.Id = pce.ExitReason
		WHERE pce.ExitDate < nocareending.DispensedByDate OR pce.ExitDate IS NULL OR pce.ExitDate >= @ftodate
	)
	tv )source 
	WHERE ExitNbr = 1
	
	Close symmetric key Key_CTC   
End








