IF NOT EXISTS
(
	SELECT *
	FROM sys.objects
	WHERE object_id = OBJECT_ID(N'[dbo].[HeiMilestone]')
		  AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[HeiMilestone](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[PatientMasterVisitId] [int] NOT NULL,
	[PatientId] [int] NOT NULL,
	[TypeAssessedId] [int] NULL,
	[AchievedId] [bit] NULL,
	[StatusId] [int] NULL,
	[Comment] [text] NULL,
	[CreatedBy] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[DeleteFlag] [bit] NOT NULL,
	[DateAssessed] [datetime] NULL,
 CONSTRAINT [PK_HeiMilestone] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END