IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HeiMilestone]') AND type in (N'U')) 
BEGIN
DROP TABLE HeiMilestone
END