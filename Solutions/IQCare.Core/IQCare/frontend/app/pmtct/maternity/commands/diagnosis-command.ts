export interface DiagnosisCommand {
    Id?: number;
    PatientId?: number;
    PatientMasterVisitId?: number;
    Diagnosis?: string;
    ManagementPlan?: string;
    CreatedBy?: number;
}
