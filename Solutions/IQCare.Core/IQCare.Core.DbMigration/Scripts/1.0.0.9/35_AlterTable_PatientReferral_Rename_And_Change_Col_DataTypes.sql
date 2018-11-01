
IF (EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_NAME = 'PatientReferral'))
BEGIN
ALTER TABLE PatientReferral ALTER COLUMN ReferredTo INT NULL 

ALTER TABLE PatientReferral ALTER COLUMN ReferralReason VARCHAR(250) NULL 

ALTER TABLE PatientReferral ALTER COLUMN ReferralDate DATETIME NULL 

ALTER TABLE PatientReferral ALTER COLUMN ReferredBy INT NULL 

  EXEC sp_rename 'PatientReferral', 'PmtctReferral';
END
