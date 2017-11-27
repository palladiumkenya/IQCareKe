EXECUTE sp_msforeachtable 'ALTER TABLE ? disable trigger ALL'
Go
Set Nocount on
Go
UPDATE AppAdmin
SET
	AppVer='Ver 1.0.0.5 Kenya HMIS',
	DBVer='Ver 1.0.0.5 Kenya HMIS',
	RelDate='15-Nov-2017',
	VersionName='Ver 1.0.0.5 Kenya HMIS'


--  UPDATE LookupItems
	UPDATE LookupItem SET Name='Obese' WHERE Name='O' AND DisplayName='Overweight/Obese'
	UPDATE LookupItem SET Name='Normal' WHERE Name='N' AND DisplayName='Normal'

-- new master item
	IF NOT EXISTS(SELECT * FROM LookupMaster WHERE Name='AdverseEventOutcome')
	BEGIN
		INSERT INTO LookupMaster(Name,DisplayName) VALUES('AdverseEventOutcome','AEO');
	END


-- Adverse Events outcome option

	IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Got Well')
	BEGIN
		INSERT INTO LookupItem(name,DisplayName) VALUES('Recovering/Resolving','Recovering/Resolving');
	END
	ELSE
	 BEGIN
	   UPDATE LookupItem SET name='Recovering/Resolving',DisplayName='Recovering/Resolving' WHERE name IN('Got Well','Recovering/Resolving')
	 END

	IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='No Change')
	BEGIN
		INSERT INTO LookupItem(name,DisplayName) VALUES('Recoverd/Resolved','Recoverd/Resolved');
	END
	ELSE
	BEGIN 
	  UPDATE LookupItem SET name='Recoverd/Resolved',DisplayName='Recoverd/Resolved' WHERE name IN('No Change','Recoverd/Resolved')
	END



	--IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='')
	--BEGIN 
	--	INSERT INTO LookupItem(Name,DisplayName) VALUES('','')
	--END

	-- lookupmaster item
	Exec LookupMasterItem_Insert @ItemName=N'Recovering/Resolving' ,@MasterName=N'AdverseEventOutcome' ,@OrdRank=1.00
	Go
	Exec LookupMasterItem_Insert @ItemName=N'Recoverd/Resolved' ,@MasterName=N'AdverseEventOutcome' ,@OrdRank=2.00
	Go
	Exec LookupMasterItem_Insert @ItemName=N'Requires or Prolongs hospitalization' ,@MasterName=N'AdverseEventOutcome' ,@OrdRank=3.00
	Go	
	Exec LookupMasterItem_Insert @ItemName=N'Caused a congenital anomaly' ,@MasterName=N'AdverseEventOutcome' ,@OrdRank=4.00	
	Go
	Exec LookupMasterItem_Insert @ItemName=N'Requires intervention to prevent permanent damage' ,@MasterName=N'AdverseEventOutcome' ,@OrdRank=5.00
	Go
	Exec LookupMasterItem_Insert @ItemName=N'Died due to ADR' ,@MasterName=N'AdverseEventOutcome' ,@OrdRank=6.00
	Go
	Exec LookupMasterItem_Insert @ItemName=N'Died not due to ADR' ,@MasterName=N'AdverseEventOutcome' ,@OrdRank=7.00
	Go

	

	-- lookupmaster
Exec LookupMasterItem_Insert @ItemName=N'Abnormal dreams or nightmares (Frightening or unpleasant dreams)' ,@MasterName=N'AdverseEvents' ,@OrdRank=1.00
Go
Exec LookupMasterItem_Insert @ItemName=N'Anxiety' ,@MasterName=N'AdverseEvents' ,@OrdRank=1.01
Go
Exec LookupMasterItem_Insert @ItemName=N'Confusion/abnormal thinking' ,@MasterName=N'AdverseEvents' ,@OrdRank=1.02
Go
Exec LookupMasterItem_Insert @ItemName=N'Depression/mood changes (frequently feeling very low)' ,@MasterName=N'AdverseEvents' ,@OrdRank=1.03
Go
Exec LookupMasterItem_Insert @ItemName=N'Dizziness/spinning sensation/vertigo' ,@MasterName=N'AdverseEvents' ,@OrdRank=1.04
Go
Exec LookupMasterItem_Insert @ItemName=N'Fatigue/tiredness/weakness' ,@MasterName=N'AdverseEvents' ,@OrdRank=1.05
Go
Exec LookupMasterItem_Insert @ItemName=N'Insomnia (lacking sleep at night)/sleep problems' ,@MasterName=N'AdverseEvents' ,@OrdRank=1.06
Go
Exec LookupMasterItem_Insert @ItemName=N'Poor concentration/ memory problems' ,@MasterName=N'AdverseEvents' ,@OrdRank=1.07
Go
Exec LookupMasterItem_Insert @ItemName=N'Burning and tingling in limbs/ Paresthesia/painful neuropathy' ,@MasterName=N'AdverseEvents' ,@OrdRank=1.08
Go
Exec LookupMasterItem_Insert @ItemName=N'Suicide ideation (thoughts on ending the life)' ,@MasterName=N'AdverseEvents' ,@OrdRank=1.09
Go
Exec LookupMasterItem_Insert @ItemName=N'Skin rash/hypersensitivity reaction' ,@MasterName=N'AdverseEvents' ,@OrdRank=1.10
Go
Exec LookupMasterItem_Insert @ItemName=N'Abdominal discomfort/abdominal pain' ,@MasterName=N'AdverseEvents' ,@OrdRank=1.11
Go
Exec LookupMasterItem_Insert @ItemName=N'Nausea/vomiting' ,@MasterName=N'AdverseEvents' ,@OrdRank=1.12
Go
Exec LookupMasterItem_Insert @ItemName=N'Diarrhoea' ,@MasterName=N'AdverseEvents' ,@OrdRank=1.13
Go
Exec LookupMasterItem_Insert @ItemName=N'Jaundice' ,@MasterName=N'AdverseEvents' ,@OrdRank=1.14
Go
Exec LookupMasterItem_Insert @ItemName=N'Fat changes/lipodystrophy/lipohypertrophy' ,@MasterName=N'AdverseEvents' ,@OrdRank=1.15
Go
Exec LookupMasterItem_Insert @ItemName=N'Gynaecomastia' ,@MasterName=N'AdverseEvents' ,@OrdRank=1.16
Go
Exec LookupMasterItem_Insert @ItemName=N'Headache' ,@MasterName=N'AdverseEvents' ,@OrdRank=1.17
Go
Exec LookupMasterItem_Insert @ItemName=N'Anaemia/pancytopenia' ,@MasterName=N'AdverseEvents' ,@OrdRank=1.18
Go
Exec LookupMasterItem_Insert @ItemName=N'Renal failure/renal insufficiency' ,@MasterName=N'AdverseEvents' ,@OrdRank=1.19
Go	
Exec LookupMasterItem_Insert @ItemName=N'Other Specify' ,@MasterName=N'AdverseEvents' ,@OrdRank=1.20
Go	