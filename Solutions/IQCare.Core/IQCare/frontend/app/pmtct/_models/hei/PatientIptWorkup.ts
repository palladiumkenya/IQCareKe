export interface PatientIptWorkup {
    Id?: number;
    PatientMasterVisitId?: number;
    PatientId?: number;
    YellowColouredUrine?: number;
    Numbness?: number;
    YellownessOfEyes?: boolean;
    AbdominalTenderness?: boolean;
    LiverFunctionTests?: string;
    DeleteFlag?: boolean;
    CreatedBy?: number;
    CreateDate?: Date;
    StartIpt?: boolean;
    IptStartDate?: Date;
    IptRegimen?: number;
}
