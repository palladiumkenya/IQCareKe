export interface PatientEncounter {
    Id?: number;
    PatientMasterVisitId?: number;
    PatientEncounterId?: number;
    EncounterTypeId?: number;
    EncounterStartTime?: Date;
    EncounterEndTime?: Date;
    PatientId?: number;
    PersonId?: number;
    VisitNumber?: number;
    Encounter?: string;
    UserName?: string;
    DeleteFlag?: boolean;
}
