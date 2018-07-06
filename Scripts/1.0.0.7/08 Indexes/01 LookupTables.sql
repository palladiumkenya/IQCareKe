IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[LookupItem]') AND name = N'NCI_LookupItem_Name')
DROP INDEX [NCI_LookupItem_Name] ON [dbo].[LookupItem]
GO
CREATE UNIQUE NONCLUSTERED INDEX [NCI_LookupItem_Name] ON [dbo].[LookupItem]
(
	[Name] ASC
)
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[LookupMaster]') AND name = N'NCI_LookupMaster_Name')
DROP INDEX [NCI_LookupMaster_Name] ON [dbo].[LookupMaster]
GO
CREATE UNIQUE NONCLUSTERED INDEX [NCI_LookupMaster_Name] ON [dbo].[LookupMaster]
(
	[Name] ASC
)
GO