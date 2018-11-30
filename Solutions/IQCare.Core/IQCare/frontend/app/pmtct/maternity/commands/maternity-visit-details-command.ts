export interface MaternityVisitDetailsCommand {
    patientId?: number;
    patientMasterVisitId?: number;
    ageAtMenarche?: number;
    pregnancyId?: number;
    visitNumber?: number;
    visitType?: number;
    treatedForSyphilis?: number;
    deleteFlag?: boolean;
    createdBy?: number;
    createDate?: Date;
    postpartum?: string;
    auditData?: string;
    
}
