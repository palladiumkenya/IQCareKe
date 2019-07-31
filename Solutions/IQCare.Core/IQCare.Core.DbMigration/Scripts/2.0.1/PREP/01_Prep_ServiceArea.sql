IF NOT EXISTS(select * from ServiceArea where Name='PREP')
BEGIN
INSERT INTO ServiceArea([Name],Code,DisplayName,CreatedBy,CreateDate,DeleteFlag)
values('PREP','PREP','PREP',1,GETDATE(),1)
END