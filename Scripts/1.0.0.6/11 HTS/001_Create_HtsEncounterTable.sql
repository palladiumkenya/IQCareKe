IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'HtsEncounter')
CREATE TABLE dbo.HtsEncounter
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

	[PersonId] INT NOT NULL 
		CONSTRAINT FK_HtsEncounter_PersonId_Person_Id 
		FOREIGN KEY REFERENCES dbo.person([Id]),

	[ProviderId] INT NOT NULL,

	[PatientEncounterID] INT NOT NULL
		CONSTRAINT FK_HtsEncounter_PatientEncounterID_PatientEncounter_Id
		FOREIGN KEY REFERENCES dbo.PatientEncounter([Id]),

	[EverTested] INT NOT NULL,
	
	[MonthsSinceLastTest] INT NULL,

	[MonthSinceSelfTest] INT NOT NULL DEFAULT 0,

	[TestedAs] INT NOT NULL,

	[TestingStrategy] INT NOT NULL,

	[EncounterRemarks] VARCHAR(max) NULL,

	[FinalResultGiven] INT NULL,

	[CoupleDiscordant] INT NULL,

	[TestEntryPoint] INT NOT NULL,

	[Consent] INT NOT NULL,

	[EverSelfTested] INT NOT NULL,

	[GeoLocation] VARCHAR(200) NOT NULL
)

ELSE
	ALTER TABLE dbo.HtsEncounter 
		ALTER COLUMN [TestEntryPoint] INT NOT NULL