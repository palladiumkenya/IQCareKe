IF  EXISTS(select * FROM sys.views where name like  'VW_ModuleIdentifiers')
BEGIN
EXEC('
ALTER VIEW [dbo].[VW_ModuleIdentifiers]
AS
SELECT     mm.ModuleID, mm.ModuleName,mm.DisplayName, mm.CanEnroll, mpi.ID AS FieldId, mpi.FieldName, mpi.FieldType, mpi.Label AS FieldLabel, mpi.AutoPopulateNumber,mi.RequiredFlag
FROM         dbo.mst_module AS mm INNER JOIN
                      dbo.lnk_PatientModuleIdentifier AS mi ON mm.ModuleID = mi.ModuleID INNER JOIN
                      dbo.mst_PatientIdentifier AS mpi ON mpi.ID = mi.FieldID
WHERE     (mm.DeleteFlag = 0) OR
                      (mm.DeleteFlag IS NULL)')

END

IF NOT EXISTS(select * FROM sys.views where name like  'VW_ModuleIdentifiers')
BEGIN
EXEC('
CREATE VIEW [dbo].[VW_ModuleIdentifiers]
AS
SELECT     mm.ModuleID, mm.ModuleName,mm.DisplayName, mm.CanEnroll, mpi.ID AS FieldId, mpi.FieldName, mpi.FieldType, mpi.Label AS FieldLabel, mpi.AutoPopulateNumber,mi.RequiredFlag
FROM         dbo.mst_module AS mm INNER JOIN
                      dbo.lnk_PatientModuleIdentifier AS mi ON mm.ModuleID = mi.ModuleID INNER JOIN
                      dbo.mst_PatientIdentifier AS mpi ON mpi.ID = mi.FieldID
WHERE     (mm.DeleteFlag = 0) OR
                      (mm.DeleteFlag IS NULL)')

END