export interface FamilyPlanningCommand {
    Id: number;
    PatientId: number;
    PatientMasterVisitId: number;
    FamilyPlanningStatusId: number;
    ReasonNotOnFPId: number;
    DeleteFlag: boolean;
    CreatedBy: number;
    CreateDate: Date;
    VisitDate: Date;
    AuditData: string;
}
