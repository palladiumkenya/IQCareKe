
/****** Object:  StoredProcedure [dbo].[pr_WaitingList_GetPatientsOnWaitingList]    Script Date: 12/11/2014 16:16:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_WaitingList_GetPatientsOnWaitingList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_WaitingList_GetPatientsOnWaitingList]
GO
/****** Object:  StoredProcedure [dbo].[pr_WaitingList_GetPatientsWaitingList]    Script Date: 12/11/2014 16:16:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_WaitingList_GetPatientsWaitingList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_WaitingList_GetPatientsWaitingList]
GO
/****** Object:  StoredProcedure [dbo].[pr_WaitingList_SavePatientsWaitingList]    Script Date: 12/11/2014 16:16:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_WaitingList_SavePatientsWaitingList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_WaitingList_SavePatientsWaitingList]
GO
/****** Object:  StoredProcedure [dbo].[pr_WaitingListChangePatientStatus]    Script Date: 12/11/2014 16:16:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_WaitingListChangePatientStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_WaitingListChangePatientStatus]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WaitingList_QueuePatient]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WaitingList_QueuePatient]
GO
/****** Object:  StoredProcedure [dbo].[WaitingList_SystemCleanup]    Script Date: 6/9/2016 9:23:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WaitingList_SystemCleanup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WaitingList_SystemCleanup]
GO

/****** Object:  StoredProcedure [dbo].[WaitingList_SystemCleanup]    Script Date: 6/9/2016 9:23:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Joseph Njung'e
-- Create date: 20160706
-- Description:	Remove overdue patient from the queue
-- Statuses 0=Pending, 1 = Served Inactive, 2 = Not Served (System Clean up), 3 = Not Served (User deleted)
-- =============================================
CREATE PROCEDURE [dbo].[WaitingList_SystemCleanup] 	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare	@MaxHrs int ;
	Set @MaxHrs =  12;

	Update W Set
		[Status] = 2,
		UpdateDate = getdate(),
		UpdatedBy = 0
	From dtl_WaitingList As W
	Where (Status = 0)
		And (datediff(Hour, CreateDate, getdate()) > @MaxHrs);
END

GO


/****** Object:  StoredProcedure [dbo].[WaitingList_QueuePatient]    Script Date: 6/9/2016 9:23:34 PM ******/
Set Ansi_nulls On
Go

Set Quoted_identifier On
Go

-- =============================================
-- Author:		Joseph Njung'e
-- Create date: 20150607
-- Description:	Queue Patient
-- =============================================
CREATE PROCEDURE [dbo].[WaitingList_QueuePatient] 
	-- Add the parameters for the stored procedure here
	@PatientId int , 
	@QueueId int,
	@ModuleId int ,
	@Priority int =1,
	@QueueStatus int = 0,
	@UserId int
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

	Insert Into dtl_WaitingList (
		Ptn_PK
		,ListID
		,ModuleID
		,Priority
		,Status
		,CreateDate
		,CreatedBy
	)
	Values (
		@PatientId
		,@QueueId
		,@ModuleId
		,@Priority
		,@QueueStatus
		,getdate()
		,@UserId
	);

	Select scope_identity() WaitingListId

End

Go

/****** Object:  StoredProcedure [dbo].[pr_WaitingListChangePatientStatus]    Script Date: 12/11/2014 16:16:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_WaitingListChangePatientStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		vincent Yahuma
-- Create date: 2014-Nov-03
-- Description:	Change status of a patient in the waiting list
-- =============================================
CREATE PROCEDURE [dbo].[pr_WaitingListChangePatientStatus](@WaitingListID int, @rowStatus int,@UserID int )

AS
BEGIN
	UPDATE dtl_WaitingList SET [Status]=@rowStatus,UpdateDate=GETDATE(),UpdatedBy=@UserID 
	WHERE WaitingListID=@WaitingListID
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[pr_WaitingList_SavePatientsWaitingList]    Script Date: 12/11/2014 16:16:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_WaitingList_SavePatientsWaitingList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Vincent Yahuma
-- Create date: 2014-Oct-24
-- Description:	Procedure for  saving patients waiting list 
-- =============================================
CREATE PROCEDURE [dbo].[pr_WaitingList_SavePatientsWaitingList](
@ItemsList xml,
@PatientID int,
@ModuleID int,
@UserID int
)
AS
BEGIN
declare @BItems Table(WaitingListID int,ListID int,[Priority] int,RowStatus int)

INSERT INTO @BItems
SELECT bi.record.query(''WaitingListID'').value(''.'',''int'')WaitingListID,
bi.record.query(''ListID'').value(''.'',''int'')ListID,
bi.record.query(''Priority'').value(''.'',''int'')Priority,
bi.record.query(''RowStatus'').value(''.'',''int'')RowStatus
FROM @ItemsList.nodes(''/root/row'') as bi(record)
--update records that status has changed

UPDATE dtl_WaitingList SET [Status]=RowStatus,UpdateDate=GETDATE(),UpdatedBy=@UserID FROM dtl_WaitingList wl
JOIN @BItems BItems on BItems.WaitingListID=wl.WaitingListID and wl.Status<>BItems.RowStatus


--Insert new records
 INSERT INTO dtl_WaitingList ( 
 Ptn_PK,
 ListID,
 ModuleID,
 Priority,
 Status,
 CreateDate,
 CreatedBy)
 SELECT @PatientID,Bitems.ListID,@ModuleID,Bitems.Priority,0,GETDATE(),@UserID
 FROM @BItems BItems where BItems.WaitingListID NOT IN(SELECT WaitingListID FROM dtl_WaitingList)
 



END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[pr_WaitingList_GetPatientsWaitingList]    Script Date: 12/11/2014 16:16:40 ******/
/****** Object:  StoredProcedure [dbo].[pr_WaitingList_GetPatientsWaitingList]    Script Date: 12/10/2015 13:13:10 ******/
Set Ansi_nulls On
Go

Set Quoted_identifier On
Go

-- =============================================
-- Author:		Vincent Yahuma
-- Create date: 2014-Oct-22
-- Description:	<Gets all the waiting list a patient is on,,>
-- =============================================
CREATE PROCEDURE [dbo].[pr_WaitingList_GetPatientsWaitingList](@PatientID as int)
	
AS
BEGIN
Select
	(
		Select
			QueueName
		From vw_WaitingQueue
		Where QueueId = wl.ListID
	)
	ListName,
	md.ModuleName,
	md.ModuleID,
	cast(datediff(Hour, wl.CreateDate, getdate()) As varchar) + ' hrs ' +
	cast(datediff(Minute, wl.CreateDate, getdate()) % 60 As varchar) + ' mins' TimeOnList,
	wl.CreateDate,
	ur.UserName AddedBy,
	wl.ListID,
	wl.WaitingListID,
	wl.[Priority],
	1 [Persisted],
	wl.[Status] RowStatus
From dtl_WaitingList As wl
	Inner Join mst_User As ur On wl.CreatedBy = ur.UserID
	Left Join mst_module md On md.ModuleID = wl.ModuleID
Where wl.[Status] = 0
	And wl.Ptn_PK = @PatientID
Order By wl.CreateDate

End

Go

/****** Object:  StoredProcedure [dbo].[pr_WaitingList_GetPatientsOnWaitingList]    Script Date: 09/28/2015 08:42:51 ******/
/****** Object:  StoredProcedure [dbo].[pr_WaitingList_GetPatientsOnWaitingList]    Script Date: 12/10/2015 12:32:51 ******/
/****** Object:  StoredProcedure [dbo].[pr_WaitingList_GetPatientsOnWaitingList]    Script Date: 03/17/2016 18:51:17 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_WaitingList_GetPatientsOnWaitingList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_WaitingList_GetPatientsOnWaitingList]
GO
/****** Object:  StoredProcedure [dbo].[pr_WaitingList_GetPatientsOnWaitingList]    Script Date: 03/17/2016 18:51:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_WaitingList_GetPatientsOnWaitingList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[pr_WaitingList_GetPatientsOnWaitingList] AS' 
END
GO


-- =============================================
-- Author:		Vincent Yahuma
-- Create date: 2014-Oct-22
-- Description:	<Get the list of all patients on the waiting list,,>
-- =============================================
ALTER PROCEDURE [dbo].[pr_WaitingList_GetPatientsOnWaitingList](@ListID as int, @ModuleID as int,@Password varchar(50))
	
AS
BEGIN

Declare @SymKey varchar(400) , @LabQueueId int, @PharmacyQueueId int;
--Set @SymKey = 'Open symmetric key Key_CTC decryption by password=' + @password + ''
--Exec (@SymKey)

Select 
	@PharmacyQueueId = QueueId
From vw_WaitingQueue
Where QueueName = 'Pharmacy';

Select 
	@LabQueueId = QueueId
From vw_WaitingQueue
Where QueueName = 'Laboratory';

Select	wl.Ptn_PK,
		convert(varchar(50), decryptbykey(pt.FirstName)) As [FirstName],
		convert(varchar(50), decryptbykey(pt.LastName)) As [LastName],
		pt.DOB,
		Case pt.Sex When 16 Then 'Male'		Else 'Female'		End Sex,
		wl.[Priority],
		cast(datediff(Hour, wl.CreateDate, getdate()) As varchar) + ' hrs ' +
		cast(datediff(Minute, wl.CreateDate, getdate()) % 60 As varchar) + ' mins' TimeOnList,
		wl.CreateDate,
		ur.UserID,
		ur.UserName,
		pt.PatientFacilityID,
		WaitingListID,
		(
			Select UserFirstName + ' ' + UserLastName		From mst_User			Where UserID = wl.WaitingFor
		)
		WaitingFor,
		ModuleID
From
	(
		Select
			WaitingListID,
			Ptn_PK,
			ListID,
			ModuleID,
			Priority,
			Status,
			CreateDate,
			CreatedBy,
			WaitingFor
		From dtl_WaitingList 
		Union All
		Select
			0 WaitingListID,
			Ptn_pk,
			@PharmacyQueueId listID,
			(
				Select Top 1 PPS.ModuleId	From Lnk_PatientProgramStart PPS	Where PPS.Ptn_pk = PO.Ptn_PK Order By PPS.StartDate Desc
			)
			ModuleID,
			1 [priority],
			0 [Status],
			CreateDate,
			UserID,
			0 WaitingFor
		From ord_PatientPharmacyOrder PO
		Where DispensedByDate Is Null And PO.DeleteFlag= 0
		Union All 
		Select
			0 WaitingListID,
			Ptn_pk,
			@LabQueueId listID,
			(
				Select Top 1	PPS.ModuleId  	From Lnk_PatientProgramStart PPS	Where PPS.Ptn_pk = PO.Ptn_PK	Order By PPS.StartDate Desc
			)
			ModuleID,
			1 [priority],
			0 [Status],
			CreateDate,
			UserID,
			0 WaitingFor
		From ord_LabOrder PO
		Where PO.OrderStatus = 'Pending' And PO.DeleteFlag= 0

		Union All

		Select
			0 WaitingListID,
			Ptn_pk,
			(select Top 1 QueueId from vw_WaitingQueue Where QueueName='Clinical Service') listID,			
			PO.TargetModuleId ModuleID,
			1 [priority],
			0 [Status],
			CreateDate,
			UserID,
			0 WaitingFor
		From ord_ClinicalServiceOrder PO
		Where PO.OrderStatus  <> 'Complete'		And PO.DeleteFlag= 0

	) As wl
	Inner Join mst_Patient As pt On pt.Ptn_Pk = wl.Ptn_PK
		And pt.DeleteFlag = 0 Or pt.DeleteFlag Is Null
	Inner Join mst_User As ur On wl.CreatedBy = ur.UserID
Where wl.[Status] = 0
	And wl.ListID = @ListID
	And
		Case
			When @ModuleID = 0 OR @ListId = @LabQueueId OR @ListId = @PharmacyQueueId Then 1
			When @ModuleID = ModuleID Then 1
			When ModuleID = 0 Then 1
		End = 1
	And datediff(Hour, wl.CreateDate, getdate()) < 24
Order By [Priority] Desc, datediff(Minute, wl.CreateDate, getdate()) Desc

--Close Symmetric Key Key_CTC

End


GO

