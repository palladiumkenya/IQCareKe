





update LookupItem set DisplayName ='TDF+FTC' where [Name]='PRP1B'
update LookupItem set DisplayName ='TDF+FTC' where [Name]='PRP1B'




update lmi  set lmi.DisplayName='TDF+FTC'
from LookupMasterItem lmi 
inner join LookupItem lt on lmi.LookupItemId=lt.Id 
where lt.[Name]='PRP1B'
