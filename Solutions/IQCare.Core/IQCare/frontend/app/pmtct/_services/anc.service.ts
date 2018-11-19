import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { environment } from '../../../environments/environment';
import { Observable, of } from 'rxjs/index';
import { catchError, tap } from 'rxjs/operators';
import { PatientEducationCommand } from '../_models/PatientEducationCommand';
import { ClientMonitoringCommand } from '../_models/ClientMonitoringCommand';
import { HaartProphylaxisCommand } from '../_models/HaartProphylaxisCommand';
import { ReferralAppointmentCommandService } from './referral-appointment-command.service';
import { PatientPreventiveService } from '../_models/PatientPreventiveService';
import { PatientProfile } from '../_models/patientProfile';
import { PncVisitDetailsCommand } from '../_models/PncVisitDetailsCommand';
import { HivStatusCommand } from '../_models/HivStatusCommand';
import {PatientChronicIllness} from '../_models/PatientChronicIllness';
import {DrugAdministrationCommand} from '../maternity/commands/drug-administration-command';
import {AdministerDrugInfo} from '../maternity/commands/administer-drug-info';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})

export class AncService {
    private API_URL = environment.API_URL;
    private API_PMTCT_URL = environment.API_PMTCT_URL;
    private _url = '/api/anc/';
    private _url_pedc = '/api/PatientEducationExamination/post';
    private _url_cm = '/api/ClientMonitoring/';
    private _url_haart = '/api/HaartProphylaxis/';
    private _url_ref = '/api/PatientReferralAndAppointment/AddPatientReferralInfo';
    private _url_app = '/api/PatientReferralAndAppointment/AddPatientNextAppointment';
    private _url_pre = '/api/ANCPreventivervice/';
    private _url_pci = '/api/PatientChronicIllness/post';

    public profile: PatientProfile = {};


    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlerService) { }

    public saveANCVisitDetails(ancVisitDetailsCommand: PncVisitDetailsCommand): Observable<any> {
        return this.http.post<any>(this.API_URL + '/api/VisitDetails/AddANCVisit', JSON.stringify(ancVisitDetailsCommand),
            httpOptions).pipe(
            tap(saveANCVisitDetails => this.errorHandler.log(`successfully saved ANC visit details`)),
            catchError(this.errorHandler.handleError<any>('Error saving ANC visit details'))
        );
    }

    public savePatientEducation(patientEducationCommand: PatientEducationCommand): Observable<PatientEducationCommand> {
        return this.http.post<any>(this.API_URL + '' + this._url_pedc, JSON.stringify(patientEducationCommand),
            httpOptions).pipe(
                tap(savePatientEducation => this.errorHandler.log('Error posting patientEducationCommand')),
                catchError(this.errorHandler.handleError<any>('PatientEducationExaminationController'))
            );
    }

    public saveClientMonitoring(clientMonitoringCommand: ClientMonitoringCommand): Observable<ClientMonitoringCommand> {
        return this.http.post<any>(this.API_URL + '' + this._url_cm, JSON.stringify(clientMonitoringCommand), httpOptions).pipe(
            tap(saveClientMonitoring => this.errorHandler.log('Error posting client monitoring Command')),
            catchError(this.errorHandler.handleError<any>('ClientMonitoringController' + this.API_URL + '' + this._url_pedc))
        );
    }

    public saveHaartProphylaxis(haartProphylaxisCommand: HaartProphylaxisCommand): Observable<HaartProphylaxisCommand> {
        return this.http.post<any>(this.API_URL + '' + this._url_haart, JSON.stringify(haartProphylaxisCommand), httpOptions).pipe(
            tap(saveHaartProphylaxis => this.errorHandler.log('Error posting HaartProphylaxis Command')),
            catchError(this.errorHandler.handleError<any>('HaartProphylaxisController'))
        );
    }

    public savePatientChronicIllness(chronicIllnessCommand: any): Observable<any> {
        if (chronicIllnessCommand.length == 0) {
            return of([]);
        }
        return this.http.post<any>(this.API_URL + '' + this._url_pci, JSON.stringify(chronicIllnessCommand), httpOptions).pipe(
            tap(savePatientChronicIllness => this.errorHandler.log('Error posting Patient Chronic Illness Command')),
            catchError(this.errorHandler.handleError<any>('PatientChronicIllnessController'))
        );
    }

    public saveDrugAdministration(drug: any): Observable<any> {
        if (drug.length == 0) {
            return of([]);
        }
        return this.http.post<any>(this.API_PMTCT_URL + '/api/PatientDrugAdministration/Add',
            JSON.stringify(drug), httpOptions).pipe(
            tap(saveDrugAdministration => this.errorHandler.log('Error posting Patient Drug Administration Command')),
            catchError(this.errorHandler.handleError<any>('DrugAdministration Controller'))
        );
    }


    public saveReferral(referralCommand: ReferralAppointmentCommandService): Observable<ReferralAppointmentCommandService> {
        return this.http.post<any>(this.API_URL + '' + this._url_ref, JSON.stringify(referralCommand), httpOptions).pipe(
            tap(saveReferralAppointment => this.errorHandler.log('Error posting saveReferral Command')),
            catchError(this.errorHandler.handleError<any>('ReferralAppointmentController'))
        );
    }

    public saveAppointment(referralCommand: ReferralAppointmentCommandService): Observable<ReferralAppointmentCommandService> {
        return this.http.post<any>(this.API_URL + '' + this._url_app, JSON.stringify(referralCommand), httpOptions).pipe(
            tap(saveReferralAppointment => this.errorHandler.log('Error posting Appointment Command')),
            catchError(this.errorHandler.handleError<any>('ReferralAppointmentController'))
        );
    }

    public savePreventiveServices(patientPreventiveService: PatientPreventiveService): Observable<PatientPreventiveService> {
        return this.http.post<any>(this.API_URL + '' + this._url_pre, JSON.stringify(patientPreventiveService), httpOptions).pipe(
            tap(savePreventiveServices => this.errorHandler.log('Error posting Preventive Service Command')),
            catchError(this.errorHandler.handleError<any>('PreventiveServiceController'))
        );
    }

    /* public saveHivStatus(htsAncEncounter: any): Observable<any> {
         const Indata = {
             'Encounter': htsAncEncounter
         };
 
         return this.http.post<any>(this.API_URL + '/api/HtsEncounter',
             JSON.stringify(Indata), httpOptions).pipe(
                 tap(saveHivStatus => this.errorHandler.log('SaveHivStatus command')),
                 catchError(this.errorHandler.handleError<any>('PreventiveServiceController'))
             );
     }*/

    public saveAncHivStatus(hivStatusCommand: HivStatusCommand, anyTests: any[]): Observable<any> {
        if (anyTests.length == 0) {
            return of([]);
        }

        const Indata = {
            'Encounter': hivStatusCommand
        };

        return this.http.post<any>(this.API_URL + '/api/HtsEncounter', JSON.stringify(Indata), httpOptions).pipe(
            tap(savePncBabyExamination => this.errorHandler.log(`successfully saved pnc hiv status`)),
            catchError(this.errorHandler.handleError<any>('Error saving pnc hiv status'))
        );
    }

    public saveHivResults(serviceAreaId: number, patientMasterVisitId: number,
        patientId: number, providerId: number, htsEncounterId: number, testing: any[], finalResultsBody: {}): Observable<any> {
        const Indata = {
            'Testing': testing,
            'FinalTestingResult': finalResultsBody,
            'HtsEncounterId': htsEncounterId,
            'ProviderId': providerId,
            'PatientId': patientId,
            'PatientMasterVisitId': patientMasterVisitId,
            'ServiceAreaId': serviceAreaId
        };

        return this.http.post<any>(this.API_URL + '/api/HtsEncounter/addTestResults', JSON.stringify(Indata), httpOptions).pipe(
            tap(saveHivResults => this.errorHandler.log('SaveHivResults command')),
            catchError(this.errorHandler.handleError<any>('PreventiveServiceController'))
        );
    }
}

