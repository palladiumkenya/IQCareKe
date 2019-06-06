import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { Observable } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { PrepStatusCommand } from '../_models/commands/PrepStatusCommand';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})
export class PrepService {
    private API_URL = environment.API_URL;
    private PREP_API_URL = environment.API_PREP_URL;

    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlerService) {

    }

    public StiScreeningTreatment(): Observable<any> {
        return this.http.post<any>(this.API_URL + '', JSON.stringify(''), httpOptions).pipe(

        );
    }

    public savePrepStatus(prepStatusCommand: PrepStatusCommand): Observable<any> {
        return this.http.post<any>(this.PREP_API_URL + '/api/PrepStatus/AddPrepStatus',
            JSON.stringify(prepStatusCommand), httpOptions).pipe(
                tap(savePrepStatus => this.errorHandler.log(`successfully added prep status details`)),
                catchError(this.errorHandler.handleError<any>('Error saving prep status'))
            );
    }
}
