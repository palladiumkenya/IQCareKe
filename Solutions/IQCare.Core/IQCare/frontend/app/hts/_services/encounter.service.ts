import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import {Observable} from 'rxjs/Observable';
import { catchError, tap } from 'rxjs/operators';
import 'rxjs/add/observable/throw';

import { environment } from '../../../environments/environment';
import {Encounter} from '../_models/encounter';
import {FinalTestingResults} from '../_models/testing';
import {ErrorHandlerService} from '../../shared/_services/errorhandler.service';


const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class EncounterService {
    private API_URL = environment.API_URL;
    private _url = '/api/HtsEncounter';
    private _lookupurl = '/api/lookup';
    private lookup = '/api/Lookup/getCustomOptions';

    constructor(private http: HttpClient,
                private errorHandler: ErrorHandlerService) { }

    public getEncounters(patientId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._url + '/' + patientId ).pipe(
            tap(getEncounters => this.errorHandler.log('fetched all client encounters')),
            catchError(this.errorHandler.handleError<any[]>('getEncounters', []), )
        );
    }

    public getEncounter(encounterId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + this._url + '/getEncounter/' + encounterId).pipe(
            tap(getEncounter => this.errorHandler.log('fetched a single client encounter')),
            catchError(this.errorHandler.handleError<any[]>('getEncounter', []))
        );
    }

    public getCustomOptions(): Observable<any[]> {
        const options = JSON.stringify(['HIVTestKits', 'HIVResults', 'HIVFinalResults', 'YesNo', 'YesNoNA', 'ReasonsPartner']);

        return this.http.post<any[]>(this.API_URL + this.lookup, options, httpOptions).pipe(
            tap(getCustomOptions => this.errorHandler.log('fetched all custom options')),
            catchError(this.errorHandler.handleError<any[]>('getCustomOptions'))
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
            tap((addTesting: any) => this.errorHandler.log(`added Testing`)),
            catchError(this.errorHandler.handleError<any>('addTesting'))
        );
    }

    public getHtsEncounterOptions(): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._lookupurl + '/htsOptions').pipe(
            tap(htsoptions => this.errorHandler.log('fetched all hts options')),
            catchError(this.errorHandler.handleError<any[]>('getHtsOptions', []), )
        );
    }

    public getEncounterType(): Observable<any> {
        return this.http.get<any>(this.API_URL + this._lookupurl +
            '/optionsByGroupandItemName?groupName=EncounterType&itemName=Hts-encounter', httpOptions).pipe(
            tap((getEncounterType: any) => this.errorHandler.log(`get encounter type`)),
            catchError(this.errorHandler.handleError<any>('getEncounterType'))
        );
    }

    public addEncounter(encounter: Encounter): Observable<Encounter> {
        const encounterBody = encounter;

        const Indata = {
            'Encounter': encounterBody
        };

        return this.http.post(this.API_URL + this._url, JSON.stringify(Indata), httpOptions).pipe(
            tap((addedEncounter: Encounter) => this.errorHandler.log(`added encounter w/ id`)),
            catchError(this.errorHandler.handleError<Encounter>('addEncounter'))
        );
    }

    public editEncounter(encounter: Encounter, encounterID: number, patientMasterVisitId: number): Observable<Encounter> {
        const encounterBody = encounter;
        const Indata = {
            'Encounter': encounterBody
        };

        return this.http.put(this.API_URL + '/api/HtsEncounter/updateEncounter/' + encounterID + '/' + patientMasterVisitId,
            JSON.stringify(Indata), httpOptions).pipe(
            tap((editEncounter: Encounter) => this.errorHandler.log(`edited encounter w/ id` + encounterID)),
            catchError(this.errorHandler.handleError<Encounter>('editEncounter'))
        );
    }
}
