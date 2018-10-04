update mst_feature set featuretypeid = (select top 1 id from mst_decode where name = 'Module Actions') where featurename='Drug Dispense'

CREATE TABLE [dbo].[dtl_PatientLockedRecordsForDispensing](
	[ptn_pk] [int] NULL,
	[ptn_pharmacy_pk] [int] NULL,
	[UserName] [varchar](200) NULL,
	[StartDate] [datetime] NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[dtl_PatientPharmacyOrderpartialDispense](
	[ptn_pharmacy_pk] [int] NULL,
	[drug_pk] [int] NULL,
	[batchid] [int] NULL,
	[DispensedQuantity] [int] NULL,
	[DispensedBy] [int] NULL,
	[DispensedByDate] [datetime] NULL,
	[comments] [varchar](1000) NULL,
	[createdate] [datetime] NULL,
	[updatedate] [datetime] NULL,
	[deleteflag] [bit] NULL
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[dtl_TBScreening](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Ptn_Pk] [int] NULL,
	[Visit_Pk] [int] NOT NULL,
	[LocationID] [int] NULL,
	[UserID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
	[TBFindings] [int] NULL,
	[TBAvailableResults] [int] NULL CONSTRAINT [DF_dtl_TBScreening_TBAvailableResults]  DEFAULT (NULL),
	[SputumSmear] [int] NULL,
	[SputumSmearDate] [datetime] NULL,
	[GeneExpert] [int] NULL,
	[GeneExpertDate] [datetime] NULL,
	[SputumDST] [int] NULL,
	[SputumDSTDate] [datetime] NULL,
	[ChestXRay] [int] NULL CONSTRAINT [DF_dtl_TBScreening_ChestXRay]  DEFAULT (NULL),
	[ChestXRayDate] [datetime] NULL,
	[TissueBiopsy] [int] NULL CONSTRAINT [DF_dtl_TBScreening_TissueBiopsy]  DEFAULT (NULL),
	[TissueBiopsyDate] [datetime] NULL,
	[CXRResults] [int] NULL,
	[OtherCXR] [varchar](250) NULL,
	[TBClassification] [int] NULL,
	[PatientClassification] [int] NULL,
	[TBPlan] [int] NULL,
	[OtherTBPlan] [varchar](250) NULL,
	[TBRegimen] [int] NULL,
	[OtherTBRegimen] [varchar](50) NULL,
	[TBRegimenStartDate] [datetime] NULL,
	[TBRegimenEndDate] [datetime] NULL,
	[TBTreatmentOutcome] [int] NULL,
	[OtherTBTreatmentOutcome] [varchar](50) NULL,
	[IPT] [int] NULL,
	[INHStartDate] [datetime] NULL,
	[INHEndDate] [datetime] NULL,
	[PyridoxineStartDate] [datetime] NULL,
	[PyridoxineEndDate] [datetime] NULL,
	[AdherenceAddressed] [int] NULL CONSTRAINT [DF_dtl_TBScreening_AdherenceAddressed]  DEFAULT (NULL),
	[AnyMissedDoses] [int] NULL CONSTRAINT [DF_dtl_TBScreening_AnyMissedDoses]  DEFAULT (NULL),
	[ReferredForAdherence] [int] NULL CONSTRAINT [DF_dtl_TBScreening_ReferredForAdherence]  DEFAULT (NULL),
	[OtherTBSideEffects] [varchar](250) NULL,
	[TBConfirmedSuspected] [int] NULL CONSTRAINT [DF_dtl_TBScreening_TBConfirmedSuspected]  DEFAULT (NULL),
	[INHStopDate] [datetime] NULL,
	[ContactsScreenedForTB] [int] NULL CONSTRAINT [DF_dtl_TBScreening_ContactsScreenedForTB]  DEFAULT (NULL),
	[IfNoSpecifyWhy] [varchar](250) NULL,
	[FacilityPatientReferredTo] [int] NULL,
	[ReasonDeclinedIPT] [int] NULL,
	[OtherReasonDeclinedIPT] [varchar](200) NULL,
	[IPTAdherence] [int] NULL,
	[IPTContraindication] [int] NULL,
	[IPTDiscontinued] [int] NULL,
	[TBRxDuration] [int] NULL,
	[IsCough] [int] NULL,
	[IsFever] [int] NULL,
	[IsFailureThriveOrPoorWeightGain] [int] NULL,
	[IsLethargy] [int] NULL,
	[IsContact] [int] NULL,
	[IsNoticeableWeightLoss] [int] NULL,
	[IsNightSweats] [int] NULL,
	[EligibleForIPT] [int] NULL CONSTRAINT [DF_dtl_TBScreening_EligibleForIPT]  DEFAULT ((0)),
	[IPTWPYellowColoredUrine] [int] NULL,
	[IPTWPNumbnessBurning] [int] NULL,
	[IPTWPYellownessEyes] [int] NULL,
	[IPTWPTenderness] [int] NULL,
	[OtherReasonDiscontinuedIPT] [varchar](250) NULL,
 CONSTRAINT [PK_dtl_TBScreening_1] PRIMARY KEY CLUSTERED 
(
	[Visit_Pk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[DTL_Adult_Initial_Evaluation_Form](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Ptn_pk] [int] NOT NULL,
	[Visit_Pk] [int] NOT NULL,
	[LocationId] [int] NOT NULL,
	[UserId] [int] NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
	[Temperature] [decimal](18, 2) NULL,
	[RespirationRate] [decimal](18, 2) NULL,
	[HeartRate] [decimal](18, 2) NULL,
	[SystolicBloodPressure] [decimal](18, 2) NULL,
	[DiastolicBloodPressure] [decimal](18, 2) NULL,
	[DiagnosisConfirmed] [bit] NULL,
	[ConfirmHIVPosDate] [datetime] NULL,
	[ChildAccompaniedByCaregiver] [bit] NULL,
	[TreatmentSupporterRelationship] [int] NULL,
	[HealthEducation] [bit] NULL,
	[DisclosureStatus] [int] NULL,
	[SchoolingStatus] [int] NULL,
	[HIVSupportgroup] [bit] NULL,
	[PatientReferredFrom] [int] NULL,
	[NursesComments] [varchar](8000) NULL,
	[PresentingComplaints] [int] NULL,
	[LMPassessmentValid] [bit] NULL,
	[LMPDate] [datetime] NULL,
	[LMPNotaccessedReason] [int] NULL,
	[EDD] [datetime] NULL,
	[RespiratoryDiseaseName] [varchar](1000) NULL,
	[RespiratoryDiseaseDate] [datetime] NULL,
	[RespiratoryDiseaseTreatment] [varchar](1000) NULL,
	[CardiovascularDiseaseName] [varchar](1000) NULL,
	[CardiovascularDiseaseDate] [datetime] NULL,
	[CardiovascularDiseaseTreatment] [varchar](1000) NULL,
	[GastroIntestinalDiseaseName] [varchar](1000) NULL,
	[GastroIntestinalDiseaseDate] [datetime] NULL,
	[GastroIntestinalDiseaseTreatment] [varchar](1000) NULL,
	[NervousDiseaseName] [varchar](1000) NULL,
	[NervousDiseaseDate] [datetime] NULL,
	[NervousDiseaseTreatment] [varchar](1000) NULL,
	[DermatologyDiseaseName] [varchar](1000) NULL,
	[DermatologyDiseaseDate] [datetime] NULL,
	[DermatologyDiseaseTreatment] [varchar](1000) NULL,
	[MusculoskeletalDiseaseName] [varchar](1000) NULL,
	[MusculoskeletalDiseaseDate] [datetime] NULL,
	[MusculoskeletalDiseaseTreatment] [varchar](1000) NULL,
	[PsychiatricDiseaseName] [varchar](1000) NULL,
	[PsychiatricDiseaseDate] [datetime] NULL,
	[PsychiatricDiseaseTreatment] [varchar](1000) NULL,
	[HematologicalDiseaseName] [varchar](1000) NULL,
	[HematologicalDiseaseDate] [datetime] NULL,
	[HematologicalDiseaseTreatment] [varchar](1000) NULL,
	[GenitalUrinaryDiseaseName] [varchar](1000) NULL,
	[GenitalUrinaryDiseaseDate] [datetime] NULL,
	[GenitalUrinaryDiseaseTreatment] [varchar](1000) NULL,
	[OphthamologyDiseaseName] [varchar](1000) NULL,
	[OphthamologyDiseaseDate] [datetime] NULL,
	[OphthamologyDiseaseTreatment] [varchar](1000) NULL,
	[ENTDiseaseName] [varchar](1000) NULL,
	[ENTDiseaseDate] [datetime] NULL,
	[ENTDiseaseTreatment] [varchar](1000) NULL,
	[OtherDiseaseName] [varchar](1000) NULL,
	[OtherDiseaseDate] [datetime] NULL,
	[OtherDiseaseTreatment] [varchar](1000) NULL,
	[SchoolPerfomance] [int] NULL,
	[TBAssessmentICF] [int] NULL,
	[TBFindings] [int] NULL,
	[TBresultsAvailable] [bit] NULL,
	[SputumSmear] [int] NULL,
	[SputumSmearDate] [datetime] NULL,
	[ChestXRay] [bit] NULL,
	[ChestXRayDate] [datetime] NULL,
	[TissueBiopsy] [bit] NULL,
	[TissueBiopsyDate] [datetime] NULL,
	[CXR] [int] NULL,
	[OtherCXR] [varchar](1000) NULL,
	[TBTypePeads] [int] NULL,
	[PeadsTBPatientType] [int] NULL,
	[TBPlan] [int] NULL,
	[TBPlanOther] [varchar](1000) NULL,
	[TBRegimen] [int] NULL,
	[TBRegimenStartDate] [datetime] NULL,
	[TBRegimenEndDate] [datetime] NULL,
	[TBTreatmentOutcomesPeads] [int] NULL,
	[NoTB] [bit] NULL,
	[TBReason] [int] NULL,
	[INHStartDate] [datetime] NULL,
	[INHEndDate] [datetime] NULL,
	[PyridoxineStartDate] [datetime] NULL,
	[PyridoxineEndDate] [datetime] NULL,
	[SuspectTB] [bit] NULL,
	[StopINHDate] [datetime] NULL,
	[ContactsScreenedForTB] [bit] NULL,
	[TBNotScreenedSpecify] [varchar](1000) NULL,
	[LongTermMedications] [bit] NULL,
	[SulfaTMPDate] [datetime] NULL,
	[HormonalContraceptivesDate] [datetime] NULL,
	[AntihypertensivesDate] [datetime] NULL,
	[HypoglycemicsDate] [datetime] NULL,
	[AntifungalsDate] [datetime] NULL,
	[AnticonvulsantsDate] [datetime] NULL,
	[OtherLongTermMedications] [varchar](1000) NULL,
	[OtherCurrentLongTermMedications] [datetime] NULL,
	[HIVRelatedHistory] [int] NULL,
	[InitialCD4] [decimal](18, 2) NULL,
	[InitialCD4Percent] [decimal](18, 2) NULL,
	[InitialCD4Date] [datetime] NULL,
	[HighestCD4Ever] [decimal](18, 2) NULL,
	[HighestCD4Percent] [decimal](18, 2) NULL,
	[HighestCD4EverDate] [datetime] NULL,
	[CD4atARTInitiation] [decimal](18, 2) NULL,
	[CD4atARTInitiationDate] [datetime] NULL,
	[CD4AtARTInitiationPercent] [decimal](18, 2) NULL,
	[MostRecentCD4] [decimal](18, 2) NULL,
	[MostRecentCD4Percent] [decimal](18, 2) NULL,
	[MostRecentCD4Date] [datetime] NULL,
	[PreviousViralLoad] [decimal](18, 2) NULL,
	[PreviousViralLoadDate] [datetime] NULL,
	[OtherHIVRelatedHistory] [varchar](8000) NULL,
	[ARVExposure] [bit] NULL,
	[PMTC1StartDate] [datetime] NULL,
	[PMTC1Regimen] [varchar](1000) NULL,
	[PEP1Regimen] [varchar](1000) NULL,
	[PEP1StartDate] [datetime] NULL,
	[HAART1Regimen] [varchar](1000) NULL,
	[HAART1StartDate] [datetime] NULL,
	[Impression] [varchar](8000) NULL,
	[Diagnosis] [int] NULL,
	[HIVRelatedOI] [varchar](1000) NULL,
	[NonHIVRelatedOI] [varchar](1000) NULL,
	[WHOStageIConditions] [int] NULL,
	[WHOStageIIConditions] [int] NULL,
	[WHOStageIIIConditions] [int] NULL,
	[WHOStageIVConditions] [int] NULL,
	[InitiationWHOstage] [int] NULL,
	[WHOStage] [int] NULL,
	[WABStage] [int] NULL,
	[TannerStaging] [int] NULL,
	[Mernarche] [bit] NULL,
	[SpecifyAntibioticAllery] [varchar](1000) NULL,
	[DrugAllergiesToxicitiesPaeds] [int] NULL,
	[OtherDrugAllergy] [varchar](1000) NULL,
	[ARVSideEffects] [bit] NULL,
	[ShortTermEffects] [int] NULL,
	[OtherShortTermEffects] [varchar](1000) NULL,
	[LongTermEffects] [int] NULL,
	[OtherLongtermEffects] [varchar](1000) NULL,
	[WorkUpPlan] [varchar](8000) NULL,
	[LabEvaluationPeads] [bit] NULL,
	[SpecifyLabEvaluation] [varchar](200) NULL,
	[Counselling] [int] NULL,
	[OtherCounselling] [varchar](1000) NULL,
	[WardAdmission] [bit] NULL,
	[ReferToSpecialistClinic] [varchar](1000) NULL,
	[TransferOut] [varchar](1000) NULL,
	[ARTTreatmentPlanPeads] [int] NULL,
	[SwitchReason] [int] NULL,
	[StartART] [bit] NULL,
	[ARTEligibilityCriteria] [int] NULL,
	[OtherARTEligibilityCriteria] [varchar](1000) NULL,
	[SubstituteRegimen] [bit] NULL,
	[NumberDrugsSubstituted] [int] NULL,
	[StopTreatment] [bit] NULL,
	[StopTreatmentCodes] [int] NULL,
	[RegimenPrescribed] [int] NULL,
	[OtherRegimenPrescribed] [varchar](1000) NULL,
	[OIProphylaxis] [int] NULL,
	[ReasonCTXPrescribed] [int] NULL,
	[OtherTreatment] [varchar](8000) NULL,
	[SexualActiveness] [bit] NULL,
	[SexualOrientation] [int] NULL,
	[HighRisk] [int] NULL,
	[KnowSexualPartnerHIVStatus] [bit] NULL,
	[ParnerHIVStatus] [int] NULL,
	[GivenPWPMessages] [bit] NULL,
	[SaferSexImportanceExplained] [bit] NULL,
	[UnsafeSexImportanceExplained] [bit] NULL,
	[PDTDone] [bit] NULL,
	[Pregnant] [bit] NULL,
	[PMTCTOffered] [bit] NULL,
	[IntentionOfPregnancy] [bit] NULL,
	[DiscussedFertilityOptions] [bit] NULL,
	[DiscussedDualContraception] [bit] NULL,
	[CondomsIssued] [bit] NULL,
	[CondomNotIssued] [varchar](1000) NULL,
	[STIScreened] [bit] NULL,
	[VaginalDischarge] [bit] NULL,
	[UrethralDischarge] [bit] NULL,
	[GenitalUlceration] [bit] NULL,
	[STITreatmentPlan] [varchar](8000) NULL,
	[OnFP] [bit] NULL,
	[FPMethod] [int] NULL,
	[CervicalCancerScreened] [bit] NULL,
	[CervicalCancerScreeningResults] [int] NULL,
	[ReferredForCervicalCancerScreening] [bit] NULL,
	[HPVOffered] [bit] NULL,
	[OfferedHPVVaccine] [int] NULL,
	[HPVDoseDate] [datetime] NULL,
	[RefferedToFupF] [int] NULL,
	[SpecifyOtherRefferedTo] [varchar](1000) NULL,
	[otherpresentingcomplaints] [varchar](8000) NULL,
	[Additionalpresentingcomplaints] [varchar](8000) NULL,
	[MedHistoryFP] [int] NULL,
	[MedHistoryLastFP] [varchar](200) NULL,
	[AnyARVExposure] [int] NULL,
	[ARVExposerdosesmissed] [int] NULL,
	[ARVExposerdelaydoses] [int] NULL,
	[MernarcheDate] [datetime] NULL,
	[ReasonNotDisclosed] [int] NULL,
	[OtherDisclosureStatus] [varchar](200) NULL,
	[HIVSupportGroupMembership] [varchar](1000) NULL,
	[HighestLevelAttained] [int] NULL
) ON [PRIMARY]
SET ANSI_PADDING OFF
ALTER TABLE [dbo].[DTL_Adult_Initial_Evaluation_Form] ADD [OtherMedicalConditions] [varchar](200) NULL
ALTER TABLE [dbo].[DTL_Adult_Initial_Evaluation_Form] ADD [OtherGeneralConditions] [varchar](200) NULL
SET ANSI_PADDING ON
ALTER TABLE [dbo].[DTL_Adult_Initial_Evaluation_Form] ADD [OtherAbdomenConditions] [varchar](1000) NULL
ALTER TABLE [dbo].[DTL_Adult_Initial_Evaluation_Form] ADD [OtherCardiovascularConditions] [varchar](1000) NULL
SET ANSI_PADDING OFF
ALTER TABLE [dbo].[DTL_Adult_Initial_Evaluation_Form] ADD [OtherOralCavityConditions] [varchar](200) NULL
ALTER TABLE [dbo].[DTL_Adult_Initial_Evaluation_Form] ADD [OtherGenitourinaryConditions] [varchar](200) NULL
SET ANSI_PADDING ON
ALTER TABLE [dbo].[DTL_Adult_Initial_Evaluation_Form] ADD [OtherCNSConditions] [varchar](1000) NULL
ALTER TABLE [dbo].[DTL_Adult_Initial_Evaluation_Form] ADD [OtherChestLungsConditions] [varchar](1000) NULL
ALTER TABLE [dbo].[DTL_Adult_Initial_Evaluation_Form] ADD [OtherSkinConditions] [varchar](1000) NULL
ALTER TABLE [dbo].[DTL_Adult_Initial_Evaluation_Form] ADD [OtherMedicalConditionNotes] [varchar](8000) NULL
SET ANSI_PADDING OFF
ALTER TABLE [dbo].[DTL_Adult_Initial_Evaluation_Form] ADD [SpecifyotherARTchangereason] [varchar](200) NULL
ALTER TABLE [dbo].[DTL_Adult_Initial_Evaluation_Form] ADD [OtherARTStopCode] [varchar](200) NULL
ALTER TABLE [dbo].[DTL_Adult_Initial_Evaluation_Form] ADD [Fluconazole] [int] NULL
ALTER TABLE [dbo].[DTL_Adult_Initial_Evaluation_Form] ADD [OtherOIProphylaxis] [varchar](200) NULL
ALTER TABLE [dbo].[DTL_Adult_Initial_Evaluation_Form] ADD [secondLineRegimenSwitch] [int] NULL
ALTER TABLE [dbo].[DTL_Adult_Initial_Evaluation_Form] ADD [PatientReferredFromOthers] [varchar](200) NULL
ALTER TABLE [dbo].[DTL_Adult_Initial_Evaluation_Form] ADD [weightforage] [int] NULL
ALTER TABLE [dbo].[DTL_Adult_Initial_Evaluation_Form] ADD [weightforheight] [int] NULL
ALTER TABLE [dbo].[DTL_Adult_Initial_Evaluation_Form] ADD [Menarche] [bit] NULL
ALTER TABLE [dbo].[DTL_Adult_Initial_Evaluation_Form] ADD [MenarcheDate] [datetime] NULL
ALTER TABLE [dbo].[DTL_Adult_Initial_Evaluation_Form] ADD [ProgressionInWHOstage] [bit] NULL
ALTER TABLE [dbo].[DTL_Adult_Initial_Evaluation_Form] ADD [SpecifyWHOprogression] [varchar](200) NULL
ALTER TABLE [dbo].[DTL_Adult_Initial_Evaluation_Form] ADD [CervicalCancerScreenedDate] [datetime] NULL
SET ANSI_PADDING ON
ALTER TABLE [dbo].[DTL_Adult_Initial_Evaluation_Form] ADD [PreviousAdmissionDiagnosis] [varchar](1000) NULL
 CONSTRAINT [PK__DTL_Adul__3214EC271BB453DA] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

CREATE TABLE [dbo].[dtl_Multiselect_line](
	[Ptn_pk] [int] NOT NULL,
	[ValueID] [int] NOT NULL,
	[Visit_Pk] [int] NOT NULL,
	[FieldName] [varchar](100) NULL,
	[FieldID] [int] NULL,
	[UserId] [int] NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
	[DateField1] [datetime] NULL,
	[DateField2] [datetime] NULL,
	[NumericField] [int] NULL,
	[Other_Notes] [varchar](500) NULL,
	[deleteflag] [int] NULL,
	[ValueIdOtherNotes] [varchar](500) NULL,
 CONSTRAINT [PK_dtl_Multiselect_line] PRIMARY KEY CLUSTERED 
(
	[ValueID] ASC,
	[Ptn_pk] ASC,
	[Visit_Pk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO


ALTER TABLE Ord_Visit
ADD PatientClassification int,
IsEnrolDifferenciatedCare bit,
ARTRefillModel int;

ALTER TABLE Mst_ItemMaster
ADD QtyUnitDisp int,
--syrup int,
MorDose int,
MidDose int,
EvenDose int,
NightDose int;

ALTER TABLE Ord_PatientPharmacyOrder
ADD TreatmentPlan int,
NotDispensedNote varchar(8000);

ALTER TABLE Dtl_PatientPharmacyOrder
ADD MorningDose decimal(10,2),
MiddayDose decimal(10,2),
EveningDose decimal(10,2),
NightDose decimal(10,2),
comments varchar(500);

ALTER TABLE Mst_User
ADD Email varchar(50),
Designation int;

ALTER TABLE mst_regimen
ADD Purpose nvarchar(5),
RegimenLineID int;

ALTER TABLE mst_regimen
ALTER COLUMN RegimenName varchar(255);






SET IDENTITY_INSERT mst_Feature ON
insert into mst_Feature (FeatureID,featureName,ReportFlag,DeleteFlag,AdminFlag,UserID,createdate,updatedate,optionalflag,systemid,published,CountryId,ModuleId,MultiVisit,seq,registrationformflag,canlink,FeatureTypeId,ReferenceId) 
values(254,'Dashboard',0,0,0,1,getdate(),null,null,0,null,null,201,null,null,null,null,null,null)
insert into mst_Feature (FeatureID,featureName,ReportFlag,DeleteFlag,AdminFlag,UserID,createdate,updatedate,optionalflag,systemid,published,CountryId,ModuleId,MultiVisit,seq,registrationformflag,canlink,FeatureTypeId,ReferenceId) 
values(255,'Dispense',0,0,0,1,getdate(),null,null,0,null,null,201,null,null,null,null,null,null)
insert into mst_Feature (FeatureID,featureName,ReportFlag,DeleteFlag,AdminFlag,UserID,createdate,updatedate,optionalflag,systemid,published,CountryId,ModuleId,MultiVisit,seq,registrationformflag,canlink,FeatureTypeId,ReferenceId) 
values(256,'Stock Summary Web',0,0,0,1,getdate(),null,null,0,null,null,201,null,null,null,null,null,null)
insert into mst_Feature (FeatureID,featureName,ReportFlag,DeleteFlag,AdminFlag,UserID,createdate,updatedate,optionalflag,systemid,published,CountryId,ModuleId,MultiVisit,seq,registrationformflag,canlink,FeatureTypeId,ReferenceId) 
values(257,'Stock Management',0,0,0,1,getdate(),null,null,0,null,null,201,null,null,null,null,null,null)
SET IDENTITY_INSERT mst_Feature OFF

insert into [lnk_GroupFeatures] values(1,255,1,getdate(),null)
insert into [lnk_GroupFeatures] values(1,255,2,getdate(),null)
insert into [lnk_GroupFeatures] values(1,255,3,getdate(),null)
insert into [lnk_GroupFeatures] values(1,255,4,getdate(),null)
insert into [lnk_GroupFeatures] values(1,255,5,getdate(),null)

Update mst_regimen set deleteflag = 1

INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 1, N'AF4B', N'ABC + 3TC + EFV', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 1, N'AF4A', N'ABC + 3TC + NVP', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 1, N'AF1B', N'AZT + 3TC + EFV', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 1, N'AF1A', N'AZT + 3TC + NVP', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 1, N'AF3B', N'd4T + 3TC + EFV', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 1, N'AF3A', N'd4T + 3TC + NVP', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 1, N'AF2B', N'TDF + 3TC + EFV', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 1, N'AF2A', N'TDF + 3TC + NVP', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 1, N'AF5X', N'other Adult 1st line', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 3, N'AS5B', N'ABC + 3TC + ATV/r', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 3, N'AS5A', N'ABC + 3TC + LPV/r', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 3, N'AS1B', N'AZT + 3TC + ATV/r', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 3, N'AS1A', N'AZT + 3TC + LPV/r', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 3, N'AS2C', N'TDF + 3TC + ATV/r', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 3, N'AS2A', N'TDF + 3TC + LPV/r', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 3, N'AS6X', N'other Adult 2nd line', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 5, N'AT2A', N'ETV + 3TC + DRV + RTV', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 5, N'AT1A', N'RAL + 3TC + DRV + RTV', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 5, N'AT1B', N'RAL + 3TC + DRV + RTV + AZT', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 5, N'AT1C', N'RAL + 3TC + DRV + RTV + TDF', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 5, N'AT2X', N'other Adult 3rd line', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 1, N'CF2E', N'ABC + 3TC + ATV/r', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 1, N'CF2B', N'ABC + 3TC + EFV', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 1, N'CF2D', N'ABC + 3TC + LPV/r', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 1, N'CF2A', N'ABC + 3TC + NVP', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 1, N'CF1D', N'AZT + 3TC + ATV/r', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 1, N'CF1B', N'AZT + 3TC + EFV', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 1, N'CF1C', N'AZT + 3TC + LPV/r', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 1, N'CF1A', N'AZT + 3TC + NVP', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 1, N'CF3B', N'd4T + 3TC + EFV for children weighing >=  25kg', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 1, N'CF3A', N'd4T + 3TC + NVP for children weighing >=  25kg', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 1, N'CF4D', N'TDF + 3TC + ATV/r', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 1, N'CF4B', N'TDF + 3TC + EFV', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 1, N'CF4C', N'TDF + 3TC + LPV/r', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 1, N'CF4A', N'TDF + 3TC + NVP', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 1, N'CF5X', N'other Paediatric 1st line', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 3, N'CS2C', N'ABC + 3TC + ATV/r', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 3, N'CS2A', N'ABC + 3TC + LPV/r', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 3, N'CS1B', N'AZT + 3TC + ATV/r', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 3, N'CS1A', N'AZT + 3TC + LPV/r', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 3, N'CS4X', N'other Paediatric 2nd line', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 5, N'CT2A', N'ETV + 3TC + DRV + RTV', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 5, N'CT1A', N'RAL + 3TC + DRV + RTV', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 5, N'CT1C', N'RAL + 3TC + DRV + RTV + ABC', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 5, N'CT1B', N'RAL + 3TC + DRV + RTV + AZT', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'222', 5, N'CT3X', N'other Paediatric 3rd line', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'224', 6, N'PA4X', N'other PEP regimens - Adults', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'224', 6, N'PA1C', N'AZT + 3TC + ATV/r', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'224', 6, N'PA1B', N'AZT + 3TC + LPV/r', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'224', 6, N'PA3C', N'TDF + 3TC + ATV/r', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'224', 6, N'PA3B', N'TDF + 3TC + LPV/r', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'224', 6, N'PC3A', N'ABC + 3TC + LPV/r', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'224', 6, N'PC4X', N'other PEP regimens - Children', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'224', 6, N'PC1A', N'AZT + 3TC + LPV/r', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'223', 7, N'PM1X', N'other PMTCT regimens - Women', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'223', 7, N'PM10', N'AZT + 3TC + ATV/r', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'223', 7, N'PM4', N'AZT + 3TC + EFV', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'223', 7, N'PM5', N'AZT + 3TC + LPV/r', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'223', 7, N'PM3', N'AZT + 3TC + NVP', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'223', 7, N'PM1', N'AZT from 14Wks to Delivery + NVP stat + AZT stat + 3TC BD during labour; then AZT/3TC 1 Wk post-partum', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'223', 7, N'PM2', N'NVP stat + AZT stat + 3TC BD during labour; then AZT/3TC 1wk post-partum', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'223', 7, N'PM11', N'TDF + 3TC + ATV/r', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'223', 7, N'PM9', N'TDF + 3TC + EFV', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'223', 7, N'PM7', N'TDF + 3TC + LPV/r', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'223', 7, N'PM6', N'TDF + 3TC + NVP', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'223', 7, N'PC5', N'3TC Liquid BD', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'223', 7, N'PC1X', N'other PMTCT regimens - Infants', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'223', 7, N'PC4', N'AZT Liquid BD for 6 weeks ', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'223', 7, N'PC6', N'NVP Liquid OD for 12 weeks ', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'223', 7, N'PC2', N'NVP OD for Breastfeeding Infants until 1 week after complete cessation of Breastfeeding ', 0)
GO
INSERT [dbo].[mst_Regimen] ([Purpose], [RegimenLineID], [RegimenCode], [RegimenName], [DeleteFlag]) VALUES (N'223', 7, N'PC1', N'NVP OD up to 6 weeks of age ', 0)
GO