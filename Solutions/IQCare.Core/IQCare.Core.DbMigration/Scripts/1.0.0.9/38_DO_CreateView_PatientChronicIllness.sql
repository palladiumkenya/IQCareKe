CREATE View PatientChronicIllnessView 
AS
SELECT pc.Id,pc.PatientId,pc.PatientMasterVisitId,pc.ChronicIllness AS ChronicIllnessId,lk.DisplayName AS ChronicIllness ,pc.Treatment ,pc.Dose
      ,pc.Duration ,pc.DeleteFlag,pc.CreateBy AS CreatedBy,pc.CreateDate AS DateCreated ,pc.OnsetDate ,pc.Active
  FROM PatientChronicIllness pc INNER JOIN LookupItem lk ON lk.Id = pc.ChronicIllness