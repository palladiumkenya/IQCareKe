IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_ILMessageStats]'))
	DROP VIEW [dbo].[vw_ILMessageStats]
GO
/****** Object:  View [dbo].[vw_ILMessageStats]    Script Date: 7/11/2018 11:04:43 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[vw_ILMessageStats]
AS
SELECT ISNULL(ROW_NUMBER() OVER (ORDER BY T.MessageType ASC), - 1) AS RowID, * FROM (SELECT
MessageType, 
COUNT(Id) AS Count, 
IsSuccess
FROM dbo.ApiInbox
GROUP BY MessageType, IsSuccess
HAVING        (MessageType IS NOT NULL)) T
GO



