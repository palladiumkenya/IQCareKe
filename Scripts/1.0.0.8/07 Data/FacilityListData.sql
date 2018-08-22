IF NOT EXISTS(SELECT * FROM FacilityList WHERE Name = 'Other')
BEGIN
	INSERT INTO FacilityList(MFLCode,Name)
	VALUES(99999, 'Other');
END