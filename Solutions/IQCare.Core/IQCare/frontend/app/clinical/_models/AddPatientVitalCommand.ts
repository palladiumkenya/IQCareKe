export interface AddPatientVitalCommand{
    PatientId?: number;
    PatientmasterVisitId?: number;
    Temperature?: number;
    RespiratoryRate?: number;
    HeartRate?: number;
    BpDiastolic?: number;
    BpSystolic?: number;
    Height?: number;
    Weight?: number;
    Spo2?: number;
    Bmi?: number;
    HeadCircumference?: number;
    BmiZ?: string;
    WeightForAge?: number;
    WeightForHeight?: number;
    VisitDate?: Date;
    Muac ? : number;
    Comment:string;
}