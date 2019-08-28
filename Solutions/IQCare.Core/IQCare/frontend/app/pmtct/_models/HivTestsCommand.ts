export interface HivTestsCommand {
    HtsEncounterId: number;
    ProviderId: number;
    PatientId: number;
    PatientMasterVisitId: number;
    ServiceAreaId: number;

    // Testing
    Testing: Testing[];

    // HtsEncounter Result
    FinalTestingResult: FinalTestingResult;
}

export interface Testing {
    KitId: number;
    KitLotNumber: string;
    ExpiryDate: Date;
    Outcome: number;
    TestRound: number;
    SyphilisResult?: number;
}

export interface FinalTestingResult {
    FinalResultHiv1: number;
    FinalResultHiv2?: number;
    FinalResult: number;
    FinalResultGiven: number;
    CoupleDiscordant?: number;
    FinalResultsRemarks: string;
    AcceptedPartnerListing: number;
    ReasonsDeclinePartnerListing?: number;
}
