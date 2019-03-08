IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'FK_PatientDelivery_PatientProfile') AND parent_object_id = OBJECT_ID(N'dbo.PatientDelivery'))
ALTER TABLE [dbo].[PatientDelivery] DROP CONSTRAINT [FK_PatientDelivery_PatientProfile]

