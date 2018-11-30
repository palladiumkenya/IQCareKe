CREATE PROCEDURE [dbo].[Get_Weight_ForHeight_Zscore_Constants]
(
	@Sex INT = NULL,
	@Height INT = NULL
)

AS
 IF (@Height BETWEEN 45  AND 110) 
 BEGIN
	 SELECT * FROM [dbo].[z_whz_young] WHERE Sex = @Sex	And LengthCm = @Height;
 END
  ELSE IF (@Height  Between 65  AND 120) 
 BEGIN
 	 SELECT * FROM [dbo].[z_whz_old] WHERE Sex = @Sex AND HeightCm = @height;
 END
 ELSE
 BEGIN	
	 SELECT 1
 END