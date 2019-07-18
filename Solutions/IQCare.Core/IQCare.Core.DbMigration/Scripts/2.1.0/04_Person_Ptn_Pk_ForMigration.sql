If Not Exists (Select * From sys.columns Where Name = N'Ptn_Pk' And Object_ID = Object_id(N'Person'))    
Begin
  Alter table dbo.Person Add Ptn_Pk int NULL
End