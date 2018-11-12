export interface PncVisitDetailsCommand {
    PatientId: number;
    ServiceAreaId: number;
    VisitDate: Date;
    VisitNumber: number;
    VisitType: number;
    UserId: number;
    DaysPostPartum?: number;
    PatientMasterVisitId: number;
}
