/****** Object:  StoredProcedure [dbo].[pr_IQTools_ImportReports]    Script Date: 12/11/2014 16:16:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_IQTools_ImportReports]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_IQTools_ImportReports]
GO
/****** Object:  StoredProcedure [dbo].[pr_IQTools_UpdateReport]    Script Date: 12/11/2014 16:16:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_IQTools_UpdateReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_IQTools_UpdateReport]
GO
/****** Object:  StoredProcedure [dbo].[pr_IQTools_UpdateReport]    Script Date: 12/11/2014 16:16:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_IQTools_UpdateReport]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Njung''e
-- Create date: 18/03/2014
-- Description:	Updates the report stylesheet
-- =============================================
Create PROCEDURE [dbo].[pr_IQTools_UpdateReport] 
	-- Add the parameters for the stored procedure here
	@reportID int , 
	@template varbinary(max)=null,
	@reportName varchar(36)= null,
	@templateFileName varchar(36)= null,
	@templateFileExt varchar(36)= null,
	@templateContentType varchar(36)= null,
	@fileLength int=0
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

-- Insert statements for procedure here
Update dbo.IQToolsExcelReports Set
	ReportStylesheet = @template,
	ReportName = Isnull(@reportName,ReportName),
	[FileName] = Nullif(@templateFileName,''''),
	FileExt = Nullif(@templateFileExt,''''),
	ContentType = Nullif(@templateContentType,''''),
	FileLength = @fileLength
Where IQToolsCatID = @reportID;

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[pr_IQTools_ImportReports]    Script Date: 12/11/2014 16:16:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_IQTools_ImportReports]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Joseph
-- Create date: 
-- Description:	
-- =============================================
Create PROCEDURE [dbo].[pr_IQTools_ImportReports] 
	-- Add the parameters for the stored procedure here
	@data xml  , 
	@overwrite bit = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	If(@overwrite = 1)
		Truncate table dbo.IQToolsExcelReports;
		
	Declare @D Table(CatID int,ReportName varchar(50));
	
	Insert Into @D
	Select  
			a.c.value(''catID[1]'',''int'') CatID, 
			a.c.value(''Category[1]'',''varchar(200)'') ReportName
		From @data.nodes(''//reports'') a(c);
		
	Insert Into dbo.IQToolsExcelReports(IQToolsCaTID,ReportName,ReportStylesheet)
	Select 
		D.CatID, 
		D.ReportName,
		Null ReportStylesheet
	From @D D Where D.CatID Not In (Select IQToolsCaTID From IQToolsExcelReports);
		
	Update A
		Set ReportName = D.ReportName
	From dbo.IQToolsExcelReports  A
	Inner Join	@D D On D.CatID = A.IQToolsCaTID;
END
' 
END
GO