IF NOT EXISTS (SELECT 1  FROM   INFORMATION_SCHEMA.COLUMNS  WHERE  TABLE_NAME = 'PatientDelivery'
                      AND COLUMN_NAME = 'PregnancyId'
                      AND TABLE_SCHEMA='dbo')
  BEGIN
	ALTER TABLE PatientDelivery ADD PregnancyId INT NULL;
	ALTER TABLE PatientDelivery ADD CONSTRAINT FK_PatientDelivery_PregnancyId FOREIGN KEY(PregnancyId) REFERENCES Pregnancy(Id);  END
GO


