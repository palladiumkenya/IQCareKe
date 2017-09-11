-- Move patient from mst_patient to patient
DECLARE @RC int
EXECUTE @RC = [dbo].[SP_mst_PatientToGreencardRegistration]
GO