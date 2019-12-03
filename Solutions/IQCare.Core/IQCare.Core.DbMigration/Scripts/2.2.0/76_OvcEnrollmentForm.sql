IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OvcEnrollmentForm]') AND type in (N'U')) 
BEGIN
CREATE TABLE [dbo].[OvcEnrollmentForm](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int]  NOT NULL,
	[PartnerOVCServices] varchar(max) NULL,
	[EnrollmentDate] [datetime] NULL,
	[CPMISEnrolled] int null,
	[CreatedBy] [int] NULL,
	[DeleteFlag] [bit] NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_OvcEnrollmentForm] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END