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
    SERVICE = '[ClientState] service'
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

export class IsPnsTracingDone implements Action {
    readonly type = ClientActionTypes.PNS_TRACING;

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
    | SelectedService;
