IF EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_ILMessengerLog]'))
BEGIN
		DROP VIEW dbo.vw_ILMessengerLog
 END
 GO

 /****** Object:  View [dbo].[PersonExtView]    Script Date: 07-Jun-2018 18:39:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_ILMessengerLog]'))
EXEC dbo.sp_executesql @statement = N'
CREATE VIEW [dbo].[vw_ILMessengerLog]
AS
SELECT 
Uid,''Outgoing'' [MessageType],[DateSent] [DateGenerated],NULL [DateReceived],[Message],NULL [Status],LogMessage [ErrorLog]

FROM 
ApiOutbox

UNION

SELECT
uid,''Incoming'' [MessageType],NULL [DateGenerated],[DateReceived][DateReceived],Message[Message],Processed [Status],LogMessage [ErrorLog]
FROM ApiInbox
' 
GO