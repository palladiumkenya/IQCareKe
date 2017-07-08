
--------------------------------------------------------------------------------------------------
If Not Exists(Select 1 From lnk_DrugGeneric  Where Drug_pk=1147 And GenericID = 1) Begin
	Insert Into lnk_DrugGeneric (Drug_pk,GenericID,CreateDate,DeleteFlag,UserID)
	Values(1147,1,Getdate(),0,1);
End
If Not Exists(Select 1 From lnk_DrugGeneric  Where Drug_pk=1147 And GenericID = 175) Begin
	Insert Into lnk_DrugGeneric (Drug_pk,GenericID,CreateDate,DeleteFlag,UserID)
	Values(1147,175,Getdate(),0,1);
End

If Not Exists(Select 1 From lnk_DrugGeneric  Where Drug_pk=1146 And GenericID = 1) Begin
	Insert Into lnk_DrugGeneric (Drug_pk,GenericID,CreateDate,DeleteFlag,UserID)
	Values(1146,1,Getdate(),0,1);
End
If Not Exists(Select 1 From lnk_DrugGeneric  Where Drug_pk=1146 And GenericID = 175) Begin
	Insert Into lnk_DrugGeneric (Drug_pk,GenericID,CreateDate,DeleteFlag,UserID)
	Values(1146,175,Getdate(),0,1);
End

UPDATE Mst_Drug SET drugName='Tenofovir DF-TDF300/3TC200/EFV600 300mg/200mg/600mg'
where DrugName='Tenofovir DF-TDF300/FTC200/EFV600 300mg/200mg/600mg' and Drug_pk=1166
If Not Exists(Select 1 From lnk_DrugGeneric  Where Drug_pk=1166 And GenericID = 280) Begin
	Insert Into lnk_DrugGeneric (Drug_pk,GenericID,CreateDate,DeleteFlag,UserID)
	Values(1166,280,Getdate(),0,1);
End
If Not Exists(Select 1 From lnk_DrugGeneric  Where Drug_pk=1166 And GenericID = 175) Begin
	Insert Into lnk_DrugGeneric (Drug_pk,GenericID,CreateDate,DeleteFlag,UserID)
	Values(1166,175,Getdate(),0,1);
End
If Not Exists(Select 1 From lnk_DrugGeneric  Where Drug_pk=1166 And GenericID = 102) Begin
	Insert Into lnk_DrugGeneric (Drug_pk,GenericID,CreateDate,DeleteFlag,UserID)
	Values(1166,102,Getdate(),0,1);
End
Go


Update mst_Generic Set GenericAbbrevation = Nullif(ltrim(rtrim(GenericAbbrevation)),'');
GO
--------------------------------------------------------------------------------------------------
With Drugs as ( Select	O.ptn_pharmacy_pk,
						O.Ptn_pk,
						O.LocationID,
						O.VisitID,
						dbo.fn_Drug_Abbrev_Constella(P.Drug_Pk) Regimen,
						R.RegimenType
From dbo.ord_PatientPharmacyOrder O
Inner Join
	dbo.dtl_PatientPharmacyOrder P On P.ptn_pharmacy_pk = O.ptn_pharmacy_pk
Inner Join
	dbo.Mst_Drug D On D.Drug_pk = P.Drug_Pk
inner join
	dbo.dtl_RegimenMap R on O.VisitID = R.Visit_Pk
	where P.Drug_Pk = 1147)

UPDATE A
SET A.RegimenType = REPLACE(A.RegimenType, '3TC', 'ABC/3TC')
from dtl_RegimenMap A inner join Drugs B on A.Visit_Pk = B.VisitID
where a.Ptn_Pk = b.Ptn_pk and A.orderid = B.ptn_pharmacy_pk and len(A.RegimenType) < 9
Go
--------------------------------------------------------------------------------------------------------

With Drugs as ( Select	O.ptn_pharmacy_pk,
						O.Ptn_pk,
						O.LocationID,
						O.VisitID,
						dbo.fn_Drug_Abbrev_Constella(P.Drug_Pk) Regimen,
						R.RegimenType
From dbo.ord_PatientPharmacyOrder O
Inner Join
	dbo.dtl_PatientPharmacyOrder P On P.ptn_pharmacy_pk = O.ptn_pharmacy_pk
Inner Join
	dbo.Mst_Drug D On D.Drug_pk = P.Drug_Pk
inner join
	dbo.dtl_RegimenMap R on O.VisitID = R.Visit_Pk
	where P.Drug_Pk = 1146)

UPDATE A
SET A.RegimenType = REPLACE(A.RegimenType, 'ABC', 'ABC/3TC')
from dtl_RegimenMap A inner join Drugs B on A.Visit_Pk = B.VisitID
where a.Ptn_Pk = b.Ptn_pk and A.orderid = B.ptn_pharmacy_pk and len(A.RegimenType) < 9
Go
------------------------------------------------------------------------------------------------------

With Drugs as ( Select	O.ptn_pharmacy_pk,
						O.Ptn_pk,
						O.LocationID,
						O.VisitID,
						dbo.fn_Drug_Abbrev_Constella(P.Drug_Pk) Regimen,
						R.RegimenType
From dbo.ord_PatientPharmacyOrder O
Inner Join
	dbo.dtl_PatientPharmacyOrder P On P.ptn_pharmacy_pk = O.ptn_pharmacy_pk
Inner Join
	dbo.Mst_Drug D On D.Drug_pk = P.Drug_Pk
inner join
	dbo.dtl_RegimenMap R on O.VisitID = R.Visit_Pk
	where P.Drug_Pk = 1166)

UPDATE A
SET A.RegimenType = REPLACE(A.RegimenType, 'TDF', 'TDF/3TC/EFV')
from dtl_RegimenMap A inner join Drugs B on A.Visit_Pk = B.VisitID
where a.Ptn_Pk = b.Ptn_pk and A.orderid = B.ptn_pharmacy_pk and len(A.RegimenType) < 9
Go
------------------------------------------------------------------------------------------

update dtl_regimenmap set regimenType = isnull(dbo.[FN_DistinctList](regimentype,'/'),'')
Go
update mst_generic set genericabbrevation = 'LPV/r' where genericabbrevation = 'LOPr'
Go
update mst_generic set genericabbrevation = 'LPV/r' where genericabbrevation = 'LOP/r'
Go
update mst_generic set genericabbrevation = 'ATV' where genericabbrevation = 'ATR'
Go
--if not exists(select 1 from mst_generic where GenericName = 'Atazanavir/Ritonavir')
--begin
--insert into mst_generic (GenericName,GenericAbbrevation,deleteflag) values('Atazanavir/Ritonavir','ATV/r',0)
--end