import {PreventiveService} from './PreventiveService';

export interface PatientPreventiveService {
    preventiveService: PreventiveService[];
    InsecticideTreatedNet?: number;
    InsecticideGivenDate?: Date;
    AntenatalExercise?: number;
    PartnerTestingVisit?: number;
    FinalHIVResult?: number;
    CreatedBy?: number;
}
