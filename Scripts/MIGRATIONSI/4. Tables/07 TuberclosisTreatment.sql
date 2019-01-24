IF NOT EXISTS
(
	SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[TuberclosisTreatment]')
          AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[TuberclosisTreatment](
		[Id][INT]NOT NULL IDENTITY(1,1),
		[PatientId][INT] NOT NULL,
		[PatientMasterVisitId][INT] NOT NULL,
		[CreatedBy][INT] NOT NULL,
		[TBRxStartDate][DateTime],
		[TBRxEndDate][DateTime],
		[RegimenId][int],
		[CreateDate][DateTime] NOT NULL DEFAULT(GETDATE()),
		[UpdateDate][DateTime] NULL,
		[DeleteFlag][Bit] NOT NULL DEFAULT(0),
		[AuditData][xml]
	)ON [PRIMARY]
END
GO