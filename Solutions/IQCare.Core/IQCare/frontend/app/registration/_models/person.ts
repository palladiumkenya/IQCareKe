export class Person {
    FirstName: string;
    LastName: string;
    MiddleName: string;
    DateOfBirth: string;
    MaritalStatus: number;
    Sex: number = 0;
    isPartner: boolean;
    partnerRelationship: number;
    patientId: any;
    createdBy: number;
}

export class RegistrationVariables {
    personAge: number;
}
