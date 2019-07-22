-- =============================================
CREATE PROCEDURE sp_UpdateBlueCardBaselineTransferInTreatment
	-- Add the parameters for the stored procedure here
	@ptn_pk int,
	@TransferInDate datetime,
	@TreatmentStartDate datetime,
	@CurrentTreatment int,
	@FacilityFrom varchar(50),
	@CountyFrom int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS(select * from dtl_PatientHivPrevCareIE where ARTTransferInDate is not null AND Ptn_pk = @ptn_pk AND ARTTransferInDate <> '1900-01-01')
	BEGIN
		UPDATE PCE
		SET ARTTransferInDate = @TransferInDate,
		FromDistrict = (SELECT TOP 1 CountyName FROM County WHERE CountyId = @CountyFrom),
		ARTTransferInFrom = @FacilityFrom
		FROM dtl_PatientHivPrevCareIE PCE
		WHERE PCE.Ptn_pk = @ptn_pk;

		UPDATE mst_Patient SET DateTransferredin = @TransferInDate WHERE Ptn_Pk = @ptn_pk;
	END

	IF EXISTS(SELECT *  FROM dtl_PatientHivPrevCareEnrollment WHERE ptn_pk = @ptn_pk)
	BEGIN
		UPDATE PPC
		SET PPC.ARTStartDate = @TreatmentStartDate
		FROM dtl_PatientHivPrevCareEnrollment PPC
		WHERE PPC.ptn_pk = @ptn_pk;
	END

	IF EXISTS(SELECT * FROM dtl_PatientBlueCardPriorART WHERE Ptn_pk = @ptn_pk)
	BEGIN
		UPDATE BPA
		SET BPA.Regimen = (select TOP 1 DisplayName from LookupItem where Id= @CurrentTreatment)
		FROM dtl_PatientBlueCardPriorART BPA
		WHERE BPA.Ptn_pk = @ptn_pk;
	END
END
