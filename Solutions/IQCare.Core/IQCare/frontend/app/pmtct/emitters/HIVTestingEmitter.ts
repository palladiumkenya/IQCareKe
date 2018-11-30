export interface HIVTestingEmitter {
    testingDone?: number;
    hivTest?: any;
    kitName?: number;
    testResult?: number;
    lotNumber?: string;
    expiryDate?: Date;
    nextAppointmentDate?: Date;
    finalResult?: number;

    consentOption: number;
    ancTestEntryPoint: number;
}
