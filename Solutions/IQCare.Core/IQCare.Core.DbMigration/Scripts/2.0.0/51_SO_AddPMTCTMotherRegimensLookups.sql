
IF NOT EXISTS(SELECT * FROM LookupItem WHERE [name]='PM 6 TDF + 3TC + NVP') BEGIN
	INSERT INTO LookupItem(Name,DisplayName) VALUES('PM 6 TDF + 3TC + NVP','PM 6 TDF + 3TC + NVP');
 END;

IF NOT EXISTS(SELECT * FROM LookupItem WHERE [name]='PM 7 TDF + 3TC + LPV/r') BEGIN
	INSERT INTO LookupItem(Name,DisplayName) VALUES('PM 7 TDF + 3TC + LPV/r','PM 7 TDF + 3TC + LPV/r');
 END;

 IF NOT EXISTS(SELECT * FROM LookupItem WHERE [name]='PM 8 SD Nevirapine (NVP)') BEGIN
	INSERT INTO LookupItem(Name,DisplayName) VALUES('PM 8 SD Nevirapine (NVP)','PM 8 SD Nevirapine (NVP)');
 END;

IF NOT EXISTS(SELECT * FROM LookupItem WHERE [name]='PM 9 TDF + 3TC + EFV') BEGIN
	INSERT INTO LookupItem(Name,DisplayName) VALUES('PM 9 TDF + 3TC + EFV','PM 9 TDF + 3TC + EFV');
 END;

IF NOT EXISTS(SELECT * FROM LookupItem WHERE [name]='PM 10 AZT + 3TC + ATV/r') BEGIN
  INSERT INTO LookupItem(Name,DisplayName) VALUES('PM 10 AZT + 3TC + ATV/r','PM 10 AZT + 3TC + ATV/r');
 END;

IF NOT EXISTS(SELECT * FROM LookupItem WHERE [name]='PM 11 TDF + 3TC + ATV/r') BEGIN
  INSERT INTO LookupItem(Name,DisplayName) VALUES('PM 11 TDF + 3TC + ATV/r','PM 11 TDF + 3TC + ATV/r');
END;

IF NOT EXISTS(SELECT * FROM LookupItem WHERE [name]='Others Specify') BEGIN
	INSERT INTO LookupItem(Name,DisplayName) VALUES('Others Specify','Other Specify');
 END;

 If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherRegimen') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM 6 TDF + 3TC + NVP')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherRegimen'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM 6 TDF + 3TC + NVP'),'PM 6 TDF + 3TC + NVP',5) end;
 If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherRegimen') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM 7 TDF + 3TC + LPV/r')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherRegimen'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM 7 TDF + 3TC + LPV/r'),'PM 7 TDF + 3TC + LPV/r',6) end;
 If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherRegimen') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM 8 SD Nevirapine (NVP)')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherRegimen'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM 8 SD Nevirapine (NVP)'),'PM 8 SD Nevirapine (NVP)',7) end;
 If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherRegimen') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM 9 TDF + 3TC + EFV')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherRegimen'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM 9 TDF + 3TC + EFV'),'PM 9 TDF + 3TC + EFV',8) end;
 If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherRegimen') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM 10 AZT + 3TC + ATV/r')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherRegimen'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM 10 AZT + 3TC + ATV/r'),'PM 10 AZT + 3TC + ATV/r',9) end;
 If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherRegimen') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM 11 TDF + 3TC + ATV/r')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherRegimen'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM 11 TDF + 3TC + ATV/r'),'PM 11 TDF + 3TC + ATV/r',10) end;
 If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherRegimen') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Others Specify')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherRegimen'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Others Specify'),'Others Specify',11) end;




