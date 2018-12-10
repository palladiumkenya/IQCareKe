IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientCounselling]') AND type in (N'U')) 
BEGIN
CREATE TABLE [dbo].[PatientCounselling](
    [Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[PatientMasterVisitId] [int] NOT NULL,
	[CounsellingTopicId] [int] NULL,
	[CounsellingDate] [datetime] NULL,
	[Description] [varchar](max) NULL,
	[DeleteFlag] [bit] NULL,
	[CreatedBy] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[AuditData] [varchar](max) NULL,
    CONSTRAINT [FK_PatientCounselling_PatientMasterVisit] FOREIGN KEY([PatientMasterVisitId]) REFERENCES [dbo].[PatientMasterVisit] ([Id]),
	CONSTRAINT [PK_PatientCounselling] PRIMARY KEY CLUSTERED ([Id] ASC)
) 

END