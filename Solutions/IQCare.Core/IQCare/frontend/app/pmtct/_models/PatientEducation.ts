export interface PatientEducation {
    Id?: number;
    PatientId?: number;
    PatientMasterVisitId?: number;
    CounsellingTopicId?: number;
    CounsellingTopic?: string;
    CounsellingDate?: Date;
    description: string;
}
