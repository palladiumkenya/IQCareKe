import { Action } from '@ngrx/store';

export enum ClientActionTypes {
    CONSENT_TESTING = '[ClientState] ConsentTesting',
    TESTED = '[ClientState] Tested',
    CONSENT_PARTNER_LISTING = '[ClientState] ConsentPartnerListing',
    IS_POSITIVE = '[ClientState] IsPositive',
    IS_REFERRED = '[ClientState] isReferred',
    TESTED_AS = '[ClientState] testedAs',
    ENROLLED = '[ClientState] isEnrolled',
    PNS_SCREENED = '[ClientState] isPnsScreened',
    PNS_TRACING = '[ClientState] isPnsTracingDone',
    CLEAR_STATE = '[ClientState] clearState',
    SERVICE = '[ClientState] service',
    PERSONID = '[ClientState] PersonId',
    FAMILY_SCREENED = '[ClientState] isFamilyScreeningDone',
    FAMILY_TRACING = '[ClientState] isFamilyTracingDone',
    PNS_SCREENED_POSITIVE = '[ClientState] PnsScreenedPositive',
    FAMILY_SCREENED_POSITIVE = '[ClientState] FamilyScreenedPositive'
}

export class SelectedService implements Action {
    readonly type = ClientActionTypes.SERVICE;

    constructor(public payload: string) { }
}

export class ConsentTesting implements Action {
    readonly type = ClientActionTypes.CONSENT_TESTING;

    constructor(public payload: boolean) { }
}

export class Tested implements Action {
    readonly type = ClientActionTypes.TESTED;

    constructor(public payload: boolean) { }
}

export class ConsentPartnerListing implements Action {
    readonly type = ClientActionTypes.CONSENT_PARTNER_LISTING;

    constructor(public payload: boolean) { }
}

export class IsPositive implements Action {
    readonly type = ClientActionTypes.IS_POSITIVE;

    constructor(public payload: boolean) { }
}

export class PersonId implements Action {
    readonly type = ClientActionTypes.PERSONID;

    constructor(public payload: number) {
    }
}

export class IsReferred implements Action {
    readonly type = ClientActionTypes.IS_REFERRED;

    constructor(public payload: boolean) { }
}

export class TestedAs implements Action {
    readonly type = ClientActionTypes.TESTED_AS;

    constructor(public payload: boolean) { }
}

export class IsEnrolled implements Action {
    readonly type = ClientActionTypes.ENROLLED;

    constructor(public payload: boolean) { }
}

export class IsPnsScreened implements Action {
    readonly type = ClientActionTypes.PNS_SCREENED;

    constructor(public payload: any) { }
}

export class IsFamilyScreeningDone implements Action {
    readonly type = ClientActionTypes.FAMILY_SCREENED;

    constructor(public payload: any) { }
}

export class IsFamilyTracingDone implements Action {
    readonly type = ClientActionTypes.FAMILY_TRACING;

    constructor(public payload: any) { }
}

export class IsPnsTracingDone implements Action {
    readonly type = ClientActionTypes.PNS_TRACING;

    constructor(public payload: any) { }
}

export class PnsScreenedPositive implements Action {
    readonly type = ClientActionTypes.PNS_SCREENED_POSITIVE;

    constructor(public payload: any) { }
}

export class FamilyScreenedPositive implements Action {
    readonly type = ClientActionTypes.FAMILY_SCREENED_POSITIVE;

    constructor(public payload: any) { }
}

export class ClearState implements Action {
    readonly type = ClientActionTypes.CLEAR_STATE;
}

export type ClientActions
    = ConsentTesting
    | Tested
    | ConsentPartnerListing
    | IsPositive
    | IsReferred
    | TestedAs
    | IsEnrolled
    | IsPnsScreened
    | IsPnsTracingDone
    | ClearState
    | SelectedService
    | PersonId
    | IsFamilyScreeningDone
    | IsFamilyTracingDone
    | PnsScreenedPositive
    | FamilyScreenedPositive;
