select * from sys.triggers where name like '%chai%';

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[trg_chai_insert_patient_allergy]'))
DROP TRIGGER [dbo].[trg_chai_insert_patient_allergy]
GO

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[trg_chai_insert_patient_clinical_status]'))
DROP TRIGGER [dbo].[trg_chai_insert_patient_clinical_status]
GO

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[trg_chai_insert_patient_prescription]'))
DROP TRIGGER [dbo].[trg_chai_insert_patient_prescription]
GO

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[trg_chai_insert_patient_vitals]'))
DROP TRIGGER [dbo].[trg_chai_insert_patient_vitals]
GO

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[trg_chai_insert_patient_who]'))
DROP TRIGGER [dbo].[trg_chai_insert_patient_who]
GO

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[trg_chai_patient_info]'))
DROP TRIGGER [dbo].[trg_chai_patient_info]
GO

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[trg_chai_update_patient_allergy]'))
DROP TRIGGER [dbo].[trg_chai_update_patient_allergy]
GO

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[trg_chai_update_patient_clinical_status]'))
DROP TRIGGER [dbo].[trg_chai_update_patient_clinical_status]
GO

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[trg_chai_update_patient_vitals]'))
DROP TRIGGER [dbo].[trg_chai_update_patient_vitals]
GO

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[trg_chai_update_patient_who]'))
DROP TRIGGER [dbo].[trg_chai_update_patient_who]
GO
