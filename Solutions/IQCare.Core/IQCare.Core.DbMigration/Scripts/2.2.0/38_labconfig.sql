-- PCR
UPDATE [dbo].[Mst_LabTestParameter] SET DataType = 'SELECTLIST' WHERE ReferenceId = 'PCR';

IF NOT EXISTS(SELECT * FROM [dbo].[dtl_LabTestParameterResultOption] where ParameterId=(SELECT TOP 1 Id FROM [dbo].[Mst_LabTestParameter] WHERE ReferenceId = 'PCR'))
BEGIN
	IF EXISTS(SELECT TOP 1 Id FROM [dbo].[Mst_LabTestParameter] WHERE ReferenceId = 'PCR')
	BEGIN
		INSERT INTO dtl_LabTestParameterResultOption (ParameterId, Value, DeleteFlag)
		VALUES((SELECT TOP 1 Id FROM [dbo].[Mst_LabTestParameter] WHERE ReferenceId = 'PCR'), 'Positive', 0);

		INSERT INTO dtl_LabTestParameterResultOption (ParameterId, Value, DeleteFlag)
		VALUES((SELECT TOP 1 Id FROM [dbo].[Mst_LabTestParameter] WHERE ReferenceId = 'PCR'), 'Negative', 0);
	END
END


-- HEPATITIS C
UPDATE [dbo].[Mst_LabTestParameter] SET DataType = 'SELECTLIST' WHERE ParameterName = 'Hepatitis C antibody';
IF NOT EXISTS(SELECT * FROM [dbo].[dtl_LabTestParameterResultOption] where ParameterId=(SELECT TOP 1 Id FROM [dbo].[Mst_LabTestParameter] WHERE ParameterName = 'Hepatitis C antibody'))
BEGIN
	IF EXISTS(SELECT TOP 1 Id FROM [dbo].[Mst_LabTestParameter] WHERE ParameterName = 'Hepatitis C antibody')
	BEGIN
		INSERT INTO dtl_LabTestParameterResultOption (ParameterId, Value, DeleteFlag)
		VALUES((SELECT TOP 1 Id FROM [dbo].[Mst_LabTestParameter] WHERE ParameterName = 'Hepatitis C antibody'), 'Positive', 0);

		INSERT INTO dtl_LabTestParameterResultOption (ParameterId, Value, DeleteFlag)
		VALUES((SELECT TOP 1 Id FROM [dbo].[Mst_LabTestParameter] WHERE ParameterName = 'Hepatitis C antibody'), 'Negative', 0);
	END
END


-- HAPITITIS B
UPDATE [dbo].[Mst_LabTestParameter] SET DataType = 'SELECTLIST' WHERE ParameterName = 'Hepatitis B core – antibody IgM (HBsAb)';
IF NOT EXISTS(SELECT * FROM [dbo].[dtl_LabTestParameterResultOption] where ParameterId=(SELECT TOP 1 Id FROM [dbo].[Mst_LabTestParameter] WHERE ParameterName = 'Hepatitis B core – antibody IgM (HBsAb)'))
BEGIN
	IF EXISTS(SELECT TOP 1 Id FROM [dbo].[Mst_LabTestParameter] WHERE ParameterName = 'Hepatitis B core – antibody IgM (HBsAb)')
	BEGIN
		INSERT INTO dtl_LabTestParameterResultOption (ParameterId, Value, DeleteFlag)
		VALUES((SELECT TOP 1 Id FROM [dbo].[Mst_LabTestParameter] WHERE ParameterName = 'Hepatitis B core – antibody IgM (HBsAb)'), 'Positive', 0);

		INSERT INTO dtl_LabTestParameterResultOption (ParameterId, Value, DeleteFlag)
		VALUES((SELECT TOP 1 Id FROM [dbo].[Mst_LabTestParameter] WHERE ParameterName = 'Hepatitis B core – antibody IgM (HBsAb)'), 'Negative', 0);
	END
END


UPDATE [dbo].[Mst_LabTestParameter] SET DataType = 'SELECTLIST' WHERE ParameterName = 'Hepatitis B core – antibody. total';
IF NOT EXISTS(SELECT * FROM [dbo].[dtl_LabTestParameterResultOption] where ParameterId=(SELECT TOP 1 Id FROM [dbo].[Mst_LabTestParameter] WHERE ParameterName = 'Hepatitis B core – antibody. total'))
BEGIN
	IF EXISTS(SELECT TOP 1 Id FROM [dbo].[Mst_LabTestParameter] WHERE ParameterName = 'Hepatitis B core – antibody. total')
	BEGIN
		INSERT INTO dtl_LabTestParameterResultOption (ParameterId, Value, DeleteFlag)
		VALUES((SELECT TOP 1 Id FROM [dbo].[Mst_LabTestParameter] WHERE ParameterName = 'Hepatitis B core – antibody. total'), 'Positive', 0);

		INSERT INTO dtl_LabTestParameterResultOption (ParameterId, Value, DeleteFlag)
		VALUES((SELECT TOP 1 Id FROM [dbo].[Mst_LabTestParameter] WHERE ParameterName = 'Hepatitis B core – antibody. total'), 'Negative', 0);
	END
END

UPDATE [dbo].[Mst_LabTestParameter] SET DataType = 'SELECTLIST' WHERE ParameterName = 'Hepatitis B surface – antibody (HBsAb)';
IF NOT EXISTS(SELECT * FROM [dbo].[dtl_LabTestParameterResultOption] where ParameterId=(SELECT TOP 1 Id FROM [dbo].[Mst_LabTestParameter] WHERE ParameterName = 'Hepatitis B surface – antibody (HBsAb)'))
BEGIN
	IF EXISTS(SELECT TOP 1 Id FROM [dbo].[Mst_LabTestParameter] WHERE ParameterName = 'Hepatitis B surface – antibody (HBsAb)')
	BEGIN
		INSERT INTO dtl_LabTestParameterResultOption (ParameterId, Value, DeleteFlag)
		VALUES((SELECT TOP 1 Id FROM [dbo].[Mst_LabTestParameter] WHERE ParameterName = 'Hepatitis B surface – antibody (HBsAb)'), 'Positive', 0);

		INSERT INTO dtl_LabTestParameterResultOption (ParameterId, Value, DeleteFlag)
		VALUES((SELECT TOP 1 Id FROM [dbo].[Mst_LabTestParameter] WHERE ParameterName = 'Hepatitis B surface – antibody (HBsAb)'), 'Negative', 0);
	END
END

UPDATE [dbo].[Mst_LabTestParameter] SET DataType = 'SELECTLIST' WHERE ParameterName = 'Hepatitis B surface – antigen (HBsAg)';
IF NOT EXISTS(SELECT * FROM [dbo].[dtl_LabTestParameterResultOption] where ParameterId=(SELECT TOP 1 Id FROM [dbo].[Mst_LabTestParameter] WHERE ParameterName = 'Hepatitis B surface – antigen (HBsAg)'))
BEGIN
	IF EXISTS(SELECT TOP 1 Id FROM [dbo].[Mst_LabTestParameter] WHERE ParameterName = 'Hepatitis B surface – antigen (HBsAg)')
	BEGIN
		INSERT INTO dtl_LabTestParameterResultOption (ParameterId, Value, DeleteFlag)
		VALUES((SELECT TOP 1 Id FROM [dbo].[Mst_LabTestParameter] WHERE ParameterName = 'Hepatitis B surface – antigen (HBsAg)'), 'Positive', 0);

		INSERT INTO dtl_LabTestParameterResultOption (ParameterId, Value, DeleteFlag)
		VALUES((SELECT TOP 1 Id FROM [dbo].[Mst_LabTestParameter] WHERE ParameterName = 'Hepatitis B surface – antigen (HBsAg)'), 'Negative', 0);
	END
END