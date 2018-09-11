export interface PatientPregnancy {
    id: number;
    patientId: number;
    patientMasterVisitId: number;
    lmp?: Date;
    edd?: Date;
    gravidae?: number;
    gestation: number;
    parity?: number;
    parity2?: number;
    outcome: number;
    dateOfOutcome?: Date;
}
