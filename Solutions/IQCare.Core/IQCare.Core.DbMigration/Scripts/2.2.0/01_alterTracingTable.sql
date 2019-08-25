ALTER TABLE Tracing
ADD TracingDateOfDeath  DateTime,
 TracingTransferFacility  VarChar(255),
 TracingTransferDate DateTime;

 ALTER TABLE Tracing
ADD PatientMasterVisitId int;