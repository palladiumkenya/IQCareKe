If Not Exists(Select * from sys.columns where Name = N'Frequency' AND Object_ID = Object_ID(N'mst_Facility'))
BEGIN
ALTER  TABLE mst_Facility ADD Frequency  INT   CONSTRAINT DF_DEFAULT_FREQUENCY DEFAULT 1 NOT NULL;
END

go

If  Exists(Select * from sys.columns where Name = N'Frequency' AND Object_ID = Object_ID(N'mst_Facility'))
BEGIN
IF EXISTS(SELECT * FROM information_schema.columns 
           WHERE table_name='mst_Facility' AND column_name='Frequency' 
             AND column_default IS NULL) 
BEGIN 
  ALTER TABLE [mst_Facility]  ADD CONSTRAINT DF_DEFAULT_FREQUENCY DEFAULT 1 FOR [Frequency] --Hoodaticus
END
END