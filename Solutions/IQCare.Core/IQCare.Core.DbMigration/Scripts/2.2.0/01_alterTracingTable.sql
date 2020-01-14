





IF NOT EXISTS(SELECT  1 FROM sys.columns 
          WHERE Name = N'TracingDateOfDeath'
          AND Object_ID = Object_ID(N'Tracing'))
		  BEGIN
ALTER TABLE Tracing
ADD TracingDateOfDeath  DateTime
END


IF NOT EXISTS(SELECT  1 FROM sys.columns 
          WHERE Name = N'TracingTransferFacility'
          AND Object_ID = Object_ID(N'Tracing'))
		  BEGIN
 ALTER TABLE Tracing
ADD TracingTransferFacility  VarChar(255)
END


IF NOT EXISTS(SELECT  1 FROM sys.columns 
          WHERE Name = N'TracingTransferDate'
          AND Object_ID = Object_ID(N'Tracing'))
		  BEGIN
 ALTER TABLE Tracing ADD
 TracingTransferDate DateTime;
 END


 
IF NOT EXISTS(SELECT  1 FROM sys.columns 
          WHERE Name = N'PatientMasterVisitId'
          AND Object_ID = Object_ID(N'Tracing'))
		  BEGIN
 ALTER TABLE Tracing
ADD PatientMasterVisitId int
END

