export interface PregnancyViewModel {
    Id?: number;
    patientId?: number;
    patientMasterVisitId?: number;
    lmp?: Date;
    edd?: Date;
    gestation?: number;
    gravidae?: number;
    parity?: number;
    parity2?: number;
    outcome?: number;
    dateOfOutcome?: Date;
}
