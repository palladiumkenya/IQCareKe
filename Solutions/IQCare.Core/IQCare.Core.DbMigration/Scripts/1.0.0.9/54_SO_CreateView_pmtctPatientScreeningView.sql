

CREATE VIEW [dbo].[pmtctPatientScreeningView]
AS
SELECT     
    Id ,
 PatientId ,
  ScreeningTypeId,
  (SELECT top 1 l.MasterName FROM LookupItemView l WHERE l.MasterId=s.ScreeningTypeId ) ScreeningType,
   PatientMasterVisitId ,
    (case ScreeningDone
         when 1 then (SELECT id FROM LookupItem WHERE name='Yes')
		 WHEN 0 then (SELECT id FROM LookupItem WHERE name='No')
		  end) screeningDone,
	 ScreeningDate ,
	  ScreeningCategoryId ,
	  (SELECT top 1 l.ItemName FROM LookupItemView l WHERE l.MasterId=s.ScreeningCategoryId) ScreeningCategory,
	  ScreeningValueId,
	  (SELECT top 1 l.ItemName FROM LookupItemView l WHERE l.ItemId=s.ScreeningValueId) ScreeningValue,
      Comment ,
	 Active ,
	 DeleteFlag ,
	 CreatedBy,
	CreateDate ,
	 VisitDate
FROM            dbo.PatientScreening s
GO


