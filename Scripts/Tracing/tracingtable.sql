IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientTracing]') AND type in (N'U'))
	CREATE TABLE PatientTracing(
		TracingId INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
		PatientId int NOT NULL, 
		PatientMasterVisitId int NOT NULL, 
		TracingDate DateTime,
		TracingMethod int, 
		TracingOutcome int, 
		TracingOutcomeOther VarChar(255),
		TracingDateOfDeath DateTime, 
		TracingTransferFacility VarChar(255), 
		TracingTransferDate DateTime,
		TracingNotes VarChar(255), 
		TracingStatus int,
		CreateDate DateTime, 
		CraetedBy int
	)
GO