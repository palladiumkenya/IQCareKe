export interface PatientScreeningCommand {
    Id: number;
    PatientId: number;
    PatientMasterVisitId: number;
    ScreeningTypeId: number;
    ScreeningDone: boolean;
    ScreeningDate?: Date;
    ScreeningCategoryId?: number;
    ScreeningValueId?: number;
    Comment: string;
    Active: boolean;
    DeleteFlag: boolean;
    CreatedBy: number;
    CreateDate: Date;
    AuditData: string;
    VisitDate?: Date;
}
