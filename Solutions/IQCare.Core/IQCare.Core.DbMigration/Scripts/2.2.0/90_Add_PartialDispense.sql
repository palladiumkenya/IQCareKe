
If Not Exists(Select * from sys.columns where Name = N'PartialDispense' AND Object_ID = Object_ID(N'mst_Facility'))
BEGIN
ALTER  TABLE mst_Facility ADD PartialDispense  INT   CONSTRAINT DF_DEFAULT_PartialDispense DEFAULT 0 NOT NULL;
END


If  Exists(Select * from sys.columns where Name = N'PartialDispense' AND Object_ID = Object_ID(N'mst_Facility'))
BEGIN
IF EXISTS(SELECT * FROM information_schema.columns 
           WHERE table_name='mst_Facility' AND column_name='PartialDispense' 
             AND column_default IS NULL) 
BEGIN 
  ALTER TABLE [mst_Facility]  ADD CONSTRAINT DF_DEFAULT_PartialDispense DEFAULT 0 FOR [PartialDispense] --Hoodaticus
END
END