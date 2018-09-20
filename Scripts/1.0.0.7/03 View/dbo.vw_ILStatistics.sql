IF EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_ILStatistics]'))
BEGIN
		DROP VIEW dbo.vw_ILStatistics
 END
 GO

 /****** Object:  View [dbo].[PersonExtView]    Script Date: 07-Jun-2018 18:39:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_ILStatistics]'))
EXEC dbo.sp_executesql @statement = N'
CREATE VIEW [dbo].[vw_ILStatistics]
AS
SELECT        
		1 [Id]
		,''Outgoing Messages'' [Outbox_Messages]
		,COUNT(Id) [Outbox]
		,(SELECT COUNT(ID) FROM ApiInbox) [Inbox]
		,''Incoming Messages'' [Incoming_Messages]
FROM            dbo.ApiOutbox
' 
GO