INSERT INTO [dbo].[Mst_LabTestParameter]
           ([ReferenceId] ,[ParameterName],[LabTestId],[DataType] ,[OrdRank] ,[UserId],[CreateDate],[DeleteFlag])
     VALUES
           ('RDT','RDT' ,(Select Top 1 Id From mst_LabTestMaster where Name='Malaria Random Diagnostic Test' ),'SELECTLIST' ,1.00 ,1,GETDATE(),0),
		   ('MPS','MPS' ,(Select Top 1 Id From mst_LabTestMaster where Name='Malaria (Bs for MPS)' ),'SELECTLIST' ,1.00 ,1,GETDATE(),0),
		   ('SPECIES_REPORT','Species Report' ,(Select Top 1 Id From mst_LabTestMaster where Name='Malaria (Bs for MPS)' ),'TEXT' ,1.00 ,1,GETDATE(),0),
		   ('CAS','Cryptococcus Antigen Test' ,(Select Top 1 Id From mst_LabTestMaster where Name='Malaria (Bs for MPS)' ),'SELECTLIST' ,1.00 ,1,GETDATE(),0),
		   ('SAFB','Sputum For AFB' ,(Select Top 1 Id From mst_LabTestMaster where Name='Sputum For AFB' ),'SELECTLIST' ,1.00 ,1,GETDATE(),0),
		   ('QBL','Quantify Bacillary Load' ,(Select Top 1 Id From mst_LabTestMaster where Name='Sputum For AFB' ),'TEXT' ,1.00 ,1,GETDATE(),0),
		   ('GeneXpert','GeneXpert' ,(Select Top 1 Id From mst_LabTestMaster where Name='GeneXpert' ),'SELECTLIST' ,1.00 ,1,GETDATE(),0)

GO