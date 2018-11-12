IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Pharmacy_GetPharmacyPrescriptions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Pharmacy_GetPharmacyPrescriptions]
GO


/****** Object:  StoredProcedure [dbo].[pr_Pharmacy_GetPharmacyPrescriptions]    Script Date: 9/17/2018 11:24:19 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--exec pr_Pharmacy_GetPharmacyPrescriptions 1018, '''ttwbvXWpqb5WOLfLrBgisw=='''
CREATE PROCEDURE [dbo].[pr_Pharmacy_GetPharmacyPrescriptions] @LocationID INT = NULL
	,@DBKey VARCHAR(50)
AS
BEGIN
	DECLARE @SymKey VARCHAR(400)

	SET @SymKey = 'Open symmetric key Key_CTC decryption by password=' + @DBKey + ''

	EXEC (@SymKey)

	DECLARE @allIDs AS VARCHAR(max)

	SELECT @allIDs = stuff((
				SELECT ',Case When Cast(mst.[' + cast(fieldName AS VARCHAR(100)) + '] as varchar(50)) = '''' Then Null Else Cast(mst.[' + cast(fieldName AS VARCHAR(100)) + '] as varchar(50)) End '
				FROM mst_patientidentifier
				FOR XML PATH('')
				), 1, 1, '')

	DECLARE @LocationID_var VARCHAR(10) = (
			SELECT cast(@LocationID AS VARCHAR)
			)

	EXEC (
			'



select ord.Ptn_pk, Cast(coalesce(' + @allIDs + ') as varchar(50)) as PatientID, 



convert(varchar(50), decryptbykey(mst.firstname))[fname],convert(varchar(50), decryptbykey(mst.lastname))[lname],



CONVERT(varchar(15), CAST(ord.CreateDate as time),100)[OrderedByDate],







CONVERT(int,ROUND(DATEDIFF(hour,mst.DOB,GETDATE())/8766.0,0)) AS Age,



case when lr.ptn_pk is null then ''Available'' else ''Being attended to by ''+lr.UserName end as [Status], ord.VisitID



from ord_PatientPharmacyOrder ord inner JOIN mst_Patient mst on ord.Ptn_pk = mst.Ptn_Pk



inner JOIN ord_Visit ordV ON ord.VisitID = ordV.Visit_Id



left join dtl_PatientLockedRecordsForDispensing lr on ord.Ptn_pk = lr.ptn_pk



where 
--DispensedByDate is NULL and 
(DispensedBy=0 or DispensedBy is NULL) and ord.LocationID=' + @LocationID_var + 
			' 



and (mst.DeleteFlag = 0 OR mst.DeleteFlag IS NULL) and (ordV.DeleteFlag is NULL OR ordV.DeleteFlag = 0)



and CONVERT(VARCHAR(24),ord.OrderedByDate,106) = CONVERT(VARCHAR(24),GETDATE(),106)



order BY ord.CreateDate asc



'
			)
END
GO


