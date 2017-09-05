 /***** Object:  Trigger [dbo].[Insert_Results_LabTracker]    Script Date: 5/26/2017 8:57:46 AM *****/
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[Insert_Results_LabTracker]') )
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
	

