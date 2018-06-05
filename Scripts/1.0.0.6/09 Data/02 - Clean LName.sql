--declare @Ptn table   (ptn_pk int);
--declare @Person table   (ptn_pk int)
exec pr_OpenDecryptedSession

IF  EXISTS (Select * From sys.objects Where object_id = object_id(N'[dbo].[PtnLname]')	And type In (N'U'))Drop Table PtnLname;
Go
IF  EXISTS (Select * From sys.objects Where object_id = object_id(N'[dbo].[PersonLname]')	And type In (N'U'))Drop Table PersonLname;
Go
select Ptn_Pk into PtnLName from Patientview where MiddleName='Lname'
--insert into @Person
select PersonId Into PersonLname from gcPatientView where MiddleName='Lname'

Update P set MidName = Null FROM Person P inner join PersonLName L On P.Id = L.PersonId

--Select * From mst_Patient P Inner Join PtnLname L On P.Ptn_Pk = L.Ptn_Pk

Update P Set MiddleName= NUll From mst_Patient P Inner Join PtnLname L On P.Ptn_Pk = L.Ptn_Pk

IF  EXISTS (Select * From sys.objects Where object_id = object_id(N'[dbo].[PtnLname]')	And type In (N'U'))Drop Table PtnLname;
Go
IF  EXISTS (Select * From sys.objects Where object_id = object_id(N'[dbo].[PersonLname]')	And type In (N'U'))Drop Table PersonLname;
Go