-- Script to remove duplicate Pharmacy refill from the list of appointment reasons
Update D Set deleteflag = 1 From
mst_Decode D inner join
(select Id, Name, ROW_NUMBER() over(PARTITION by name order by id) CX
 From mst_Decode where name='Pharmacy Refill') x on x.ID=d.id
 where cx> 1
 
 Go
 -- Script to remove "All" from the list of appointment status to view all appointments leave the field as "Select"
Delete From mst_Decode  where CodeID=3 and Name='All'
Go