export interface CircumcisionCommand {
    Id?: number;
    PatientId: number;
    ClientCircumcised: number;
    ReferredToVMMC: number;
    CreatedBy: number;
    CreateDate: Date;
    DeleteFlag: boolean;
}
