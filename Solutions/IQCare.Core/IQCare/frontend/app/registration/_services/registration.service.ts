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

    public registerClient(person: Person): Observable<Person> {

        const Indata = {
            'Person': person
        };

        return this.http.post(this.API_URL + this._url, JSON.stringify(Indata), httpOptions).pipe(
            tap((registeredClient: Person) => this.log(`added client w/ id`)),
            catchError(this.handleError<Person>('registerClient'))
        );
    }

    public addPatient(personId: number, dateOfBirth: string): Observable<any> {
        const Indata = {
            PersonId: personId,
            DateOfBirth: dateOfBirth
        };

        return this.http.post<any>(this.API_URL + this._url + '/addPatient', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPatient: any) => this.log(`added patient w/ id`)),
            catchError(this.handleError<any>('addPatient'))
        );
    }

    public addPersonContact(personId: number, physicalAddress: string, mobileNumber: string,
                            alternativeNumber: string, emailAddress: string, userId: number): Observable<any> {
        const Indata = {
            PersonId: personId,
            PhysicalAddress: physicalAddress,
            MobileNumber: mobileNumber,
            AlternativeNumber: alternativeNumber,
            EmailAddress: emailAddress,
            UserId: userId
        };

        return this.http.post<any>(this.API_URL + this._url + '/addPersonContact', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPersonContact: any) => this.log(`added person contact w/ id`)),
            catchError(this.handleError<any>('addPersonContact'))
        );
    }

    public addPersonMaritalStatus(personId: number, maritalStatusId: number, userId: number): Observable<any> {
        const Indata = {
            PersonId: personId,
            MaritalStatusId: maritalStatusId,
            UserId: userId
        };

        return this.http.post<any>(this.API_URL + this._url + '/addPersonMaritalStatus', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPersonMaritalStatus: any) => this.log(`added person marital status w/ id`)),
            catchError(this.handleError<any>('addPersonMaritalStatus'))
        );
    }

    public addPersonLocation(personId: number, countyId: number, subCountyId: number, wardId: number, userId: number, landMark: string): Observable<any> {
        const Indata = {
            PersonId: personId,
            CountyId: countyId,
            SubCountyId: subCountyId,
            WardId: wardId,
            Village: ' ',
            LandMark: landMark,
            UserId: userId
        };

        return this.http.post<any>(this.API_URL + this._url + '/AddPersonLocation', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPersonLocation: any) => this.log(`added person location w/ id`)),
            catchError(this.handleError<any>('addPersonLocation'))
        );
    }

    public addPersonRelationship(personRelationship: any): Observable<any> {
        return this.http.post<any>(this.API_URL + this._url + '/addPersonRelationship',
            JSON.stringify(personRelationship), httpOptions).pipe(
            tap((addPersonRelationship: any) => this.log(`added new person relationship w/ id`)),
            catchError(this.handleError<any>('addPersonRelationship'))
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
