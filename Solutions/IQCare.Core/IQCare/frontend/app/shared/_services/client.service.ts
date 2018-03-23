import { Injectable } from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {of} from 'rxjs/observable/of';
import 'rxjs/add/observable/throw';
import {Observable} from 'rxjs/Observable';
import {catchError, tap} from 'rxjs/operators';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class ClientService {
    private API_URL = environment.API_URL;
    private _url = '/api/Register';

    constructor(private http: HttpClient) { }

    public getClientDetails(patientId: number, serviceAreaId): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._url + '/?patientId=' + patientId + '&serviceAreaId=' + serviceAreaId).pipe(
            tap(clientDetails => this.log('fetched all client details')),
            catchError(this.handleError<any[]>('getClientDetails'))
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
