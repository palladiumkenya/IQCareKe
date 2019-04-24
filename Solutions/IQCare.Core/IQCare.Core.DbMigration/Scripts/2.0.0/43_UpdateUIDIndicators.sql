IF NOT EXISTS(SELECT  1 FROM sys.columns 
         WHERE Name = N'UID'
          and Object_ID = Object_ID(N'[dbo].[Indicator]'))
BEGIN
    ALTER TABLE [dbo].[Indicator] ADD    [UID] varchar(max)  null
END

GO

IF NOT EXISTS(SELECT  1 FROM sys.columns 
         WHERE Name = N'UID'
          and Object_ID = Object_ID(N'[dbo].[Section]'))
BEGIN
    ALTER TABLE [dbo].[Section] ADD    [UID] varchar(max)  null
END

go

IF EXISTS(select * from Section where SectionName='Voluntary Male Circumcision')
BEGIN
UPDATE Section SET  [UID]='bbMNLyKCnkm' where SectionName='Voluntary Male Circumcision'
END



go

IF EXISTS(select * from Section where SectionName='Voluntary Male Circumcision')
BEGIN
UPDATE Section SET SectionName='MOH 731-4 Medical Male Circumcision Revision 2018'
where SectionName='Voluntary Male Circumcision'
END


go


IF EXISTS(select * from Section where SectionName='Post-Exposure Prophylaxis')
BEGIN
UPDATE Section SET  [UID]='do43iCbnDrs' where SectionName='Post-Exposure Prophylaxis'
END



go

IF EXISTS(select * from Section where SectionName='Post-Exposure Prophylaxis')
BEGIN
UPDATE Section SET SectionName='MOH 731-5 Post Exposure Prophylaxis Revision 2018'
where SectionName='Post-Exposure Prophylaxis'
END


go


IF EXISTS(select * from Section where SectionName='Methadone Assisted Therapy')
BEGIN
UPDATE Section SET  [UID]='RRC2sWfhVqF' where SectionName='Methadone Assisted Therapy'
END



go

IF EXISTS(select * from Section where SectionName='Methadone Assisted Therapy')
BEGIN
UPDATE Section SET SectionName='MOH 731-6 Methadone Assisted Therapy revision 2018'
where SectionName='Methadone Assisted Therapy'
END





--update IndicatorDetails

select * from Indicator
IF EXISTS(select * from Indicator where Code='HV04-01'  and IndicatorName='Circumcised <1')
BEGIN
UPDATE  Indicator set  [UID] ='ZPcn7zMAZ1V'
 where Code='HV04-01'  and IndicatorName='Circumcised <1'
END

GO

IF EXISTS(select * from Indicator where Code='HV04-02'  and IndicatorName='Circumcised 1-9yrs')
BEGIN
UPDATE  Indicator set  [UID] ='GGKtWopm7vD'
 where Code='HV04-02'  and IndicatorName='Circumcised 1-9yrs'
END

GO


IF EXISTS(select * from Indicator where Code='HV04-03'  and IndicatorName='Circumcised 10-14yrs')
BEGIN
UPDATE  Indicator set  [UID] ='hx68TrqkSh1'
 where  Code='HV04-03'  and IndicatorName='Circumcised 10-14yrs'
END

go



IF EXISTS(select * from Indicator where Code='HV04-04'  and IndicatorName='Circumcised 15-19yrs')
BEGIN
UPDATE  Indicator set  [UID] ='i99a9CT4YeV'
 where  Code='HV04-04'  and IndicatorName='Circumcised 15-19yrs'
END

go

IF EXISTS(select * from Indicator where Code='HV04-05'  and IndicatorName='Circumcised 20-24yrs')
BEGIN
UPDATE  Indicator set  [UID] ='WyVWHKRa4i8'
 where  Code='HV04-05'  and IndicatorName='Circumcised 20-24yrs'
END

go


IF EXISTS(select * from Indicator where Code='HV04-06'  and IndicatorName='Circumcised 25+')
BEGIN
UPDATE  Indicator set  [UID] ='yivJXC2pAOM'
  where Code='HV04-06'  and IndicatorName='Circumcised 25+'
END

go



IF EXISTS(select * from Indicator where Code='(Sum HV04-01 to HV04-06)HV04-07'  and IndicatorName='Circumcised Total')
BEGIN
UPDATE  Indicator set  [UID] ='dcuHzdP43Tt'
  where Code='(Sum HV04-01 to HV04-06)HV04-07'  and IndicatorName='Circumcised Total'
END



IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Circumcised HIV+'  and Code='HV04-08')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy,[UID])
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Number Circumcised'),'Circumcised HIV+','HV04-08'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1','H6zXw6KeKb7')
END

go



IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Circumcised HIV-'  and Code='HV04-09')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy,[UID])
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Number Circumcised'),'Circumcised HIV-','HV04-09'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1','zhY6Bj4mKt8')
END

go


IF NOT EXISTS(select * from [dbo].[Indicator] where IndicatorName='Circumcised_HIV NK'  and Code='HV04-10')
BEGIN
INSERT INTO [dbo].[Indicator](SubSectionId,IndicatorName,Code,DataTypeId,CreateDate,DeleteFlag,CreatedBy,[UID])
VALUES((select Id  from [dbo].[SubSection] where SubSectionName='Number Circumcised'),'Circumcised_HIV NK','HV04-10'
,(select Id from [dbo].[DataType] where [Name]='Numeric'),GETDATE(),'0','1','R8GqrTWygR4')
END



go



IF EXISTS(select * from Indicator where Code='HV04-11'  and IndicatorName='Surgical')
BEGIN
UPDATE  Indicator set  [UID] ='JYYtp6AwPD6'
  where Code='HV04-11'  and IndicatorName='Surgical'
END

go




IF EXISTS(select * from Indicator where Code='HV04-12'  and IndicatorName='Devices')
BEGIN
UPDATE  Indicator set  [UID] ='BOJNBgkeXBu'
  where Code='HV04-12'  and IndicatorName='Devices'
END

go

IF EXISTS(select * from Indicator where Code='HV04-13'  and IndicatorName='Adverse Even During Moderate')
BEGIN
UPDATE  Indicator set  [UID] ='KzhBouepg13'
  where Code='HV04-13'  and IndicatorName='Adverse Even During Moderate'
END

go


IF EXISTS(select * from Indicator where Code='HV04-14'  and IndicatorName='Adverse Events During Servere')
BEGIN
UPDATE  Indicator set  [UID] ='zVZywwIJxcR'
  where Code='HV04-14'  and IndicatorName='Adverse Events During Servere'
END

go




IF EXISTS(select * from Indicator where Code='HV04-15'  and IndicatorName='Adverse Events Post Moderate')
BEGIN
UPDATE  Indicator set  [UID] ='OseYV3376eB'
  where Code='HV04-15'  and IndicatorName='Adverse Events Post Moderate'
END

go



IF EXISTS(select * from Indicator where Code='HV04-16'  and IndicatorName='Adverse Events Post Servere')
BEGIN
UPDATE  Indicator set  [UID] ='kzlGNwD5SMc'
 where Code='HV04-16'  and IndicatorName='Adverse Events Post Servere'
END

go


IF EXISTS(select * from Indicator where Code='HV04-17'  and IndicatorName='Follow Up Visit <14 days')
BEGIN
UPDATE  Indicator set  [UID] ='ZkAhKhZxRp5'
 where Code='HV04-17'  and IndicatorName='Follow Up Visit <14 days'
END

go



IF EXISTS(select * from Indicator where Code='HV05-01'  and IndicatorName='Exposed Occupational')
BEGIN
UPDATE  Indicator set  [UID] ='v3dZh9KqTv0'
 where Code='HV05-01'  and IndicatorName='Exposed Occupational'
END

go


IF EXISTS(select * from Indicator where Code='HV05-02'  and IndicatorName='Exposed Other')
BEGIN
UPDATE  Indicator set  [UID] ='BHbWgZR2CAQ'
 where Code='HV05-02'  and IndicatorName='Exposed Other'
END

go


IF EXISTS(select * from Indicator where Code='HV05-03'  and IndicatorName='Exposed Total')
BEGIN
UPDATE  Indicator set   [UID] ='DO8TG2tc7M0'
 where Code='HV05-03'  and IndicatorName='Exposed Total'
END

go



IF EXISTS(select * from Indicator where Code='HV05-03'  and IndicatorName='Exposed Total')
BEGIN
UPDATE  Indicator set   [UID] ='DO8TG2tc7M0'
 where Code='HV05-03'  and IndicatorName='Exposed Total'
END

go







IF EXISTS(select * from Indicator where Code='HV05-04'  and IndicatorName='PEP Occupational')
BEGIN
UPDATE  Indicator set   [UID] ='SMUVPzuyFAB'
 where Code='HV05-04'  and IndicatorName='PEP Occupational'
END

go


IF EXISTS(select * from Indicator where Code='HV05-05'  and IndicatorName='PEP Other')
BEGIN
UPDATE  Indicator set   [UID] ='KrDWcJMS9Vi'
 where Code='HV05-05'  and IndicatorName='PEP Other'
END

go


IF EXISTS(select * from Indicator where Code='(Sum HV05-01 to HV05-05)HV05-06'  and IndicatorName='PEP Total')
BEGIN
UPDATE  Indicator set   [UID] ='WQlvPW3w3IJ'
 where Code='(Sum HV05-01 to HV05-05)HV05-06'  and IndicatorName='PEP Total'
END

go



IF EXISTS(select * from Indicator where Code='HV06-01'  and IndicatorName='KeyPop on MAT')
BEGIN
UPDATE  Indicator set   [UID] ='a3GYG7B0AZU'
 where Code='HV06-01'  and IndicatorName='KeyPop on MAT'
END

go


IF EXISTS(select * from Indicator where Code='HV06-02'  and IndicatorName='MAT Clients Known HIV+')
BEGIN
UPDATE  Indicator set   [UID] ='OuqOHS02uvU'
 where Code='HV06-02'  and IndicatorName='MAT Clients Known HIV+'
END

go

IF EXISTS(select * from Indicator where Code='HV06-03'  and IndicatorName='MAT Clients Test HIV')
BEGIN
UPDATE  Indicator set   [UID] ='joRfQbNfg23'
 where  Code='HV06-03'  and IndicatorName='MAT Clients Test HIV'
END

go


IF EXISTS(select * from Indicator where Code='HV06-04'  and IndicatorName='MAT Clients Test New HIV+')
BEGIN
UPDATE  Indicator set   [UID] ='AzuCQPugful'
 where  Code='HV06-04'  and IndicatorName='MAT Clients Test New HIV+'
END














