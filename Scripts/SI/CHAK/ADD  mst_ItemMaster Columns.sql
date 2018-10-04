
IF NOT EXISTS(select top 1 QtyUnitDisp from mst_ItemMaster)
BEGIN
ALTER  table mst_ItemMaster ADD  QtyUnitDisp int null
END

IF NOT EXISTS(select top 1 MorDose from mst_ItemMaster)
BEGIN
ALTER  table mst_ItemMaster ADD  MorDose int null
END

IF NOT EXISTS(select top 1 MidDose from mst_ItemMaster)
BEGIN
ALTER  table mst_ItemMaster ADD  MidDose int null
END

IF NOT EXISTS(select top 1 EvenDose from mst_ItemMaster)
BEGIN
ALTER  table mst_ItemMaster ADD  EvenDose int null
END

IF NOT EXISTS(select top 1 NightDose from mst_ItemMaster)
BEGIN
ALTER  table mst_ItemMaster ADD  NightDose int null
END


