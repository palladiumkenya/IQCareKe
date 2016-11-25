SET IDENTITY_INSERT Mst_BillPaymentType On
GO
Insert Into Mst_BillPaymentType (TypeID,TypeName,TypeDescription,Locked,PluginName,	Credit, Active)
Select	ID,
		D.Name As PaymentName,
		Case D.Name When 'Cash' Then 'Cash payment'
					When 'Deposit' then 'Use deposit to pay'
					When 'Cheque' then 'Bankers Cheques'
					When 'Exempt' then 'Patient Exempted from paying'
					When 'Exemption' then 'Patient Exempted from paying'
					When 'NHIF' then 'National Hospital Insurance Fund'
					When 'WriteOff' then 'Bill written off'
					Else D.Name
					End  TypeDescription,
		Case When D.Name In ('Cash','Cheque','Deposit','Waiver', 'Exemption','WriteOff')Then 1 Else 0 End Locked,
		Case When D.Name In ('Cash','Cheque','Deposit') Then 'PayBillCashAndDeposit.ascx' 
			 When D.Name ='Exemption' Then 'PayBillExempt.ascx' 
			 Else 'PayBillCorporate.ascx' End,
		Case When D.Name In ('Cash','Deposit','Waiver' ,'WriteOff','Cheque')Then 0 Else 1 End Credit,
		Case When D.Name ='Exempt' Then 1 Else ~Convert(bit, Isnull(D.DeleteFlag, 0)) End Active
From dbo.mst_Decode D
Inner Join
	dbo.mst_Code C On D.CodeID = C.CodeID
Where (C.DeleteFlag = 0)
And (C.Name = 'PaymentType') 
And D.Name Not In (Select TypeName From Mst_BillPaymentType);

print cast(@@ROWCount as varchar(30)) + ' payments methods migrated'; 
Go

Go
SET IDENTITY_INSERT Mst_BillPaymentType Off
GO
Insert Into Mst_BillPaymentType (TypeName,TypeDescription,Locked,PluginName,Credit,	Active)
Select * From
(
Select	'Cash' As PaymentName,
		'Cash Payment' TypeDescription,
		1 Locked,
		'PayBillCashAndDeposit.ascx' PluginName,
		0 Credit,
		1 Active
Union
Select	'Deposit' As PaymentName,
		'Use deposit to pay' TypeDescription,
		1 Locked,
		'PayBillCashAndDeposit.ascx' PluginName,
		0,
		1 Active
Union
Select	'Cheque' As PaymentName,
		'Bankers Cheques' TypeDescription,
		1 Locked,
		'PayBillCashAndDeposit.ascx' PluginName,
		0,
		1 Active
Union
Select	'Exemption' As PaymentName,
		'Patient Exempted from paying' TypeDescription,
		1 Locked,
		'PayBillExempt.ascx' PluginName,
		1,
		1 Active
Union
Select	'Waiver' As PaymentName,
		'Patient waived from paying' TypeDescription,
		1 Locked,
		'PayBillCorporate.ascx' PluginName,
		0,
		1 Active
Union
Select	'WriteOff' As PaymentName,
		'Bill written off' TypeDescription,
		1 Locked,
		'PayBillCashAndDeposit.ascx' PluginName,
		0,
		1 Active
Union
Select	'NHIF' As PaymentName,
		'National Hospital Insurance Fund' TypeDescription,
		0 Locked,
		'PayBillCorporate.ascx' PluginName,
		1,
		1 Active
) D Where PaymentName Not In (Select TypeName From Mst_BillPaymentType);
Go
Update Mst_BillPaymentType Set Active = 0, Credit=0 Where TypeName='Exempt'

Update Mst_BillPaymentType Set PluginName='PayBillExempt.ascx', Credit=0, Locked=1 Where TypeName='Exemption'
Update Mst_BillPaymentType Set  Credit=1 Where TypeName In ('Exemption','NHIF')
Update Mst_BillPaymentType Set  Credit=0, Locked= 1 Where TypeName In ('Cheque','WriteOff')
GO

Update Mst_Code Set DeleteFlag = 1, UpdateDate=getdate() Where Name='PaymentType'
Go