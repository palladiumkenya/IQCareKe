IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientMergingLog]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[PatientMergingLog](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[PreferredPatientId] [int] NOT NULL,
		[UnPreferredPatientId] [int] NOT NULL,
		[CreatedBy] [int] NOT NULL,
		[CreateDate] [datetime] NOT NULL  DEFAULT (getdate()),
	PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

END