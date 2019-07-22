-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Felix
-- Create date: 05-Jul-2019
-- Description:	Delete Errorneous lab order
-- =============================================
CREATE PROCEDURE PatientLabTracker_Delete_LabOrder 
	-- Add the parameters for the stored procedure here
	@LabOrderId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE FROM PatientLabTracker WHERE LabOrderId = @LabOrderId;
END
GO
