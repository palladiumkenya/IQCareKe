If Not Exists(Select * from sys.columns where Name = N'NotesCategoryId' AND Object_ID = Object_ID(N'PatientClinicalNotes'))
BEGIN
	ALTER TABLE PatientClinicalNotes ADD NotesCategoryId int
END
GO

If Not Exists(Select * from sys.columns where Name = N'Active' AND Object_ID = Object_ID(N'PatientClinicalNotes'))
BEGIN
	ALTER TABLE PatientClinicalNotes ADD Active Bit
END
GO