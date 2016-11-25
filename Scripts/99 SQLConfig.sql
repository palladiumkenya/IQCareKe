Use [Master]
go

exec sp_configure 'Show Advanced Options',1
go
Reconfigure
go
exec sp_configure 'Ad Hoc Distributed Queries',1
go
exec sp_configure 'clr enabled',1
go
exec sp_configure 'default trace enabled',1
go
exec sp_configure 'remote access',1
go
exec sp_configure 'remote proc trans',1
go
exec sp_configure 'SMO and DMO XPs',1
go
exec sp_configure 'xp_cmdshell',1
go
Reconfigure
go