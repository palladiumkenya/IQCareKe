import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {environment} from '../../../environments/environment';
import {catchError, tap} from 'rxjs/operators';
import {PnsTracing} from '../_models/pnstracing';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class PnstracingService {
    private API_URL = environment.API_URL;
    private lookup = '/api/Lookup/getCustomOptions';
    private url = '/api/HtsEncounter';

    constructor(private http: HttpClient) { }

    public getTracingOptions(): Observable<any[]> {
        const tracingOptions = JSON.stringify(['TracingMode', 'YesNo', 'PnsTracingOutcome']);

        return this.http.post<any[]>(this.API_URL + this.lookup, tracingOptions, httpOptions).pipe(
            tap(getTracingOptions => this.log('fetched tracing options')),
            catchError(this.handleError<any[]>('getTracingOptions'))
        );
    }

    public addPnsTracing(pnsTracingForm: PnsTracing): Observable<any> {
        return this.http.post<any>(this.API_URL + this.url + '/pnsTracing', JSON.stringify(pnsTracingForm), httpOptions).pipe(
            tap(addPnsTracing => this.log('successfully add pns tracing')),
            catchError(this.handleError<any[]>('addPnsTracing'))
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
