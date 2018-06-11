export class Search {
    identifierValue: string = '';
    firstName: string = '';
    midName: string = '';
    lastName: string = '';
}

export class SearchList{

    Id: number;
    PersonId: number;
    FirstName: string; 
    MiddleName: string;
    LastName: string;
    Sex: number;
    Active?: boolean;
    DeleteFlag?: boolean;
   CreateDate?: Date;
   CreatedBy?: number; 
   AuditData: string;
   DateOfBirth?:Date;
   DobPrecision?: boolean;
   PersonIdentifier: string;
   PersonIdentifierType: string;
   PersonIdentifierValue: string;
   PatientIdentifier: string;
   PatientIdentifierType: string;
    PatientIdentifierValue: string;
}