import { ClientActions, ClientActionTypes } from './app.states';


export function consentReducer(state: any = {}, action: ClientActions) {
    switch (action.type) {
        case ClientActionTypes.SERVICE:
            return { ...state, service: action.payload };
        case ClientActionTypes.CONSENT_TESTING:
            return { ...state, consent: action.payload };
        case ClientActionTypes.TESTED:
            return { ...state, tested: action.payload };
        case ClientActionTypes.IS_POSITIVE:
            return { ...state, isPositive: action.payload };
        case ClientActionTypes.PERSONID:
            return { ...state, PersonId: action.payload };
        case ClientActionTypes.PATIENTID:
            return { ...state, PatientId: action.payload };
        case ClientActionTypes.CONSENT_PARTNER_LISTING:
            return { ...state, consentPartnerListing: action.payload };
        case ClientActionTypes.IS_REFERRED:
            return { ...state, isReferred: action.payload };
        case ClientActionTypes.TESTED_AS:
            return { ...state, testedAs: action.payload };
        case ClientActionTypes.ENROLLED:
            return { ...state, isEnrolled: action.payload };
        case ClientActionTypes.PNS_SCREENED:
            const newIsPnsScreened = ('isPnsScreened' in state) ?
                [...state['isPnsScreened'], JSON.parse(action.payload)] : [JSON.parse(action.payload)];
            return { ...state, isPnsScreened: newIsPnsScreened };
        case ClientActionTypes.PNS_TRACING:
            const newIsPnsTracingDone = ('isPnsTracingDone' in state) ?
                [...state['isPnsTracingDone'], JSON.parse(action.payload)] : [JSON.parse(action.payload)];
            return { ...state, isPnsTracingDone: newIsPnsTracingDone };
        case ClientActionTypes.FAMILY_SCREENED:
            const newIsFamilyScreeningDone = ('isFamilyScreeningDone' in state) ?
                [...state['isFamilyScreeningDone'], JSON.parse(action.payload)] : [JSON.parse(action.payload)];
            return { ...state, isFamilyScreeningDone: newIsFamilyScreeningDone };
        case ClientActionTypes.FAMILY_TRACING:
            const newIsFamilyTracingDone = ('isFamilyTracingDone' in state) ?
                [...state['isFamilyTracingDone'], JSON.parse(action.payload)] : [JSON.parse(action.payload)];
            return { ...state, isFamilyTracingDone: newIsFamilyTracingDone };
        case ClientActionTypes.PNS_SCREENED_POSITIVE:
            const newPnsScreenedPositive = ('PnsScreenedPositive' in state) ?
                [...state['PnsScreenedPositive'], JSON.parse(action.payload)] : [JSON.parse(action.payload)];
            return { ...state, PnsScreenedPositive: newPnsScreenedPositive };
        case ClientActionTypes.FAMILY_SCREENED_POSITIVE:
            const newFamilyScreenedPositive = ('FamilyScreenedPositive' in state) ?
                [...state['FamilyScreenedPositive'], JSON.parse(action.payload)] : [JSON.parse(action.payload)];
            return { ...state, FamilyScreenedPositive: newFamilyScreenedPositive };
        case ClientActionTypes.CLEAR_STATE:
            state = {};
            return state;
        default:
            return state;
    }
}
