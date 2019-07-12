export interface PregnancyIndicatorLogCommand {
    Id?: number;
    PatientId: number;
    PatientMasterVisitId: number;
    LMP?: Date;
    EDD?: Date;
    Outcome?: number;
    DateOfOutcome?: Date;
    CreatedBy?: number;
}
