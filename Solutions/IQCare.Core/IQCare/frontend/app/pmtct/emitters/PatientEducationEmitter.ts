import {CounsellingTopicsEmitters} from './counsellingTopicsEmitters';

export interface PatientEducationEmitter {
    breastExamDone?: number;
    counsellingDate?: Date;
    counsellingTopics: CounsellingTopicsEmitters[];
    treatedSyphilis?: number;
}
