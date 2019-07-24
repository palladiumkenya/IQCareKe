If Not Exists(Select * from sys.columns where Name = N'RegimenUse' AND Object_ID = Object_ID(N'PatientARVHistory'))
BEGIN
ALTER  TABLE PatientARVHistory ADD RegimenUse INT      NULL;
END

go


If Not Exists(Select * from sys.columns where Name = N'Weeks' AND Object_ID = Object_ID(N'PatientARVHistory'))
BEGIN
ALTER  TABLE PatientARVHistory ADD Weeks INT      NULL;
END


If Not Exists(Select * from sys.columns where Name = N'Months' AND Object_ID = Object_ID(N'PatientARVHistory'))
BEGIN
ALTER  TABLE PatientARVHistory ADD Months INT      NULL;
END

If Not Exists(Select * from sys.columns where Name = N'InitiationDate' AND Object_ID = Object_ID(N'PatientARVHistory'))
BEGIN
ALTER  TABLE PatientARVHistory ADD InitiationDate DATETIME      NULL;
END
