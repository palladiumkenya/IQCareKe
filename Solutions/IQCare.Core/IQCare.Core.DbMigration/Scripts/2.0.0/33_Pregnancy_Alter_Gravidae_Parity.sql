UPDATE [dbo].[Pregnancy] SET [Gravidae] = '' WHERE [Gravidae] = 'null';
UPDATE [dbo].[Pregnancy] SET [Parity] = '' WHERE [Parity] = 'null';

ALTER TABLE [dbo].[Pregnancy] ALTER COLUMN [Gravidae] int null
ALTER TABLE [dbo].[Pregnancy] ALTER COLUMN [Parity] int null