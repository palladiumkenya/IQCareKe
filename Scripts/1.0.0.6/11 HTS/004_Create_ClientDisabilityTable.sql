CREATE TABLE ClientDisability
(
	[Id] INT NOT NULL IDENTITY(1,1)
		PRIMARY KEY,

	[PersonId] INT NOT NULL 
		CONSTRAINT FK_ClientDisability_PersonId_Person_Id
		FOREIGN KEY REFERENCES dbo.person([Id]),

	[PatientEncounterID] INT NOT NULL
		CONSTRAINT FK_ClientDisability_PatientEncounterID_PatientEncounter_Id
		FOREIGN KEY REFERENCES dbo.PatientEncounter([Id]),

	[DisabilityId] INT NOT NULL
)