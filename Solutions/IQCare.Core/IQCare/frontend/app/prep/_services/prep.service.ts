import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { Observable, of } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { PrepStatusCommand } from '../_models/commands/PrepStatusCommand';
import { AllergiesCommand } from '../_models/commands/AllergiesCommand';
import { ClientCircumcisionStatusCommand } from '../_models/commands/ClientCircumcisionStatusCommand';
import { PregnancyIndicatorCommand } from '../_models/commands/PregnancyIndicatorCommand';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})
export class PrepService {
    private API_URL = environment.API_URL;
    private PREP_API_URL = environment.API_PREP_URL;
    private MATERNITY_API_URL = environment.API_PMTCT_URL;

    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlerService) {

    }

    public StiScreeningTreatment(STIScreeningCommand: any): Observable<any> {
        if (STIScreeningCommand.Screenings.length == 0) {
            return of([]);
        }
        return this.http.post<any>(this.MATERNITY_API_URL + '/api/PatientScreening/PostPatientScreenings',
            JSON.stringify(STIScreeningCommand), httpOptions).pipe(
                tap(StiScreeningTreatment => this.errorHandler.log(`successfully added sti screening details`)),
                catchError(this.errorHandler.handleError<any>('Error adding sti screening details'))
            );
    }

    public getStiScreeningTreatment(patientId: number, patientMasterVisitId: number): Observable<any[]> {
        return this.http.get<any[]>(this.MATERNITY_API_URL + '/api/PatientScreening/' + patientId + '/' + patientMasterVisitId).pipe(
            tap(getStiScreeningTreatment => this.errorHandler.log(`successfully fetched sti screening details`)),
            catchError(this.errorHandler.handleError<any>('Error fetching sti screening details'))
        );
    }

    public savePrepStatus(prepStatusCommand: PrepStatusCommand): Observable<any> {
        return this.http.post<any>(this.PREP_API_URL + '/api/PrepStatus/AddPrepStatus',
            JSON.stringify(prepStatusCommand), httpOptions).pipe(
                tap(savePrepStatus => this.errorHandler.log(`successfully added prep status details`)),
                catchError(this.errorHandler.handleError<any>('Error saving prep status'))
            );
    }

    public getPrepStatus(patientId: number, patientEncounterId: number): Observable<any[]> {
        return this.http.get<any[]>(this.PREP_API_URL + '/api/PrepStatus/GetPrepStatus/' + patientId + '/' + patientEncounterId).pipe(
            tap(getPrepStatus => this.errorHandler.log(`successfully fetched prep status details`)),
            catchError(this.errorHandler.handleError<any>('Error fetching prep status'))
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

    public getPatientAdverseEvents(patientId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + '/api/AdverseEvents/GetPatientAdverseEvents/' + patientId).pipe(
            tap(getPatientAdverseEvents => this.errorHandler.log('Successfully fetched adverse events')),
            catchError(this.errorHandler.handleError<any>('Error in fetching Patient Adverse Events'))
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

    public getPatientAllergies(patientId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + '/api/PatientAllergy/GetPatientAllergy/' + patientId).pipe(
            tap(getPatientAllergies => this.errorHandler.log('Successfully fetched patient allergies')),
            catchError(this.errorHandler.handleError<any>('Error in fetching Patient Allergies'))
        );
    }

    public saveCircumcisionStatus(clientCircumcisionStatusCommand: ClientCircumcisionStatusCommand): Observable<any> {
        return this.http.post<any>(this.PREP_API_URL + '/api/CircumcisionStatus/AddCircumcisionStatus',
            JSON.stringify(clientCircumcisionStatusCommand), httpOptions).pipe(
                tap(saveCircumcisionStatus => this.errorHandler.log('Successfully saved patient circumcision status')),
                catchError(this.errorHandler.handleError<any>('Error in saving Patient circumcision status'))
            );
    }

    public getCircumcisionStatus(patientId: number): Observable<any> {
        return this.http.get<any>(this.PREP_API_URL + '/api/CircumcisionStatus/GetCircumcisionStatus/' + patientId).pipe(
            tap(getCircumcisionStatus => this.errorHandler.log('Successfully saved patient circumcision status')),
            catchError(this.errorHandler.handleError<any>('Error in saving Patient circumcision status'))
        );
    }

    public savePregnancyIndicatorCommand(pregnancyIndicatorCommand: PregnancyIndicatorCommand): Observable<any> {
        return this.http.post<any>(this.MATERNITY_API_URL + '/api/PregnancyIndicator/AddPregnancyIndicator',
            JSON.stringify(pregnancyIndicatorCommand), httpOptions).pipe(
                tap(savePregnancyIndicatorCommand => this.errorHandler.log('Successfully saved patient pregnancy indicator status')),
                catchError(this.errorHandler.handleError<any>('Error in saving Patient pregnancy indicator status'))
            );
    }

    AddEditBehaviourRisk(EncounterTypeId: number,
        createdby: number, patientid: number, patientmastervisitid: number, visitdate: string, serviceareaId:
            number, riskassessment: any[], clinicalnotes: any[]): Observable<any[]> {
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

        console.log(Indata);

        return this.http.post<any>(this.PREP_API_URL + '/api/BehaviourRisk/AddAssessmentVisitDetail', JSON.stringify(Indata)
            , httpOptions).pipe(
                tap((submitRiskAssessments: any) => this.errorHandler.log(`Submit RiskAssessment Results`)),
                catchError(this.errorHandler.handleError<any>('submitRiskAssessmentResults'))
            );

    }

    CheckencounterExists(patientid: number): Observable<any[]> {
        const Indata = {
            'PatientId': patientid
        };
        return this.http.post<any>(this.PREP_API_URL + '/api/BehaviourRisk/Encounterexists', JSON.stringify(Indata), httpOptions)
            .pipe(tap(CheckencounterExists => this.errorHandler.log('checked if RiskAssessmentEncounter Exists')),
                catchError(this.errorHandler.handleError<any[]>('CheckencounterExists'))
            );
    }

    GetAssessmentDetails(patientid: number, patientmastervisitid: number): Observable<any[]> {
        const Indata = {
            'PatientId': patientid,
            'PatientMasterVisitId': patientmastervisitid
        };

        return this.http.post<any>(this.PREP_API_URL + '/api/BehaviourRisk/GetAssessmentFormDetails', JSON.stringify(Indata), httpOptions)
            .pipe(tap(GetAssessmentDetails => this.errorHandler.log('GetAssessment Form Details ')),
                catchError(this.errorHandler.handleError<any[]>('GetAssessmentDetails'))
            );

    }



    public getPatientMasterVisits(patientId: number, patientmastervisitid: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + '/api/PatientServices/GetMasterVisits/' + patientId + '/' + patientmastervisitid).pipe(
            tap(getPatientEncounters => this.errorHandler.log('get ')),
            catchError(this.errorHandler.handleError<any[]>('getPatientEncounters', []))
        );
    }

    public getPrepEncounterHistory(patientId: number, serviceAreaId: number): Observable<any[]> {
        return this.http.get<any[]>(this.PREP_API_URL + '/api/PrepEncounter/GetPrepEncounters/' + patientId + '/' + serviceAreaId).pipe(
            tap(getPrepEncounterHistory => this.errorHandler.log(`successfully fetched prep encounters`)),
            catchError(this.errorHandler.handleError<any>('Error fetching prep encounters'))
        );
    }
}
