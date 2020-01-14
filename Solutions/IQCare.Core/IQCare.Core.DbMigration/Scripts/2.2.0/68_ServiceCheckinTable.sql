IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServiceCheckin]') AND type in (N'U'))
BEGIN
CREATE TABLE ServiceCheckin(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[ServiceId] [int] NOT NULL,
	[Start] [datetime] NOT NULL,
	[End] [datetime] NULL,
	[Active] [bit] NOT NULL,
	[VisitDate] [datetime] NULL,
	[Status] [int] NULL,
	[CreateDate] [datetime] NOT NULL,
	[EMRType] [int] NULL,
	[DeleteFlag] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[AuditData] [varchar](max) NULL
	)




	ALTER TABLE [dbo].[ServiceCheckIn] ADD  CONSTRAINT [DF_ServiceCheckin_start]  DEFAULT (getdate()) FOR [Start]
ALTER TABLE  [dbo].[ServiceCheckIn] ADD  CONSTRAINT [DF_ServiceCheckin_Active]  DEFAULT ((1)) FOR [Active]


ALTER TABLE  [dbo].[ServiceCheckIn] ADD  CONSTRAINT [DF_ServiceCheckin_Status]  DEFAULT ((0)) FOR [Status]


ALTER TABLE [dbo].[ServiceCheckIn] ADD  CONSTRAINT [DF_ServiceCheckin_createDate]  DEFAULT (getdate()) FOR [CreateDate]


ALTER TABLE [dbo].[ServiceCheckIn] ADD  CONSTRAINT [DF_ServiceCheckIn_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]



END