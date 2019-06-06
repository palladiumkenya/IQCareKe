import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { Observable, of } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { PrepStatusCommand } from '../_models/commands/PrepStatusCommand';
import { AllergiesCommand } from '../_models/commands/AllergiesCommand';
import { ClientCircumcisionStatusCommand } from '../_models/commands/ClientCircumcisionStatusCommand';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})
export class PrepService {
    private API_URL = environment.API_URL;
    private PREP_API_URL = environment.API_PREP_URL;

    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlerService) {

    }

    public StiScreeningTreatment(): Observable<any> {
        return this.http.post<any>(this.API_URL + '', JSON.stringify(''), httpOptions).pipe(

        );
    }

    public savePrepStatus(prepStatusCommand: PrepStatusCommand): Observable<any> {
        return this.http.post<any>(this.PREP_API_URL + '/api/PrepStatus/AddPrepStatus',
            JSON.stringify(prepStatusCommand), httpOptions).pipe(
                tap(savePrepStatus => this.errorHandler.log(`successfully added prep status details`)),
                catchError(this.errorHandler.handleError<any>('Error saving prep status'))
            );
    }

    AddEditBehaviourRisk(EncounterTypeId: number,
        createdby: number, patientid: number, patientmastervisitid: number, visitdate: string, serviceareaId:
            number, riskassessment: any[], clinicalnotes: any[]) {
        const Indata = {
            'EncounterTypeId': EncounterTypeId,
            'UserId': createdby,
            'PatientId': patientid,
            'PatientMasterVisitId': patientmastervisitid,
            'VisitDate': visitdate,
            'ServiceAreaId': serviceareaId,
            'riskAssessments': riskassessment,
            'ClinicalNotes': clinicalnotes
        };



        return this.http.post<any>(this.API_URL + '/api/BehaviourRisk/AddAssessmentVisitDetail', JSON.stringify(Indata)
            , httpOptions).pipe(
                tap((submitRiskAssessments: any) => this.errorHandler.log(`Submit RiskAssessment Results`)),
                catchError(this.errorHandler.handleError<any>('submitRiskAssessmentResults'))
            );

    }

    CheckencounterExists(patientid: number): Observable<any[]> {
        const Indata = {
            'PatientId': patientid
        };
        return this.http.post<any>(this.API_URL + '/api/BehaviourRisk/Encounterexists', JSON.stringify(Indata), httpOptions)
            .pipe(tap(CheckencounterExists => this.errorHandler.log('checked if RiskAssessmentEncounter Exists')),
                catchError(this.errorHandler.handleError<any[]>('CheckencounterExists'))
            );
    }

    GetAssessmentDetails(patientid: number, patientmastervisitid: number): Observable<any[]> {
        const Indata = {
            'PatientId': patientid,
            'PatientMasterVisitId': patientmastervisitid
        };

        return this.http.post<any>(this.API_URL + '/api/BehaviourRisk/GetAssessmentFormDetails', JSON.stringify(Indata), httpOptions)
            .pipe(tap(GetAssessmentDetails => this.errorHandler.log('GetAssessment Form Details ')),
                catchError(this.errorHandler.handleError<any[]>('GetAssessmentDetails'))
            );

    }

    public savePatientAdverseEvents(adverseEventsCommand: any[]): Observable<any> {
        if (adverseEventsCommand.length == 0) {
            return of([]);
        }

        const Indata = {
            'AdverseEvents': adverseEventsCommand
        };

        return this.http.post<any>(this.API_URL + '/api/AdverseEvents/AddAdverseEvents', JSON.stringify(Indata), httpOptions).pipe(
            tap(savePatientAdverseEvents => this.errorHandler.log('Successfully saved adverse events')),
            catchError(this.errorHandler.handleError<any>('Error in saving Patient Adverse Events'))
        );
    }

    public savePatientAllergies(allergiesCommand: AllergiesCommand[]): Observable<any> {
        if (allergiesCommand.length == 0) {
            return of([]);
        }

        const Indata = {
            'PatientAllergies': allergiesCommand
        };

        return this.http.post<any>(this.API_URL + '/api/PatientAllergy/AddAllergy', JSON.stringify(Indata), httpOptions).pipe(
            tap(savePatientAllergies => this.errorHandler.log('Successfully saved patient allergies')),
            catchError(this.errorHandler.handleError<any>('Error in saving Patient Allergies'))
        );
    }

    public saveCircumcisionStatus(clientCircumcisionStatusCommand: ClientCircumcisionStatusCommand): Observable<any> {
        return this.http.post<any>(this.PREP_API_URL + '/api/CircumcisionStatus/AddCircumcisionStatus',
            JSON.stringify(clientCircumcisionStatusCommand), httpOptions).pipe(
                tap(saveCircumcisionStatus => this.errorHandler.log('Successfully saved patient circumcision status')),
                catchError(this.errorHandler.handleError<any>('Error in saving Patient circumcision status'))
            );
    }
}
