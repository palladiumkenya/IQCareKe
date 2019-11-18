IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OtzActivityForm]') AND type in (N'U')) 
BEGIN
CREATE TABLE [dbo].[OtzActivityForm](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientMasterVisitId] [int] NULL,
	[AttendedSupportGroup] [int] NULL,
	[Remarks] [text] NULL,
	[UserId] [int] NULL,
	[DeleteFlag] [bit] NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_OtzActivityForm] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OtzActivityTopics]') AND type in (N'U')) 
BEGIN
CREATE TABLE [dbo].[OtzActivityTopics](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ActivityFormId] [int] NOT NULL,
	[TopicId] [int] NULL,
	[DateCompleted] [datetime] NULL,
 CONSTRAINT [PK_OtzActivityTopics] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
