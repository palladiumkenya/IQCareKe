 DECLARE @PregnancyParamId INT = (SELECT TOP 1 Id FROM Mst_LabTestParameter WHERE ParameterName = 'Pregnancy');
 
 UPDATE Mst_LabTestParameter SET DataType = 'SELECTLIST' WHERE Id = @PregnancyParamId;

INSERT INTO [dbo].[dtl_LabTestParameterResultOption] ([ParameterId] ,[Value] ,[DeleteFlag]) VALUES 
             (@PregnancyParamId,'Positive',0),
	         (@PregnancyParamId,'Negative',0);