export interface PatientIptOutcome {
    Id?: number;
    PatientMasterVisitId?: number;
    PatientId?: number;
    IptEvent?: boolean;
    ReasonForDiscontinuation?: string;
    DeleteFlag?: boolean;
    CreatedBy?: number;
    CreateDate?: Date;
}
