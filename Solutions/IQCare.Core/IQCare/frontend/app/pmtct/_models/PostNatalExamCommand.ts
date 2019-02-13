import { PostNatalExamResult } from './PostNatalExamCommand';
export interface PostNatalExamCommand {
    Id: number;
    PatientId: number;
    PatientMasterVisitId: number;
    ExaminationTypeId: number;
    CreateBy: number;
    DeleteFlag: boolean;
    PostNatalExamResults: PostNatalExamResult[];
}

export interface PostNatalExamResult {
    ExamId: number;
    FindingId: number;
    FindingsNotes: string;
}
