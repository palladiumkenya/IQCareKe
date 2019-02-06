export interface ChronicIllnessEmitter {
    PatientId?: number;
    PatientmasterVisitId?: number;
    chronicIllnessId: number;
    chronicIllness?: string;
    onSetDate?: Date;
    currentTreatment?: string;
    dose?: number;
}
