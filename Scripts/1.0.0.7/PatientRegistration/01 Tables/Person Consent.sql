IF NOT EXISTS
(
	SELECT *
	FROM sys.objects
	WHERE object_id = OBJECT_ID(N'[dbo].[PersonConsent]')
		  AND type IN(N'U')
)

CREATE TABLE [dbo].[PersonConsent]
(
[Id] int  IDENTITY(1,1) not null,
[PersonId] int not null,
EmergencyContactId  int null,
[ConsentType] [int] NULL,
[ConsentDate] [datetime]  NULL,
[ConsentValue] [int] NULL,
[ConsentReason] [varchar](max) NULL,
[Active] [bit] NOT NULL CONSTRAINT [DF_PersonConsent_IsActive]  DEFAULT ((1)),
[DeleteFlag] [bit] NOT NULL CONSTRAINT [DF_PersonConsent_Void]  DEFAULT ((0)),
[CreatedBy] [int] NOT NULL,
[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_PersonConsent_CreateDate]  DEFAULT (getdate()),
[AuditData] [xml] NULL
CONSTRAINT [PK_person_Consent] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO





