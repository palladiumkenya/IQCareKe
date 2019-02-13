
IF NOT EXISTS(SELECT * FROM [dbo].[Form]  WHERE Name ='Automated Indicator Reporting (AIR)')
BEGIN
INSERT INTO [dbo].[Form] ([Name],CreatedBy,CreateDate,DeleteFlag)
VALUES('Automated Indicator Reporting (AIR)','1',GETDATE(),'0')
END


GO


IF NOT EXISTS(SELECT * FROM [dbo].[Section]  WHERE SectionName ='HIV Counselling and Testing')
BEGIN
INSERT INTO [dbo].[Section] ([FormId],SectionName,Active,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id from [dbo].[Form] where Name='Automated Indicator Reporting (AIR)'),'HIV Counselling and Testing','0',GetDate(),'0','1')

END

go


IF NOT EXISTS(SELECT * FROM [dbo].[Section]  WHERE SectionName ='Prevention of Mother-to-Child Transmission (PMTCT)')
BEGIN
INSERT INTO [dbo].[Section] ([FormId],SectionName,Active,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id from [dbo].[Form] where Name='Automated Indicator Reporting (AIR)'),'Prevention of Mother-to-Child Transmission (PMTCT)','0',GetDate(),'0','1')

END

go




IF NOT EXISTS(SELECT * FROM [dbo].[Section]  WHERE SectionName ='HIV and TB treatment')
BEGIN
INSERT INTO [dbo].[Section] ([FormId],SectionName,Active,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id from [dbo].[Form] where Name='Automated Indicator Reporting (AIR)'),'HIV and TB treatment','0',GetDate(),'0','1')

END

GO

IF NOT EXISTS(SELECT * FROM [dbo].[Section]  WHERE SectionName ='Voluntary Male Circumcision')
BEGIN
INSERT INTO [dbo].[Section] ([FormId],SectionName,Active,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id from [dbo].[Form] where Name='Automated Indicator Reporting (AIR)'),'Voluntary Male Circumcision','0',GetDate(),'0','1')

END


GO

IF NOT EXISTS(SELECT * FROM [dbo].[Section]  WHERE SectionName ='Post-Exposure Prophylaxis')
BEGIN
INSERT INTO [dbo].[Section] ([FormId],SectionName,Active,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id from [dbo].[Form] where Name='Automated Indicator Reporting (AIR)'),'Post-Exposure Prophylaxis','0',GetDate(),'0','1')

END

GO
IF NOT EXISTS(SELECT * FROM [dbo].[Section]  WHERE SectionName ='Methadone Assisted Therapy')
BEGIN
INSERT INTO [dbo].[Section] ([FormId],SectionName,Active,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id from [dbo].[Form] where Name='Automated Indicator Reporting (AIR)'),'Methadone Assisted Therapy','0',GetDate(),'0','1')

END



GO
--SubCategory

IF NOT EXISTS(select * from [dbo].[SubSection] where SubSectionName='Number Assessed for HIV risk')
BEGIN
INSERT INTO [dbo].[SubSection]([SectionId],[SubSectionName],[Active],[CreateDate],[DeleteFlag],[CreatedBy])
VALUES((Select Id from [dbo].[Section] WHERE SectionName ='HIV Counselling and Testing'),'Number Assessed for HIV risk',0,GetDate(),0,1)
END

GO

IF NOT EXISTS(select * from [dbo].[SubSection] where SubSectionName='Number Self Testing for HIV')
BEGIN
INSERT INTO [dbo].[SubSection]([SectionId],[SubSectionName],[Active],[CreateDate],[DeleteFlag],[CreatedBy])
VALUES((Select Id from [dbo].[Section] WHERE SectionName ='HIV Counselling and Testing'),'Number Self Testing for HIV',0,GetDate(),0,1)
END

GO


IF NOT EXISTS(select * from [dbo].[SubSection] where SubSectionName='Post-Exposure Prophylaxis(PEP)')
BEGIN
INSERT INTO [dbo].[SubSection]([SectionId],[SubSectionName],[Active],[CreateDate],[DeleteFlag],[CreatedBy])
VALUES((Select Id from [dbo].[Section] WHERE SectionName ='Post-Exposure Prophylaxis'),'Post-Exposure Prophylaxis(PEP)',0,GetDate(),0,1)
END

GO


IF NOT EXISTS(select * from [dbo].[SubSection] where SubSectionName='Methadone Assisted Therapy(MAT)')
BEGIN
INSERT INTO [dbo].[SubSection]([SectionId],[SubSectionName],[Active],[CreateDate],[DeleteFlag],[CreatedBy])
VALUES((Select Id from [dbo].[Section] WHERE SectionName ='Methadone Assisted Therapy'),'Methadone Assisted Therapy(MAT)',0,GetDate(),0,1)
END


GO


IF NOT EXISTS(select * from [dbo].[SubSection] where SubSectionName='Number Circumcised')
BEGIN
INSERT INTO [dbo].[SubSection]([SectionId],[SubSectionName],[Active],[CreateDate],[DeleteFlag],[CreatedBy])
VALUES((Select Id from [dbo].[Section] WHERE SectionName ='Voluntary Male Circumcision'),'Number Circumcised',0,GetDate(),0,1)
END

GO

IF NOT EXISTS(select * from [dbo].[SubSection] where SubSectionName='Type of Circumcision')
BEGIN
INSERT INTO [dbo].[SubSection]([SectionId],[SubSectionName],[Active],[CreateDate],[DeleteFlag],[CreatedBy])
VALUES((Select Id from [dbo].[Section] WHERE SectionName ='Voluntary Male Circumcision'),'Type of Circumcision',0,GetDate(),0,1)
END

GO


IF NOT EXISTS(select * from [dbo].[SubSection] where SubSectionName='Circumcision Adverse Events')
BEGIN
INSERT INTO [dbo].[SubSection]([SectionId],[SubSectionName],[Active],[CreateDate],[DeleteFlag],[CreatedBy])
VALUES((Select Id from [dbo].[Section] WHERE SectionName ='Voluntary Male Circumcision'),'Circumcision Adverse Events',0,GetDate(),0,1)
END


GO

--DataType
IF NOT EXISTS(Select * from [dbo].[DataType] where [Name]='Text')
BEGIN
INSERT INTO [dbo].[DataType]([Name],CreateDate,CreatedBy)
VALUES('Text',GETDATE(),'1')
END

GO


IF NOT EXISTS(Select * from [dbo].[DataType] where [Name]='Numeric')
BEGIN
INSERT INTO [dbo].[DataType]([Name],CreateDate,CreatedBy)
VALUES('Numeric',GETDATE(),'1')
END



--Indicator 

IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Assessed 15-19' and Code='(M)HV01-37')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Number Assessed for HIV risk'),'Assessed 15-19','(M)HV01-37'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END




IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Assessed 20-24' and Code='(M)HV01-38')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Number Assessed for HIV risk'),'Assessed 20-24','(M)HV01-38'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END


IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Assessed 25-29' and Code='(M)HV01-39')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Number Assessed for HIV risk'),'Assessed 25-29','(M)HV01-39'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END



IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Assessed 30+'  and Code='(M)HV01-40')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Number Assessed for HIV risk'),'Assessed 30+','(M)HV01-40'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END


IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Assessed 15-19'  and Code='(F)HV01-41')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Number Assessed for HIV risk'),'Assessed 15-19','(F)HV01-41'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END




IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Assessed 20-24'  and Code='(F)HV01-42')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Number Assessed for HIV risk'),'Assessed 20-24','(F)HV01-42'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END

IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Assessed 25-29'  and Code='(F)HV01-43')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Number Assessed for HIV risk'),'Assessed 25-29','(F)HV01-43'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END


IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Assessed 30+'  and Code='(F)HV01-44')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Number Assessed for HIV risk'),'Assessed 30+','(F)HV01-44'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END


--



IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Total Assessed for HIV Risk'  and Code='Sum HV01-37 to HV01-44')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Number Assessed for HIV risk'),'Total Assessed for HIV Risk','Sum HV01-37 to HV01-44'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END


go


IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Self Testing 15-24'  and Code='(M)HV01-46')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Number Self Testing for HIV'),'Self Testing 15-24','(M)HV01-46'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END


IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Self Testing 15-24'  and Code='(F)HV01-47')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Number Self Testing for HIV'),'Self Testing 15-24','(F)HV01-47'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END

IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Self Testing 25+'  and Code='(M)HV01-48')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Number Self Testing for HIV'),'Self Testing 25+','(M)HV01-48'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END

IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Self Testing 25+'  and Code='(F)HV01-49')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Number Self Testing for HIV'),'Self Testing 25+','(F)HV01-49'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END


IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Self Testing Total'  and Code='(Sum HV01-46 To HV01-49)HV01-50')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Number Self Testing for HIV'),'Self Testing Total','(Sum HV01-46 To HV01-49)HV01-50'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END



--


IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Exposed Occupational'  and Code='HV05-01')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Post-Exposure Prophylaxis(PEP)'),'Exposed Occupational','HV05-01'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END




IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Exposed Other'  and Code='HV05-02')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Post-Exposure Prophylaxis(PEP)'),'Exposed Other','HV05-02'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END



IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Exposed Total'  and Code='HV05-03')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Post-Exposure Prophylaxis(PEP)'),'Exposed Total','HV05-03'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END



IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='PEP Occupational'  and Code='HV05-04')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Post-Exposure Prophylaxis(PEP)'),'PEP Occupational','HV05-04'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END





IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='PEP Other'  and Code='HV05-05')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Post-Exposure Prophylaxis(PEP)'),'PEP Other','HV05-05'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END



IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='PEP Total'  and Code='(Sum HV05-01 to HV05-05)HV05-06')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Post-Exposure Prophylaxis(PEP)'),'PEP Total','(Sum HV05-01 to HV05-05)HV05-06'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END




IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='KeyPop on MAT'  and Code='HV06-01')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Methadone Assisted Therapy(MAT)'),'KeyPop on MAT','HV06-01'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END




IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='MAT Clients Known HIV+'  and Code='HV06-02')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Methadone Assisted Therapy(MAT)'),'MAT Clients Known HIV+','HV06-02'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END



IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='MAT Clients Test HIV'  and Code='HV06-03')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Methadone Assisted Therapy(MAT)'),'MAT Clients Test HIV','HV06-03'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END


IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='MAT Clients Test New HIV+'  and Code='HV06-04')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Methadone Assisted Therapy(MAT)'),'MAT Clients Test New HIV+','HV06-04'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END





IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='HIV+ MAT Clients on ART'  and Code='HV06-05')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Methadone Assisted Therapy(MAT)'),'HIV+ MAT Clients on ART','HV06-05'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END



IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Circumcised <1'  and Code='HV04-01')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Number Circumcised'),'Circumcised <1','HV04-01'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END



IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Circumcised 1-9yrs'  and Code='HV04-02')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Number Circumcised'),'Circumcised 1-9yrs','HV04-02'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END



IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Circumcised 10-14yrs'  and Code='HV04-03')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Number Circumcised'),'Circumcised 10-14yrs','HV04-03'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END




IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Circumcised 15-19yrs'  and Code='HV04-04')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Number Circumcised'),'Circumcised 15-19yrs','HV04-04'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END




IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Circumcised 20-24yrs'  and Code='HV04-05')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Number Circumcised'),'Circumcised 20-24yrs','HV04-05'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END







IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Circumcised 25+'  and Code='HV04-06')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Number Circumcised'),'Circumcised 25+','HV04-06'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END




IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Circumcised Total'  and Code='(Sum HV04-01 to HV04-06)HV04-07')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Number Circumcised'),'Circumcised Total','(Sum HV04-01 to HV04-06)HV04-07'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END




IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Surgical'  and Code='HV04-11')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Type of Circumcision'),'Surgical','HV04-11'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END


IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Devices'  and Code='HV04-12')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Type of Circumcision'),'Devices','HV04-12'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END




IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Adverse Even During Moderate'  and Code='HV04-13')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Circumcision Adverse Events'),'Adverse Even During Moderate','HV04-13'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END



IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Adverse Events During Servere'  and Code='HV04-14')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Circumcision Adverse Events'),'Adverse Events During Servere','HV04-14'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END





IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Adverse Events Post Moderate'  and Code='HV04-15')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Circumcision Adverse Events'),'Adverse Events Post Moderate','HV04-15'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END



IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Adverse Events Post Servere'  and Code='HV04-16')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Circumcision Adverse Events'),'Adverse Events Post Servere','HV04-16'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END



IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Follow Up Visit <14 days'  and Code='HV04-17')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy)
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Circumcision Adverse Events'),'Follow Up Visit <14 days','HV04-17'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1')
END