import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {Observable} from 'rxjs/Observable';
import {of} from 'rxjs/observable/of';
import {Person} from '../_models/person';
import {catchError, tap} from 'rxjs/operators';
import {Search} from '../_models/search';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class SearchService {
    private API_URL = environment.API_URL;
    private _url = '/api/Register/search';

    constructor(private http: HttpClient) { }

    public searchClient(person: Search): Observable<any[]> {

        return this.http.get<any[]>(this.API_URL + this._url + '?identificationNumber=' + person.identifierValue +
        '&firstName=' + person.firstName + '&middleName=' + person.midName + '&lastName=' + person.lastName, httpOptions).pipe(
            tap((searchClient: any) => this.log(`search client`)),
            catchError(this.handleError<any>('searchClients'))
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
