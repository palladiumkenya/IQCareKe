declare @CCCNumberIdentifierTypeId int, @IsNew int, @IsTransferIn int;

Select @IsNew = ItemId From LookupItemView where MasterName='PatientType' And ItemName='New'
Select @IsTransferIn = ItemId From LookupItemView where MasterName='PatientType' And ItemName='Transfer-In'


select @CCCNumberIdentifierTypeId=Id from Identifiers where code='CCCNumber'

--Select *,
--	case charindex('-',rtrim(ltrim(IdentifierValue))) when 6 then substring(rtrim(ltrim(IdentifierValue)),1,5) Else Null end Gr
--,case charindex('-',rtrim(ltrim(IdentifierValue))) when 6 then replace(rtrim(ltrim(IdentifierValue)),'-','') Else Null End Gr2
--From PatientIdentifier P
--Where --IdentifierTypeId = @CCCNumberIdentifierTypeId and charindex('-',rtrim(ltrim(IdentifierValue)))>0 and
-- len(rtrim(ltrim(IdentifierValue))) =10 and AssigningFacility Is not null
--update records created in greencard
Update P Set
	AssigningFacility = case charindex('-',rtrim(ltrim(IdentifierValue))) when 6 then substring(rtrim(ltrim(IdentifierValue)),1,5) Else Null end
,IdentifierValue = case charindex('-',rtrim(ltrim(IdentifierValue))) when 6 then replace(rtrim(ltrim(IdentifierValue)),'-','') Else P.IdentifierValue End
From PatientIdentifier P
Where IdentifierTypeId = @CCCNumberIdentifierTypeId and charindex('-',rtrim(ltrim(IdentifierValue)))>0
and len(rtrim(ltrim(IdentifierValue))) =11 and AssigningFacility Is null

--update records migrated from bluecard
Update I Set AssigningFacility = cast(P.FacilityId As varchar(5))
From PatientIdentifier I
Inner Join Patient P On I.PatientId = P.Id
		And I.IdentifierTypeId = @CCCNumberIdentifierTypeId
Where I.AssigningFacility Is Null
And P.PatientType = @IsNew
And P.DeleteFlag = 0
And Len(ltrim(rtrim(IdentifierValue))) > 0 and Len(cast(P.FacilityId As varchar(10))) = 5

