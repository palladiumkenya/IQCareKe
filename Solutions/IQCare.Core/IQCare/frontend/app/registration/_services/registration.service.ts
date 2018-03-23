import { Injectable } from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {of} from 'rxjs/observable/of';
import 'rxjs/add/observable/throw';
import {catchError, tap} from 'rxjs/operators';
import {Person} from '../_models/person';
import {Contact} from '../_models/contacts';
import {PersonPopulation} from '../_models/personPopulation';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class RegistrationService {
    private API_URL = environment.API_URL;
    private _lookupurl = '/api/lookup';
    private _url = '/api/Register';

    constructor(private http: HttpClient) { }

    public getRegistrationOptions(): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._lookupurl + '/registrationOptions').pipe(
            tap(registrationoptions => this.log('fetched all registration options')),
            catchError(this.handleError<any[]>('getRegistrationOptions'))
        );
    }

    public registerClient(person: Person, contact: Contact,
                        personPopulation: PersonPopulation): Observable<Person> {

        const Indata = {
            'Person': person,
            'Contact': contact,
            'PersonPopulation': personPopulation
        };

        return this.http.post(this.API_URL + this._url, JSON.stringify(Indata), httpOptions).pipe(
            tap((registeredClient: Person) => this.log(`added client w/ id`)),
            catchError(this.handleError<Person>('registerClient'))
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
