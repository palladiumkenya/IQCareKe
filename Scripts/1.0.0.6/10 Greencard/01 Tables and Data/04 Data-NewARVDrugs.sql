

CREATE TABLE NewDrugList (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	DrugName varchar(100),
	Abbreviation varchar(30),
	Strength varchar(30)
);

INSERT INTO NewDrugList(DrugName,Abbreviation,strength) VALUES('Dolutegravir-50mg','DTG','50mg');
INSERT INTO NewDrugList(DrugName,Abbreviation,strength) VALUES('Darunavir-300mg','DRV','300mg');
INSERT INTO NewDrugList(DrugName,Abbreviation,strength) VALUES('Darunavir-600mg','DRV','600mg');
INSERT INTO NewDrugList(DrugName,Abbreviation,strength) VALUES('Atazanavir/Ritonavir-300mg/100mg','ATV/r','300mg/100mg');
INSERT INTO NewDrugList(DrugName,Abbreviation,strength) VALUES('Lopinavir/Ritonavir-200mg/50mg','LPV/r','200mg/50mg');
INSERT INTO NewDrugList(DrugName,Abbreviation,strength) VALUES('Lopinavir/Ritonavir-80mg/20mg/ml','LPV/r','80mg/20mg/ml');
INSERT INTO NewDrugList(DrugName,Abbreviation,strength) VALUES('Lopinavir/Ritonavir-40mg/10mg','LPV/r','40mg/10mg');
INSERT INTO NewDrugList(DrugName,Abbreviation,strength) VALUES('Reltegravir-400mg','RAL','400mg');
INSERT INTO NewDrugList(DrugName,Abbreviation,strength) VALUES('Tenofavir/Lamivudine/Efavirenz/Dolutegravir-300mg/300mg/50mg','TDF/3TC/DTG','300mg/300mg/50mg');

Declare @ItemId int=0,@genericId int,@count int,@counter int=0,@maxId int=0,@minId int=0;
Declare @drugName varchar(100),@abbreviation varchar(10),@strength varchar(10);

SET @count=(SELECT count(Id) FROM NewDrugList);
SET @minId=(SELECT MIN(Id) FROM NewDrugList);
SET @maxId=(SELECT max(Id) FROM NewDrugList);


WHILE (@minId <=@maxId)
BEGIN
	SET @drugName=(SELECT DrugName FROM NewDrugList WHERE Id=@minId);
	SET @abbreviation=(SELECT Abbreviation FROM NewDrugList WHERE Id=@minId);
	SET @strength=(SELECT Strength FROM NewDrugList WHERE Id=@minId);

	IF NOT EXISTS(SELECT * FROM Mst_ItemMaster m WHERE m.ItemName=''+@drugName+'')
		BEGIN
			INSERT INTO [dbo].[Mst_ItemMaster]([ItemCode],[ItemName],[ItemTypeID],[DeleteFlag],[CreatedBy],[CreateDate],[abbreviation])
			VALUES (0,''+@drugName+'',378,0,1,GETDATE(),''+@abbreviation+'');
		
			SET @itemId =SCOPE_IDENTITY();

			-- : lnk_ItemDrugType
			INSERT INTO Lnk_ItemDrugType(ItemTypeId,DrugTypeId,CreateDate,UserId) VALUES(@ItemId,(SELECT d.DrugTypeID FROM mst_DrugType d WHERE d.DrugTypeName='ARV Medication'),GETDATE(),1);

			--TODO: mst_Generic
			INSERT INTO [dbo].[mst_Generic]([GenericName],[GenericAbbrevation],[DeleteFlag],[UserID],[CreateDate])
			VALUES
				   (''+@drugName+''
				   ,''+@abbreviation+''
				   ,0
				   ,1
				   ,GETDATE());
			SET @genericId=SCOPE_IDENTITY();

			--: lnk_drugGeneric
			INSERT INTO lnk_DrugGeneric(Drug_pk,GenericID,CreateDate,UserID,DeleteFlag) VALUES(@ItemId,@genericId,GETDATE(),1,0);

			--: lnk_DrugTypeGeneric
			INSERT INTO lnk_DrugTypeGeneric(DrugTypeId,GenericId,CreateDate,UserId) VALUES((SELECT d.DrugTypeID FROM mst_DrugType d WHERE d.DrugTypeName='ARV Medication'),@genericId,GETDATE(),1);

			--:lnk_DrugStrength
			INSERT INTO lnk_DrugStrength(StrengthId,GenericID,UserId,CreateDate,DrugId) VALUES((SELECT s.StrengthId FROM mst_Strength s WHERE s.StrengthName=''+@strength+''),@genericId,1,GETDATE(),@ItemId);

		END
		ELSE
			BEGIN
			  UPDATE Mst_ItemMaster SET abbreviation=''+@abbreviation+'' WHERE  ItemName=''+@drugName+''
			END
	SET @minId=@minId+1;

END


-- DROP TABLE
DROP TABLE NewDrugList;
GO











