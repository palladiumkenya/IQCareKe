export interface PatientReferralEditCommand {
    Id: number;
    PatientId: number;
    PatientMasterVisitId: number;
    ReferredFrom: number;
    ReferredTo: number;
    ReferralReason: string;
    ReferralDate: Date;
    ReferredBy: number;
    DeleteFlag: number;
    CreateDate: Date;
    CreateBy: number;
}
