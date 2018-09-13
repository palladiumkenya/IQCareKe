export interface PatientAppointmet {
    Id?: number;
    PatientId?: number;
    PatientMasterVisitId?: number;
    ServiceAreaId?: number;
    AppointmentDate?: Date;
    ReasonId?: number;
    Description?: string;
    StatusId?: number;
    StatusDate?: Date;
    DifferentiatedCareId?: number;
    DeleteFlag?: number;
    CreatedBy?: number;
}
