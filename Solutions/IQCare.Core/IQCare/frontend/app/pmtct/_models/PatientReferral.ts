export interface PatientReferral {
    Id?: number;
    PatientId?: number;
    PatientMasterVisitId?: number;
    ReferredFrom?: number;
    ReferredTo?: number;
    ReferralReason?: string;
    ReferralDate?: Date;
    RefferedBY?: number;
    DeleteFlag?: number;
}
