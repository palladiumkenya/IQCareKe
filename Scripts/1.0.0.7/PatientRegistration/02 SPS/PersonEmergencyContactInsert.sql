

/****** Object:  StoredProcedure [dbo].[PatientTreatmentSupporter_Insert]    Script Date: 5/7/2018 8:12:47 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author: Felix
-- Create date: 28-Apr-2017
-- Description: Insert into Patient Treatment Supporter
-- =============================================
CREATE PROCEDURE [dbo].[PersonEmergencyContact_Insert] 
 -- Add the parameters for the stored procedure here
 @PersonId int, 
 @EmergencyContactPersonId int,
 @MobileContact varbinary(max),
 @CreatedBy int,
 @ContactType int,
 @RegisteredToClinic bit
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

 IF @MobileContact IS NULL
  SET @MobileContact = NULL;
 ELSE
  SET @MobileContact = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@MobileContact);

    -- Insert statements for procedure here
 INSERT INTO PersonEmergencyContact([PersonId], [EmergencyContactPersonId], [MobileContact], [DeleteFlag], [CreatedBy], [CreateDate],ContactType,RegisteredToClinic)
 VALUES(@PersonId,  @EmergencyContactPersonId, @MobileContact, 0, @CreatedBy, GETDATE(),@ContactType,@RegisteredToClinic);

 SELECT SCOPE_IDENTITY() Id;
END




GO


