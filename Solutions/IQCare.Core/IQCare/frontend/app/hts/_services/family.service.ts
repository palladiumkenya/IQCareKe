import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { catchError, tap } from 'rxjs/operators';
import { FamilyScreening } from '../_models/familyScreening';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { FamilyTracing } from '../_models/familyTracing';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class FamilyService {
    private API_URL = environment.API_URL;
    private url = '/api/Register';
    private lookup = '/api/Lookup/getCustomOptions';
    private pns = '/api/HtsEncounter/pnsScreening';
    private family = '/api/HtsEncounter/tracing';

    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlerService) { }

    public getClientFamily(patientId: number): Observable<any[]> {
        const relationshipTypes = JSON.stringify(['Sibling', 'Child', 'Father', 'Mother', 'Other']);

        return this.http.post<any[]>(this.API_URL + this.url + '/getPartners/?patientId='
            + patientId, relationshipTypes, httpOptions).pipe(
                tap(getClientFamily => this.errorHandler.log('fetched all family')),
                catchError(this.errorHandler.handleError<any[]>('getClientFamily'))
            );
    }

    public getCustomOptions(): Observable<any[]> {
        const options = JSON.stringify(['YesNo', 'ScreeningHivStatus', 'FamilyScreening']);

        return this.http.post<any[]>(this.API_URL + this.lookup, options, httpOptions).pipe(
            tap(getCustomOptions => this.errorHandler.log('fetched family screening options')),
            catchError(this.errorHandler.handleError<any[]>('getCustomOptions'))
        );
    }

    public getFamilyTracingOptions(): Observable<any[]> {
        const options = JSON.stringify(['TracingMode', 'FamilyTracingOutcome', 'YesNo', 'TracingType']);

        return this.http.post<any[]>(this.API_URL + this.lookup, options, httpOptions).pipe(
            tap(getFamilyTracingOptions => this.errorHandler.log('fetched family tracing options')),
            catchError(this.errorHandler.handleError<any[]>('getFamilyTracingOptions'))
        );
    }

    public addFamilyScreening(screeningForm: FamilyScreening, familyScreening: any[]): Observable<any[]> {
        const Indata = {
            'Screening': familyScreening,
            'ScreeningDate': screeningForm.dateOfScreening,
            'BookingDate': screeningForm.dateBooked,
            'PatientId': screeningForm.patientId,
            'PersonId': screeningForm.personId,
            'PatientMasterVisitId': screeningForm.patientMasterVisitId,
            'UserId': screeningForm.userId
        };

        return this.http.post<any[]>(this.API_URL + this.pns, JSON.stringify(Indata), httpOptions).pipe(
            tap(addFamilyScreening => this.errorHandler.log(`add family screening w/ id`)),
            catchError(this.errorHandler.handleError<any[]>('addFamilyScreening'))
        );
    }

    public addFamilyTracing(tracing: FamilyTracing, tracingType: number): Observable<any> {
        const Indata = {
            'TracingDate': tracing.dateFamilyContacted,
            'TracingMode': tracing.mode,
            'TracingOutcome': tracing.outcome,
            'Consent': tracing.consent,
            'DateBookedTesting': tracing.dateBooked,
            'PersonId': tracing.personId,
            'UserId': tracing.userId,
            'TracingType': tracingType
        };

        return this.http.post<any>(this.API_URL + this.family, JSON.stringify(Indata), httpOptions).pipe(
            tap(addFamilyTracing => this.errorHandler.log(`add family tracing w/ id`)),
            catchError(this.errorHandler.handleError<any[]>('addFamilyTracing'))
        );
    }
}
