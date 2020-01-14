

update LookupItem set DisplayName ='TDF' where [Name]='PRP1C'
update LookupItem set DisplayName ='TDF' where [Name]='PRP1C'


update LookupItem set DisplayName ='TDF+FTC' where [Name]='PRP1A'
update LookupItem set DisplayName ='TDF+FTC' where [Name]='PRP1A'


update lmi  set lmi.DisplayName='TDF'
from LookupMasterItem lmi 
inner join LookupItem lt on lmi.LookupItemId=lt.Id 
where lt.[Name]='PRP1C'

update lmi  set lmi.DisplayName='TDF+FTC'
from LookupMasterItem lmi 
inner join LookupItem lt on lmi.LookupItemId=lt.Id 
where lt.[Name]='PRP1A'



