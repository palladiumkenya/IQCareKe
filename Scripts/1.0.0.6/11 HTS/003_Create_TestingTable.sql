CREATE TABLE Testing
(
	[Id] INT NOT NULL IDENTITY(1,1) 
		 PRIMARY KEY,

	[HtsEncounterId] INT NOT NULL
		CONSTRAINT FK_Testing_HtsEncounterId_HtsEncounter_Id
		FOREIGN KEY REFERENCES dbo.HtsEncounter([Id]), 

	[ProviderId] INT NOT NULL,

	[KitId] INT NOT NULL,

	[KitLotNumber] NVARCHAR(300),

	[ExpiryDate] DATETIME,

	[Outcome] INT NOT NULL,
	
	[TestRound] INT NOT NULL,
	
)