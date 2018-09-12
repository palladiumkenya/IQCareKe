import {PatientDrugAdministration} from './PatientDrugAdministration';
import {PatientChronicIllness} from './PatientChronicIllness';

export interface HaartProphylaxisCommand {
    PatientDrugAdministration: PatientDrugAdministration[];
    PatientChronicIllnesses: PatientChronicIllness[];
    OtherIllness?: number;
}
