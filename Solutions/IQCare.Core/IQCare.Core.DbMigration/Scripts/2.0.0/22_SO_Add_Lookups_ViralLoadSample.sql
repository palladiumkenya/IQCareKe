IF NOT EXISTS(SELECT * FROM LookupMaster WHERE [name]='ViralLoadSampleTaken')
BEGIN
   INSERT INTO LookupMaster(Name,DisplayName) VALUES('ViralLoadSampleTaken','ViralLoad Sample Taken')
END