export interface PrepStatusCommand {
    Id?: number;
    PatientId: number;
    PatientEncounterId: number;
    SignsOrSymptomsHIV: number;
    AdherenceCounsellingDone: number;
    ContraindicationsPrepPresent: number;
    PrepStatusToday: number;
    CreatedBy: number;
}
