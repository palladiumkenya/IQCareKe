export interface PatientChronicIllness {
    Id?: number;
    PatientId?: number;
    PatientMasterVisitId?: number;
    ChronicIllness?: number;
    Treatment?: string;
    Dose?: number;
    Duration?: number;
    DeleteFlag?: boolean;
    OnsetDate?: Date;
    Active?: number;
    CreateBy?: number;
}
