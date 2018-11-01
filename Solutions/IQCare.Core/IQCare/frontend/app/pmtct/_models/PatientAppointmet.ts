export interface PatientAppointment {
    PatientMasterVisitId: number;
    ServiceAreaId: number;
    PatientId: number;
    AppointmentDate: Date;
    Description: string;
    StatusDate?: Date;
    DifferentiatedCareId?: number;
    AppointmentReason: string;
    CreatedBy: number;
}
