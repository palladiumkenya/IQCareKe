UPDATE [dbo].[Mst_LabTestParameter] SET DataType = 'SELECTLIST' where LabTestId = (select top 1 Id from [dbo].[mst_LabTestMaster] where Name = 'Epstein Barr Virus (EBV)');

IF EXISTS(SELECT * FROM [dbo].[Mst_LabTestParameter] where LabTestId = (select top 1 Id from [dbo].[mst_LabTestMaster] where Name = 'Epstein Barr Virus (EBV)') AND ParameterName = 'Epstein Barr Virus (EBV)' AND DeleteFlag = 0)
BEGIN
	INSERT INTO [dbo].[dtl_LabTestParameterResultOption] (ParameterId, Value, DeleteFlag)
	VALUES((SELECT top 1 Id FROM [dbo].[Mst_LabTestParameter] where LabTestId = (select top 1 Id from [dbo].[mst_LabTestMaster] where Name = 'Epstein Barr Virus (EBV)') AND ParameterName = 'Epstein Barr Virus (EBV)' AND DeleteFlag = 0), 'Positive', 0);

	INSERT INTO [dbo].[dtl_LabTestParameterResultOption] (ParameterId, Value, DeleteFlag)
	VALUES((SELECT top 1 Id FROM [dbo].[Mst_LabTestParameter] where LabTestId = (select top 1 Id from [dbo].[mst_LabTestMaster] where Name = 'Epstein Barr Virus (EBV)') AND ParameterName = 'Epstein Barr Virus (EBV)' AND DeleteFlag = 0), 'Negative', 0);
END