import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { tap, catchError } from 'rxjs/operators';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class PsmartService {
    private API_URL = environment.API_URL;

    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlerService) { }


    public getPersonPsmartData(personId: number): Observable<any[]> {
        console.log('psmart' + personId);
        return this.http.get<any[]>(this.API_URL + '/api/HtsEncounter/getPsmartData/' + personId).pipe(
            tap(getPersonPsmartData => this.errorHandler.log('get person psmart data')),
            catchError(this.errorHandler.handleError<any[]>('getPersonPsmartData'))
        );
    }
}
