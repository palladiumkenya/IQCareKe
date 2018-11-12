
 IF NOT EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'Billing'
          AND Object_ID = Object_ID(N'mst_Facility'))
BEGIN
ALTER TABLE mst_Facility
ADD Billing int null
END

go


 IF NOT EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'Wards'
          AND Object_ID = Object_ID(N'mst_Facility'))
BEGIN
ALTER TABLE mst_Facility
ADD Wards int null
END