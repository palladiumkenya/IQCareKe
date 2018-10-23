export interface PatientIpt {
    Id?: number;
    PatientMasterVisitId?: number;
    PatientId?: number;
    IptDueDate?: Date;
    IptDateCollected?: Date;
    Weight?: number;
    Hepatotoxicity?: boolean;
    Peripheralneoropathy?: boolean;
    Rash?: boolean;
    AdheranceMeasurement?: number;
    DeleteFlag?:  boolean;
    CreatedBy?: number;
    CreateDate?: Date;
    HepatotoxicityAction?: string;
    PeripheralneoropathyAction?: string;
    RashAction?: string;
    AdheranceMeasurementAction?: string;
}
