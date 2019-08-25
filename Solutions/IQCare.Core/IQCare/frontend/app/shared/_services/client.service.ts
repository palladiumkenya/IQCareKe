import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { ErrorHandlerService } from './errorhandler.service';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class ClientService {
    private API_URL = environment.API_URL;
    private _url = '/api/Register';

    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlerService) { }

    public getPersonDetails(personId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + this._url + '/getPerson/' + personId).pipe(
            tap(personDetails => this.errorHandler.log('fetched person details')),
            catchError(this.errorHandler.handleError<any[]>('getPersonDetails'))
        );
    }

    public getClientDetails(patientId: number, serviceAreaId): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._url + '?patientId=' + patientId + '&serviceAreaId=' + serviceAreaId).pipe(
            tap(clientDetails => this.errorHandler.log('fetched all client details')),
            catchError(this.errorHandler.handleError<any[]>('getClientDetails'))
        );
    }
}
