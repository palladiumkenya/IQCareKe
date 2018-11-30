CREATE PROCEDURE [dbo].[Get_Weight_ForAge_Zscore_Constants]
(
	@Sex INT = NULL,
	@DateOfBirth DATETIME = NULL
)
AS

BEGIN
DECLARE @ageInDays INT =  DATEDIFF(DAY, @DateOfBirth, GETDATE()), 
@ageInMonths INT = DATEDIFF(MONTH, @DateOfBirth,  GETDATE()) ;


 IF (@ageInDays <= 1856) 
 BEGIN
	 SELECT * FROM [dbo].[z_waz_young] WHERE Sex = @Sex AND agedays = @ageInDays;
 END
 ELSE  IF(@ageInMonths >=61)
 BEGIN
	 SELECT * FROM [dbo].[z_waz_old] WHERE Sex = @Sex AND ageMos = @ageInMonths;
 END
 ELSE
BEGIN
    SELECT 1
END

END