USE [IQCareV1]
GO
/****** Object:  StoredProcedure [dbo].[pr_Admin_UpdateFacility_Constella]    Script Date: 31/10/2018 14:43:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[pr_Admin_UpdateFacility_Constella] (
	@FacilityName VARCHAR(50)
	,@CountryID VARCHAR(10)
	,@PosID VARCHAR(10)
	,@SatelliteID VARCHAR(10)
	,@NationalID VARCHAR(10)
	,@ProvinceId INT
	,@DistrictId INT
	,@Image VARCHAR(50)
	,@Currency VARCHAR(50)
	,@AppGracePeriod INT
	,@DateFormat VARCHAR(20)
	,@PepFarStartDate DateTime
	,@Status INT
	,@SystemId INT
	,@Preferred INT
	,@Paperless INT
	,@UserID INT
	,@FacilityID INT
	,@FacilityLogo VARCHAR(50)
	,@FacilityAddress VARCHAR(200)
	,@FacilityTel VARCHAR(50)
	,@FacilityCell VARCHAR(50)
	,@FacilityFax VARCHAR(50)
	,@FacilityEmail VARCHAR(50)
	,@FacilityURL VARCHAR(50)
	,@FacilityFooter VARCHAR(500)
	,@FacilityTemplate INT
	,@StrongPassword INT
	,@ExpirePaswordFlag INT
	,@ExpirePaswordDays VARCHAR(50)
	,@DateConstraint INT = NULL
	,@Billing INT = NULL
	,@PMSCM INT = NULL
	,@Wards INT = NULL
	,@LMIS INT = NULL
	)
AS
BEGIN
	IF EXISTS (
			SELECT *
			FROM mst_Facility
			WHERE FacilityName = @FacilityName
				AND FacilityId <> @FacilityId
			)
		RETURN 0

	IF @Status = 1
	BEGIN
		DECLARE @PCount INT

		SELECT @PCount = count(ptn_pk)
		FROM mst_patient
		WHERE (
				deleteflag = 0
				OR deleteflag IS NULL
				)
			AND locationid = @FacilityId

		IF @PCount > 0
		BEGIN
			RAISERROR (
					'Facility containing Patient records cannot be Inactivated.'
					,11
					,1
					)

			RETURN
		END
	END

	UPDATE [mst_Facility]
	SET [FacilityName] = @FacilityName
		,[CountryID] = @CountryID
		,[PosID] = @PosID
		,[SatelliteID] = @SatelliteID
		,[Currency] = @Currency
		,[AppGracePeriod] = @AppGracePeriod
		,[NationalID] = @NationalID
		,[ProvinceId] = @ProvinceId
		,[DistrictId] = @DistrictId
		,[DateFormat] = @DateFormat
		,[PepFarStartDate] = Nullif(@PepFarStartDate, '01-Jan-1900')
		,[DeleteFlag] = @Status
		,[SystemId] = @SystemId
		,[Preferred] = @Preferred
		,[Paperless] = @Paperless
		,[UserID] = @UserID
		,[UpdateDate] = getdate()
		,[FacilityAddress] = @FacilityAddress
		,[FacilityTel] = @FacilityTel
		,[FacilityCell] = @FacilityCell
		,[FacilityFax] = @FacilityFax
		,[FacilityEmail] = @FacilityEmail
		,[FacilityURL] = @FacilityURL
		,[FacilityFooter] = @FacilityFooter
		,[FacilityTemplate] = @FacilityTemplate
		,[FacilityLogo] = @FacilityLogo
		,[StrongPassFlag] = @StrongPassword
		,[ExpPwdFlag] = @ExpirePaswordFlag
		,[ExpPwdDays] = @ExpirePaswordDays
		,[DateConstraint] = @DateConstraint
		,[Billing] = @Billing
		,[PMSCM] = @PMSCM
		,Wards = @Wards
		,LMIS = @LMIS
	WHERE ([FacilityID] = @FacilityID)

	IF @Image != ''
		UPDATE mst_facility
		SET IMAGE = @Image

	----- One Time Activity.....................
	IF EXISTS (
			SELECT Distinct mp.Ptn_Pk
			FROM mst_Patient mp
			JOIN mst_Facility mf ON mp.LocationID = mf.FacilityID
			WHERE mp.CountryId <> @CountryID
				AND mf.FacilityID = @FacilityID
				AND mp.STATUS = 0
				AND ISNULL(mf.DeleteFlag, 0) = 0
				AND ISNULL(mp.DeleteFlag, 0) = 0
			)
	BEGIN
		UPDATE p
		SET P.CountryID = @CountryID
		FROM mst_Patient P
		JOIN mst_Facility F ON P.LocationID = F.FacilityID
		WHERE P.LocationID = @FacilityID And P.CountryId <> @CountryID
	END
END


