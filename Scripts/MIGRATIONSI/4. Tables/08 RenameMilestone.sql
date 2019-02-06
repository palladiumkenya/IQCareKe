
IF  EXISTS
(
	SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[PatientMilestone]')
          AND type IN(N'U')
)
BEGIN
 IF  EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'MilestoneAssessedId'   --MilestoneAssessedId
          AND Object_ID = Object_ID(N'PatientMilestone'))
BEGIN
EXEC sp_rename '[dbo].[PatientMilestone].MilestoneAssessedId', 'TypeAssessedId','column';
ALTER TABLE dbo.PatientMilestone ALTER Column MilestoneAssessedId int not null
END

IF  EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'MilestoneAchievedId'
          AND Object_ID = Object_ID(N'PatientMilestone'))  --MilestoneAchievedId
BEGIN
EXEC sp_rename '[dbo].[PatientMilestone].MilestoneAchievedId', 'AchievedId','column';


END
IF  EXISTS(SELECT * FROM sys.columns  --MilestoneStatusId
          WHERE Name = N'MilestoneStatusId'
          AND Object_ID = Object_ID(N'PatientMilestone'))
BEGIN
EXEC sp_rename '[dbo].[PatientMilestone].MilestoneStatusId', 'StatusId','column';
END
IF  EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'MilestoneComments'  --MilestoneComments
          AND Object_ID = Object_ID(N'PatientMilestone'))
BEGIN
EXEC sp_rename '[dbo].[PatientMilestone].MilestoneComments', 'Comment','column';
END
IF  EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'MilestoneDate'  --MilestoneDate
          AND Object_ID = Object_ID(N'PatientMilestone'))
BEGIN
EXEC sp_rename '[dbo].[PatientMilestone].MilestoneDate', 'DateAssessed','column';
END
END

IF EXISTS(Select * from sys.columns where Name=N'AchievedId'  AND Object_ID = Object_ID(N'PatientMilestone'))
BEGIN
Alter table [dbo].[PatientMilestone] ALTER COLUMN  AchievedId bit NULL
END
