import { Injectable } from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {of} from 'rxjs/observable/of';
import {catchError, tap} from 'rxjs/operators';
import {Enrollment} from '../_models/enrollment';
import {Person} from '../_models/person';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class EnrollmentService {
    private API_URL = environment.API_URL;
    private _url = '/api/Register/enrollment';

    constructor(private http: HttpClient) { }

    public enrollClient(clientEnrollment: Enrollment): Observable<Enrollment> {
        const Indata = {
            'ClientEnrollment': clientEnrollment
        };

        return this.http.post(this.API_URL + this._url, JSON.stringify(Indata), httpOptions).pipe(
            tap((enrolledClient: Enrollment) => this.log(`enrolled client w/ id`)),
            catchError(this.handleError<Enrollment>('clientEnrollment'))
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
