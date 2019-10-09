IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ServiceRoomView]'))
DROP VIEW [dbo].[ServiceRoomView]
GO

/****** Object:  View [dbo].[gcPatientView]    Script Date: 7/23/2018 11:11:14 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[ServiceRoomView]
AS

select src.Id,src.ServicePointId,ltv.ItemName as ServicePointName,src.RoomId,r.RoomName as RoomName,r.DisplayName as RoomDisplayName, src.ServiceAreaId,sa.DisplayName as ServiceAreaName,src.DeleteFlag,src.Active,src.CreateDate,src.CreatedBy,src.UpdateDate,src.UpdatedBy
 from [dbo].[ServiceRoom] src inner join Rooms r on r.Id=src.RoomId
inner join LookupItemView ltv on ltv.ItemId=src.ServicePointId
inner join ServiceArea sa on sa.Id=src.ServiceAreaId;