export interface PatientIcfAction {
    Id?: number;
    PatientId?: number;
    PatientMasterVisitId?: number;
    SputumSmear?: number;
    ChestXray?: number;
    StartAntiTb?: boolean;
    InvitationOfContacts?: number;
    EvaluatedForIpt?: boolean;
    DeleteFlag?: boolean;
    CreatedBy?: number;
    CreateDate?: Date;
    GeneXpert?: number;
}
