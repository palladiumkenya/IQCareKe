IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Scheduler_AppointmentList_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Scheduler_AppointmentList_Constella]
GO
/****** Object:  StoredProcedure [dbo].[pr_Scheduler_AppointmentList_Constella]    Script Date: 5/11/2018 11:17:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create procedure [dbo].[pr_Scheduler_AppointmentList_Constella]                                   
(                      
                     
	 @LocationId int,            
	 @password varchar(50) =null ,
	 @AppStatus int = null,  
	 @AppReason int = null,                    
	 @FromDate  datetime = null,                      
	 @ToDate  datetime= Null  ,
	 @PatientId int =null,
	 @ModuleId int = null,
	 @VisitId int = null                    
)                       
                  
AS             
Begin
         
  --    Declare @SymKey varchar(400)
--Set @SymKey = 'Open symmetric key Key_CTC decryption by password=' + N'''ttwbvXWpqb5WOLfLrBgisw==''' + ''
--Exec (@SymKey)

declare @greenCardServiceAreaId int, @name varchar(100), @isCCc bit;
set @isCCc =0

select @isCCc = count(*)  From LookupItem where id=@ModuleId and name= 'MoH 257 GREENCARD';

Select	@FromDate = case when @FromDate Is not Null Then dateadd(Second, 0, dateadd(Day, datediff(Day, 0, @FromDate), 0)) Else Null End,
		@ToDate = case when @ToDate Is not Null Then dateadd(Second, -1, dateadd(Day, datediff(Day, 0, @ToDate) + 1, 0)) Else Null End;

if(@ModuleId Is Not Null) BEgin

Select @ModuleId = case when ModuleName In ('CCC Patient Card MoH 257','Green Card') Then 203 Else @ModuleId End ,
		@isCCc = case when ModuleName In ('CCC Patient Card MoH 257','Green Card') Then 1 Else @isCCc End
From mst_module where moduleid=@moduleid

End

Select	PA.Ptn_Pk PatientId,
		AppointmentId,
		convert(varchar(50), decryptbykey(P.FirstName)) FirstName,
		nullif(convert(varchar(50), decryptbykey(P.MiddleName)), '') MiddleName,
		convert(varchar(50), decryptbykey(P.LastName)) LastName,
		coalesce(nullif(P.PatientEnrollmentID, ''), nullif(P.PatientClinicID, ''), P.PatientFacilityID) PatientEnrollmentId,
		P.Status PatientStatus,
		PA.LocationId,
		FacilityName =
		(
			Select Top 1
				F.FacilityName
			From mst_Facility F
			Where F.FacilityID = PA.LocationID
		),
		Pa.Visit_pk VisitId,
		AppDate AppointmentDate,
		Case
			When AppStatus = 14 Then PA.UpdateDate
			Else Null
		End MetDate,
		AppReason PurposeId,
		AR.Name Purpose,
		AppStatus AppointmentStatusId,
		StatusName [AppointmentStatus],
		PA.EmployeeID ProviderId,
		E.FirstName + ' ' + E.LastName As Provider,
		PA.AppNote,
		PA.ModuleId ServiceAreaId,
 ServiceArea =
		(
		CASE (Select top 1
				ModuleName
			From mst_module M
			Where M.ModuleId = PA.ModuleId) WHEN 'CCC Patient Card MoH 257' THEN 'CCC' When 'GreenCard' Then 'CCC'
			ELSE (Select top 1 isnull(DisplayName,ModuleName)			From mst_module M	Where M.ModuleId = PA.ModuleId)			END
		),
		isnull(PA.UpdateDate, PA.CreateDate) StatusDate,
		UC.CreatedById,
		UC.CreatedBy,
		MD.UpdatedById,
		MD.UpdatedBy
From dtl_PatientAppointment PA
Inner Join mst_patient P On p.Ptn_Pk = PA.Ptn_pk
Left outer Join vw_AppointmentReasons AR On AR.ID = AppReason 
Inner Join
	(
		Select
			ID StatusID,
			Name StatusName
		From mst_decode
		Where codeId = 3
	) ST On ST.StatusID = PA.AppStatus
Inner Join
	(
		Select
			UserId CreatedById,
			UserFirstName + ' ' + UserLastName CreatedBy
		From mst_User
	) UC On UC.CreatedById = PA.UserID
Left Outer Join
	(
		Select
			UserId UpdatedById,
			UserFirstName + ' ' + UserLastName UpdatedBy
		From mst_User
	) MD On MD.UpdatedById = PA.UpdateUserId
Left Outer Join mst_Employee E On E.EmployeeID = PA.EmployeeID
Where PA.LocationID = @LocationID
And PA.DeleteFlag = 0
And P.DeleteFlag = 0
And
	Case
		When @AppStatus Is Null Or @AppStatus = PA.AppStatus Then 1
		Else 0
	End = 1
And
	Case
		When @AppReason Is Null Or @AppReason = PA.AppReason Then 1
		Else 0
	End = 1
And
	Case
		When @FromDate Is Not Null And @ToDate Is Not Null And AppDate Between @FromDate And @ToDate Then 1
		When @FromDate Is Null Or @ToDate Is Null Then 1
		Else 0
	End = 1
And (Case
	When @VisitId Is Null Or PA.Visit_pk = @VisitId Then 1
	Else 0
End = 1)
And (Case
	When @PatientId Is Null Or PA.Ptn_pk = @PatientId Then 1
	Else 0
End = 1)

And (Case
	When @ModuleId Is Null Or PA.ModuleId = @ModuleId Then 1
	Else 0
End = 1)

union 


Select
  --Convert(int,row_number() Over(Order by PA.AppointmentDate)) Id
  Pt.ptn_pk PatientId
  ,PA.Id AppointmentId
 -- , PA.AppointmentDate
  ,	convert(varchar(50), decryptbykey(ps.FirstName)) FirstName
  ,	nullif(convert(varchar(50), decryptbykey(ps.MidName)), '') MiddleName
  ,	convert(varchar(50), decryptbykey(ps.LastName)) LastName
  ,(SELECT top 1 coalesce(i.IdentifierValue,'') FROM PatientIdentifier i WHERE i.PatientId=pt.Id AND i.IdentifierTypeId IN(SELECT top 1 Id FROM Identifiers WHERE Code='CCCNumber')) PatientEnrollmentId
  ,CASE (SELECT top 1 PatientStatus FROM gcPatientView WHERE PatientId=pt.Id) WHEN 'Active' then 0 
  else ''
  END PatientStatus
  ,(SELECT top 1 LocationID FROM mst_Patient WHERE Ptn_Pk=pt.ptn_pk) LocationId
  ,(Select Top 1 F.FacilityName From mst_Facility F Where f.DeleteFlag=0) FacilityName
 ,PA.PatientMasterVisitId VisitId
 ,PA.AppointmentDate AppointmentDate
 , CASE WHEN (SELECT top 1 [Name] FROM LookupItem i WHERE i.Id=PA.StatusId)='pending' THEN NULL
		ELSE
		 PA.StatusDate 
		END MetDate
 ,PA.ReasonId PurposeId
 ,(SELECT top 1 [Name] FROM LookupItem l WHERE l.Id=PA.ReasonId) Purpose
 ,PA.StatusId AppointmentStatusId
 ,(SELECT top 1 [Name] FROM LookupItem i WHERE i.Id=PA.StatusId) [AppointmentStatus]
  ,0 providerId
 ,'' [Provider]
  ,'' AppNote
   ,203  ServiceAreaId
 ,ServiceArea ='CCC'
		--(
		--	Select
		--		ModuleName
		--	From mst_module M
		--	Where M.ModuleId = 203
		--)
		-- ,PA.CreatedBy
 	--	,PA. PurposeId
		--AR.Name Purpose,
		--PA.s AppointmentStatusId,
		--StatusName [AppointmentStatus],
		--PA.EmployeeID ProviderId,
		--E.FirstName + ' ' + E.LastName As Provider,
		--PA.AppNote,
		--PA.ModuleId ServiceAreaId,

  --,	sum(Case L.Name   When 'Missed' Then 1	   Else 0	   End) Missed
  --,	sum(Case L.Name   When 'Met' Then 1	   Else 0	   End) Met
  --,	sum(Case L.Name   When 'Pending' Then 1	   Else 0   End) Pending
  --,	sum(Case L.Name   When 'PreviouslyMissed' Then 1	   Else 0   End) PreviouslyMissed


  ------------------------------------------------
  --		,CASE WHEN (SELECT top 1 [Name] FROM LookupItem i WHERE i.Id=PA.StatusId)='pending' THEN NULL
		--ELSE
		-- PA.StatusDate 
		--END StatusDate
		, PA.StatusDate StatusDate
		,PA.CreatedBy CreatedById
		,(SELECT u.UserFirstName + ' '+ u.UserLastName FROM mst_User u WHERE u.UserID=PA.CreatedBy) CreatedBy
		,'' UpdatedById
		,'' UpdatedBy
From PatientAppointment PA
--Inner Join LookupItem L On L.Id = PA.StatusId
INNER JOIN Patient Pt ON pt.Id=PA.PatientId
INNER JOIN Person ps ON ps.Id=Pt.PersonId
inner join (select itemid,ItemName, ID MstId, Name mstname from LookupItemView LV inner join mst_Decode d on d.Name=lv.ItemName where MasterName = 'Appointmentstatus') APPStatus
on APPStatus.ItemId=pa.StatusId
WHERE 
	PA.DeleteFlag=0
	And
	Case
		When @FromDate Is Not Null And @ToDate Is Not Null And AppointmentDate Between @FromDate And @ToDate Then 1
		When @FromDate Is Null Or @ToDate Is Null Then 1
		Else 0
	End = 1
	And (Case
	When @PatientId Is Null Or Pt.ptn_pk = @PatientId Then 1
	Else 0
End = 1)
And
	Case
		When @AppStatus Is Null Or @AppStatus = AppStatus.MstId Then 1
		Else 0
	End = 1
And(@isccc=1 or @moduleid Is null)
Order By AppDate Desc
--Close Symmetric Key Key_CTC

End

