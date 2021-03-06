/****** Object:  StoredProcedure [dbo].[Ord_Visit_Insert]    Script Date: 6/27/2019 11:12:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: Felix Kithinji
-- Create date: 15-Mar-2017
-- Description: Insert
-- =============================================
ALTER PROCEDURE [dbo].[Ord_Visit_Insert]
 -- Add the parameters for the stored procedure here
 @Ptn_Pk int,
 @LocationID int,
 @VisitDate datetime,
 @VisitType int,
 @UserID int,
 @CreateDate datetime,
 @ModuleId int
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

 DECLARE @Id int

    -- Insert statements for procedure here
 Insert Into ord_Visit(Ptn_Pk, LocationID, VisitDate, VisitType, DeleteFlag, UserID, CreateDate, CreatedBy, ModuleId)
 Values(
  @Ptn_Pk,
  @LocationID,
  @VisitDate,
  @VisitType,
  0,
  @UserID,
  @CreateDate,
  @UserID,
  @ModuleId
 );
SELECT SCOPE_IDENTITY() Id;
END


