IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientDrugAdministration]') AND type in (N'U')) 
BEGIN
CREATE TABLE [dbo].[PatientDrugAdministration](
	[Id] [int] NOT NULL,
	[PatientId] [int] NOT NULL,
	[PatientMasterVisitId] [int] NOT NULL,
	[DrugAdministered] [int] NULL,
	[Value] [int] NULL,
	[Description] [varchar](max) NULL,
	[DeleteFlag] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreateDate] [int] NULL,
	[AuditData] [varchar](max) NULL,
 CONSTRAINT [PK_PatientDrugAdministration] PRIMARY KEY CLUSTERED (	[Id] ASC)
 )
END