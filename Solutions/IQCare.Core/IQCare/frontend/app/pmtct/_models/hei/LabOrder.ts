export interface LabOrder {
    Ptn_Pk: number;
    PatientId: number;
    LocationId: number;
    FacilityId: number;
    VisitId: number;
    ModuleId: number;
    OrderedBy: number;
    OrderDate: Date;
    ClinicalOrderNotes: string;
    CreateDate: Date;
    OrderStatus: string;
    UserId: number;
    PatientMasterVisitId: number;
    LabTests: LabTestInfo[];
}


export interface LabTestInfo {
    Id: number;
    Notes: string;
    LabTestName: string;
}
