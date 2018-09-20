IF NOT EXISTS(SELECT TOP 1 Id FROM [dbo].[Interop_PlacerType] WHERE Name = N'IQCARE')
BEGIN
	SET IDENTITY_INSERT [dbo].[Interop_PlacerType] ON
	INSERT INTO [dbo].[Interop_PlacerType] ([Id], [Name]) VALUES (1, N'IQCARE');
	SET IDENTITY_INSERT [dbo].[Interop_PlacerType] OFF
END;


IF NOT EXISTS(SELECT TOP 1 Id FROM [dbo].[Interop_PlacerType] WHERE Name = N'KENYAEMR')
BEGIN
	SET IDENTITY_INSERT [dbo].[Interop_PlacerType] ON
	INSERT INTO [dbo].[Interop_PlacerType] ([Id], [Name]) VALUES (2, N'KENYAEMR');
	SET IDENTITY_INSERT [dbo].[Interop_PlacerType] OFF
END;


IF NOT EXISTS(SELECT TOP 1 Id FROM [dbo].[Interop_PlacerType] WHERE Name = N'ADT')
BEGIN
	SET IDENTITY_INSERT [dbo].[Interop_PlacerType] ON
	INSERT INTO [dbo].[Interop_PlacerType] ([Id], [Name]) VALUES (3, N'ADT');
	SET IDENTITY_INSERT [dbo].[Interop_PlacerType] OFF
END;


IF NOT EXISTS(SELECT TOP 1 Id FROM [dbo].[Interop_PlacerType] WHERE Name = N'T4A')
BEGIN
	SET IDENTITY_INSERT [dbo].[Interop_PlacerType] ON
	INSERT INTO [dbo].[Interop_PlacerType] ([Id], [Name]) VALUES (4, N'T4A');
	SET IDENTITY_INSERT [dbo].[Interop_PlacerType] OFF
END;


IF NOT EXISTS(SELECT TOP 1 Id FROM [dbo].[Interop_PlacerType] WHERE Name = N'TIBU')
BEGIN
	SET IDENTITY_INSERT [dbo].[Interop_PlacerType] ON
	INSERT INTO [dbo].[Interop_PlacerType] ([Id], [Name]) VALUES (5, N'TIBU');
	SET IDENTITY_INSERT [dbo].[Interop_PlacerType] OFF
END;


IF NOT EXISTS(SELECT TOP 1 Id FROM [dbo].[Interop_PlacerType] WHERE Name = N'SETS')
BEGIN
	SET IDENTITY_INSERT [dbo].[Interop_PlacerType] ON
	INSERT INTO [dbo].[Interop_PlacerType] ([Id], [Name]) VALUES (6, N'SETS');
	SET IDENTITY_INSERT [dbo].[Interop_PlacerType] OFF
END;

IF NOT EXISTS(SELECT TOP 1 Id FROM [dbo].[Interop_PlacerType] WHERE Name = N'AFYAMOBILE')
BEGIN
	SET IDENTITY_INSERT [dbo].[Interop_PlacerType] ON
	INSERT INTO [dbo].[Interop_PlacerType] ([Id], [Name]) VALUES (6, N'AFYAMOBILE');
	SET IDENTITY_INSERT [dbo].[Interop_PlacerType] OFF
END;


IF NOT EXISTS(SELECT TOP 1 Id FROM [dbo].[IdentifierType] WHERE Name = N'Appointment')
BEGIN
	SET IDENTITY_INSERT [dbo].[IdentifierType] ON
	INSERT INTO [dbo].[IdentifierType] ([Id], [Name]) VALUES (3, N'Appointment');
	SET IDENTITY_INSERT [dbo].[IdentifierType] OFF
END;

IF NOT EXISTS(SELECT TOP 1 Id FROM [dbo].[IdentifierType] WHERE Name = N'HTSENCOUNTER')
BEGIN
	SET IDENTITY_INSERT [dbo].[IdentifierType] ON
	INSERT INTO [dbo].[IdentifierType] ([Id], [Name]) VALUES (4, N'HTSENCOUNTER');
	SET IDENTITY_INSERT [dbo].[IdentifierType] OFF
END;