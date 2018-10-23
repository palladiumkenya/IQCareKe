export interface Vaccination {
    Id?: number;
    PatientId?: number;
    PatientMasterVisitId?: number;
    Vaccine?: number;
    VaccineStage?: number;
    DeleteFlag?: number;
    CreatedBy?: number;
    CreateDate?: Date;
    VaccineDate?: Date;
    Active?: number;
    AppointmentId?: number;
    Period?: number;
    NextSchedule?: Date;
}
