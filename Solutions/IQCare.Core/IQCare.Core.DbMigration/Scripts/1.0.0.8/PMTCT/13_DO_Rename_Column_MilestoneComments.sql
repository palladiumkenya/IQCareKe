IF (EXISTS (SELECT *  FROM sys.all_columns  WHERE name = 'MilestoneComments'))
BEGIN 
EXEC sp_rename 'PatientMilestone.MilestoneComments', 'Comment', 'COLUMN';
END

IF (EXISTS (SELECT *  FROM sys.all_columns  WHERE name = 'MilestoneAssessedId'))
BEGIN 
EXEC sp_rename 'PatientMilestone.MilestoneAssessedId', 'TypeAssessedId', 'COLUMN';
END


IF (EXISTS (SELECT *  FROM sys.all_columns  WHERE name = 'MilestoneAchievedId'))
BEGIN 
EXEC sp_rename 'PatientMilestone.MilestoneAchievedId', 'AchievedId', 'COLUMN';
END

IF (EXISTS (SELECT *  FROM sys.all_columns  WHERE name = 'MilestoneStatusId'))
BEGIN 
EXEC sp_rename 'PatientMilestone.MilestoneStatusId', 'StatusId', 'COLUMN';
END
