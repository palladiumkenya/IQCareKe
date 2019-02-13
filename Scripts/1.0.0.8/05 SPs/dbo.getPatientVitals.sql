

/****** Object:  StoredProcedure [dbo].[getPatientVitals]    Script Date: 10/8/2018 3:05:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[getPatientVitals]
(
@PatientMasterVisitID int, @PatientID INT
)
AS
BEGIN
SELECT * FROM PatientVitals WHERE Patientid = @PatientID 
END
GO


