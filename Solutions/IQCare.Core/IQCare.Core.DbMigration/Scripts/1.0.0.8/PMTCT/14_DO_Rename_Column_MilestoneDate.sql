IF (EXISTS (SELECT *  FROM sys.all_columns  WHERE name = 'MilestoneDate'))
BEGIN 
EXEC sp_rename 'PatientMilestone.MilestoneDate', 'DateAssessed', 'COLUMN';
END