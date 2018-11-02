If Not Exists(Select * from sys.columns where Name = N'NotesCategoryId' AND Object_ID = Object_ID(N'PatientClinicalNotes'))
BEGIN
	ALTER TABLE PatientClinicalNotes ADD NotesCategoryId int
END
GO

If Not Exists(Select * from sys.columns where Name = N'Active' AND Object_ID = Object_ID(N'PatientClinicalNotes'))
BEGIN
	ALTER TABLE PatientClinicalNotes ADD Active Bit NOT NULL
END
GO

UPDATE PatientClinicalNotes SET Active = 0

ALTER TABLE PatientClinicalNotes ALTER COLUMN Active Bit NOT NULL