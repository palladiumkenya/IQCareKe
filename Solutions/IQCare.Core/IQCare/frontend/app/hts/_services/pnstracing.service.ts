import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {environment} from '../../../environments/environment';
import {catchError, tap} from 'rxjs/operators';
import {PnsTracing} from '../_models/pnstracing';
import {ErrorHandlerService} from '../../shared/_services/errorhandler.service';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class PnstracingService {
    private API_URL = environment.API_URL;
    private lookup = '/api/Lookup/getCustomOptions';
    private url = '/api/HtsEncounter';

    constructor(private http: HttpClient,
                private errorHandler: ErrorHandlerService) { }

    public getTracingOptions(): Observable<any[]> {
        const tracingOptions = JSON.stringify(['TracingMode', 'YesNo', 'PnsTracingOutcome', 'TracingType']);

        return this.http.post<any[]>(this.API_URL + this.lookup, tracingOptions, httpOptions).pipe(
            tap(getTracingOptions => this.errorHandler.log('fetched tracing options')),
            catchError(this.errorHandler.handleError<any[]>('getTracingOptions'))
        );
    }

    public addPnsTracing(pnsTracingForm: PnsTracing): Observable<any> {
        return this.http.post<any>(this.API_URL + this.url + '/tracing', JSON.stringify(pnsTracingForm), httpOptions).pipe(
            tap(addPnsTracing => this.errorHandler.log('successfully add pns tracing')),
            catchError(this.errorHandler.handleError<any[]>('addPnsTracing'))
        );
    }
}
