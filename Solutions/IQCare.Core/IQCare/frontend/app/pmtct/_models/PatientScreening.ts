export interface PatientScreening {
    Id?: number;
    PatientId?: number;
    PatientMasterVisitId?: number;
    ScreeningTypeId?: number;
    ScreeningDone?: boolean;
    ScreeningDate?: Date;
    ScreeningCategoryId?: number;
    ScreeningValueId?: number;
    Comment?: string;

}