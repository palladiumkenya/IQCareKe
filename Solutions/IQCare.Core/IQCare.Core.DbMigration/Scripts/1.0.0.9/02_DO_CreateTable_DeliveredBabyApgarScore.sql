CREATE TABLE DeliveredBabyApgarScore
(
  Id INT IDENTITY(1,1) NOT NULL,
  ApgarScoreId INT NOT NULL,
  DeliveredBabyBirthInformationId INT NOT NULL,
  PRIMARY KEY(Id),
  CONSTRAINT [FK_DeliveredBabyApgarScore_PatientDelivery] FOREIGN KEY([DeliveredBabyBirthInformationId]) 
  REFERENCES [dbo].[DeliveredBabyBirthInformation] ([BirthId])
)