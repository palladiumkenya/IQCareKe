DECLARE @rdtParamId INT  = (Select Top 1 Id From Mst_LabTestParameter where ParameterName='RDT')
DECLARE @mpsParamId INT  = (Select Top 1 Id From Mst_LabTestParameter where ParameterName='MPS')
DECLARE @cryptococcusParamId INT  = (Select Top 1 Id From Mst_LabTestParameter where ParameterName='Cryptococcus Antigen Test')
DECLARE @sputumParamId INT  = (Select Top 1 Id From Mst_LabTestParameter where ParameterName='Sputum For AFB')
DECLARE @geneXpertParamId INT  = (Select Top 1 Id From Mst_LabTestParameter where ParameterName='GeneXpert')


INSERT INTO [dbo].[dtl_LabTestParameterResultOption] ([ParameterId] ,[Value] ,[DeleteFlag])
     VALUES  (@rdtParamId,'Positive',0),
	         (@rdtParamId,'Negative',0),
			 (@mpsParamId,'Positive',0),
			 (@mpsParamId,'Negative',0),
			 (@cryptococcusParamId,'Positive',0),
			 (@cryptococcusParamId,'Negative',0),
			 (@sputumParamId,'Positive',0),
			 (@sputumParamId,'Negative',0),
			 (@geneXpertParamId,'MTB+RR',0),
			 (@geneXpertParamId,'MTB+RS',0),
			 (@geneXpertParamId,'MTB-ve',0),
			 (@geneXpertParamId,'ND',0);

GO