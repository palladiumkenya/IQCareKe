--Mark duplicates in LookupItem and LookupMaster tables as deleted
;
With dups As( Select *  ,row_number() Over (Partition By Name Order By Id) R From LookupItem)
Update I
Set DeleteFlag = 1
   ,Name = I.Name + '(deleted duplicate:' + cast(R - 1 As varchar(3)) + ' original:' +
	cast((Select Top 1 Id From dups x Where R = 1 And x.Name = I.Name) As varchar(6))
From LookupItem I
Inner Join dups D	On I.Id = D.Id
Where D.R > 1

Go

;
With dupMaster As( Select *  ,row_number() Over (Partition By Name Order By Id) R From LookupMaster)
Update M
Set DeleteFlag = 1
   ,Name = M.Name + '(deleted duplicate:' + cast(R - 1 As varchar(3)) + ' original:' +
	cast((Select Top 1 Id From dupMaster x Where R = 1 And x.Name = M.Name) As varchar(6))
From LookupMaster M
Inner Join dupMaster D	On M.Id = D.Id
Where D.R > 1

Go