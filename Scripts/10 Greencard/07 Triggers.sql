CREATE TRIGGER [dbo].[Insert_Results_LabTracker]
ON dbo.dtl_LabOrderTestResult

 AFTER UPDATE
AS
    IF UPDATE(ResultValue) OR UPDATE(ResultText)
    BEGIN
      UPDATE c
            SET ResultValues = isnull(i.ResultValue,0),
          ResultTexts = i.ResultText,
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
         );
    END ;
GO