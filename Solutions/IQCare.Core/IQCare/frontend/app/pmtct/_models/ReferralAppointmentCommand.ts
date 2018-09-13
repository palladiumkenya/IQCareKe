import {PatientReferral} from './PatientReferral';
import {PatientAppointmet} from './PatientAppointmet';

export interface ReferralAppointmentCommand {
    PatientReferral: PatientReferral[];
    PatientAppointment: PatientAppointmet[];
    CreatedBy?: number;
}
