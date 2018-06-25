
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF NOT EXISTS
(
	SELECT *
	FROM sys.objects
	WHERE object_id = OBJECT_ID(N'[dbo].[PersonEmergencyContact]')
		  AND type IN(N'U')
)
BEGIN

CREATE TABLE [dbo].[PersonEmergencyContact](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NOT NULL,
	[EmergencyContactPersonId] [int] NOT NULL,
	[RegisteredToClinic] [bit] NULL,
	[MobileContact] [varchar](max) NULL,
	[ContactType] [int] null,
	[DeleteFlag] [bit] NOT NULL CONSTRAINT [DF_PersonEmergencyContact_Void]  DEFAULT ((0)),
	[CreatedBy] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[AuditData] [xml] NULL,
 CONSTRAINT [PK_PersonEmergencyContact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[PersonEmergencyContact]  WITH CHECK ADD  CONSTRAINT [FK_PersonEmergencyContact_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([Id])
GO

ALTER TABLE [dbo].[PersonEmergencyContact] CHECK CONSTRAINT [FK_PersonEmergencyContact_Person]



END