export interface AllergiesCommand {
    Id?: number;
    PatientId: number;
    PatientMasterVisitId?: number;
    Allergen: string;
    DeleteFlag: boolean;
    CreateBy: number;
    CreateDate: Date;
    AuditData: string;
    Reaction: number;
    Severity: number;
    OnsetDate?: Date;
}
