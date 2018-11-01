CREATE VIEW PatientDrugAdministrationView 
AS
SELECT pd.*,drg.DisplayName AS StrDrugAdministered, 
val.DisplayName AS StrValue FROM PatientDrugAdministration pd
 INNER JOIN LookupItem drg ON pd.DrugAdministered = drg.Id
 INNER JOIN LookupItem val ON pd.Value = val.Id