export interface Milestone {
    Id?: number;
    PatientId?: number;
    PatientMasterVisitId?: number;
    TypeAssessed?: number;
    Achieved?: boolean;
    Status?: number;
    Comment?: string;
    CreatedBy?: number;
    CreateDate?: Date;
    DeleteFlag?: number;
}
