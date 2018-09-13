export interface PatientChronicIllness {
    Id?: number;
    PatientId?: number;
    PatientMasterVisitId?: number;
    ChronicIllness?: number;
    Treatment?: string;
    Dose?: number;
    Duration?: number;
    DeleteFlag?: number;
    OnsetDate?: Date;
    Active?: boolean;
    CreateBy?: number;
}
