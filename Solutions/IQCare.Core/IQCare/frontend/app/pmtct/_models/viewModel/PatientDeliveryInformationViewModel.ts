export interface PatientDeliveryInformationViewModel {
    id: number;
    patientMasterVisitId: number;
    profileId: number;
    durationOfLabour: number;
    dateOfDelivery: Date;
    timeOfDelivery: string;
    modeOfDelivery: string;
    placentaComplete: string;
    bloodLossCapacity?: number;
    bloodLossClassification: string;
    motherCondition: string;
    deliveryComplicationsExperienced: string;
    deliveryComplicationNotes: string;
    deliveryConductedBy: string;
    maternalDeathAudited: string;
    maternalDeathAuditDate?: Date;
    apgarScores: string;
}
