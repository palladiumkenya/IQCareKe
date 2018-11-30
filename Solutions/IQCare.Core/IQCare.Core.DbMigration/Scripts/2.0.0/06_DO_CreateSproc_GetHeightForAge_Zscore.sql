CREATE PROCEDURE [dbo].[Get_HeightForAge_Zscore_Constants]
(
	@Sex INT = NULL,
	@DateOfBirth DATETIME = NULL
)

AS
BEGIN
DECLARE @ageInDays INT =  DATEDIFF(DAY, @DateOfBirth, GETDATE()), 
@ageInMonths INT = DATEDIFF(MONTH, @DateOfBirth,  GETDATE()) ;

  IF (@ageInDays Between 0 And 1856) 
 BEGIN
	 SELECT *
	 FROM [dbo].[z_haz_young] WHERE Sex = @Sex AND agedays = @ageInDays;
 END
 ELSE  IF (@ageInMonths Between 61 And 229) 
 BEGIN
	 SELECT *
	 FROM [dbo].[z_haz_old] WHERE Sex = @Sex AND Agemos = @ageInMonths;
 END
 ELSE
 BEGIN	
	 SELECT 1
 END
END 