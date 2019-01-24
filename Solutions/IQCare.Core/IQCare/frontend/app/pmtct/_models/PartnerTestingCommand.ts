export interface PartnerTestingCommand {
    PatientId: number;
    PatientMasterVisitId: number;
    PartnerTested: number;
    PartnerHIVResult: number;
    CreateDate: Date;
    CreatedBy: number;
    DeleteFlag: boolean;
    AuditData: string;
}
