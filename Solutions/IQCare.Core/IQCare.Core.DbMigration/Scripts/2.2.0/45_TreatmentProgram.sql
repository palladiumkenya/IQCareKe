

IF NOT EXISTS(select * from mst_Decode dc inner join mst_Code c on c.CodeID=dc.CodeID and c.Name='Treatment Program' 
and dc.Name='Treatment')
BEGIN
INSERT INTO mst_Decode ([Name],CodeID,[SRNo],DeleteFlag,UserID,CreateDate,SystemId)
values('Treatment',(select CodeID from mst_Code where Name='Treatment Program'), 7.00,0,1,GetDate(),0)
END
IF NOT EXISTS(select * from mst_Decode dc inner join mst_Code c on c.CodeID=dc.CodeID 
and c.Name='Treatment Program' and dc.Name='prophylaxis')
BEGIN
INSERT INTO mst_Decode ([Name],CodeID,[SRNo],DeleteFlag,UserID,CreateDate,SystemId)
values('prophylaxis',(select CodeID from mst_Code where Name='Treatment Program'), 8.00,0,1,GetDate(),0)
END
