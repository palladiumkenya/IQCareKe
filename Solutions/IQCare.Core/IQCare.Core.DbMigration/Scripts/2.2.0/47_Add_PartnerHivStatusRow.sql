IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PatientPartnerProfile' AND COLUMN_NAME = 'PartnerHivStatus')
 BEGIN
 BEGIN ALTER TABLE  PatientPartnerProfile ADD  [PartnerHivStatus] [int] NULL END; 
 END





