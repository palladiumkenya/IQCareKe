
 IF NOT EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'QtyUnitDisp'
          AND Object_ID = Object_ID(N'mst_ItemMaster'))
BEGIN
ALTER  table mst_ItemMaster ADD  QtyUnitDisp int null
END
 IF NOT EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'MorDose'
          AND Object_ID = Object_ID(N'mst_ItemMaster'))
BEGIN
ALTER  table mst_ItemMaster ADD  MorDose int null
END

IF NOT EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'MidDose'
          AND Object_ID = Object_ID(N'mst_ItemMaster'))
BEGIN
ALTER  table mst_ItemMaster ADD  MidDose int null
END

 IF NOT EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'EvenDose '
          AND Object_ID = Object_ID(N'mst_ItemMaster'))
BEGIN
ALTER  table mst_ItemMaster ADD  EvenDose int null
END

 IF NOT EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'NightDose'
          AND Object_ID = Object_ID(N'mst_ItemMaster'))
BEGIN
ALTER  table mst_ItemMaster ADD  NightDose int null
END


 IF NOT EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'Syrup'
          AND Object_ID = Object_ID(N'mst_ItemMaster'))
BEGIN
ALTER TABLE mst_ItemMaster ADD Syrup int null
END

