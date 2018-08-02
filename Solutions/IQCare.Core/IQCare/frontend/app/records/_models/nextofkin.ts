import { Person } from './person';
export class NextOfKin extends Person {
    kinContactRelationship: number;
    kinConsentToSMS: number;
    consentDeclineReason: string;
}
