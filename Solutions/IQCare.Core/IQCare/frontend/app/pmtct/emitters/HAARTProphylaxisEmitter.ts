import {OtherIllnessesEmitter} from './OtherIllnessesEmitter';

export interface HAARTProphylaxisEmitter {
    onArvBeforeANCVisit?: number;
    startedHaartANC?: number;
    cotrimoxazole?: number;
    aztFortheBaby?: number;
    nvpForBaby?: number;
    illness?: number;
    otherIllness: OtherIllnessesEmitter[];
}
