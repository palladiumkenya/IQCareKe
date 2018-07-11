IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_ILMessageViewer]'))
	DROP VIEW [dbo].[vw_ILMessageViewer]
GO
/****** Object:  View [dbo].[vw_ILMessageViewer]    Script Date: 7/11/2018 11:02:07 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[vw_ILMessageViewer]
AS
SELECT
Id, 
DateReceived, 
CASE (SenderId) WHEN '1' THEN 'ADT' WHEN '2' THEN 'T4A' WHEN '3' THEN 'MLAB' WHEN '4' THEN 'MLAB_SMS_APP' WHEN '5' THEN 'KENYAEMR' ELSE 'IL' END AS SenderSystem,
						  SenderId, Message, Processed, DateProcessed, LogMessage, MessageType, IsSuccess
FROM            dbo.ApiInbox
GO



