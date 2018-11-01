import { HivTestsCommand } from './../_models/HivTestsCommand';
import { PatientMasterVisitEncounter } from './../_models/PatientMasterVisitEncounter';
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { Observable, of } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { PncVisitDetailsCommand } from '../_models/PncVisitDetailsCommand';
import { HivStatusCommand } from '../_models/HivStatusCommand';
import { PatientDiagnosisCommand } from '../_models/PatientDiagnosisCommand';
import { PatientReferralCommand } from '../_models/PatientReferralCommand';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})
export class PncService {
    private API_URL = environment.API_URL;
    private API_LAB_URL = environment.API_LAB_URL;
    private API_MATERNITY_URL = environment.API_MATERNITY_URL;

    constructor(private http: HttpClient, private errorHandler: ErrorHandlerService) { }

    public savePncMasterVisit(patientMasterVisitEncounter: PatientMasterVisitEncounter): Observable<any> {
        return this.http.post<PatientMasterVisitEncounter>(this.API_URL + '/api/PatientMasterVisit',
            JSON.stringify(patientMasterVisitEncounter), httpOptions).pipe(
                tap(savePncMasterVisit => this.errorHandler.log(`successfully added pnc patientmastervisit`)),
                catchError(this.errorHandler.handleError<any>('Error saving pnc patientmastervisit'))
            );
    }

    public savePncVisitDetails(pncVisitDetailsCommand: PncVisitDetailsCommand): Observable<any> {
        return this.http.post(this.API_URL + '/api/VisitDetails/AddPNCVisitDetails',
            JSON.stringify(pncVisitDetailsCommand), httpOptions).pipe(
                tap(savePncVisitDetails => this.errorHandler.log(`successfully saved pnc visit details`)),
                catchError(this.errorHandler.handleError<any>('Error saving pnc visit details'))
            );
    }

    public savePncPostNatalExam(): Observable<any> {
        return this.http.post<any>(this.API_URL + '/',
            JSON.stringify(''), httpOptions).pipe(
                tap(savePncPostNatalExam => this.errorHandler.log(`successfully saved pnc postnatal exam`)),
                catchError(this.errorHandler.handleError<any>('Error saving pnc postnatal exam'))
            );
    }

    public savePncBabyExamination(): Observable<any> {
        return this.http.post<any>(this.API_URL + '', JSON.stringify(''), httpOptions).pipe(
            tap(savePncBabyExamination => this.errorHandler.log(`successfully saved pnc baby examination`)),
            catchError(this.errorHandler.handleError<any>('Error saving pnc baby examination'))
        );
    }

    public savePncHivStatus(hivStatusCommand: HivStatusCommand, anyTests: any[]): Observable<any> {
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

    public savePncHivTests(hivTestsCommand: HivTestsCommand): Observable<any> {
        return this.http.post<any>(this.API_URL + '/api/HtsEncounter/addTestResults', JSON.stringify(hivTestsCommand), httpOptions).pipe(
            tap(savePncHivTests => this.errorHandler.log(`successfully saved pnc hiv tests`)),
            catchError(this.errorHandler.handleError<any>('Error saving pnc hiv tests'))
        );
    }

    public savePncDrugAdministration(): Observable<any> {
        return this.http.post<any>(this.API_URL + '', JSON.stringify(''), httpOptions).pipe(
            tap(savePncDrugAdministration => this.errorHandler.log(`successfully saved pnc drug administration`)),
            catchError(this.errorHandler.handleError<any>('Error saving pnc drug administration'))
        );
    }

    public savePartnerTesting(): Observable<any> {
        return this.http.post<any>(this.API_URL + '', JSON.stringify(''), httpOptions).pipe(
            tap(savePartnerTesting => this.errorHandler.log(`successfully saved pnc partner testing`)),
            catchError(this.errorHandler.handleError<any>('Error saving pnc partner testing'))
        );
    }

    public saveDiagnosis(pncPatientDiagnosis: PatientDiagnosisCommand): Observable<any> {
        return this.http.post<any>(this.API_MATERNITY_URL + '/api/PatientDiagnosis/AddDiagnosis',
            JSON.stringify(pncPatientDiagnosis), httpOptions).pipe(
                tap(saveDiagnosis => this.errorHandler.log(`successfully saved pnc diagnosis`)),
                catchError(this.errorHandler.handleError<any>('Error saving pnc diagnosis'))
            );
    }

    public savePncReferral(pncReferralCommand: PatientReferralCommand): Observable<any> {
        return this.http.post<any>(this.API_URL + '/api/PatientReferralAndAppointment/postReferral',
            JSON.stringify(pncReferralCommand), httpOptions).pipe(
                tap(savePncReferral => this.errorHandler.log(`successfully saved pnc referral`)),
                catchError(this.errorHandler.handleError<any>('Error saving pnc referral'))
            );
    }
}
