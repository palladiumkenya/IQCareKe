--Add Interop user
If Not Exists(Select 1 From mst_user where username='interop') Begin
	Insert Into mst_User (UserLastName, UserFirstName,UserName,Password,DeleteFlag,OperatorID,CreateDate,UpdateDate,EmployeeId)
	values('System', 'Interop', 'interop','ZUt2RvNgf/afw9XDlIubHllmY/UFXsXr',0,1,getdate(),getdate(),null)
End
Go
--Delete duplicates from Patientidentifiers

;with DupId As (Select Id, PatientEnrollmentId, IdentifierTypeId,IdentifierValue, PatientId,
row_number() over(partition by cast(IdentifierTypeId as varchar(3))+' ' +cast(PatientId as varchar(5) )+  ' ' + cast(PatientEnrollmentId as varchar(8))
+ ' '+ cast(IdentifierValue as varchar(10)) Order by id) RowIndex
 from PatientIdentifier)

Delete I From DupId D Inner Join PatientIdentifier I on D.Id= I.Id where D.RowIndex >1

Go