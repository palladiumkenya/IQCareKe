

IF NOT EXISTS (select * from Mst_LabTestParameter where ReferenceId='RDT' and ParameterName ='RDT' )
BEGIN 
INSERT INTO [dbo].[Mst_LabTestParameter]
           ([ReferenceId] ,[ParameterName],[LabTestId],[DataType] ,[OrdRank] ,[UserId],[CreateDate],[DeleteFlag])
     VALUES
           ('RDT','RDT' ,(Select Top 1 Id From mst_LabTestMaster where Name='Malaria Random Diagnostic Test' ),'SELECTLIST' ,1.00 ,1,GETDATE(),0)
END
IF NOT EXISTS (select * from Mst_LabTestParameter where ReferenceId='MPS' and ParameterName ='MPS' )
BEGIN
INSERT INTO [dbo].[Mst_LabTestParameter]
           ([ReferenceId] ,[ParameterName],[LabTestId],[DataType] ,[OrdRank] ,[UserId],[CreateDate],[DeleteFlag])
		   VALUES
		   ('MPS','MPS' ,(Select Top 1 Id From mst_LabTestMaster where Name='Malaria (Bs for MPS)' ),'SELECTLIST' ,1.00 ,1,GETDATE(),0)
END
IF NOT EXISTS (select * from Mst_LabTestParameter where ReferenceId='SPECIES_REPORT' and ParameterName ='Species Report' )
BEGIN
INSERT INTO [dbo].[Mst_LabTestParameter]
           ([ReferenceId] ,[ParameterName],[LabTestId],[DataType] ,[OrdRank] ,[UserId],[CreateDate],[DeleteFlag])
		   VALUES
('SPECIES_REPORT','Species Report' ,(Select Top 1 Id From mst_LabTestMaster where Name='Malaria (Bs for MPS)' ),'TEXT' ,1.00 ,1,GETDATE(),0)
END

IF NOT EXISTS (select * from Mst_LabTestParameter where ReferenceId='CAS' and ParameterName ='Cryptococcus Antigen Test' )
BEGIN
		INSERT INTO [dbo].[Mst_LabTestParameter]
           ([ReferenceId] ,[ParameterName],[LabTestId],[DataType] ,[OrdRank] ,[UserId],[CreateDate],[DeleteFlag]) VALUES
		   ('CAS','Cryptococcus Antigen Test' ,(Select Top 1 Id From mst_LabTestMaster where Name='Malaria (Bs for MPS)' ),'SELECTLIST' ,1.00 ,1,GETDATE(),0)
END

IF NOT EXISTS (select * from Mst_LabTestParameter where ReferenceId='SAFB' and ParameterName ='Sputum For AFB' )
BEGIN
INSERT INTO [dbo].[Mst_LabTestParameter]
           ([ReferenceId] ,[ParameterName],[LabTestId],[DataType] ,[OrdRank] ,[UserId],[CreateDate],[DeleteFlag]) VALUES
		   ('SAFB','Sputum For AFB' ,(Select Top 1 Id From mst_LabTestMaster where Name='Sputum For AFB' ),'SELECTLIST' ,1.00 ,1,GETDATE(),0)
END


IF NOT EXISTS (select * from Mst_LabTestParameter where ReferenceId='QBL' and ParameterName ='Quantify Bacillary Load' )
BEGIN
INSERT INTO [dbo].[Mst_LabTestParameter]
           ([ReferenceId] ,[ParameterName],[LabTestId],[DataType] ,[OrdRank] ,[UserId],[CreateDate],[DeleteFlag]) VALUES
		   ('QBL','Quantify Bacillary Load' ,(Select Top 1 Id From mst_LabTestMaster where Name='Sputum For AFB' ),'TEXT' ,1.00 ,1,GETDATE(),0)
END

IF NOT EXISTS (select * from Mst_LabTestParameter where ReferenceId='GeneXpert' and ParameterName ='GeneXpert' )
BEGIN
INSERT INTO [dbo].[Mst_LabTestParameter]
    ([ReferenceId] ,[ParameterName],[LabTestId],[DataType] ,[OrdRank] ,[UserId],[CreateDate],[DeleteFlag]) VALUES
		   ('GeneXpert','GeneXpert' ,(Select Top 1 Id From mst_LabTestMaster where Name='GeneXpert' ),'SELECTLIST' ,1.00 ,1,GETDATE(),0)

END
GO
