/***** Object:  Trigger [dbo].[Insert_Results_LabTracker]    Script Date: 5/26/2017 8:57:46 AM *****/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Insert_Results_LabTracker]') AND type in (N'T'))
DROP TRIGGER [dbo].[Insert_Results_LabTracker]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:  <pwangoo>
-- Create date: <20042017>
-- Description: <Insert labresults to patientlab tracker>
-- =============================================


CREATE TRIGGER [dbo].[Insert_Results_LabTracker]
ON [dbo].[dtl_LabOrderTestResult]

 AFTER UPDATE
AS
    IF UPDATE(ResultValue) OR UPDATE(ResultText) OR UPDATE(DetectionLimit) OR UPDATE(Undetectable)
    BEGIN
      UPDATE c
            SET ResultValues = COALESCE(i.ResultValue,i.DetectionLimit,50),
          ResultTexts = i.ResultText,
    ResultOptions = i.ResultOption,
       ResultDate = i.StatusDate,
    ResultUnits = i.ResultUnit,
          Results = 'Complete'    
        FROM PatientLabTracker AS c
          JOIN inserted AS i
            ON i.LabOrderTestId = c.LabOrderTestId     -- use the appropriate column for joining
          JOIN deleted AS d
            ON  i.Id = d.Id
    AND (( i.ResultValue <> d.ResultValue
    OR d.ResultValue IS NULL)
    OR ( i.ResultText <> d.ResultText
    OR d.ResultText IS NULL)
    OR ( i.ResultOption <> d.ResultOption
    OR d.ResultOption IS NULL)
 OR ( i.DetectionLimit <> d.DetectionLimit
    OR d.DetectionLimit IS NULL)
 OR ( i.Undetectable <> d.Undetectable
    OR d.Undetectable IS NULL)
         );
    END ;
	
/***** Object:  Trigger [dbo].[GenerateLabOrderNumber]    Script Date: 5/26/2017 8:57:46 AM *****/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GenerateLabOrderNumber]') AND type in (N'T'))
DROP TRIGGER [dbo].[GenerateLabOrderNumber]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  <Patrick Wangoo>[dbo].[ord_LabOrder]
-- Create date: 2017 March 17
-- Description: Generate Order Number on insert
-- =============================================
CREATE TRIGGER [dbo].[GenerateLabOrderNumber]    ON  [dbo].[ord_LabOrder]  AFTER INSERT
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
