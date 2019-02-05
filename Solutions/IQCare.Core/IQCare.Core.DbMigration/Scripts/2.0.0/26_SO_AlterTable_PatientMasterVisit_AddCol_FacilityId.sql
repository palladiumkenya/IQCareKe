IF NOT EXISTS (SELECT *  FROM sys.all_columns  WHERE name = 'FacilityId' AND Object_ID = OBJECT_ID(N'PatientMasterVisit'))
BEGIN 
ALTER TABLE PatientMasterVisit  ADD  FacilityId INT  NULL;
END
