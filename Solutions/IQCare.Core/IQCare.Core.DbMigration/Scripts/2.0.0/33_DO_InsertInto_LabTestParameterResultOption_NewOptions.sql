DECLARE @rdtParamId INT  = (Select Top 1 Id From Mst_LabTestParameter where ParameterName='RDT')
DECLARE @mpsParamId INT  = (Select Top 1 Id From Mst_LabTestParameter where ParameterName='MPS')
DECLARE @cryptococcusParamId INT  = (Select Top 1 Id From Mst_LabTestParameter where ParameterName='Cryptococcus Antigen Test')
DECLARE @sputumParamId INT  = (Select Top 1 Id From Mst_LabTestParameter where ParameterName='Sputum For AFB')
DECLARE @geneXpertParamId INT  = (Select Top 1 Id From Mst_LabTestParameter where ParameterName='GeneXpert')


IF NOT EXISTS (SELECT * FROM [dbo].[dtl_LabTestParameterResultOption] WHERE ParameterId=@rdtParamId and [Value] ='Positive' )
BEGIN
INSERT INTO [dbo].[dtl_LabTestParameterResultOption] ([ParameterId] ,[Value] ,[DeleteFlag])
     VALUES  (@rdtParamId,'Positive',0)
END

IF NOT EXISTS (SELECT * FROM [dbo].[dtl_LabTestParameterResultOption] WHERE ParameterId=@rdtParamId and [Value] ='Negative' )
BEGIN
INSERT INTO [dbo].[dtl_LabTestParameterResultOption] ([ParameterId] ,[Value] ,[DeleteFlag])
	      VALUES   (@rdtParamId,'Negative',0)
END

IF NOT EXISTS (SELECT * FROM [dbo].[dtl_LabTestParameterResultOption] WHERE ParameterId=@mpsParamId and [Value] ='Positive' )
BEGIN
INSERT INTO [dbo].[dtl_LabTestParameterResultOption] ([ParameterId] ,[Value] ,[DeleteFlag])
	      VALUES   (@mpsParamId,'Positive',0)
END

IF NOT EXISTS (SELECT * FROM [dbo].[dtl_LabTestParameterResultOption] WHERE ParameterId=@mpsParamId and [Value] ='Negative' )
BEGIN
INSERT INTO [dbo].[dtl_LabTestParameterResultOption] ([ParameterId] ,[Value] ,[DeleteFlag])
	      VALUES   (@mpsParamId,'Negative',0)
END

IF NOT EXISTS (SELECT * FROM [dbo].[dtl_LabTestParameterResultOption] WHERE ParameterId=@cryptococcusParamId and [Value] ='Negative' )
BEGIN
INSERT INTO [dbo].[dtl_LabTestParameterResultOption] ([ParameterId] ,[Value] ,[DeleteFlag])
	      VALUES   (@cryptococcusParamId,'Negative',0)
END
IF NOT EXISTS (SELECT * FROM [dbo].[dtl_LabTestParameterResultOption] WHERE ParameterId=@cryptococcusParamId and [Value] ='Positive' )
BEGIN
INSERT INTO [dbo].[dtl_LabTestParameterResultOption] ([ParameterId] ,[Value] ,[DeleteFlag])
	      VALUES   (@cryptococcusParamId,'Positive',0)
END

IF NOT EXISTS (SELECT * FROM [dbo].[dtl_LabTestParameterResultOption] WHERE ParameterId=@sputumParamId and [Value] ='Positive' )
BEGIN
INSERT INTO [dbo].[dtl_LabTestParameterResultOption] ([ParameterId] ,[Value] ,[DeleteFlag])
	      VALUES   (@sputumParamId,'Positive',0)
END

IF NOT EXISTS (SELECT * FROM [dbo].[dtl_LabTestParameterResultOption] WHERE ParameterId=@sputumParamId and [Value] ='Negative' )
BEGIN
INSERT INTO [dbo].[dtl_LabTestParameterResultOption] ([ParameterId] ,[Value] ,[DeleteFlag])
	      VALUES   (@sputumParamId,'Negative',0)
END


IF NOT EXISTS (SELECT * FROM [dbo].[dtl_LabTestParameterResultOption] WHERE ParameterId=@geneXpertParamId and [Value] ='MTB+RR' )
BEGIN
INSERT INTO [dbo].[dtl_LabTestParameterResultOption] ([ParameterId] ,[Value] ,[DeleteFlag])
	      VALUES   (@geneXpertParamId,'MTB+RR',0)
END

IF NOT EXISTS (SELECT * FROM [dbo].[dtl_LabTestParameterResultOption] WHERE ParameterId=@geneXpertParamId and [Value] ='MTB+RS' )
BEGIN
INSERT INTO [dbo].[dtl_LabTestParameterResultOption] ([ParameterId] ,[Value] ,[DeleteFlag])
	      VALUES   (@geneXpertParamId,'MTB+RS',0)
END

IF NOT EXISTS (SELECT * FROM [dbo].[dtl_LabTestParameterResultOption] WHERE ParameterId=@geneXpertParamId and [Value] ='MTB-ve' )
BEGIN
INSERT INTO [dbo].[dtl_LabTestParameterResultOption] ([ParameterId] ,[Value] ,[DeleteFlag])
	      VALUES   (@geneXpertParamId,'MTB-ve',0)
END
IF NOT EXISTS (SELECT * FROM [dbo].[dtl_LabTestParameterResultOption] WHERE ParameterId=@geneXpertParamId and [Value] ='ND' )
BEGIN
INSERT INTO [dbo].[dtl_LabTestParameterResultOption] ([ParameterId] ,[Value] ,[DeleteFlag])
	      VALUES   (@geneXpertParamId,'ND',0)
END
