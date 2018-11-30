import { Person } from './person';
export class NextOfKin extends Person {
    kinContactRelationship: number;
    kinMobileNumber: number;
    kinConsentToSMS: number;
    consentDeclineReason: string;
    kinContactType: number;
}
