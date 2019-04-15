export class Priority {
    id: number;
    name: string = '';
}

export class ServiceRoomList {
    id: number;
    name: string = '';
}
export class Roomlist {
    id: number;
    displayName: string = '';
}

// tslint:disable-next-line: class-name
export class serviceAreas {
    id: number;
    displayName: string = '';
}
export class ServiceList {
    id: number;
    displayName: string = '';
    code: string;
}

export class WaitingList {
    ServiceRoomId: number;
    PriorityId: number;
}

// tslint:disable-next-line: class-name
export class servicePoint {
    id: number;
    name: string = '';
}
// tslint:disable-next-line: class-name
export class serviceRoom {
    ServiceAreaId: number;
    ServicePointId: number;
    RoomId: number;

}


export class SpecificRoomLinkage {
    serviceAreaName: string;
    serviceAreaId: number;
    servicePointId: number;
    servicePointName: string;
    roomName: string;
    roomId: number;
    id: number;
}
export class PatientList {
    FirstName: string;
    MiddleName: string;
    LastName: string;
    Id: number;
    ServiceAreaName: string;
    ServicePointName: string;
    RoomName: string;
    Priority: string;
    CreatedBy: string;
    CreateDate: Date;
    PatientId: number;
    PersonId: number;

}
export class PatientWaitingList {
    FirstName: string;
    MiddleName: string;
    LastName: string;
    Id: number;
    ServiceAreaName: string;
    ServicePointName: string;
    RoomName: string;
    Priority: string;
    CreatedBy: string;
    CreateDate: Date;
    PatientId: number;
    PersonId: number;
    Status:boolean;
}
export class Person {
    age: string;
    ageNumber: number;
    isHtsEnrolled: string;
    rowID: number;
    personId: number;
    patientId: number;
    ptn_pk: number;
    firstName: string;
    midName: string = '';
    lastName: string;
    sex: number;
    gender: string;
    dateOfBirth: Date;
    dobPrecision: boolean;
    patientType: string;
    nationalId: number;
    registrationDate: Date;
    enrollmentDate: Date;
    identifierValue: number;
    serviceAreaId: number;
    serviceAreaName: string;
    physicalAddress: string;
    mobileNumber: string;
    maritalStatusId: number;
    maritalStatusName: string;
    landMark: string;
    county: string;
    subCounty: string;
    ward: string;
}
