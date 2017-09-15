update dbo.Mst_LabTestParameter set DeleteFlag=1
FROM dbo.mst_LabTestMaster A INNER JOIN
dbo.Mst_LabTestParameter  ON A.Id = dbo.Mst_LabTestParameter.LabTestId
WHERE        (A.Name = 'CD4')  AND (dbo.Mst_LabTestParameter.ReferenceId = 'CD4PERCENT')