IF EXISTS(select * FROM sys.views where name = 'SubCountyView')
DROP VIEW [dbo].[SubCountyView]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW  [dbo].[SubCountyView]
AS

select distinct CountyId,SubCountyId,SubCountyName from County 

go