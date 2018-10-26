export interface PatientIcf {
    Id?: number;
    PatientMasterVisitId?: number;
    PatientId?: number;
    Cough?: boolean;
    Fever?: boolean;
    WeightLoss?: boolean;
    NightSweats?: boolean;
    DeleteFlag?: boolean;
    CreatedBy?: number;
    CreateDate?: Date;
    OnAntiTbDrugs?: boolean;
    OnIpt?: boolean;
    EverBeenOnIpt?: boolean;
    ContactWithTb?: boolean;
}
