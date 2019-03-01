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

export interface EditAppointmentCommand
{
    AppointmentId : number;
    AppointmentDate: Date;
    Description: string;
}
