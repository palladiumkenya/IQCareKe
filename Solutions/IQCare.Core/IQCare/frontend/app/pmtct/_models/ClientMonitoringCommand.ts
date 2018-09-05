export interface ClientMonitoringCommand {
    PatientId?: number;
    PatientmasterVisitId?: number;
    WhoStage?: number;
    FacilityId?: number;
    ServiceAreaId?: number;
    ClinicalNotes?: string;
    ScreeningTypeId?: number;
    ScreeningDone?: number;
    ScreeningDate?: Date;
    ScreeningTB?: number;
    CaCxMethod?: number;
    CaCxResult?: number;
    Comments?: string;

}
