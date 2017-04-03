-- ================================================
-- Template generated from Template Explorer using:
-- Create Trigger (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- See additional Create Trigger templates for more
-- examples of different Trigger statements.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
USE [IQCareDefault]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Patrick Wangoo>
-- Create date: 2017 March 17
-- Description:	Generate Order Number on insert
-- =============================================
ALTER TRIGGER [dbo].[GeneratePatientFacilityID]    ON  [dbo].[mst_Patient]   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
    Declare @RegYear int, @NewPtn_Pk int, @MaxFacilityID varchar(10);

	Declare @table table(RegYear int, PatientId int);
	Insert Into @table(RegYear, PatientId)
	Select  datepart(Year,I.RegistrationDate), I.Ptn_Pk From Inserted I  ;
	While Exists(Select 1 From @table) Begin
	   Select Top 1 @RegYear = t.RegYear, @NewPtn_Pk=t.PatientId From @table t  ;
	   Select @MaxFacilityID= max(Convert(varchar,Replace(PatientFacilityID,'-',''))) From 
		mst_Patient Where  PatientFacilityID Like Convert(varchar,@RegYear)+'-%'
		If(@MaxFacilityID Is Null)
			Select @MaxFacilityID = Convert(varchar(4), @RegYear)  + Replicate('0', 5) + Convert(varchar, 1);
		Else
			Select @MaxFacilityID = Convert(int,@MaxFacilityID)+1;
	
		Select @MaxFacilityID = Stuff(@MaxFacilityID,5,0,'-')	;
	
		Update mst_Patient Set PatientFacilityID = @MaxFacilityID Where Ptn_Pk =@NewPtn_Pk And PatientFacilityID Is Null;

		Delete From @table Where  PatientId = @NewPtn_Pk;

	End
    
 
    
END

