


IF NOT EXISTS (select * from Mst_LabTestMaster where ReferenceId='MALARIA_RANDOM_DIAGNOSTIC_TEST' and Name ='Malaria Random Diagnostic Test' )
BEGIN 
INSERT INTO [dbo].[mst_LabTestMaster] ([ReferenceId],[Name] ,[IsGroup],[DepartmentId] ,[Active] ,[CreateDate] ,[DeleteFlag])
     VALUES  ('MALARIA_RANDOM_DIAGNOSTIC_TEST' ,'Malaria Random Diagnostic Test',0,6 ,1,GETDATE(),0)
END

IF NOT EXISTS (select * from Mst_LabTestMaster where ReferenceId='MALARIA_MPS' and Name ='Malaria (Bs for MPS)' )
BEGIN 
INSERT INTO [dbo].[mst_LabTestMaster] ([ReferenceId],[Name] ,[IsGroup],[DepartmentId] ,[Active] ,[CreateDate] ,[DeleteFlag])
     VALUES 
	         ('MALARIA_MPS' ,'Malaria (Bs for MPS)',0,6 ,1,GETDATE(),0)
END

IF NOT EXISTS (select * from Mst_LabTestMaster where ReferenceId='CRYPTOCOCCUS_ANTIGEN_TEST' and Name ='Cryptococcus Antigen Test' )
BEGIN 
INSERT INTO [dbo].[mst_LabTestMaster] ([ReferenceId],[Name] ,[IsGroup],[DepartmentId] ,[Active] ,[CreateDate] ,[DeleteFlag])
     VALUES 
			 ('CRYPTOCOCCUS_ANTIGEN_TEST' ,'Cryptococcus Antigen Test',0,6 ,1,GETDATE(),0)
END
IF NOT EXISTS (select * from Mst_LabTestMaster where ReferenceId='SPUTUM_AFB' and Name ='Sputum For AFB' )
BEGIN 
INSERT INTO [dbo].[mst_LabTestMaster] ([ReferenceId],[Name] ,[IsGroup],[DepartmentId] ,[Active] ,[CreateDate] ,[DeleteFlag])
     VALUES  ('SPUTUM_AFB' ,'Sputum For AFB',0,6 ,1,GETDATE(),0)
END
IF NOT EXISTS (select * from Mst_LabTestMaster where ReferenceId='GENEXPERT' and Name ='GeneXpert' )
BEGIN 
INSERT INTO [dbo].[mst_LabTestMaster] ([ReferenceId],[Name] ,[IsGroup],[DepartmentId] ,[Active] ,[CreateDate] ,[DeleteFlag])
     VALUES 
			 ('GENEXPERT' ,'GeneXpert',0,6 ,1,GETDATE(),0)
END
