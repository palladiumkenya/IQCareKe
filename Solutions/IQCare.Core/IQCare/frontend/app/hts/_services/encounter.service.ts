import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import {Observable} from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError, tap } from 'rxjs/operators';
import 'rxjs/add/observable/throw';

import { environment } from '../../../environments/environment';
import {Encounter} from '../_models/encounter';
import {FinalTestingResults} from '../_models/testing';


const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class EncounterService {
    private API_URL = environment.API_URL;
    private _url = '/api/HtsEncounter';
    private _lookupurl = '/api/lookup';
    private lookup = '/api/Lookup/getCustomOptions';

    constructor(private http: HttpClient) { }

    public getEncounters(patientId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._url + '/' + patientId ).pipe(
            tap(getEncounters => this.log('fetched all client encounters')),
            catchError(this.handleError<any[]>('getEncounters', []), )
        );
    }

    public getCustomOptions(): Observable<any[]> {
        const options = JSON.stringify(['HIVTestKits', 'HIVResults', 'HIVFinalResults', 'YesNo', 'ReasonsPartner']);

        return this.http.post<any[]>(this.API_URL + this.lookup, options, httpOptions).pipe(
            tap(getCustomOptions => this.log('fetched all custom options')),
            catchError(this.handleError<any[]>('getCustomOptions'))
        );
    }

    public addTesting(finalTestingResults: FinalTestingResults, hivResults1: any[], hivResults2: any[],
                      htsEncounterId: number, providerId: number, patientId: number,
                      patientMasterVisitId: number, serviceAreaId: number): Observable<any> {
        const finalResultsBody = finalTestingResults;
        const hivResultsBody = hivResults1;
        if ( hivResults2.length > 0 ) {
            hivResultsBody.push.apply(hivResults2);
        }

        const Indata = {
            'Testing': hivResultsBody,
            'FinalTestingResult': finalResultsBody,
            'HtsEncounterId': htsEncounterId,
            'ProviderId': providerId,
            'PatientId': patientId,
            'PatientMasterVisitId': patientMasterVisitId,
            'ServiceAreaId': serviceAreaId
        };

        return this.http.post<any>(this.API_URL + this._url + '/addTestResults', JSON.stringify(Indata), httpOptions).pipe(
            tap((addTesting: any) => this.log(`added Testing`)),
            catchError(this.handleError<any>('addTesting'))
        );
    }

    public getHtsEncounterOptions(): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._lookupurl + '/htsOptions').pipe(
            tap(htsoptions => this.log('fetched all hts options')),
            catchError(this.handleError<any[]>('getHtsOptions', []), )
        );
    }

    public getEncounterType(): Observable<any> {
        return this.http.get<any>(this.API_URL + this._lookupurl +
            '/optionsByGroupandItemName?groupName=EncounterType&itemName=Hts-encounter', httpOptions).pipe(
            tap((getEncounterType: any) => this.log(`get encounter type`)),
            catchError(this.handleError<any>('getEncounterType'))
        );
    }

    public addEncounter(encounter: Encounter): Observable<Encounter> {
        const encounterBody = encounter;
        /* const finalResultsBody = finalTestingResults;
        const hivResultsBody = hivResults1;
        if ( hivResults2.length > 0 ) {
            hivResultsBody.push.apply(hivResults2);
        } */

        const Indata = {
            'Encounter': encounterBody,
            // 'Testing': hivResultsBody,
            // 'FinalTestingResult': finalResultsBody
        };

        return this.http.post(this.API_URL + this._url, JSON.stringify(Indata), httpOptions).pipe(
            tap((addedEncounter: Encounter) => this.log(`added encounter w/ id`)),
            catchError(this.handleError<Encounter>('addEncounter'))
        );
    }

    private handleError<T> (operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {

            // TODO: send the error to remote logging infrastructure
            console.error(error); // log to console instead

            // TODO: better job of transforming error for user consumption
            this.log(`${operation} failed: ${error.message}`);

            return Observable.throw(error.message);
        };
    }

    /** Log a HeroService message with the MessageService */
    private log(message: string) {
        console.log(message);
    }

}
