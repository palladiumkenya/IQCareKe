export interface ClientMonitoringCommand {
    PatientId?: number;
    PatientmasterVisitId?: number;
    ViralLoadSampleTaken?: boolean;
    WhoStage?: number;
    FacilityId?: number;
    ServiceAreaId?: number;
    ClinicalNotes?: string;
    ScreeningTypeId?: number;
    ScreeningDone?: boolean;
    ScreeningDate?: Date;
    ScreenedTB?: number;
    CaCxMethod?: number;
    CaCxResult?: number;
    Comments?: string;
    CreatedBy?: number;

}
