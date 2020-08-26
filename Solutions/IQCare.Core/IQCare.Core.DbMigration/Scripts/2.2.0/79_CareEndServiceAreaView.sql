

IF  OBJECT_ID('PatientCareEndServiceAreaView', 'V') IS NOT NULL
    DROP VIEW [PatientCareEndServiceAreaView]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PatientCareEndServiceAreaView] as

select pe.Id,p.PersonId,pe.EnrollmentDate,pe.PatientId,ServiceAreaId,pce.ExitDate,lti.[DisplayName] AS ExitReason
 from PatientEnrollment  pe 
 inner join PatientCareending pce on pce.PatientEnrollmentId=pe.Id
 inner join Patient p on p.Id=pe.PatientId
 left join LookupItem lti on lti.Id=pce.ExitReason
 where CareEnded='1'

