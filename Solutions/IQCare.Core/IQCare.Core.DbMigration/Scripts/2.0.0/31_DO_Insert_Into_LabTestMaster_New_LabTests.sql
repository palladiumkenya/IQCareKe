INSERT INTO [dbo].[mst_LabTestMaster] ([ReferenceId],[Name] ,[IsGroup],[DepartmentId] ,[Active] ,[CreateDate] ,[DeleteFlag])
     VALUES  ('MALARIA_RANDOM_DIAGNOSTIC_TEST' ,'Malaria Random Diagnostic Test',0,6 ,1,GETDATE(),0),
	         ('MALARIA_MPS' ,'Malaria (Bs for MPS)',0,6 ,1,GETDATE(),0),
			 ('CRYPTOCOCCUS_ANTIGEN_TEST' ,'Cryptococcus Antigen Test',0,6 ,1,GETDATE(),0),
			 ('SPUTUM_AFB' ,'Sputum For AFB',0,6 ,1,GETDATE(),0),
			 ('GENEXPERT' ,'GeneXpert',0,6 ,1,GETDATE(),0)
GO