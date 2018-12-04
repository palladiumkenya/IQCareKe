ALTER TABLE PatientDelivery ADD PregnancyId INT NULL;

ALTER TABLE PatientDelivery ADD CONSTRAINT FK_PatientDelivery_PregnancyId FOREIGN KEY(PregnancyId) REFERENCES Pregnancy(Id);