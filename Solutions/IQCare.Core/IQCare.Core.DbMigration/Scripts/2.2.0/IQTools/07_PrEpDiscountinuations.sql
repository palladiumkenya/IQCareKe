

/****** Object:  View [dbo].[PrEp_Discountinuations]    Script Date: 08/21/2019 15:12:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID('[dbo].[PrEp_Discountinuations]', 'V') IS NOT NULL
    DROP VIEW [dbo].[PrEp_Discountinuations]
GO

CREATE VIEW [dbo].[PrEp_Discountinuations]
AS
SELECT        a.PatientId, d.ptn_pk, a.PatientMasterVisitId,
                             (SELECT        TOP (1) ItemName
                               FROM            dbo.LookupItemView
                               WHERE        (ItemId = a.ExitReason)) AS ExitReason, a.ExitDate, a.TransferOutfacility, a.DateOfDeath, a.CareEndingNotes
FROM            dbo.PatientCareending AS a INNER JOIN
                         dbo.PatientEnrollment AS b ON b.PatientId = a.PatientId AND a.PatientEnrollmentId = b.Id INNER JOIN
                         dbo.ServiceArea AS c ON c.Id = b.ServiceAreaId INNER JOIN
                         dbo.Patient AS d ON d.Id = a.PatientId
WHERE        (a.DeleteFlag = 1) AND (c.Name = 'PREP')

GO

