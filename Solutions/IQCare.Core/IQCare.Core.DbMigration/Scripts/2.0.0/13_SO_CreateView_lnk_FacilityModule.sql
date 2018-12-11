IF EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_ln_FacilityModule]'))
DROP VIEW [vw_ln_FacilityModule]
GO

CREATE VIEW [dbo].[vw_ln_FacilityModule]
AS
SELECT 
l.FacilityID,
l.ModuleID,
m.ModuleName,
l.UserID,
l.CreateDate,
l.UpdateDate
 FROM lnk_FacilityModule l

INNER JOIN 
mst_module m
ON 

m.ModuleID=l.ModuleID


