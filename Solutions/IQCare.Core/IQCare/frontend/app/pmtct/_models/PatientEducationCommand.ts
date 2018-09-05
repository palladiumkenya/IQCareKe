import {PatientEducation} from './PatientEducation';

export interface PatientEducationCommand {
    PatientId?: number;
    PatientMasterVisitId?: number;
    BreastExamDone?: number;
    TreatedSyphilis?: number;
    CounsellingTopics?: PatientEducation[];
}
