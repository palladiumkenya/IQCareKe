USE [IQCareDefault]
GO
/****** Object:  Trigger [dbo].[GenerateLabOrderNumber]    Script Date: 4/28/2017 9:13:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  <Patrick Wangoo>[dbo].[ord_LabOrder]
-- Create date: 2017 March 17
-- Description: Generate Order Number on insert
-- =============================================
ALTER TRIGGER [dbo].[GenerateLabOrderNumber]    ON  [dbo].[ord_LabOrder]  AFTER INSERT
AS 
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

    -- Insert statements for trigger here
    Declare @RegYear int, @NewPtn_Pk int, @MaxLabOrderId varchar(10);

 Declare @table table(RegYear int, PatientId int);

 Insert Into @table(RegYear, PatientId)

 Select  datepart(Year,I.OrderDate), I.Ptn_Pk From Inserted I  ;
 While Exists(Select 1 From @table) Begin
    Select Top 1 @RegYear = t.RegYear, @NewPtn_Pk=t.PatientId From @table t  ;
    Select @MaxLabOrderId= max(Convert(varchar,Replace(OrderNumber,'-',''))) From 
  ord_LabOrder Where  OrderNumber Like Convert(varchar,@RegYear)+'-%'
  If(@MaxLabOrderId Is Null)
   Select @MaxLabOrderId = Convert(varchar(4), @RegYear)  + Replicate('0', 5) + Convert(varchar, 1);
  Else
   Select @MaxLabOrderId = Convert(int,@MaxLabOrderId)+1;
 
  Select @MaxLabOrderId = Stuff(@MaxLabOrderId,5,0,'-') ;
 
  Update ord_LabOrder Set OrderNumber = @MaxLabOrderId Where Ptn_Pk =@NewPtn_Pk And OrderNumber Is Null
  Delete From @table Where  PatientId = @NewPtn_Pk;

  

 End
    
 
    
END
