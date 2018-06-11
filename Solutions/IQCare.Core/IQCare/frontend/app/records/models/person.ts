export class Person {
    FirstName: string;
    LastName: string;
    MiddleName: string;
    DateOfBirth: string;
    MaritalStatus: number;
    Sex: number;
    PersonId: number;
    DobPrecision: number;
    CreatedBy: number;
    Educationallevel: number;
    Occcupation: number;
    IdentifyerType: number;
    IdentifyerNumber: string;
    RegistrationDate: number;
}

export class PersonIdentification {
    PersonId: number;
}
export class Person2 {
    
    FirstName: string;
    LastName: string;
    MiddleName: string;
    DateOfBirth: string;
    MaritalStatus: number;
    Sex: number;
    PersonId: number;
    DobPrecision: number;
    CreatedBy: number;
}

export class RegistrationVariables {
    personAge: number;
    personMonth: number;
}

export class PersonLocation {
    countyId: number
    subcountyId: number
    WardId: number
    NearestHealthCenter: string;
    LandMark: string;

}
export class SubCounty {
    constructor(countyId: number,
        subCountyId: string,
        subCountyName: string) { }
   
    }


export class County {
    constructor(
        countyId: number,
        countyName: string) {}
}

export class Ward {
    constructor(
        subCountyId: number,
        wardId: number,
        wardName: string) { }
}

export class PersonAddress {
    MobilePhonenumber: string
    Alternativenumber: string
    EmailAddress: string;
}
export class Emergency {
    emgRegisteredClinic: number
    emgFirstName: string
    emgMiddleName: string
    emgLastName: string
    emgGender: number
    emgRelationShip: number
    emgPrimaryMobileNo: string
    emgConsentToCall: number
    emgLimitedConsent: string;


}

export class EmergencyList{
    emgRegisteredClinic: string
    emgFirstName: string
    emgMiddleName: string
    emgLastName: string
    emgGender: string
    emgRelationShip: string
    emgPrimaryMobileNo: number
    emgConsentToCall: string
    emgLimitedConsent: string;


}

export class NextofKinEmergency {
    nokRegisteredClinic: number
    nokFirstName: string
    nokMiddleName: string
    nokLastName: string
    nokGender: number
    nokRelationShip: number
    nokPrimaryMobileNo: string
    nokConsentToCall: number
    nokLimitedConsent: string;


}

export class NextofKinEmergencyList {
    nokRegisteredClinic: string
    nokFirstName: string
    nokMiddleName: string
    nokLastName: string
    nokGender: string
    nokRelationShip: string
    nokPrimaryMobileNo: number
    nokConsentToCall: string
    nokLimitedConsent: string;


}

export class EmergencyArray {
    PersonId: number
    firstname: string 
    middlename: string
    lastname: string
    gender: number
    EmergencyContactPersonId: number
    MobileContact: string 
    ConsentType: number
    ConsentValue: number
    ConsentReason: string
    CreatedBy: number
    DeleteFlag: boolean
    RelationshipType: number
    
 
}

export class EmergencyContacts {
    Emergency: any[];
}
