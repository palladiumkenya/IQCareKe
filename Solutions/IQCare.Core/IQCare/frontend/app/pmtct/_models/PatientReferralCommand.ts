export interface PatientReferralCommand {
    Id: number;
    PatientId: number;
    PatientMasterVisitId: number;
    ReferredFrom: number;
    ReferredTo: number;
    ReferralReason: string;
    ReferralDate: Date;
    ReferredBy: number;
    DeleteFlag: number;
    CreatedBy: number;
}
