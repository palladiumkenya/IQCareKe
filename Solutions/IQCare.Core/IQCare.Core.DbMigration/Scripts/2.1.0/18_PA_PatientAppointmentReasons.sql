/****** Object:  Table [dbo].[PatientAppointmentReasons]    Script Date: 7/16/2019 10:55:26 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PatientAppointmentReasons](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[PatientMasterVisitId] [int] NOT NULL,
	[ReasonAppointmentNotGiven] [int] NOT NULL,
 CONSTRAINT [PK_PatientAppointmentReasons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


