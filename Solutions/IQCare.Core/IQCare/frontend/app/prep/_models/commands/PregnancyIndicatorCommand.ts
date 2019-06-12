export interface PregnancyIndicatorCommand {
    PatientId: number;
    PatientMasterVisitId: number;
    LMP?: Date;
    EDD?: Date;
    PregnancyStatusId?: number;
    ANCProfile: Boolean;
    ANCProfileDate?: Date;
    Active?: Boolean;
    DeleteFlag: boolean;
    CreatedBy: number;
    CreateDate: Date;
    AuditData: string;
    VisitDate?: Date;
}
