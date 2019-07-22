export class EncounterDetails {
    RowID: number;
    EncounterId: number;
    PatientEncounterId: number;
    PatientId: number;
    EncounterDate: string;
    TestType: string;
    Provider: string;
    ResultOne: string;
    ResultTwo: string;
    finalResult: string;
    Consent: string;
    PartnerListingConsent: string;
    ServiceEntryPoint: string;
    EverTested: string;
    MonthsSinceLastTest?: number;
    TestedAs: string;
    CoupleDiscordant: string;
    EncounterRemarks: string;
    FinalResultGiven: string;
    TestingStrategy: string;
    EverSelfTested: string;
    TBScreening: string;
    Disability: string[];
    HasDisability: string;
    PartnerListingConsentDeclineReason: string;
}
