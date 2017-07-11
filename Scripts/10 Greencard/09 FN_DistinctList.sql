/****** Object:  UserDefinedFunction [dbo].[FN_DistinctList]    Script Date: 6/24/2017 5:32:55 PM ******/
DROP FUNCTION [dbo].[FN_DistinctList]
GO
/****** Object:  UserDefinedFunction [dbo].[FN_DistinctList]    Script Date: 6/24/2017 5:32:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create FUNCTION [dbo].[FN_DistinctList]
(
@List VARCHAR(MAX),
@Delim CHAR
)
RETURNS VARCHAR(MAX)
AS
BEGIN
	DECLARE @ParsedList TABLE
	(
		Item VARCHAR(MAX)
	)
	DECLARE @list1 VARCHAR(MAX), @Pos INT, @rList VARCHAR(MAX)
	SET @list = LTRIM(RTRIM(@list)) + @Delim
	SET @pos = CHARINDEX(@delim, @list, 1)
	WHILE @pos > 0
	BEGIN
		SET @list1 = LTRIM(RTRIM(LEFT(@list, @pos - 1)))
		IF @list1 <> ''
		INSERT INTO @ParsedList VALUES (CAST(@list1 AS VARCHAR(MAX)))
		SET @list = SUBSTRING(@list, @pos+1, LEN(@list))
		SET @pos = CHARINDEX(@delim, @list, 1)
	END
	SELECT @rlist = COALESCE(@rlist+'/','') + item
	FROM (SELECT DISTINCT Item FROM @ParsedList) t
	RETURN @rlist
END

GO
