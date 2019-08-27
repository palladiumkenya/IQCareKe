import {PatientEducation} from './PatientEducation';

export interface PatientEducationCommand {
    PatientId?: number;
    PatientMasterVisitId?: number;
    BreastExamDone?: number;
    TreatedSyphilis?: number;
    TestedForSyphilis?: number;
    SyphilisTestUsed?: number;
    SyphilisResults?: number;
    CreateBy?: number;
    CounsellingTopics?: PatientEducation[];
}
