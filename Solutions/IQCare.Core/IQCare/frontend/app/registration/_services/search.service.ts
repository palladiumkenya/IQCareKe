import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

import { catchError, tap } from 'rxjs/operators';
import { Search } from '../_models/search';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
};

httpOptions.headers.append('Access-Control-Allow-Origin', 'http://' + location.protocol + '//'
    + window.location.hostname + ':' + window.location.port + '/frontend');
httpOptions.headers.append('Access-Control-Allow-Origin', 'https://' + location.protocol + '//'
    + window.location.hostname + ':' + window.location.port + '/frontend');

@Injectable()
export class SearchService {
    private API_URL = environment.API_URL;
    private _url = '/api/Register/search';

    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlerService) { }

    public searchClient(person: Search): Observable<any[]> {

        return this.http.get<any[]>(this.API_URL + this._url + '?identificationNumber=' + person.identifierValue +
            '&firstName=' + person.firstName + '&middleName=' + person.midName + '&lastName=' + person.lastName, httpOptions).pipe(
                tap((searchClient: any) => this.errorHandler.log(`search client`)),
                catchError(this.errorHandler.handleError<any>('searchClients'))
            );
    }

    public lastHtsEncounter(personId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/HtsEncounter/lastHtsEncounters/' + personId, httpOptions).pipe(
            tap((lastHtsEncounter: any) => this.errorHandler.log(`get client last encounter`)),
            catchError(this.errorHandler.handleError<any>('lastHtsEncounter'))
        );
    }

    public setSession(personId: number, patientPk: number): Observable<any> {
        const Indata = {
            'personId': personId,
            'patientPk': patientPk
        };
        return this.http.post(location.protocol + '//' + window.location.hostname
            + '/IQCare/CCC/WebService/PersonService.asmx/SetPatientSessionFromUniversalRegistration',
            JSON.stringify(Indata), httpOptions).pipe(
                tap((setSession: any) => this.errorHandler.log(`setSession`)),
                catchError(this.errorHandler.handleError<any>('setSession'))
            );
    }
}
