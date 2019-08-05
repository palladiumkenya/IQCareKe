export interface PatientAppointmentEditCommand {
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
