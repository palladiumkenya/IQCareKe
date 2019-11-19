


ALTER VIEW PREP_EncountersView
AS
SELECT 
ISNULL(ROW_NUMBER() OVER(ORDER BY PE.Id ASC), -1) AS RowID,
PE.Id, 
PM.Id PatientMasterVisitId,
PE.PatientId, 
PE.ServiceAreaId, 
PE.EncounterTypeId, 
CASE WHEN REPLACE(ltv.DisplayName,'-encounter','') = 'PrepRiskAssessment' then 'Behaviour Risk Assessment'
WHEN REPLACE(ltv.DisplayName,'-encounter','') = 'prep' then 'PrEP Encounter'
WHEN REPLACE(ltv.DisplayName,'-encounter','') = 'PrepRiskAssessment' then 'Behaviour Risk Assessment'
WHEN REPLACE(ltv.DisplayName,'-encounter','') = 'MonthlyRefill' then 'MonthlyRefill'
 END as  EncounterType,
PS.PrepStatusToday, 
PA.AppointmentDate,
PE.EncounterStartTime,
PreStatus = (SELECT TOP 1 ItemName FROM LookupItemView WHERE ItemId = PS.PrepStatusToday AND MasterName = 'PrEP_Status'),
ProviderName = (SELECT TOP 1 (UserFirstName + ' ' + UserLastName) FROM [dbo].[mst_User] WHERE UserID = PM.CreatedBy),
PM.VisitDate

FROM PatientMasterVisit PM
INNER JOIN PatientEncounter PE ON PE.PatientMasterVisitId = PM.Id
LEFT JOIN (select prs.PatientEncounterId,prs.PrepStatusToday,prs.CondomsIssued,prs.CreateDate,prs.PatientId from (select prs.PatientId,prs.PatientEncounterId,prs.PrepStatusToday,prs.CondomsIssued,prs.CreateDate,ROW_NUMBER() OVER(partition by prs.PatientId,prs.PatientEncounterId order by prs.CreateDate desc)rownum from PatientPrEPStatus prs
)prs where prs.rownum ='1')PS on PS.PatientEncounterId=PE.Id
--LEFT JOIN PatientPrEPStatus PS ON PS.PatientEncounterId = PE.Id
LEFT JOIN ServiceArea sa on sa.Id=PM.ServiceId
LEFT JOIN( 
select pa.PatientId,pa.PatientMasterVisitId,pa.AppointmentDate,pa.ServiceAreaId from (select pa.PatientId,pa.PatientMasterVisitId,pa.ServiceAreaId,pa.AppointmentDate,ROW_NUMBER()OVER(partition by pa.PatientId,pa.PatientMasterVisitid order by pa.PatientMasterVisitId desc)rownum from PatientAppointment pa
inner join PatientMasterVisit pm on pm.Id=pa.PatientMasterVisitId
)pa where  pa.rownum='1')
PA ON PA.PatientMasterVisitId = PM.Id 
LEFT JOIN LookupItemView ltv  on ltv.ItemId=PE.EncounterTypeId

where sa.Code='PREP' and ltv.ItemName !='CareEnded'
GO

