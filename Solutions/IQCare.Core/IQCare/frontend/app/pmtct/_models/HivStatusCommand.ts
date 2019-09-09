export interface HivStatusCommand {
    PersonId: number;
    ProviderId: number;
    PatientEncounterID: number;
    PatientMasterVisitId: number;

    PatientId: number;
    EverTested?: number;
    MonthsSinceLastTest?: number;
    MonthSinceSelfTest?: number;
    TestedAs?: number;
    TestingStrategy?: number;
    EncounterRemarks?: string;
    TestEntryPoint: number;
    Consent: number;
    EverSelfTested?: number;
    GeoLocation?: string;
    HasDisability?: number;
    Disabilities?: any[];
    TbScreening?: number;
    ServiceAreaId: number;
    EncounterTypeId: number;
    EncounterDate?: Date;
    EncounterType: number;
    HivCounsellingDone: number;
    OtherDisability: string;
}
