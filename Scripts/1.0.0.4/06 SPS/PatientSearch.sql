IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_Clinical_GetPatientSearchresults]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_Clinical_GetPatientSearchresults]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		<Joseph Njung'e>
-- Create date: <08-08-2014,>
-- Description:	<Modified Find Patient>
-- =============================================
Create PROCEDURE [dbo].[Pr_Clinical_GetPatientSearchresults]
	-- Add the parameters for the stored procedure here
	@Sex int = Null, 
	@Firstname varchar(50) = Null, 
	@LastName varchar(50) = Null, 
	@MiddleName varchar(50) = Null, 
	@DOB datetime = Null, 
	@RegistrationDate datetime = Null,
	@IdentifierName  varchar(50) = null,
	@EnrollmentId varchar(50) = Null, 
	@FacilityId int = Null,  
	@Status int = Null,
	@Password varchar(50) = Null,    
	@ModuleId int = 999,
	@FilterByModuleId bit= 0,
	@PhoneNumber varchar(50) = Null,
	@top int = 100,
	@RuleFilter varchar(400)=''
	With Recompile
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Declare @Query nvarchar(max), 
			@ParamDefinition nvarchar(2000),
			@Identifiers varchar(4000),
			@IdentifierSQL varchar(200),
			@ByModule varchar(1000), @ByStatus varchar(120);
			
	Declare @SymKey nvarchar(400)	;	 
	Select	@Identifiers = '',@ByModule='',@ByStatus=' Status =  Null ,',@IdentifierSQL='PatientEnrollmentId Identifier';
			
	 --                                                                                    
	 --Set @SymKey = 'Open symmetric key Key_CTC decryption by password='+ @password + ''                                                                                        
	 --Exec(@SymKey) 
	 
	
Select @Firstname = Nullif(Ltrim(Rtrim(@Firstname)), '');
If  (@Firstname Is Not Null) Select @Firstname = '%' + @Firstname + '%';

Select @MiddleName = Nullif(Ltrim(Rtrim(@MiddleName)), '');
If  (@MiddleName Is Not Null) Select @MiddleName = '%' + @MiddleName + '%';

Select @LastName = Nullif(Ltrim(Rtrim(@LastName)), '')
If  (@LastName Is Not Null) Select @LastName ='%' + @LastName + '%';

Select @EnrollmentId = Convert(varchar,Nullif(Ltrim(Rtrim(@EnrollmentID)), ''));

Select @PhoneNumber =Nullif(ltrim(rtrim(@PhoneNumber)),'');

Select @IdentifierName = nullif(ltrim(rtrim(@IdentifierName)),'');

If (@EnrollmentId Is Not Null) 
Begin
	declare @SS varchar(1000);
	if(@IdentifierName Is Null) Begin
		Select @ss = Substring((Select ',P.[' + Convert(varchar(Max), FieldName) + ']'
				From dbo.mst_patientidentifier
				Order By Id
				For xml Path (''))
			, 2, 1000);
		Select @Identifiers = ' AND(' + Replace(@SS, ',', ' like ''%' + Convert(varchar,@enrollmentid) + ''' or ') + ' = ''' 
		+ Convert(varchar,@enrollmentid) + ''' or P.IQNumber=''' + Convert(varchar,@enrollmentid) +''')'
		
	End
	Else Begin
		 Select @Identifiers = ' AND  P.['  + @IdentifierName +']'+ ' Like ''' +  @enrollmentid + '%'''  ;
		 Select @IdentifierSQL = ' P.['  + @IdentifierName +'] As  Identifier ';
	End
	
End
If(@ModuleID <> 999)
Begin
	Select @ByModule= ' Left Outer Join (Select	P.Ptn_pk,P.ModuleId,P.StartDate EnrollmentDate,	Case CT.CareEnded When 1 Then ''Care Ended'' When 0 Then ''Active (Restarted)''  Else ''Active'' End CareStatus,		
		Case CT.CareEnded When 1 Then CT.PatientExitReasonName Else Null End  CareEndReason,	Isnull(CT.EnrollmentIndex,1)EnrollmentIndex From dbo.Lnk_PatientProgramStart As P
Left Outer Join (Select	CE.Ptn_Pk,	CE.CareEnded,	CE.PatientExitReason,	D.Name As PatientExitReasonName,CE.CareEndedDate,TC.TrackingID,
TC.ModuleId	,Row_number() Over(Partition By TC.Ptn_Pk Order By TC.TrackingId Desc) EnrollmentIndex From dbo.dtl_PatientCareEnded As CE Inner Join	dbo.dtl_PatientTrackingCare As TC On TC.TrackingID = CE.TrackingId
Inner Join	dbo.mst_Decode As D On D.ID = CE.PatientExitReason Where TC.ModuleId = @ModuleID ) As CT On CT.Ptn_Pk = P.Ptn_pk And CT.ModuleId = P.ModuleId Where P.ModuleID=@ModuleID ) CT On CT.Ptn_Pk=P.Ptn_Pk And CT.ModuleID=@ModuleID And EnrollmentIndex=1'

Select @ByStatus = ' [Status] = Case When CT.ModuleID Is Null Then ''Not Enrolled'' Else IsNull(CT.CareEndReason,CT.CareStatus) End , '
End


Set @Query=N'Select Top (@top) * From (Select  P.Ptn_Pk PatientId,Convert(varchar(50), Decryptbykey(FirstName)) As FirstName, Convert(varchar(50), Decryptbykey(MiddleName)) As Middlename,
		Convert(varchar(50), Decryptbykey(LastName)) As LastName,IQNumber, PatientFacilityId,PatientEnrollmentID, '+ @IdentifierSQL +', LocationId,	F.FacilityName,
		Case DOBPrecision	When 0 Then ''No'' When 1 Then ''Yes'' End As [Precision],Dob ,	Convert(varchar(100), Decryptbykey([Address])) [Address],	Convert(varchar(100), Decryptbykey(Phone)) [Phone],
		convert(decimal(5,2),round(cast(datediff(dd,P.DOB,isnull(P.DateofDeath,getdate()))/365.25 as decimal(5,2)),2)) Age,	P.RegistrationDate,'+@ByStatus +' Sex = Case P.Sex When 16 Then ''Male'' Else ''Female'' End
From dbo.mst_Patient As P Inner Join dbo.mst_Facility F	On F.FacilityID = P.LocationID'+ @ByModule +' Where  (P.DeleteFlag = 0 OR P.DeleteFlag Is Null)
And Case When @FirstName Is  Null Or Convert(varchar(50), decryptbykey(P.FirstName)) Like  @Firstname Then 1 Else 0 End = 1
And Case When @LastName Is  Null Or Convert(varchar(50), decryptbykey(P.LastName)) Like  @LastName Then 1	Else 0 End = 1
And Case When @MiddleName Is  Null Or Convert(varchar(50), decryptbykey(P.MiddleName)) Like  @MiddleName Then 1	Else 0 End = 1
And (@DOB Is Null Or P.DOB = @DOB) And (@RegistrationDate Is Null Or P.RegistrationDate= @RegistrationDate) And (@Sex Is Null Or P.Sex = @Sex) And (@Status Is Null Or P.[Status] = @status)
And Case When @PhoneNumber Is  Null Or Convert(varchar(50), decryptbykey(P.Phone)) =  @PhoneNumber Then 1	Else 0 End = 1
And (@FacilityID Is Null Or P.LocationID=@FacilityID)' +@Identifiers + ') P '+ @RuleFilter  ;

Set @Query = @Query + ' Order By [Status],P.RegistrationDate';
print @Query
Set @ParamDefinition= N'@Sex int = Null, 
	@Firstname varchar(50) = Null, 
	@LastName varchar(50) = Null, 
	@MiddleName varchar(50) = Null, 
	@DOB datetime = Null, 
	@RegistrationDate datetime = Null,
	@EnrollmentID varchar(50) = Null,
	@IdentifierName varchar(50) = null,  
	@FacilityID int = Null,  
	@Status int = Null,
	@Password varchar(50) = Null,    
	@ModuleID int = 999,
	@PhoneNumber varchar(50) = Null,
	@top int=100 ';
							 
--Set @SymKey = 'Open symmetric key Key_CTC decryption by password=' + @password ;
--Exec sp_executesql @SymKey ;
Execute sp_Executesql @Query, @ParamDefinition, @Sex, @Firstname,@LastName,@MiddleName,@DOB,@RegistrationDate,@EnrollmentID,@IdentifierName,@FacilityID,@status,@password,@moduleId,@PhoneNumber,@top;
	 
End	   
;

Go