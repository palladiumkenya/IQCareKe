IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientType_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PatientType_Update]
GO

/****** Object:  StoredProcedure [dbo].[PatientType_Update]    Script Date: 2/27/2018 11:09:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PatientType_Update]
	-- Add the parameters for the stored procedure here
	@PatientId int,
	@PatientType int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update Patient set PatientType = @PatientType where Id = @PatientId
END

GO


