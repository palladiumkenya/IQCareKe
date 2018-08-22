/*If Not Exists(Select * from sys.columns where Name = N'FacilityId' AND Object_ID = Object_ID(N'Person'))
BEGIN
ALTER TABLE Person ADD  FacilityId int null
END

go*/

If Not Exists(Select * from sys.columns where Name = N'IndexPersonId' AND Object_ID = Object_ID(N'PersonRelationship'))
BEGIN
alter table PersonRelationship add  IndexPersonId int null
END


go


If Not Exists(Select * from sys.columns where Name = N'ConsentReason' AND Object_ID = Object_ID(N'PatientConsent'))
BEGIN
ALTER TABLE PatientConsent ADD ConsentReason varchar(max) null
END