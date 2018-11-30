CREATE VIEW DeliveredBabyApgarScoreView
AS
SELECT apg.*, lki.DisplayName AS ApgarScoreType FROM  [dbo].[DeliveredBabyApgarScore] apg 
INNER JOIN LookupItem lki ON lki.Id = apg.ApgarScoreId