Set Nocount On;
Go
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
IF OBJECT_ID('tempdb..#DeleteFlags') IS NOT NULL Drop Table #DeleteFlags
Go
Create Table #DeleteFlags(	
	tablename varchar(50),
	ColumnName varchar(10)
);
Go
Insert Into #DeleteFlags
SELECT  t.name AS table_name,
--SCHEMA_NAME(schema_id) AS schema_name,
c.name AS column_name
FROM sys.tables AS t
INNER JOIN sys.columns c ON t.OBJECT_ID = c.OBJECT_ID
WHERE c.name LIKE 'DeleteFlag';
Go
Declare @Query varchar(250)
		,@tablename varchar(50)
		,@update varchar(250);
While Exists(Select 1 From #DeleteFlags) Begin

	Select @Query = 'Alter table ['+tablename + '] ADD CONSTRAINT DF_'+replace(tablename,' ','')+'_DeleteFlag'+' DEFAULT 0 FOR DeleteFlag; ', @tablename = tablename From #DeleteFlags;
	Select @update = 'Update ['+@tablename +'] Set DeleteFlag = 0 Where DeleteFlag Is Null;'
	--Print @update
	--Print 'table name == [' +@tablename+']'
	Exec (@update);
	If Not Exists (Select 1      from sys.all_columns c
      join sys.tables t on t.object_id = c.object_id
      join sys.schemas s on s.schema_id = t.schema_id
      join sys.default_constraints d on c.default_object_id = d.object_id
		where t.name = @tablename      And c.name = 'deleteflag')
      Begin
		--Print @Query
		Exec(@Query)
	 End
	--
	
	Delete From #DeleteFlags where tablename = @tablename
	
End
GO
SET ANSI_PADDING OFF
GO