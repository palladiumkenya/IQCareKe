IF (NOT EXISTS (SELECT *  FROM sys.all_columns  WHERE name = 'AuditData'))
BEGIN 
ALTER TABLE PatientMiIlestone  ADD  AuditData [xml] NULL;
END