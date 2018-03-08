import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import {Observable} from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError, map, tap } from 'rxjs/operators';

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

    constructor(private http: HttpClient) { }

    public getHtsEncounterOptions(): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._lookupurl + '/htsOptions').pipe(
            tap(htsoptions => this.log('fetched all hts options')),
            catchError(this.handleError<any[]>('getHtsOptions'))
        );
    }

    public addEncounter(encounter: Encounter, finalTestingResults: FinalTestingResults,
                        hivResults1: any[], hivResults2: any[]): Observable<Encounter> {
        const encounterBody = JSON.stringify(encounter);
        const finalResultsBody = JSON.stringify(finalTestingResults);
        const hivResults1Body = JSON.stringify(hivResults1);
        const hivResults2Body = JSON.stringify(hivResults2);

        const Indata = {'encounter': encounterBody, 'hivResults1': hivResults1Body,
            'hivResults2': hivResults2Body, 'finalResultsBody': finalResultsBody };

        return this.http.post(this.API_URL + this._url, Indata, httpOptions).pipe(
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

            // Let the app keep running by returning an empty result.
            return of(result as T);
        };
    }

    /** Log a HeroService message with the MessageService */
    private log(message: string) {
        console.log(message);
    }

}
