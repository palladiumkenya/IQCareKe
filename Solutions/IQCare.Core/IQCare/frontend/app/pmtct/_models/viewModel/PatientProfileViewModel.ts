export interface PatientProfileViewModel {
    id: number;
    patientId: number;
    patientMasterVisitId: number;
    ageMenarche: number;
    pregnancyId: number;
    visitNumber: number;
    visitType: number;
    treatedForSyphilis: number;
    createdBy: number;
    deleteFlag: boolean;
    createDate: Date;
    daysPostPartum: number;
}
