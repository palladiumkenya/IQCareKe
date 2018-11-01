import { PatientReferral } from './PatientReferral';
import { PatientAppointment } from './PatientAppointmet';

export interface ReferralAppointmentCommand {
    PatientReferral: PatientReferral;
    PatientAppointment: PatientAppointment;
    CreatedBy?: number;
}
