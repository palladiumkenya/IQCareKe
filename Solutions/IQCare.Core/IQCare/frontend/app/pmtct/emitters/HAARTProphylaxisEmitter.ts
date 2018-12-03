import {PatientChronicIllness} from '../_models/PatientChronicIllness';

export interface HAARTProphylaxisEmitter {
    onArvBeforeANCVisit?: number;
    startedHaartANC?: number;
    cotrimoxazole?: number;
    aztFortheBaby?: number;
    nvpForBaby?: number;
    illness?: number;
    chronicIllness: PatientChronicIllness[];
    otherIllness?: number;
}
