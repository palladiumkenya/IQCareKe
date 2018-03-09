CREATE TABLE dbo.HtsEncounterResult
(
	[Id] INT NOT NULL IDENTITY(1,1) 
		 PRIMARY KEY,

	[HtsEncounterId] INT NOT NULL
		CONSTRAINT FK_HtsEncounterResult_HtsEncounterId_HtsEncounter_Id
		FOREIGN KEY REFERENCES dbo.HtsEncounter([Id]),

	[RoundOneTestResult] INT NOT NULL,

	[RoundTwoTestResult] INT,

	[FinalResult] INT
)