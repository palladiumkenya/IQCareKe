


 
 DELETE  FROM  PatientScreening  where 
 ScreeningCategoryId in (select ScreeningCategoryId  from PatientScreening psc inner join LookupItemView ltv on ltv.ItemId=psc.ScreeningCategoryId
 where ltv.DisplayName='Or the opposite - being so fidgety or restless that you have been moving around a lot more than usual?' )

GO

 IF EXISTS(select * from LookupItem where DisplayName ='Moving or speaking so slowly that other people could have noticed?' and [Name]='PHQ9Questions8')
BEGIN
update LookupItem SET DisplayName='Moving or speaking so slowly that other people could have noticed?
Or the opposite - being so fidgety or restless that you have been moving around a lot more than usual?' where [Name]='PHQ9Questions8'

update LookupMasterItem set DisplayName='Moving or speaking so slowly that other people could have noticed?
Or the opposite - being so fidgety or restless that you have been moving around a lot more than usual?'
where  DisplayName='Moving or speaking so slowly that other people could have noticed?'
END

GO
IF EXISTS(select * from LookupItemView where ItemDisplayName='Or the opposite - being so fidgety or restless that you have been moving around a lot more than usual?')
BEGIN
delete from LookupItem where  DisplayName='Or the opposite - being so fidgety or restless that you have been moving around a lot more than usual?'
delete from LookupMasterItem where DisplayName='Or the opposite - being so fidgety or restless that you have been moving around a lot more than usual?'
END

Go

IF EXISTS(select * from LookupItem where DisplayName='Thoughts that you would be better off dead, or of hurting yourself in some way?'
and [Name]='PHQ9Questions10')
BEGIN
UPDATE LookupItem set Name='PHQ9Questions10' WHERE DisplayName='Thoughts that you would be better off dead, or of hurting yourself in some way?'
END


