/*
   Tuesday, April 25, 20172:38:07 PM
   User: 
   Server: LAPTOP-DR050DFG\
   Database: IQCareDefault
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.dtl_LabOrderTestResult ADD
	AuditData xml NULL,
	CreateDate datetime NULL,
	CreatedBy int NULL
GO
ALTER TABLE dbo.dtl_LabOrderTestResult SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.dtl_LabOrderTestResult', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.dtl_LabOrderTestResult', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.dtl_LabOrderTestResult', 'Object', 'CONTROL') as Contr_Per 