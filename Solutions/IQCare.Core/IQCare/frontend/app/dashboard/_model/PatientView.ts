export interface PatientView {
    age?: string;
    ageNumber?: number;
    isHtsEnrolled?: string;
    rowID?: number;
    personId?: number;
    patientId?: number;
    ptn_pk?: number;
    firstName?: string;
    midName?: string;
    lastName?: string;
    sex?: number;
    gender?: string;
    dateOfBirth?: Date;
    dobPrecision?: boolean;
    patientType?: string;
    nationalId?: number;
    registrationDate?: Date;
    enrollmentDate?: Date;
    identifierValue?: number;
    serviceAreaId?: number;
    serviceAreaName?: string;
    physicalAddress?: string;
    mobileNumber?: string;
    maritalStatusId?: number;
    maritalStatusName?: string;
    landMark?: string;
}
