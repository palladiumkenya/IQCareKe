USE [IQCare_LVCT_356]
GO

/****** Object:  StoredProcedure [dbo].[PatientTreatmentSupporter_Update]    Script Date: 5/7/2018 8:30:19 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO


CREATE PROCEDURE [dbo].[PersonEmergencyContact_Update] 
	-- Add the parameters for the stored procedure here
	@PersonId int, 
	@EmergencyContactPersonId int,
	@MobileContact varbinary(max),
	@CreatedBy int,
	@DeleteFlag bit,
	@Id int,
	@RegisteredToClinic bit,
	@ContactType int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	 IF @MobileContact IS NULL
	  SET @MobileContact = NULL;
	 ELSE
	  SET @MobileContact = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@MobileContact);

	  UPDATE PersonEmergencyContact
	  SET EmergencyContactPersonId = @EmergencyContactPersonId, PersonId = @PersonId, MobileContact = @MobileContact , CreatedBy = @CreatedBy, DeleteFlag = @DeleteFlag,ContactType=@ContactType,RegisteredToClinic=@RegisteredToClinic
	  WHERE Id = @Id

	  SELECT SCOPE_IDENTITY() Id;
END



GO


