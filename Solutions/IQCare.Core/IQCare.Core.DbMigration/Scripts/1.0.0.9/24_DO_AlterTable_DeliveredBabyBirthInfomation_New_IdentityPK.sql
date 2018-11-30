BEGIN TRANSACTION
ALTER TABLE [dbo].[DeliveredBabyApgarScore] DROP CONSTRAINT [FK_DeliveredBabyApgarScore_DeliveredBabyBirthInformation_Id]

ALTER TABLE [dbo].[DeliveredBabyBirthInformation] DROP CONSTRAINT [PK_DeliveredBabyBirthInformation_Id] WITH ( ONLINE = OFF )

ALTER TABLE [dbo].[DeliveredBabyBirthInformation] DROP COLUMN Id

ALTER TABLE [dbo].[DeliveredBabyBirthInformation] ADD Id INT NOT NULL IDENTITY(1,1);

ALTER TABLE DeliveredBabyBirthInformation ADD CONSTRAINT PK_DeliveredBabyBirthInformation_Id PRIMARY KEY(Id);

ALTER TABLE DeliveredBabyApgarScore ADD CONSTRAINT FK_DeliveredBabyApgarScore_DeliveredBabyBirthInformation_Id
 FOREIGN KEY(DeliveredBabyBirthInformationId) REFERENCES DeliveredBabyBirthInformation(Id)

COMMIT TRANSACTION