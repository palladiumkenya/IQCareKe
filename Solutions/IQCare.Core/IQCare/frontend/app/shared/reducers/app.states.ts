import { Action } from '@ngrx/store';

export enum ClientActionTypes {
    CONSENT_TESTING = '[ClientState] ConsentTesting',
    TESTED = '[ClientState] Tested',
    CONSENT_PARTNER_LISTING = '[ClientState] ConsentPartnerListing',
    IS_POSITIVE = '[ClientState] IsPositive',
    IS_REFERRED = '[ClientState] isReferred',
    TESTED_AS = '[] testedAs'
}

export class ConsentTesting implements Action {
    readonly type = ClientActionTypes.CONSENT_TESTING;

    constructor(public payload: boolean) {}
}

export class Tested implements Action {
    readonly type = ClientActionTypes.TESTED;

    constructor(public payload: boolean) {}
}

export class ConsentPartnerListing implements Action {
    readonly type = ClientActionTypes.CONSENT_PARTNER_LISTING;

    constructor(public payload: boolean) {}
}

export class IsPositive implements Action {
    readonly type = ClientActionTypes.IS_POSITIVE;

    constructor(public payload: boolean) {}
}

export class IsReferred implements Action {
    readonly type = ClientActionTypes.IS_REFERRED;

    constructor(public payload: boolean) {}
}

export class TestedAs implements Action {
    readonly type = ClientActionTypes.TESTED_AS;

    constructor(public payload: boolean) {}
}

export type ClientActions
    = ConsentTesting
    | Tested
    | ConsentPartnerListing
    | IsPositive
    | IsReferred
    | TestedAs;
