IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[WaitingListView]'))
DROP VIEW [dbo].[WaitingListView]
GO

/****** Object:  View [dbo].[gcPatientView]    Script Date: 7/23/2018 11:11:14 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[WaitingListView]
AS

select qwl.Id,  CAST(DECRYPTBYKEY(p.[FirstName]) AS VARCHAR(50)) AS [FirstName], 
	   CAST(DECRYPTBYKEY(p.[MidName]) AS VARCHAR(50)) AS [MiddleName],
	   CAST(DECRYPTBYKEY(p.[LastName]) AS VARCHAR(50)) AS [LastName],r.Id as RoomId,r.RoomName,pv.PersonId as PersonId,Cast(lvw.OrdRank as int) as PriorityRank,qwl.PatientId,qwl.ServiceRoomId,sa.Id as ServiceAreaId
,sa.DisplayName as ServiceAreaName,ltv.Itemid as ServicePointId, ltv.ItemDisplayName as ServicePointName,qwl.DeleteFlag
,lvw.ItemName as [Priority],qwl.[Status],qwl.CreatedBy,qwl.UpdateDate,qwl.UpdatedBy,qwl.CreateDate from QueueWaitingList qwl 
left join Patient pv on pv.Id=qwl.PatientId 
left join Person p on p.Id=pv.PersonId
inner join LookupItemView lvw on lvw.ItemId=qwl.[Priority] and lvw.MasterName='Priority'
 inner join ServiceRoom src on src.id=qwl.ServiceRoomId
  inner join Rooms r on r.Id=src.RoomId
inner join LookupItemView ltv on ltv.ItemId=src.ServicePointId and ltv.MasterName='ServicePoint'
inner join ServiceArea sa on sa.Id=src.ServiceAreaId




