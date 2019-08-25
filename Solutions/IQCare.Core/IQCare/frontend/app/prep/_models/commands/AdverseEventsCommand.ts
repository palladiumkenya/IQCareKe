export interface AdverseEventsCommand {
    Id?: number;
    PatientId: number;
    PatientMasterVisitId: number;
    EventName: string;
    EventCause: string;
    Severity: string;
    Action: string;
    DeleteFlag: boolean;
    CreateBy: number;
    CreateDate: Date;
    AuditData: string;
    AdverseEventId: number;
}
