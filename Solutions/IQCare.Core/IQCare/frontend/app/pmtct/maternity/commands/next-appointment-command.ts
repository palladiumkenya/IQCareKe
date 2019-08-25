export interface NextAppointmentCommand {

    PatientMasterVisitId?: number;
    ServiceAreaId?: number;
    PatientId?: number;
    AppointmentDate?: Date;
    Description?: string;
    StatusDate?: Date;
    DifferentiatedCareId?: number;
    AppointmentReason?: string;
    CreatedBy?: number;
}

export interface EditAppointmentCommand {
    AppointmentId: number;
    AppointmentDate: Date;
    Description: string;
    UserId: number;
    PatientId: number;
    PatientMasterVisitId: number;
    DifferentiatedCareId?: number;
    ReasonId: number;
    ServiceAreaId: number;
    StatusId: number;
}
