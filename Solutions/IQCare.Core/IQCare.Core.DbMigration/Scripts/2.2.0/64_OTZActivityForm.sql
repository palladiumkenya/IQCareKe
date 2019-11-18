IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OtzActivityForm]') AND type in (N'U')) 
BEGIN
CREATE TABLE [dbo].[OtzActivityForm](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientMasterVisitId] [int] NULL,
	[TopicId] [int] NULL,
	[DateCompleted] [datetime] NULL,
	[UserId] [int] NULL,
	[DeleteFlag] [bit] NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_OtzActivityForm] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
