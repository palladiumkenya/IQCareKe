export interface PregnancyCommand {
    Id?: number;
    PatientId?: number;
    PatientMasterVisitId?: number;
    Lmp?: Date;
    Edd?: Date;
    Gestation?: number;
    Gravidae?: number;
    Parity?: number;
    Parity2?: number;
    Outcome?: number;
    AgeAtMenarche ?:number;
    DateOfOutcome?: Date;
    CreatedBy?: number;
    CreateDate?: Date;
    DeleteFlag?: boolean;
}
