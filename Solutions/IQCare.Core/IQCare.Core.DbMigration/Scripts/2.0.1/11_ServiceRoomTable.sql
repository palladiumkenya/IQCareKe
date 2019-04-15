
/****** Object:  Table [dbo].[Rooms]    Script Date: 22/03/2019 12:14:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS
(
	SELECT *
	FROM sys.objects
	WHERE object_id = OBJECT_ID(N'[dbo].[ServiceRoom]')
		  AND type IN(N'U')
)
BEGIN
CREATE TABLE [dbo].[ServiceRoom](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ServicePointId] [int] NOT NULL,
	[RoomId] int not null,
	[ServiceAreaId] int null,
	[DeleteFlag] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] int null
 CONSTRAINT [PK_ServiceRoom] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

ALTER TABLE [dbo].[ServiceRoom] ADD  CONSTRAINT [DF_ServiceRoom_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
ALTER TABLE [dbo].[ServiceRoom] ADD  CONSTRAINT [DF_ServiceRoom_Active]  DEFAULT ((1)) FOR [Active]
ALTER TABLE [dbo].[ServiceRoom] ADD  CONSTRAINT [DF_ServiceRoom_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
ALTER TABLE [dbo].[ServiceRooom] ADD  Constraint [FK_ServiceRoom_ServiceAreaId]
FOREIGN KEY  (ServiceAreaId) REFERENCES ServiceArea(Id)
END