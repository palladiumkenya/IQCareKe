--STAREHE
If Not Exists(SELECT * FROM [dbo].[County] where Subcountyname='STAREHE' AND WardName = 'NAIROBI CENTRAL')
BEGIN
	INSERT INTO [dbo].[County] ([CountyId], [CountyName], [SubcountyId], [Subcountyname], [WardId], [WardName])
	VALUES(47, 'NAIROBI', 287, 'STAREHE', 1426, 'NAIROBI CENTRAL');
END;

If Not Exists(SELECT * FROM [dbo].[County] where Subcountyname='STAREHE' AND WardName = 'NGARA')
BEGIN
	INSERT INTO [dbo].[County] ([CountyId], [CountyName], [SubcountyId], [Subcountyname], [WardId], [WardName])
	VALUES(47, 'NAIROBI', 287, 'STAREHE', 1427, 'NGARA');
END;

If Not Exists(SELECT * FROM [dbo].[County] where Subcountyname='STAREHE' AND WardName = 'PANGANI')
BEGIN
	INSERT INTO [dbo].[County] ([CountyId], [CountyName], [SubcountyId], [Subcountyname], [WardId], [WardName])
	VALUES(47, 'NAIROBI', 287, 'STAREHE', 1428, 'PANGANI');
END;

If Not Exists(SELECT * FROM [dbo].[County] where Subcountyname='STAREHE' AND WardName = 'ZIWANI/KARIOKOR')
BEGIN
	INSERT INTO [dbo].[County] ([CountyId], [CountyName], [SubcountyId], [Subcountyname], [WardId], [WardName])
	VALUES(47, 'NAIROBI', 287, 'STAREHE', 1428, 'ZIWANI/KARIOKOR');
END;

If Not Exists(SELECT * FROM [dbo].[County] where Subcountyname='STAREHE' AND WardName = 'LANDIMAWE')
BEGIN
	INSERT INTO [dbo].[County] ([CountyId], [CountyName], [SubcountyId], [Subcountyname], [WardId], [WardName])
	VALUES(47, 'NAIROBI', 287, 'STAREHE', 1429, 'LANDIMAWE');
END;

If Not Exists(SELECT * FROM [dbo].[County] where Subcountyname='STAREHE' AND WardName = 'NAIROBI SOUTH')
BEGIN
	INSERT INTO [dbo].[County] ([CountyId], [CountyName], [SubcountyId], [Subcountyname], [WardId], [WardName])
	VALUES(47, 'NAIROBI', 287, 'STAREHE', 1430, 'NAIROBI SOUTH');
END;

--MATHARE
If Not Exists(SELECT * FROM [dbo].[County] where Subcountyname='MATHARE' AND WardName = 'HOSPITAL')
BEGIN
	INSERT INTO [dbo].[County] ([CountyId], [CountyName], [SubcountyId], [Subcountyname], [WardId], [WardName])
	VALUES(47, 'NAIROBI', 288, 'MATHARE', 1431, 'HOSPITAL');
END;


If Not Exists(SELECT * FROM [dbo].[County] where Subcountyname='MATHARE' AND WardName = 'MABATINI')
BEGIN
	INSERT INTO [dbo].[County] ([CountyId], [CountyName], [SubcountyId], [Subcountyname], [WardId], [WardName])
	VALUES(47, 'NAIROBI', 288, 'MATHARE', 1432, 'MABATINI');
END;


If Not Exists(SELECT * FROM [dbo].[County] where Subcountyname='MATHARE' AND WardName = 'HURUMA')
BEGIN
	INSERT INTO [dbo].[County] ([CountyId], [CountyName], [SubcountyId], [Subcountyname], [WardId], [WardName])
	VALUES(47, 'NAIROBI', 288, 'MATHARE', 1433, 'HURUMA');
END;


If Not Exists(SELECT * FROM [dbo].[County] where Subcountyname='MATHARE' AND WardName = 'NGEI')
BEGIN
	INSERT INTO [dbo].[County] ([CountyId], [CountyName], [SubcountyId], [Subcountyname], [WardId], [WardName])
	VALUES(47, 'NAIROBI', 288, 'MATHARE', 1434, 'NGEI');
END;


If Not Exists(SELECT * FROM [dbo].[County] where Subcountyname='MATHARE' AND WardName = 'MLANGO KUBWA')
BEGIN
	INSERT INTO [dbo].[County] ([CountyId], [CountyName], [SubcountyId], [Subcountyname], [WardId], [WardName])
	VALUES(47, 'NAIROBI', 288, 'MATHARE', 1435, 'MLANGO KUBWA');
END;

If Not Exists(SELECT * FROM [dbo].[County] where Subcountyname='MATHARE' AND WardName = 'KIAMAIKO')
BEGIN
	INSERT INTO [dbo].[County] ([CountyId], [CountyName], [SubcountyId], [Subcountyname], [WardId], [WardName])
	VALUES(47, 'NAIROBI', 288, 'MATHARE', 1436, 'KIAMAIKO');
END;

--KAMUKUNJI
If Not Exists(SELECT * FROM [dbo].[County] where Subcountyname='KAMUKUNJI' AND WardName = 'PUMWANI')
BEGIN
	INSERT INTO [dbo].[County] ([CountyId], [CountyName], [SubcountyId], [Subcountyname], [WardId], [WardName])
	VALUES(47, 'NAIROBI', 289, 'KAMUKUNJI', 1437, 'PUMWANI');
END;

If Not Exists(SELECT * FROM [dbo].[County] where Subcountyname='KAMUKUNJI' AND WardName = 'EASTLEIGH NORTH')
BEGIN
	INSERT INTO [dbo].[County] ([CountyId], [CountyName], [SubcountyId], [Subcountyname], [WardId], [WardName])
	VALUES(47, 'NAIROBI', 289, 'KAMUKUNJI', 1438, 'EASTLEIGH NORTH');
END;


If Not Exists(SELECT * FROM [dbo].[County] where Subcountyname='KAMUKUNJI' AND WardName = 'EASTLEIGH SOUTH')
BEGIN
	INSERT INTO [dbo].[County] ([CountyId], [CountyName], [SubcountyId], [Subcountyname], [WardId], [WardName])
	VALUES(47, 'NAIROBI', 289, 'KAMUKUNJI', 1439, 'EASTLEIGH SOUTH');
END;

If Not Exists(SELECT * FROM [dbo].[County] where Subcountyname='KAMUKUNJI' AND WardName = 'AIRBASE')
BEGIN
	INSERT INTO [dbo].[County] ([CountyId], [CountyName], [SubcountyId], [Subcountyname], [WardId], [WardName])
	VALUES(47, 'NAIROBI', 289, 'KAMUKUNJI', 1440, 'AIRBASE');
END;

If Not Exists(SELECT * FROM [dbo].[County] where Subcountyname='KAMUKUNJI' AND WardName = 'CALIFORNIA')
BEGIN
	INSERT INTO [dbo].[County] ([CountyId], [CountyName], [SubcountyId], [Subcountyname], [WardId], [WardName])
	VALUES(47, 'NAIROBI', 289, 'KAMUKUNJI', 1441, 'CALIFORNIA');
END;

--MAKADARA
If Not Exists(SELECT * FROM [dbo].[County] where Subcountyname='MAKADARA' AND WardName = 'MARINGO/HAMZA')
BEGIN
	INSERT INTO [dbo].[County] ([CountyId], [CountyName], [SubcountyId], [Subcountyname], [WardId], [WardName])
	VALUES(47, 'NAIROBI', 290, 'MAKADARA', 1442, 'MARINGO/HAMZA');
END;


If Not Exists(SELECT * FROM [dbo].[County] where Subcountyname='MAKADARA' AND WardName = 'VIWANDANI')
BEGIN
	INSERT INTO [dbo].[County] ([CountyId], [CountyName], [SubcountyId], [Subcountyname], [WardId], [WardName])
	VALUES(47, 'NAIROBI', 290, 'MAKADARA', 1443, 'VIWANDANI');
END;


If Not Exists(SELECT * FROM [dbo].[County] where Subcountyname='MAKADARA' AND WardName = 'HARAMBEE')
BEGIN
	INSERT INTO [dbo].[County] ([CountyId], [CountyName], [SubcountyId], [Subcountyname], [WardId], [WardName])
	VALUES(47, 'NAIROBI', 290, 'MAKADARA', 1444, 'HARAMBEE');
END;


If Not Exists(SELECT * FROM [dbo].[County] where Subcountyname='MAKADARA' AND WardName = 'MAKONGENI')
BEGIN
	INSERT INTO [dbo].[County] ([CountyId], [CountyName], [SubcountyId], [Subcountyname], [WardId], [WardName])
	VALUES(47, 'NAIROBI', 290, 'MAKADARA', 1445, 'MAKONGENI');
END;

--EMBAKASI WEST
If Not Exists(SELECT * FROM [dbo].[County] where Subcountyname='EMBAKASI WEST' AND WardName = 'UMOJA I')
BEGIN
	INSERT INTO [dbo].[County] ([CountyId], [CountyName], [SubcountyId], [Subcountyname], [WardId], [WardName])
	VALUES(47, 'NAIROBI', 291, 'EMBAKASI WEST', 1446, 'UMOJA I');
END;

If Not Exists(SELECT * FROM [dbo].[County] where Subcountyname='EMBAKASI WEST' AND WardName = 'UMOJA II')
BEGIN
	INSERT INTO [dbo].[County] ([CountyId], [CountyName], [SubcountyId], [Subcountyname], [WardId], [WardName])
	VALUES(47, 'NAIROBI', 291, 'EMBAKASI WEST', 1447, 'UMOJA II');
END;

If Not Exists(SELECT * FROM [dbo].[County] where Subcountyname='EMBAKASI WEST' AND WardName = 'MOWLEM')
BEGIN
	INSERT INTO [dbo].[County] ([CountyId], [CountyName], [SubcountyId], [Subcountyname], [WardId], [WardName])
	VALUES(47, 'NAIROBI', 291, 'EMBAKASI WEST', 1448, 'MOWLEM');
END;

If Not Exists(SELECT * FROM [dbo].[County] where Subcountyname='EMBAKASI WEST' AND WardName = 'KARIOBANGI SOUTH')
BEGIN
	INSERT INTO [dbo].[County] ([CountyId], [CountyName], [SubcountyId], [Subcountyname], [WardId], [WardName])
	VALUES(47, 'NAIROBI', 291, 'EMBAKASI WEST', 1449, 'KARIOBANGI SOUTH');
END;