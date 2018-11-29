export interface FamilyPlanningMethodCommand {
    Id: number;
    PatientId: number;
    PatientFPId: number;
    FPMethodId: number;
    Active: boolean;
    DeleteFlag: boolean;
    CreatedBy: number;
    CreateDate: Date;
    AuditData: string;
}
