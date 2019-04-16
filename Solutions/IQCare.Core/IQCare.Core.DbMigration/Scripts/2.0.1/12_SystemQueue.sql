If Not Exists(Select * from sys.columns where Name = N'SystemQueue' AND Object_ID = Object_ID(N'mst_Facility'))
BEGIN
ALTER  TABLE mst_Facility ADD [SystemQueue]  INT   CONSTRAINT DF_DEFAULT_SYSTEMQUEUE DEFAULT 0 NOT NULL;
END

go

If  Exists(Select * from sys.columns where Name = N'SystemQueue' AND Object_ID = Object_ID(N'mst_Facility'))
BEGIN
IF EXISTS(SELECT * FROM information_schema.columns 
           WHERE table_name='mst_Facility' AND column_name='SystemQueue' 
             AND column_default IS NULL) 
BEGIN 
  ALTER TABLE [mst_Facility]  ADD CONSTRAINT DF_DEFAULT_SYSTEMQUEUE DEFAULT 0 FOR [SystemQueue] --Hoodaticus
END
END