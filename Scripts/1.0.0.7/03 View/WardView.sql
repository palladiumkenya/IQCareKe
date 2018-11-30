IF EXISTS(select * FROM sys.views where name = 'WardView')
DROP VIEW [dbo].[WardView]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW  [dbo].[WardView]
AS
select distinct SubCountyId,WardId,WardName from County


go