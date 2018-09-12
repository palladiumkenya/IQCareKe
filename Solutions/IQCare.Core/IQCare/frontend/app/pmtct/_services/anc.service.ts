import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs/index';
import { catchError, tap } from 'rxjs/operators';
import { PatientEducationCommand } from '../_models/PatientEducationCommand';
import { ClientMonitoringCommand } from '../_models/ClientMonitoringCommand';
import { HaartProphylaxisCommand } from '../_models/HaartProphylaxisCommand';
import { ReferralAppointmentCommandService } from './referral-appointment-command.service';
import { PatientPreventiveService } from '../_models/PatientPreventiveService';
import { PatientProfile } from '../_models/patientProfile';
import { Profile } from 'selenium-webdriver/firefox';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})

export class AncService {
    private API_URL = environment.API_URL;
    private _url = '/api/anc/';
    private _url_pedc = '/api/PatientEducationExamination/';
    private _url_cm = '/api/ClientMonitoring/';
    private _url_haart = '/api/HaartProphylaxis/';
    private _url_ref = '/api/ReferralAppointment/';
    private _url_pre = '/api/PreventiveService/';
    private _url_visit = '/api/VisitDetails/';

    public profile: PatientProfile = {};


    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlerService) { }

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
        return this.http.post<any>(this.API_URL + '' + this._url_cm, JSON.stringify(haartProphylaxisCommand), httpOptions).pipe(
            tap(saveHaartProphylaxis => this.errorHandler.log('Error posting HaartProphylaxis Command')),
            catchError(this.errorHandler.handleError<any>('HaartProphylaxisController'))
        );
    }

    public saveReferralAppointment(referralAppointment: ReferralAppointmentCommandService): Observable<ReferralAppointmentCommandService> {
        return this.http.post<any>(this.API_URL + '' + this._url_ref, JSON.stringify(referralAppointment), httpOptions).pipe(
            tap(saveReferralAppointment => this.errorHandler.log('Error posting saveReferralAppointment Command')),
            catchError(this.errorHandler.handleError<any>('ReferralAppointmentController'))
        );
    }

    public savePreventiveServices(patientPreventiveService: PatientPreventiveService): Observable<PatientPreventiveService> {
        return this.http.post<any>(this.API_URL + '' + this._url_ref, JSON.stringify(patientPreventiveService), httpOptions).pipe(
            tap(savePreventiveServices => this.errorHandler.log('Error posting Preventive Service Command')),
            catchError(this.errorHandler.handleError<any>('PreventiveServiceController'))
        );
    }

    public saveHivStatus(htsAncEncounter: any): Observable<any> {
        const Indata = {
            'Encounter': htsAncEncounter
        };

        return this.http.post<any>(this.API_URL + '/api/HtsEncounter',
            JSON.stringify(Indata), httpOptions).pipe(
                tap(saveHivStatus => this.errorHandler.log('SaveHivStatus command')),
                catchError(this.errorHandler.handleError<any>('PreventiveServiceController'))
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

