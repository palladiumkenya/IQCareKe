DECLARE @FT int
EXECUTE @FT = [dbo].[FamilyTesting_To_Greencard]
GO
EXECUTE sp_msforeachtable 'ALTER TABLE ? enable trigger ALL'
Go