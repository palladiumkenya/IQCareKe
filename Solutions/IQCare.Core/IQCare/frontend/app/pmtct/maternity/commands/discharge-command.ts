export interface DischargeCommand 
{
    PatientMasterVisitId?: number;
    OutcomeStatus?: number;
    OutcomeDescription?: string;
    DateDischarged?: Date;
    CreatedBy?: number;
}


export interface UpdatePatientDischargeCommand
{
    Id ? : number;
    OutcomeStatus?: number;
    OutcomeDescription?: string;
    DateDischarged?: Date;
}