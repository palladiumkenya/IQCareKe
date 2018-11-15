export interface Milestone {
    Id?: number;
    PatientId?: number;
    PatientMasterVisitId?: number;
    TypeAssessedId?: number;
    AchievedId?: boolean;
    StatusId?: number;
    Comment?: string;
    CreatedBy?: number;
    CreateDate?: Date;
    DeleteFlag?: number;
    DateAssessed: Date;
}
